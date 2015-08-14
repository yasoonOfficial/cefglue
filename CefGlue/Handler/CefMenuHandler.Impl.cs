namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefMenuHandler
    {
        /// <summary>
        /// Called before a context menu is displayed. Return false to display
        /// the default context menu or true to cancel the display.
        /// </summary>
        private int on_before_menu(cef_menu_handler_t* self, cef_browser_t* browser, /*const*/ cef_menu_info_t* menuInfo)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_menuInfo = CefMenuInfo.From(menuInfo);

            var handled = this.OnBeforeMenu(m_browser, m_menuInfo);

            //m_menuInfo.Dispose();

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called before a context menu is displayed.
        /// Return false to display the default context menu
        /// or true to cancel the display.
        /// </summary>
        protected virtual bool OnBeforeMenu(CefBrowser browser, CefMenuInfo menuInfo)
        {
            return false;
        }

        /// <summary>
        /// Called to optionally override the default text for a context menu
        /// item. |label| contains the default text and may be modified to
        /// substitute alternate text.
        /// </summary>
        private void get_menu_label(cef_menu_handler_t* self, cef_browser_t* browser, cef_menu_id_t menuId, cef_string_t* label)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_menuId = (CefHandlerMenuId)menuId;
            var m_label = cef_string_t.ToString(label);

            var o_label = m_label;
            this.GetMenuLabel(m_browser, m_menuId, ref m_label);

            if ((object)m_label != (object)o_label)
            {
                cef_string_t.Copy(m_label, label);
            }
        }

        /// <summary>
        /// Called to optionally override the default text for a context menu item.
        /// |label| contains the default text and may be modified to substitute alternate text.
        /// </summary>
        protected virtual void GetMenuLabel(CefBrowser browser, CefHandlerMenuId menuId, ref string label)
        {
        }

        /// <summary>
        /// Called when an option is selected from the default context menu.
        /// Return false to execute the default action or true to cancel the
        /// action.
        /// </summary>
        private int on_menu_action(cef_menu_handler_t* self, cef_browser_t* browser, cef_menu_id_t menuId)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_menuId = (CefHandlerMenuId)menuId;

            var handled = this.OnMenuAction(m_browser, m_menuId);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called when an option is selected from the default context menu.
        /// Return false to execute the default action or true to cancel the action.
        /// </summary>
        protected virtual bool OnMenuAction(CefBrowser browser, CefHandlerMenuId menuId)
        {
            return false;
        }


    }
}
