namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class ExceptionBuilder
    {

        public static JSBindingException TypeAlreadyReservesMember(Type type, string scriptingName, string declaringName)
        {
            return new JSBindingException(string.Format(
                "Type '{0}' already reserves a member for object scripting named '{1}' (given from '{2}' by naming convention rules).",
                type.FullName, scriptingName, declaringName
                ));
        }

    }
}
