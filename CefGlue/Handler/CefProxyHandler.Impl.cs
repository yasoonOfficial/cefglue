namespace CefGlue
{
    using System;
    using CefGlue.Interop;
	using CefGlue.Struct;

    unsafe partial class CefProxyHandler
    {
        /// <summary>
        /// Called to retrieve proxy information for the specified |url|.
        /// </summary>
        private void get_proxy_for_url(cef_proxy_handler_t* self, /*const*/ cef_string_t* url, cef_proxy_info_t* proxy_info)
        {
            ThrowIfObjectDisposed();
			string m_url = cef_string_t.ToString(url);
			CefProxyInfo m_info = CefProxyInfo.From(proxy_info);
			GetProxyForUrl(this, m_url, m_info);
        }

		protected virtual void GetProxyForUrl(CefProxyHandler handler, string url, CefProxyInfo proxyInfo)
		{
		} 
    }
}
