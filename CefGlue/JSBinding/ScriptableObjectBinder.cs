namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    // TODO: rename ScriptableObject->JSObject
    // TODO: This is will be ClrObjectBinder ?

    // TODO: report CEF to make non-configurable (or may be non-enumberable) Cef::Accessor / Cef::Handler

    internal class ScriptableObjectBinder
    {
        private static readonly Dictionary<Type, ScriptableObjectBinder> binders = new Dictionary<Type, ScriptableObjectBinder>();

        public static ScriptableObjectBinder Get(Type type, JSBindingOptions options)
        {
            lock (binders)
            {
                ScriptableObjectBinder value;
                if (binders.TryGetValue(type, out value))
                {
                    return value;
                }
                else
                {
                    var binder = new ScriptableObjectBinder(type, options);
                    binders[type] = binder;
                    return binder;
                }
            }
        }

        private readonly Type type;
        private readonly JSBindingOptions options;
        private readonly NamingConvention namingConvention;
        private DispatchTable<MethodDef> dispatchTable;
        private DispatchTable<PropertyDef> propertyDispatchTable;

        private ScriptableObjectBinder(Type type, JSBindingOptions options)
            : this(type, options, NamingConvention.Default)
        {
        }

        private ScriptableObjectBinder(Type type, JSBindingOptions options, NamingConvention namingConvention)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (!type.IsClass && !type.IsInterface) throw new ArgumentException("The type must be a class or interface.");

            this.type = type;
            
            var attr = GetJSObjectAttribute(this.type);
            if (attr != null)
            {
                if (options == JSBindingContext.DefaultOptions)
                {
                    this.options = attr.Options;
                }
            }
            else
            {
                this.options = options;
            }

            this.namingConvention = namingConvention;

            this.Create();

            Debug.Assert(this.dispatchTable != null);
            Debug.Assert(this.propertyDispatchTable != null);

            if ((this.options & JSBindingOptions.LazyCompile) == 0)
            {
                this.Compile();
            }
        }

        public Type Type { get { return this.type; } }
        public DispatchTable<MethodDef> DispatchTable { get { return this.dispatchTable; } }
        public DispatchTable<PropertyDef> PropertyDispatchTable { get { return this.propertyDispatchTable; } }

        private void Create()
        {
            var methods = GetMethods();
            var declaringMethodNames = new HashSet<string>(methods.Select(_ => _.Name));

            var dispatchTable = new DispatchTable<MethodDef>(declaringMethodNames.Count);
            var propertyDispatchTable = new DispatchTable<PropertyDef>();

            foreach (var method in methods)
            {
                string declaringName = method.Name;
                string scriptingName = ConvertIdentifier(declaringName);
                string scriptingPropertyName = null;

                if (declaringName != scriptingName && declaringMethodNames.Contains(scriptingName))
                {
                    throw ExceptionBuilder.TypeAlreadyReservesMember(this.Type, scriptingName, declaringName);
                }

                var property = method.GetDeclaringTypeProperty();
                if (property != null)
                {
                    var declaringPropertyName = property.Name;
                    scriptingPropertyName = ConvertIdentifier(declaringPropertyName);

                    if (declaringPropertyName != scriptingPropertyName && declaringMethodNames.Contains(scriptingPropertyName))
                    {
                        throw ExceptionBuilder.TypeAlreadyReservesMember(this.Type, scriptingPropertyName, declaringPropertyName);
                    }
                }

                // method attributes
                var methodAttributes = method.IsStatic ? MethodDefAttributes.Static : MethodDefAttributes.None;

                if (property != null)
                {
                    methodAttributes |= MethodDefAttributes.Hidden;

                    if (method.IsGetterLikeSignature())
                    {
                        methodAttributes |= MethodDefAttributes.Getter;
                    }
                    else if (method.IsSetterLikeSignature())
                    {
                        methodAttributes |= MethodDefAttributes.Setter;
                    }
                }

                // put method info into a dispatch table
                // note: property accessors can't be overloaded
                var scriptableMethodInfo = dispatchTable.GetOrDefault(scriptingName);
                if (scriptableMethodInfo == null)
                {
                    scriptableMethodInfo = new MethodDef(scriptingName, methodAttributes);
                    dispatchTable.Add(scriptableMethodInfo);
                }
                scriptableMethodInfo.Add(method);

                // put property info into a property dispatch table
                if (property != null)
                {
                    var scriptablePropertyInfo = propertyDispatchTable.GetOrDefault(scriptingPropertyName);
                    if (scriptablePropertyInfo == null)
                    {
                        scriptablePropertyInfo = new PropertyDef(scriptingPropertyName);
                        propertyDispatchTable.Add(scriptablePropertyInfo);
                    }

                    scriptablePropertyInfo.Add(scriptableMethodInfo);
                }
            }

            dispatchTable.Optimize();
            propertyDispatchTable.Optimize();

            // TODO: prevent dispatch tables from modifying

            this.dispatchTable = dispatchTable;
            this.propertyDispatchTable = propertyDispatchTable;
        }

        private void Compile()
        {
            foreach (var method in this.dispatchTable.GetValues())
            {
                method.Compile();
            }
        }

        private IEnumerable<MethodInfo> GetMethods()
        {
            // TODO: exposing methods by class or interface must works same:
            // i.e. we doesn't expose standard object's methods like GetHashCode / ToString for interfaces
            // but may be we must do it, or disable it completely
            return this.Type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(_ => IsScriptable(_))
                .Where(_ => _.DeclaringType != typeof(object)) // || _.Name == "GetHashCode")
                .Where(_ => IsScriptable(_.GetDeclaringTypeProperty()))
                ;
        }

        private bool IsScriptable(MemberInfo member)
        {
            if (member == null) return true;

            JSBindAttribute scriptable;

            switch (member.MemberType)
            {
                case MemberTypes.Method:
                    if (((MethodInfo)member).GetParameters()
                        .Any(p => typeof(Delegate).IsAssignableFrom(p.ParameterType)))
                    {
                        return false; // ignore methods with Delegate parameter (eg. event add/remove)
                    }

                    scriptable = GetScriptableAttribute(member);
                    if (scriptable != null)
                    {
                        return scriptable.Visible;
                    }
                    else
                    {
                        if ((this.options & JSBindingOptions.Public) != 0)
                        {
                            return ((MethodInfo)member).IsPublic;
                        }

                        return false;
                    }

                case MemberTypes.Property:
                    // TODO: indexed properties are not allowed at this time, but can be
                    var property = (PropertyInfo)member;
                    if (property.GetIndexParameters().Length != 0) return false;

                    scriptable = GetScriptableAttribute(member);
                    return scriptable != null ? scriptable.Visible : true;

                default:
                    return false;
            }
        }

        private static JSObjectAttribute GetJSObjectAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(JSObjectAttribute), true);
            if (attributes.Length == 0) { return null; }
            return (JSObjectAttribute)attributes[0];
        }

        private static JSBindAttribute GetScriptableAttribute(MemberInfo member)
        {
            var attributes = member.GetCustomAttributes(typeof(JSBindAttribute), true);
            if (attributes.Length == 0) { return null; }
            else if (attributes.Length == 1)
            {
                return (JSBindAttribute)attributes[0];
            }
            else throw new NotSupportedException("Multiple ScriptVisibleAttribute is not implemented.");
        }

        private string ConvertIdentifier(string name)
        {
            return NameHelper.ConvertIdentifier(name, this.namingConvention);
        }

        public string CreateJavaScriptExtension(string extensionName)
        {
            if (string.IsNullOrEmpty(extensionName)) throw new ArgumentException("extensionName");
            // TODO: same code present in RegisterGlobalScriptableObjects -> refactor this
            // TODO: check for extension name is valid identifier (member access) (dot allowed)

            var js = new StringBuilder();

            // Create object:
            var extensionNameParts = extensionName.Split('.');

            for (var i = 0; i < extensionNameParts.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(extensionNameParts[i])) throw new ArgumentException("extensionName");

                var member = string.Join(".", extensionNameParts.Take(i + 1));

                if (i == 0)
                {
                    js.Append("var ");
                }
                js.AppendFormat("{0} = {0} || {{}};\n", member);
            }

            // Write properties:
            foreach (var property in this.PropertyDispatchTable.GetValues())
            {
                Debug.Assert(property.CanRead || property.CanWrite);

                js.AppendLine();
                js.AppendFormat("Object.defineProperty({0}, \"{1}\", {{\n", extensionName, property.Name);
                if (property.CanRead)
                {
                    js.AppendFormat("    get: function() {{\n");
                    foreach (var line in new string[] {
                        string.Format("native function {0}();", property.GetMethod.Name),
                        string.Format("return {0}();", property.GetMethod.Name),
                    })
                    {
                        js.AppendFormat("        {0}\n", line);
                    }
                    js.AppendFormat("    }}");
                }
                if (property.CanRead && property.CanWrite) js.Append(",\n");
                if (property.CanWrite)
                {
                    js.AppendFormat("    set: function(value) {{\n");
                    foreach (var line in new string[] {
                        string.Format("native function {0}();", property.SetMethod.Name),
                        string.Format("{0}(value);", property.SetMethod.Name),
                    })
                    {
                        js.AppendFormat("        {0}\n", line);
                    }
                    js.AppendFormat("    }}");
                }
                if (property.CanRead || property.CanWrite) js.Append(",\n");
                js.AppendFormat("    configurable: false\n");
                js.AppendFormat("}});\n");
            }

            foreach (var method in this.DispatchTable.GetValues())
            {
                if (method.Hidden) continue;

                // TODO: passing arguments, and support overloads...  (check that all overloads returns void - and set returnsVoid)
                // if has overloads - invoke via apply

                var returnsVoid = method.ReturnTypeIsVoid;
                var arguments = method.GetArgumentNames(); // TODO: if we always .apply arguments - then we can use empty args names.
                // TODO: check arguments names, and rename if needed. "arguments" is reserved. "this" is reserved.

                js.AppendLine();

                js.AppendFormat("{0}.{1} = function({2}) {{\n", extensionName, method.Name, string.Join(", ", arguments));
                foreach (var line in new string[] {
                        string.Format("native function {0}();", method.Name),
                        (returnsVoid ? "" : "return ") + string.Format("{0}.apply(this, arguments);", method.Name),
                        //(returnsVoid ? "" : "return ") + string.Format("{0}.apply(this, arguments);", method.Name,
                        //    method.HasOverloads? ".apply(this, arguments)" : "("+string.Join(", ", arguments)+")"
                        //    ),
                    })
                {
                    js.AppendFormat("    {0}\n", line);
                }
                js.AppendFormat("}};\n");
            }

            return js.ToString();
        }

        // TODO: CreateV8Handler self/universal
        // TODO: CreateV8Accessor self/universal

        public CefV8Value CreateScriptableObject(object instance)
        {
            // TODO: if object doesn't have properties -> do not create accessor

            var v8Handler = new ScriptableObjectV8Handler(this, instance);
            var v8Accessor = new ScriptableObjectV8Accessor(this, instance);

            var obj = CefV8Value.CreateObject(v8Accessor);

            foreach (var property in this.PropertyDispatchTable.GetValues())
            {
                obj.SetValue(property.Name,
                    (property.CanRead ? CefV8AccessControl.AllCanRead : 0)
                    | (property.CanWrite ? CefV8AccessControl.AllCanWrite : 0),
                    CefV8PropertyAttribute.DontDelete);
            }

            foreach (var method in this.DispatchTable.GetValues())
            {
                if (method.Hidden) continue;

                obj.SetValue(method.Name,
                    CefV8Value.CreateFunction(method.Name, v8Handler)
                    );
            }

            return obj;
        }
    }
}
