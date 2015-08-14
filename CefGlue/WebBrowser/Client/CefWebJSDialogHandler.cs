namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using CefGlue.Diagnostics;

    public class CefWebJSDialogHandler : CefJSDialogHandler
    {
        private readonly CefWebBrowserCore context;

        public CefWebJSDialogHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override bool OnJSAlert(CefBrowser browser, CefFrame frame, string message)
        {
            // TODO: CefWebJSDialogHandler.OnJSAlert
            return false;
        }

        protected override bool OnJSConfirm(CefBrowser browser, CefFrame frame, string message, out bool retval)
        {
            // TODO: CefWebJSDialogHandler.OnJSConfirm
            retval = false;
            return false;
        }

        protected override bool OnJSPrompt(CefBrowser browser, CefFrame frame, string message, string defaultValue, out bool retval, out string result)
        {
            // TODO: CefWebJSDialogHandler.OnJSPrompt
            retval = false;
            result = null;
            return false;
        }
    }
}
