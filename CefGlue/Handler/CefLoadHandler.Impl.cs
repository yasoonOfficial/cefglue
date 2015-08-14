namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefLoadHandler
    {
        /// <summary>
        /// Called when the browser begins loading a frame. The |frame| value
        /// will never be empty -- call the IsMain() method to check if this
        /// frame is the main frame. Multiple frames may be loading at the same
        /// time. Sub-frames may start or continue loading after the main frame
        /// load has ended. This method may not be called for a particular frame
        /// if the load request for that frame fails.
        /// </summary>
        private void on_load_start(cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            this.OnLoadStart(m_browser, m_frame);
        }

        /// <summary>
        /// Called when the browser begins loading a frame.
        /// The |frame| value will never be empty -- call the IsMain() method to check if this frame is the main frame.
        /// Multiple frames may be loading at the same time.
        /// Sub-frames may start or continue loading after the main frame load has ended.
        /// This method may not be called for a particular frame if the load request for that frame fails.
        /// </summary>
        protected virtual void OnLoadStart(CefBrowser browser, CefFrame frame)
        {
        }

        /// <summary>
        /// Called when the browser is done loading a frame. The |frame| value
        /// will never be empty -- call the IsMain() method to check if this
        /// frame is the main frame. Multiple frames may be loading at the same
        /// time. Sub-frames may start or continue loading after the main frame
        /// load has ended. This method will always be called for all frames
        /// irrespective of whether the request completes successfully.
        /// </summary>
        private void on_load_end(cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, int httpStatusCode)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            this.OnLoadEnd(m_browser, m_frame, httpStatusCode);
        }

        /// <summary>
        /// Called when the browser is done loading a frame.
        /// The |frame| value will never be empty -- call the IsMain() method to check if this frame is the main frame.
        /// Multiple frames may be loading at the same time.
        /// Sub-frames may start or continue loading after the main frame load has ended.
        /// This method will always be called for all frames irrespective of whether the request completes successfully.
        /// </summary>
        protected virtual void OnLoadEnd(CefBrowser browser, CefFrame frame, int httpStatusCode)
        {
        }

        /// <summary>
        /// Called when the browser fails to load a resource. |errorCode| is the
        /// error code number and |failedUrl| is the URL that failed to load. To
        /// provide custom error text assign the text to |errorText| and return
        /// true. Otherwise, return false for the default error text. See
        /// net\base\net_error_list.h for complete descriptions of the error
        /// codes.
        /// </summary>
        private int on_load_error(cef_load_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_handler_errorcode_t errorCode, /*const*/ cef_string_t* failedUrl, cef_string_t* errorText)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_failedUrl = cef_string_t.ToString(failedUrl);
            var m_errorText = cef_string_t.ToString(errorText);
            var c_errorText = m_errorText;
            var result = this.OnLoadError(m_browser, m_frame, (CefHandlerErrorCode)errorCode, m_failedUrl, ref m_errorText) ? 1 : 0;
            if ((object)c_errorText != (object)m_errorText)
            {
                cef_string_t.Copy(m_errorText, errorText);
            }
            return result;
        }

        /// <summary>
        /// Called when the browser fails to load a resource.
        /// |errorCode| is the error code number and |failedUrl| is the URL that failed to load.
        /// To provide custom error text assign the text to |errorText| and return true.
        /// Otherwise, return false for the default error text.
        /// See net\base\net_error_list.h for complete descriptions of the error codes.
        /// </summary>
        protected virtual bool OnLoadError(CefBrowser browser, CefFrame frame, CefHandlerErrorCode errorCode, string failedUrl, ref string errorText)
        {
            // FIXME: change api... instead of return true/false we can return errorText (custom), or null/empty to default error text.
            return false;
        }
    }
}
