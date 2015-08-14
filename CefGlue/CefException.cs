namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;

    public class CefException : Exception
    {
        public CefException() : base() { }
        public CefException(string message) : base(message) { }
        public CefException(string message, Exception innerException) : base(message, innerException) { }
    }
}
