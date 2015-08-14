namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefPermissionHandler
    {
        /// <summary>
        /// Called on the UI thread before a script extension is loaded.
        /// Return false to let the extension load normally.
        /// </summary>
        private int on_before_script_extension_load(cef_permission_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, /*const*/ cef_string_t* extensionName)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_extensionName = cef_string_t.ToString(extensionName);

            var handled = this.OnBeforeScriptExtensionLoad(m_browser, m_frame, m_extensionName);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called on the UI thread before a script extension is loaded.
        /// Return false to let the extension load normally.
        /// </summary>
        protected virtual bool OnBeforeScriptExtensionLoad(CefBrowser browser, CefFrame frame, string extensionName)
        {
            return false;
        }
    }
}
