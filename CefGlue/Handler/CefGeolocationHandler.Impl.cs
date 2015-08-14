namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Implement this interface to handle events related to geolocation permission
    /// requests. The methods of this class will be called on the browser process UI
    /// thread.
    /// </summary>
    unsafe partial class CefGeolocationHandler
    {
        /// <summary>
        /// Called when a page requests permission to access geolocation information.
        /// |requesting_url| is the URL requesting permission and |request_id| is the
        /// unique ID for the permission request. Call
        /// cef_geolocation_callback_t::Continue to allow or deny the permission
        /// request.
        /// </summary>
        private void on_request_geolocation_permission(cef_geolocation_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* requesting_url, int request_id, cef_geolocation_callback_t* callback)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_requestingUrl = cef_string_t.ToString(requesting_url);
            var m_callback = CefGeolocationCallback.From(callback);

            OnRequestGeolocationPermission(m_browser, m_requestingUrl, request_id, m_callback);
        }

        protected virtual void OnRequestGeolocationPermission(CefBrowser browser, string requestingUrl, int requestId, CefGeolocationCallback callback)
        {
            callback.Continue(1);
        }

        /// <summary>
        /// Called when a geolocation access request is canceled.
        /// |requesting_url| is the URL that originally requested permission and
        /// |request_id| is the unique ID for the permission request.
        /// </summary>
        private void on_cancel_geolocation_permission(cef_geolocation_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* requesting_url, int request_id)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_requestingUrl = cef_string_t.ToString(requesting_url);

            OnCancelGeolocationPermission(m_browser, m_requestingUrl, request_id);
        }

        /// <summary>
        /// Called when a geolocation access request is canceled.
        /// |requesting_url| is the URL that originally requested permission and
        /// |request_id| is the unique ID for the permission request.
        /// </summary>
        protected virtual void OnCancelGeolocationPermission(CefBrowser browser, string requestingUrl, int requestId)
        {

        }
    }
}
