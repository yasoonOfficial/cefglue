namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Diagnostics;

#if DIAGNOSTICS
    using CefGlue.Diagnostics;
#endif

    internal class TestV8Handler : CefV8Handler
    {
        protected override void Dispose(bool disposing)
        {
            ;
            base.Dispose(disposing);
        }
    }

    internal class ClientV8Handler : CefV8Handler
    {
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            try
            {
                if (name == "Log")
                {
                    var message = arguments[0].GetStringValue();
                    #if DIAGNOSTICS
                    Cef.Logger.Info(LogTarget.Default, message);
                    #endif
                    returnValue = null;
                }
                else if (name == "ReturnVoid")
                {
                    returnValue = null;
                }
                else if (name == "ReturnVoidAndDisposeThis")
                {
                    returnValue = null;

                    if (obj != null) { obj.Dispose(); obj = null; }
                }
                else if (name == "ReturnUndefined")
                {
                    returnValue = CefV8Value.CreateUndefined();
                }
                else if (name == "ReturnNull")
                {
                    returnValue = CefV8Value.CreateNull();
                }
                else if (name == "ReturnBool")
                {
                    returnValue = CefV8Value.CreateBool(true);
                }
                else if (name == "ReturnInt")
                {
                    returnValue = CefV8Value.CreateInt(12345678);
                }
                else if (name == "ReturnDouble")
                {
                    returnValue = CefV8Value.CreateDouble(1234.5678);
                }
                else if (name == "ReturnDate")
                {
                    returnValue = CefV8Value.CreateDate(DateTime.UtcNow);
                }
                else if (name == "ReturnString")
                {
                    returnValue = CefV8Value.CreateString("Some string, passed to CEF!");
                }
                else if (name == "ReturnArray")
                {
                    var array = CefV8Value.CreateArray(3);
                    array.SetValue(0, CefV8Value.CreateInt(123));
                    array.SetValue(1, CefV8Value.CreateString("hello!"));
                    array.SetValue(2, CefV8Value.CreateBool(false));

                    returnValue = array;
                }
                else if (name == "ReturnObject")
                {
                    var obj1 = CefV8Value.CreateObject();
                    obj1.SetValue("index", CefV8Value.CreateInt(123));
                    obj1.SetValue("reply", CefV8Value.CreateString("hello!"));
                    obj1.SetValue("success", CefV8Value.CreateBool(false));

                    returnValue = obj1;
                }
                else if (name == "ReturnComplexArray")
                {
                    var obj1 = CefV8Value.CreateObject();
                    obj1.SetValue("index", CefV8Value.CreateInt(123));
                    obj1.SetValue("reply", CefV8Value.CreateString("hello!"));
                    obj1.SetValue("success", CefV8Value.CreateBool(false));

                    var array = CefV8Value.CreateArray(5);
                    array.SetValue(0, CefV8Value.CreateInt(123));
                    array.SetValue(1, CefV8Value.CreateString("hello!"));
                    array.SetValue(2, CefV8Value.CreateBool(false));
                    array.SetValue(3, obj1);
                    array.SetValue(4, CefV8Value.CreateString("hello2!"));

                    obj1 = CefV8Value.CreateObject();
                    obj1.SetValue("index", CefV8Value.CreateInt(123));
                    obj1.SetValue("reply", CefV8Value.CreateString("hello!"));
                    obj1.SetValue("success", CefV8Value.CreateBool(false));

                    var obj2 = CefV8Value.CreateObject();
                    obj2.SetValue("i'm still", CefV8Value.CreateString("alive"));

                    obj1.SetValue("inner", obj2);

                    array.SetValue(5, obj1);

                    returnValue = array;
                }
                else if (name == "ReturnComplexObject")
                {
                    var obj1 = CefV8Value.CreateObject();
                    obj1.SetValue("index", CefV8Value.CreateInt(123));
                    obj1.SetValue("reply", CefV8Value.CreateString("hello!"));
                    obj1.SetValue("success", CefV8Value.CreateBool(false));

                    var obj2 = CefV8Value.CreateObject();
                    obj2.SetValue("i'm still", CefV8Value.CreateString("alive"));

                    obj1.SetValue("inner", obj2);

                    obj2.Dispose(); // force to dispose object wrapper and underlying v8 persistent handle.
                    // note, that obj2 will passed in obj before, but it anyway safe to destroy obj2 handle, 
                    // 'cause v8 api internally always open handles.

                    returnValue = obj1;
                }
                else if (name == "SubtractIntImplicit")
                {
                    var a = arguments[0].GetIntValue();
                    var b = arguments[1].GetIntValue();

                    returnValue = CefV8Value.CreateInt(a - b);
                }
                else if (name == "SubtractIntExplicit")
                {
                    if (!arguments[0].IsInt) throw new ArgumentException("arg0");
                    var a = arguments[0].GetIntValue();

                    if (!arguments[1].IsInt) throw new ArgumentException("arg1");
                    var b = arguments[1].GetIntValue();

                    returnValue = CefV8Value.CreateInt(a - b);
                }
                else if (name == "Dump")
                {
                    returnValue = CefV8Value.CreateString(Dump(arguments));
                }
                else if (name == "get_PrivateWorkingSet")
                {
                    var result = Process.GetCurrentProcess().PrivateMemorySize64 / (1024.0 * 1024.0);
                    returnValue = CefV8Value.CreateDouble(result);
                }
                else if (name == "leakTestV8Func")
                {
                    var handler = new TestV8Handler();
                    for (var i = 0; i < 100000; i++)
                    {
                        var x = CefV8Value.CreateFunction("LeakTest", handler);
                        x.Dispose();
                    }
                    returnValue = CefV8Value.CreateBool(true);
                }
                else
                {
                    returnValue = null;
                    exception = null;
                    return false;
                }

                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                returnValue = null;
                exception = ex.ToString();
                return true;
            }
        }

        private string Dump(CefV8Value[] arguments)
        {
            var result = new StringBuilder();

            var argCount = arguments != null ? arguments.Length : 0;
            for (var i = 0; i < argCount; i++)
            {
                result.AppendFormat("arg[{0}] = ", i);
                WriteV8Value(arguments[i], result);
                result.Append('\n');
            }

            return result.ToString();
        }

        private void WriteV8Value(CefV8Value value, StringBuilder result, int indent = 0)
        {
            /*
            var isUndefined = value.IsUndefined;
            var isNull = value.IsNull;
            var isBool = value.IsBool;
            var isInt = value.IsInt;
            var isDouble = value.IsDouble;
            var isDate = value.IsDate;
            var isString = value.IsString;
            var isArray = value.IsArray;
            var isObject = value.IsObject;
            var isFunction = value.IsFunction;

            result.Append("[");
            if (isUndefined) result.Append("undefined ");
            if (isNull) result.Append("null ");
            if (isBool) result.Append("bool ");
            if (isInt) result.Append("int ");
            if (isDouble) result.Append("double ");
            if (isDate) result.Append("date ");
            if (isString) result.Append("string ");
            if (isArray) result.Append("array ");
            if (isObject) result.Append("object ");
            if (isFunction) result.Append("function");
            result.Append("]");
            */

            if (value.IsUndefined)
            {
                result.Append("(undefined)");
            }
            else if (value.IsNull)
            {
                result.Append("(null)");
            }
            else if (value.IsBool)
            {
                result.AppendFormat("(bool) {0}", value.GetBoolValue() ? "true" : "false");
            }
            else if (value.IsInt)
            {
                result.AppendFormat("(int) {0}", value.GetIntValue());
            }
            else if (value.IsDouble)
            {
                result.AppendFormat("(double) {0}", value.GetDoubleValue().ToString(CultureInfo.InvariantCulture.NumberFormat));
            }
            else if (value.IsDate)
            {
                result.AppendFormat("(date) {0}", value.GetDateValue().ToString("s"));
            }
            else if (value.IsString)
            {
                result.AppendFormat("(string) {0}", value.GetStringValue());
            }
            else if (value.IsArray) // for array IsObject also is true
            {
                var indentString = string.Empty.PadLeft((indent + 1) * 4, ' ');
                result.Append("(array) [");
                var length = value.GetArrayLength();
                for (var i = 0; i < length; i++)
                {
                    result.AppendFormat("\n{0}{1} = ", indentString, i);
                    WriteV8Value(value.GetValue(i), result, indent + 1);
                }
                if (length != 0)
                {
                    result.Append('\n');
                    result.Append(indentString);
                }
                result.Append(']');
            }
            else if (value.IsFunction) // for function IsObject also is true
            {
                var name = value.GetFunctionName();
                var handler = value.GetFunctionHandler();
                var declaration = value.GetStringValue();

                result.Append("(function) ");
                result.Append(!string.IsNullOrEmpty(name) ? name : "(anonymous)");
                if (handler != null)
                {
                    result.Append(" (handler: ");
                    result.Append(handler.ToString());
                    result.Append(")");
                }
                if (!string.IsNullOrEmpty(declaration))
                {
                    result.Append(" = ");
                    result.Append(declaration);
                }
            }
            else if (value.IsObject)
            {
                var indentString = string.Empty.PadLeft((indent + 1) * 4, ' ');
                result.Append("(object) {");
                var keys = value.GetKeys().AsEnumerable();
                foreach (var key in keys)
                {
                    result.AppendFormat("\n{0}{1} = ", indentString, key);
                    WriteV8Value(value.GetValue(key), result, indent + 1);
                }
                if (keys.Any())
                {
                    result.Append('\n');
                    result.Append(indentString);
                }
                result.Append('}');
            }
            //else result.Append("(unknown)");
        }
    }
}
