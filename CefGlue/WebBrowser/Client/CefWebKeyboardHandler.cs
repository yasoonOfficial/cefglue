namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using CefGlue.Diagnostics;

    public class CefWebKeyboardHandler : CefKeyboardHandler
    {
        private readonly CefWebBrowserCore context;

        public CefWebKeyboardHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override bool OnKeyEvent(CefBrowser browser, CefHandlerKeyEventType type, int code, CefHandlerKeyEventModifiers modifiers, bool isSystemKey, bool isAfterJavaScript)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefKeyboardHandler, "OnKeyEvent: type=[{0}] code=[{1}] modifiers=[{2}] isSystemKey=[{3}] isAfterJavaScript=[{4}]", type, code, modifiers, isSystemKey, isAfterJavaScript);
#endif

            // TODO: CefWebKeyboardHandler.OnKeyEvent: return this.context.PostKeyEvent(type, code, modifiers, isSystemKey);
            return false;
        }

    }
}
