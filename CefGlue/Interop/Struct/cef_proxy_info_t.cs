namespace CefGlue.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_proxy_info_t
    {
        public cef_proxy_type_t proxyType;
        public cef_string_t proxyList;
    }
}