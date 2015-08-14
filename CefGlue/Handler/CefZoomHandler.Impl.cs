namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefZoomHandler
    {
        /// <summary>
        /// Called when the browser wants to retrieve the zoom level for the
        /// given |url|. Return true (1) if |zoomLevel| has been set to the
        /// custom zoom level. Return false (0) for the browser's default zoom
        /// handling behavior.
        /// </summary>
        private int on_get_zoom_level(cef_zoom_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* url, double* zoomLevel)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_url = cef_string_t.ToString(url);

            double m_zoomLevel = 0;
            if (OnGetZoomLevel(m_browser, m_url, ref m_zoomLevel))
            {
                *zoomLevel = m_zoomLevel;
                return 1;
            }
            else
                return 0;
        }

        /// <summary>
        /// Called when the browser wants to retrieve the zoom level for the
        /// given |url|. Return true if |zoomLevel| has been set to the
        /// custom zoom level. Return false for the browser's default zoom
        /// handling behavior.
        /// </summary>
        protected virtual bool OnGetZoomLevel(CefBrowser browser, string url, ref double zoomLevel)
        {
            return false;
        }

        /// <summary>
        /// Called when the browser's zoom level has been set to |zoomLevel| for
        /// the given |url|. Return true (1) to indicate that the new setting has
        /// been handled. Return false (0) to use the browser's default zoom
        /// handling behavior.
        /// </summary>
        private int on_set_zoom_level(cef_zoom_handler_t* self, cef_browser_t* browser, /*const*/ cef_string_t* url, double zoomLevel)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_url = cef_string_t.ToString(url);

            return OnSetZoomLevel(m_browser, m_url, zoomLevel) ? 1 : 0;
        }

        /// <summary>
        /// Called when the browser's zoom level has been set to |zoomLevel| for
        /// the given |url|. Return true to indicate that the new setting has
        /// been handled. Return false to use the browser's default zoom
        /// handling behavior.
        /// </summary>
        protected virtual bool OnSetZoomLevel(CefBrowser browser, string url, double zoomLevel)
        {
            return false;
        }
    }
}
