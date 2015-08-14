namespace CefGlue
{
    using System;
    using System.Runtime.InteropServices;
    using CefGlue.Interop;
    using Diagnostics;

    unsafe partial class CefLifeSpanHandler
    {
        /// <summary>
        /// Called before a new popup window is created.
        /// The |parentBrowser| parameter will point to the parent browser window.
        /// The |popupFeatures| parameter will contain information about the style of popup window requested.
        /// Return false to have the framework create the new popup window based on the parameters in |windowInfo|.
        /// Return true to cancel creation of the popup window.
        /// By default, a newly created popup window will have the same client and settings as the parent window.
        /// To change the client for the new window modify the object that |client| points to.
        /// To change the settings for the new window modify the |settings| structure.
        /// </summary>
        private int on_before_popup(cef_life_span_handler_t* self, cef_browser_t* parentBrowser, /*const*/ cef_popup_features_t* popupFeatures, cef_window_info_t* windowInfo, /*const*/ cef_string_t* url, cef_client_t** client, cef_browser_settings_t* settings)
        {
            ThrowIfObjectDisposed();

            var m_client = CefClient.From(*client);
            var m_parentBrowser = CefBrowser.From(parentBrowser);
            var m_popupFeatures = CefPopupFeatures.From(popupFeatures);
            var m_windowInfo = CefWindowInfo.From(windowInfo);
            var m_url = cef_string_t.ToString(url);
            var m_settings = CefBrowserSettings.From(settings);

            var o_client = m_client;
            var handled = this.OnBeforePopup(m_parentBrowser, m_popupFeatures, m_windowInfo, m_url, ref m_client, m_settings);

            if (!handled && m_client != o_client && m_client != null)
            {
                *client = m_client.GetNativePointerAndAddRef();
            }

            m_popupFeatures.Dispose();
            m_windowInfo.Dispose();
            m_settings.Dispose();

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called before a new popup window is created.
        /// The |parentBrowser| parameter will point to the parent browser window.
        /// The |popupFeatures| parameter will contain information about the style of popup window requested.
        /// Return false to have the framework create the new popup window based on the parameters in |windowInfo|.
        /// Return true to cancel creation of the popup window.
        /// By default, a newly created popup window will have the same client and settings as the parent window.
        /// To change the client for the new window modify the object that |client| points to.
        /// To change the settings for the new window modify the |settings| structure.
        /// </summary>
        protected virtual bool OnBeforePopup(
            CefBrowser parentBrowser,
            CefPopupFeatures popupFeatures,
            CefWindowInfo windowInfo,
            string url,
            ref CefClient client,
            CefBrowserSettings settings
            )
        {
            return false;
        }

        /// <summary>
        /// Called after a new window is created.
        /// </summary>
        private void on_after_created(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            ThrowIfObjectDisposed();

            this.OnAfterCreated(CefBrowser.From(browser));
        }

        /// <summary>
        /// Called after a new window is created.
        /// </summary>
        protected virtual void OnAfterCreated(CefBrowser browser)
        {
        }

        /// <summary>
        /// Called when a modal window is about to display and the modal loop
        /// should begin running. Return false to use the default modal loop
        /// implementation or true to use a custom implementation.
        /// </summary>
        private int run_modal(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);

            var handled = this.RunModal(m_browser); 

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to enter the modal loop. Provide your own modal loop here.
        /// Return true if you ran your own modal loop and false to use the default.
        /// You can also use this event to know when a modal window is about to start.
        /// </summary>
        protected virtual bool RunModal(CefBrowser browser)
        {
            return false;
        }

        /// <summary>
        /// Called when a window has recieved a request to close. Return false to
        /// proceed with the window close or true to cancel the window close. If
        /// this is a modal window and a custom modal loop implementation was
        /// provided in RunModal() this callback should be used to restore the
        /// opener window to a usable state.
        /// </summary>
        private int do_close(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);

            var handled = this.DoClose(m_browser);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called when a window has recieved a request to close.
        /// Return false to proceed with the window close or true to cancel the window close.
        /// If this is a modal window and a custom modal loop implementation was
        /// provided in RunModal() this callback should be used to restore the
        /// opener window to a usable state.
        /// </summary>
        protected virtual bool DoClose(CefBrowser browser)
        {
            return false;
        }

        /// <summary>
        /// Called just before a window is closed.
        /// If this is a modal window and you handled the RunModal() event you can use this callback to restore the opener window to a usable state.
        /// </summary>
        private void on_before_close(cef_life_span_handler_t* self, cef_browser_t* browser)
        {
            ThrowIfObjectDisposed();

            this.OnBeforeClose(CefBrowser.From(browser));
        }

        /// <summary>
        /// Called just before a window is closed.
        /// If this is a modal window and you handled the RunModal() event you can use this callback to restore the opener window to a usable state.
        /// </summary>
        protected virtual void OnBeforeClose(CefBrowser browser)
        {
        }
    }
}
