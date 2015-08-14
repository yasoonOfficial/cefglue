namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefSchemeHandlerFactory
    {
        /// <summary>
        /// Return a new scheme handler instance to handle the request. |browser| will
        /// be the browser window that initiated the request. If the request was
        /// initiated using the CefWebURLRequest API |browser| will be NULL.
        /// </summary>
        private cef_scheme_handler_t* create(cef_scheme_handler_factory_t* self, cef_browser_t* browser, /*const*/ cef_string_t* scheme_name, cef_request_t* request)
        {
            ThrowIfObjectDisposed();

            var mBrowser = CefBrowser.FromOrDefault(browser);
            var m_schemeName = cef_string_t.ToString(scheme_name);
            var m_request = CefRequest.From(request);

            var handler = this.Create(mBrowser, m_schemeName, m_request);

            return handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return a new scheme handler instance to handle the request.
        /// |browser| will be the browser window that initiated the request.
        /// If the request was initiated using the CefWebURLRequest API |browser| will be NULL.
        /// </summary>
        protected abstract CefSchemeHandler Create(CefBrowser browser, string schemeName, CefRequest request);


    }
}
