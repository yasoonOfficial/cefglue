namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;

    public class JavaScriptException : Exception
    {
        public JavaScriptException() : base() { }
        public JavaScriptException(string message) : base(message) { }
        public JavaScriptException(string message, Exception innerException) : base(message, innerException) { }
    }
}
