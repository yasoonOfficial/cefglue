namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefRequestHandler
    {
        /// <summary>
        /// Called on the UI thread before browser navigation. Return true to
        /// cancel the navigation or false to allow the navigation to proceed.
        /// </summary>
        private int on_before_browse(cef_request_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_request_t* request, cef_handler_navtype_t navType, int isRedirect)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_request = CefRequest.From(request);
            var m_navType = (CefHandlerNavType)navType;
            var m_isRedirect = isRedirect != 0;

            var handled = this.OnBeforeBrowse(m_browser, m_frame, m_request, m_navType, m_isRedirect);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called on the UI thread before browser navigation.
        /// Return true to cancel the navigation 
        /// or false to allow the navigation to proceed.
        /// </summary>
        protected virtual bool OnBeforeBrowse(CefBrowser browser, CefFrame frame, CefRequest request, CefHandlerNavType navType, bool isRedirect)
        {
            return false;
        }


        /// <summary>
        /// Called on the IO thread before a resource is loaded.  To allow the
        /// resource to load normally return false. To redirect the resource to a
        /// new url populate the |redirectUrl| value and return false.  To
        /// specify data for the resource return a CefStream object in
        /// |resourceStream|, use the |response| object to set mime type, HTTP
        /// status code and optional header values, and return false. To cancel
        /// loading of the resource return true. Any modifications to |request|
        /// will be observed.  If the URL in |request| is changed and
        /// |redirectUrl| is also set, the URL in |request| will be used.
        /// </summary>
        private int on_before_resource_load(cef_request_handler_t* self, cef_browser_t* browser, cef_request_t* request, cef_string_t* redirectUrl, cef_stream_reader_t** resourceStream, cef_response_t* response, int loadFlags)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_request = CefRequest.From(request);
            string m_redirectUrl;
            CefStreamReader m_resourceStream;
            var m_response = CefResponse.From(response);

            var handled = this.OnBeforeResourceLoad(m_browser, m_request, out m_redirectUrl, out m_resourceStream, m_response, loadFlags);

            if (!handled)
            {
                if (!string.IsNullOrEmpty(m_redirectUrl))
                {
                    cef_string_t.Copy(m_redirectUrl, redirectUrl);
                }

                if (m_resourceStream != null)
                {
                    *resourceStream = m_resourceStream.GetNativePointerAndAddRef();
                }
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called on the IO thread when a resource load is redirected. The
        /// |old_url| parameter will contain the old URL. The |new_url| parameter
        /// will contain the new URL and can be changed if desired.
        /// </summary>
        private void on_resource_redirect(cef_request_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* old_url, cef_string_t* new_url)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);

            string oldUrl = "", newUrl = "";

            cef_string_t.Copy(oldUrl, old_url);
            cef_string_t.Copy(newUrl, new_url);
            
            this.OnResourceRedirect(m_browser, oldUrl, newUrl);
        }

        /// <summary>
        /// Called on the IO thread when a resource load is redirected. The
        /// |old_url| parameter will contain the old URL. The |new_url| parameter
        /// will contain the new URL and can be changed if desired.
        /// </summary>
        protected virtual void OnResourceRedirect(CefBrowser browser, string oldUrl, string newUrl)
        {
            
        }

        /// <summary>
        /// Called on the IO thread before a resource is loaded.
        /// To allow the resource to load normally return false.
        /// To redirect the resource to a new url populate the |redirectUrl| value and return false.
        /// To specify data for the resource return a CefStream object in |resourceStream|,
        /// use the |response| object to set mime type, HTTP status code and optional header values, and return false.
        /// To cancel loading of the resource return true.
        /// Any modifications to |request| will be observed.
        /// If the URL in |request| is changed and |redirectUrl| is also set, the URL in |request| will be used.
        /// </summary>
        protected virtual bool OnBeforeResourceLoad(CefBrowser browser, CefRequest request, out string redirectUrl, out CefStreamReader resourceStream, CefResponse response, int loadFlags)
        {
            redirectUrl = null;
            resourceStream = null;
            return false;
        }


        /// <summary>
        /// Called on the UI thread after a response to the resource request is
        /// received. Set |filter| if response content needs to be monitored
        /// and/or modified as it arrives.
        /// </summary>
        private void on_resource_response(cef_request_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* url, cef_response_t* response, cef_content_filter_t** filter)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_url = cef_string_t.ToString(url);
            var m_response = CefResponse.From(response);
            CefContentFilter m_filter;

            this.OnResourceResponse(m_browser, m_url, m_response, out m_filter);

            if (m_filter != null)
            {
                *filter = m_filter.GetNativePointerAndAddRef();
            }
        }

        /// <summary>
        /// Called on the UI thread after a response to the resource request is received.
        /// Set |filter| if response content needs to be monitored and/or modified as it arrives.
        /// </summary>
        protected virtual void OnResourceResponse(CefBrowser browser, string url, CefResponse response, out CefContentFilter filter)
        {
            filter = null;
        }

        /// <summary>
        /// Called on the IO thread to handle requests for URLs with an unknown
        /// protocol component. Return true to indicate that the request should
        /// succeed because it was handled externally. Set |allowOSExecution| to
        /// true and return false to attempt execution via the registered OS
        /// protocol handler, if any. If false is returned and either
        /// |allow_os_execution| is false or OS protocol handler execution fails
        /// then the request will fail with an error condition. SECURITY WARNING:
        /// YOU SHOULD USE THIS METHOD TO ENFORCE RESTRICTIONS BASED ON SCHEME,
        /// HOST OR OTHER URL ANALYSIS BEFORE ALLOWING OS EXECUTION.
        /// </summary>
        private int on_protocol_execution(cef_request_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* url, int* allowOSExecution)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_url = cef_string_t.ToString(url);
            bool m_allowOSExecution;

            var handled = this.OnProtocolExecution(m_browser, m_url, out m_allowOSExecution);

            *allowOSExecution = m_allowOSExecution ? 1 : 0;

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called on the IO thread to handle requests for URLs with an unknown protocol component.
        /// Return true to indicate that the request should succeed because it was handled externally.
        /// Set |allowOSExecution| to true and return false to attempt execution via the registered OS protocol handler, if any.
        /// If false is returned and either |allow_os_execution| is false or OS protocol handler execution fails then the request will fail with an error condition.
        /// SECURITY WARNING: YOU SHOULD USE THIS METHOD TO ENFORCE RESTRICTIONS BASED ON SCHEME, HOST OR OTHER URL ANALYSIS BEFORE ALLOWING OS EXECUTION.
        /// </summary>
        protected virtual bool OnProtocolExecution(CefBrowser browser, string url, out bool allowOSExecution)
        {
            allowOSExecution = false;
            return false;
        }


        /// <summary>
        /// Called on the UI thread when a server indicates via the 'Content-
        /// Disposition' header that a response represents a file to download.
        /// |mimeType| is the mime type for the download, |fileName| is the
        /// suggested target file name and |contentLength| is either the value of
        /// the 'Content-Size' header or -1 if no size was provided. Set
        /// |handler| to the CefDownloadHandler instance that will recieve the
        /// file contents. Return true to download the file or false to cancel
        /// the file download.
        /// </summary>
        private int get_download_handler(cef_request_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* mimeType, /*const*/ cef_string_t* fileName, long contentLength, cef_download_handler_t** handler)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_mimeType = cef_string_t.ToString(mimeType);
            var m_fileName = cef_string_t.ToString(fileName);
            CefDownloadHandler m_handler;

            var handled = this.GetDownloadHandler(m_browser, m_mimeType, m_fileName, contentLength, out m_handler);

            if (m_handler != null)
            {
                *handler = m_handler.GetNativePointerAndAddRef();
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called on the UI thread when a server indicates via the 'Content-Disposition' header that a response represents a file to download.
        /// |mimeType| is the mime type for the download,
        /// |fileName| is the suggested target file name 
        /// and |contentLength| is either the value of the 'Content-Size' header or -1 if no size was provided.
        /// Set |handler| to the CefDownloadHandler instance that will recieve the file contents.
        /// Return true to download the file or false to cancel the file download.
        /// </summary>
        protected virtual bool GetDownloadHandler(CefBrowser browser, string mimeType, string fileName, long contentLength, out CefDownloadHandler handler)
        {
            handler = null;
            return false;
        }


        /// <summary>
        /// Called on the IO thread when the browser needs credentials from the
        /// user. |isProxy| indicates whether the host is a proxy server. |host|
        /// contains the hostname and port number. Set |username| and |password|
        /// and return true to handle the request. Return false to cancel the
        /// request.
        /// </summary>
        private int get_auth_credentials(cef_request_handler_t* self, cef_browser_t* browser, int isProxy, /*const*/ cef_string_t* host, int port, /*const*/ cef_string_t* realm, /*const*/ cef_string_t* scheme, cef_string_t* username, cef_string_t* password)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_isProxy = isProxy != 0;
            var m_host = cef_string_t.ToString(host);
            var m_realm = cef_string_t.ToString(realm);
            var m_scheme = cef_string_t.ToString(scheme);
            string m_username;
            string m_password;

            var handled = this.GetAuthCredentials(m_browser, m_isProxy, m_host, port, m_realm, m_scheme, out m_username, out m_password);

            if (handled)
            {
                cef_string_t.Copy(m_username, username);
                cef_string_t.Copy(m_password, password);
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called on the IO thread when the browser needs credentials from the user. 
        /// |isProxy| indicates whether the host is a proxy server.
        /// |host| contains the hostname and port number.
        /// Set |username| and |password| and return true to handle the request.
        /// Return false to cancel the request.
        /// </summary>
        protected virtual bool GetAuthCredentials(CefBrowser browser, bool isProxy, string host, int port, string realm, string scheme, out string username, out string password)
        {
            username = null;
            password = null;
            return false;
        }

        /// <summary>
        /// Called on the IO thread to retrieve the cookie manager. |main_url| is the
        /// URL of the top-level frame. Cookies managers can be unique per browser or
        /// shared across multiple browsers. The global cookie manager will be used if
        /// this method returns NULL.
        /// </summary>
        private cef_cookie_manager_t* get_cookie_manager(cef_request_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* main_url)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_mainUrl = cef_string_t.ToString(main_url);

            var cookieManager = this.GetCookieManager(m_browser, m_mainUrl);

            return cookieManager != null ? cookieManager.GetNativePointerAndAddRef() : null;
        }

        /// <summary>
        /// Called on the IO thread to retrieve the cookie manager. |main_url| is the
        /// URL of the top-level frame. Cookies managers can be unique per browser or
        /// shared across multiple browsers. The global cookie manager will be used if
        /// this method returns NULL.
        /// </summary>
        protected virtual CefCookieManager GetCookieManager(CefBrowser browser, string mainUrl)
        {
            return null;
        }
    }
}
