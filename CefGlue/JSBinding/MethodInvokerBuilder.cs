namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text;
    using CefGlue.Interop;
    using Diagnostics;
    using Emit;

    internal unsafe delegate void MethodInvoker(object obj, int argumentsCount, cef_v8value_t** arguments, cef_v8value_t** retval);

    internal class MethodInvokerBuilder
    {
        /// <summary>
        /// Forces a return V8.Undefined for void return type.
        /// If CEF issue #329 (http://code.google.com/p/chromiumembedded/issues/detail?id=329) 
        /// will be fixed then this value must be false.
        /// </summary>
        private static readonly bool ForceVoidToUndefined = false;

        private const int objArgIndex = 0;
        private const int argumentsCountArgIndex = 1;
        private const int argumentsArgIndex = 2;
        private const int retvalArgIndex = 3;

        public static MethodInvoker Create(MethodDef methodDef)
        {
            return new MethodInvokerBuilder(methodDef).Create();
        }

        private static readonly MethodInfo createUndefinedNativeV8ValueMethod;
        private static readonly MethodInfo throwArgumentCountMismatchMethod;

        static unsafe MethodInvokerBuilder()
        {
            createUndefinedNativeV8ValueMethod = new MethodInvokerHelper.CreateUndefinedNativeV8Value(NativeMethods.cef_v8value_create_undefined).Method;
            throwArgumentCountMismatchMethod = new Action(MethodInvokerHelper.ThrowArgumentCountMismatch).Method;
        }

        private readonly MethodDef def;

        private MethodInvokerBuilder(MethodDef methodDef)
        {
            this.def = methodDef;
        }

        public MethodInvoker Create()
        {
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.ScriptableObject, "Creating method [{0}]", GetMethodInvokerName(this.def));
            #endif

            // Passing method arguments by ref is not supported.
            if (this.def.GetMethods().Any(_ => _.GetParameters().Any(p => p.ParameterType.IsByRef)))
            {
                throw new JSBindingException("Passing method arguments by ref is not supported.");
            }

            // Mixing static and instance methods are not supported.
            if (this.def.GetMethods().All(_ => _.IsStatic)
                == this.def.GetMethods().All(_ => !_.IsStatic)
                )
            {
                throw new JSBindingException("Mixing static and instance methods are not supported.");
            }

            var methodInvoker = typeof(MethodInvoker).GetMethod("Invoke");
            var emit = new DynamicMethodHelper(
                GetMethodInvokerName(def),
                methodInvoker.ReturnType,
                methodInvoker.GetParameters().Select(_ => _.ParameterType).ToArray()
                )
                .Emitter;

            if (!this.def.HasOverloads && this.def.GetMethods().Single().GetParameters().All(_ => !_.IsOptional))
            {
                EmitInvoker(emit,
                    this.def.GetMethods().Single(),
                    null,
                    this.def.Setter
                    );
            }
            else
            {
                var methods = this.def.GetMethods()
                    .GroupBy(_ => _.GetParameters().Length, _ => _)
                    .ToDictionary(_ => _.Key, _ => _.ToList())
                    ;

                // expand methods with optional arguments
                var methodsWithOptArgs = this.def.GetMethods().Where(_ => _.GetParameters().Any(p => p.IsOptional));
                foreach (var m in methodsWithOptArgs)
                {
                    // if full method signature already placed in methods dictionary
                    var parameters = m.GetParameters();
                    for (var k = parameters.Length - 1; k >= 0; k--)
                    {
                        if (!parameters[k].IsOptional) break;

                        if (!methods.ContainsKey(k)) methods.Add(k, new List<MethodInfo>());

                        // check, that if methods already includes this signature, then doesn't expand
                        if (!methods[k].Any(_ =>
                        {
                            var p0 = _.GetParameters();
                            for (var i = 0; i < k; i++)
                            {
                                if (parameters[i].ParameterType != p0[i].ParameterType) return false;
                            }
                            return true;
                        }))
                        {
                            methods[k].Add(m);
                        }
                    }
                }

                // type variance handling
                foreach (var k in methods.Keys)
                {
                    var mlist = methods[k];
                    if (mlist.Count > 1) // if methods for argc==k has overloads
                    {
                        // detect arguments which have type variance
                        List<int> vargs = new List<int>();
                        for (int i = 0; i < k; i++)
                        {
                            if (mlist.Select(_ => _.GetParameters()[i].ParameterType).Distinct().Count() != 1)
                            {
                                vargs.Add(i);
                            }
                        }

                        // TODO: hide methods which not reacheable by type priority (int hides short or byte)

                        // stub, throw exception for currently unsupported
                        if (vargs.Count > 0)
                        {
                            throw new JSBindingException(
                                string.Format("Overloading by argument type variance currently is not supported. name=[{0}], argc=[{1}], vargs=[{2}]",
                                    this.def.Name, k, string.Join(", ", vargs)
                                    )
                                );
                        }
                    }
                }

                var defaultLabel = emit.DefineLabel();
                var labels = new Label[methods.Keys.Max() + 1];
                for (var i = 0; i < labels.Length; i++)
                {
                    if (methods.ContainsKey(i))
                    {
                        labels[i] = emit.DefineLabel();
                    }
                    else
                    {
                        labels[i] = defaultLabel;
                    }
                }

                emit.LdArg(argumentsCountArgIndex)
                    .Switch(labels)
                    ;

                emit.MarkLabel(defaultLabel)
                    .Call(throwArgumentCountMismatchMethod)
                    ;

                // write labels
                for (var i = 0; i < labels.Length; i++)
                {
                    if (labels[i] == defaultLabel) continue;

                    emit.MarkLabel(labels[i]);

                    EmitInvoker(emit,
                        methods[i].Single(),
                        i,
                        false
                        );
                }

                // TODO: check, optimize type variance or throw if not supported
            }

            return (MethodInvoker)emit.DynamicMethod.CreateDelegate(typeof(MethodInvoker));
        }

        private static void EmitInvoker(EmitHelper emit, MethodInfo method, int? argc, bool setter)
        {
            var parameters = method.GetParameters();
            var methodArgC = parameters.Length;

            if (!argc.HasValue)
            {
                EmitThrowIfArgumentCountMismatch(emit, methodArgC);
                argc = methodArgC;
            }

            emit.LdArg(retvalArgIndex);

            // instance method
            if (!method.IsStatic)
            {
                emit.LdArg(objArgIndex);
            }

            // prepare arguments
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                if (i < argc.Value)
                {
                    var changeTypeMethod = CefConvert.GetChangeTypeMethod(typeof(cef_v8value_t*), parameter.ParameterType);

                    EmitLdV8Argument(emit, i);
                    emit.Call(changeTypeMethod);
                }
                else
                {
                    // push default arg
                    if (!parameter.IsOptional) throw new JSBindingException("MethodInvoker compilation error.");

                    EmitLdRawDefaultValue(emit, parameter.RawDefaultValue);
                }
            }

            // call target method
            if (method.IsStatic)
            {
                emit.Call(method);
            }
            else
            {
                emit.CallVirt(method);
            }


            if (method.ReturnType == typeof(void))
            {
                // If method is setter, then it can be called with retval == null from V8Accessor.
                if (setter)
                {
                    var returnValueLabel = emit.DefineLabel();

                    emit.Dup()
                        .BrTrueS(returnValueLabel)
                        .Pop()
                        .Ret()
                        .MarkLabel(returnValueLabel);
                    ;
                }

                if (ForceVoidToUndefined)
                {
                    emit.Call(createUndefinedNativeV8ValueMethod);
                }
                else
                {
                    emit.LdNull();
                }
            }
            else
            {
                // convert return value
                var retValchangeTypeMethod = CefConvert.GetChangeTypeMethod(method.ReturnType, typeof(cef_v8value_t*));
                emit.Call(retValchangeTypeMethod);
            }

            // store result at retval
            emit.StIndI();

            // return
            emit.Ret();
        }

        private static void EmitLdRawDefaultValue(EmitHelper emit, object value)
        {
            if (value == null)
            {
                emit.LdNull();
            }
            else
            {
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Empty:
                        emit.LdNull();
                        break;

                    // case TypeCode.Object:
                    // case TypeCode.DBNull:

                    case TypeCode.Boolean:
                        emit.LdConstI4(((bool)value) ? 1 : 0);
                        break;

                    case TypeCode.Char:
                        emit.LdConstI4((int)(char)value);
                        break;

                    case TypeCode.SByte:
                        emit.LdConstI4((int)(sbyte)value);
                        break;

                    case TypeCode.Byte:
                        emit.LdConstI4((int)(byte)value);
                        break;

                    case TypeCode.Int16:
                        emit.LdConstI4((int)(short)value);
                        break;

                    case TypeCode.UInt16:
                        emit.LdConstI4((int)(ushort)value);
                        break;

                    case TypeCode.Int32:
                        emit.LdConstI4((int)value);
                        break;

                    // TODO: support types for opt args
                    // case TypeCode.UInt32:
                    // case TypeCode.Int64:
                    // case TypeCode.UInt64:
                    // case TypeCode.Single:
                    // case TypeCode.Double:
                    // case TypeCode.Decimal:
                    // case TypeCode.DateTime:

                    case TypeCode.String:
                        emit.LdStr((string)value);
                        break;

                    default:
                        throw new JSBindingException(string.Format("Type '{0}' for default values is not supported.", value.GetType().FullName));
                }
            }
        }

        private static void EmitThrowIfArgumentCountMismatch(EmitHelper emit, int expectedArgumentCount)
        {
            var doneLabel = emit.DefineLabel();

            emit.LdArg(argumentsCountArgIndex)
                .LdConstI4(expectedArgumentCount)
                .BeqS(doneLabel)
                .Call(throwArgumentCountMismatchMethod)
                .MarkLabel(doneLabel)
                ;
        }

        private static void EmitLdV8Argument(EmitHelper emit, int index)
        {
            if (index < 0) throw new ArgumentException("index");

            emit.LdArg(argumentsArgIndex);
            if (index > 0)
            {
                emit.LdConstI4(index * IntPtr.Size)
                    .Add();
            }
            emit.LdIndI();
        }

        private static string GetMethodInvokerName(MethodDef method, bool includeClassName = true)
        {
            if (includeClassName)
            {
                return string.Format("MethodInvoker:[{0}].{1}",
                    method.GetMethods().First().ReflectedType.FullName,
                    method.Name
                    );
            }
            else
            {
                return string.Format("MethodInvoker:[].{0}",
                    method.Name
                    );
            }
        }
    }
}
