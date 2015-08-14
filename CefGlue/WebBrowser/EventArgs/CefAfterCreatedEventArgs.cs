using System;

namespace CefGlue.WebBrowser
{
    public class CefAfterCreatedEventArgs : EventArgs
    {
        private readonly CefBrowser browser;

        public CefAfterCreatedEventArgs(CefBrowser browser)
        {
            this.browser = browser;
        }

        public CefBrowser Browser
        {
            get { return browser; }
        }
    }
}