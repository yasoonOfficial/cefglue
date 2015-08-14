using CefGlue.Interop;

namespace CefGlue
{
    unsafe partial class CefV8Exception
    {
        public string GetMessage()
        {
            return cef_string_userfree.GetStringAndFree(cef_v8exception_t.invoke_get_message(ptr));
        }
    }
}