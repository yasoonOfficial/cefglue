namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;

    public class JSBindingException : CefException
    {
        public JSBindingException() : base() { }
        public JSBindingException(string message) : base(message) { }
        public JSBindingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
