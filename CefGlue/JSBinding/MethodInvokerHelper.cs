namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;

    internal static class MethodInvokerHelper
    {
        public static void ThrowArgumentCountMismatch()
        {
            throw new JSBindingException("Argument count mismatch.");
        }

        public unsafe delegate cef_v8value_t* CreateUndefinedNativeV8Value();
    }
}
