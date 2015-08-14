namespace CefGlue.Interop
{
    internal enum cef_proxy_type_t : int
    {
        PROXY_TYPE_DIRECT = 0,
        PROXY_TYPE_NAMED,
        PROXY_TYPE_PAC_STRING,
    }
}