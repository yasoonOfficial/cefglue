namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefKeyboardHandler
    {
        /// <summary>
        /// Called when the browser component receives a keyboard event. This method
        /// is called both before the event is passed to the renderer and after
        /// JavaScript in the page has had a chance to handle the event. |type| is the
        /// type of keyboard event, |code| is the windows scan-code for the event,
        /// |modifiers| is a set of bit- flags describing any pressed modifier keys and
        /// |isSystemKey| is true if Windows considers this a 'system key' message (see
        /// http://msdn.microsoft.com/en-us/library/ms646286(VS.85).aspx). If
        /// |isAfterJavaScript| is true then JavaScript in the page has had a chance
        /// to handle the event and has chosen not to. Only RAWKEYDOWN, KEYDOWN and
        /// CHAR events will be sent with |isAfterJavaScript| set to true. Return
        /// true if the keyboard event was handled or false to allow continued handling
        /// of the event by the renderer.
        /// </summary>
        private int on_key_event(cef_keyboard_handler_t* self, cef_browser_t* browser, cef_handler_keyevent_type_t type, int code, int modifiers, int isSystemKey, int isAfterJavaScript)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_type = (CefHandlerKeyEventType)type;
            var m_isSystemKey = isSystemKey != 0;
            var mIsAfterJavaScript = isAfterJavaScript != 0;

            var handled = this.OnKeyEvent(m_browser, m_type, code, (CefHandlerKeyEventModifiers)modifiers, m_isSystemKey, mIsAfterJavaScript);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called when the browser component receives a keyboard event.
        /// This method is called both before the event is passed to the renderer and after JavaScript in the page has had a chance to handle the event.
        /// |type| is the type of keyboard event,
        /// |code| is the windows scan-code for the event,
        /// |modifiers| is a set of bit- flags describing any pressed modifier keys and
        /// |isSystemKey| is true if Windows considers this a 'system key' message (see http://msdn.microsoft.com/en-us/library/ms646286(VS.85).aspx).
        /// If |isAfterJavaScript| is true then JavaScript in the page has had a chance to handle the event and has chosen not to.
        /// Only RAWKEYDOWN, KEYDOWN and CHAR events will be sent with |isAfterJavaScript| set to true.
        /// Return true if the keyboard event was handled or false to allow continued handling of the event by the renderer.
        /// </summary>
        protected virtual bool OnKeyEvent(CefBrowser browser, CefHandlerKeyEventType type, int code, CefHandlerKeyEventModifiers modifiers, bool isSystemKey, bool isAfterJavaScript)
        {
            return false;
        }

    }
}
