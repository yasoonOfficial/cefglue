namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefProxyHandler
    {
        /// <summary>
        /// Called to retrieve proxy information for the specified |url|.
        /// </summary>
        private void get_proxy_for_url(cef_proxy_handler_t* self, /*const*/ cef_string_t* url, cef_proxy_info_t* proxy_info)
        {
            ThrowIfObjectDisposed();
            // TODO: CefProxyHandler.get_proxy_for_url
            throw new NotImplementedException();
        }

    }
}
