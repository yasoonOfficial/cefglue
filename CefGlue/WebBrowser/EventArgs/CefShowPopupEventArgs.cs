using System;

namespace CefGlue.WebBrowser
{
    public class CefShowPopupEventArgs : EventArgs
    {
        private readonly CefBrowser parentBrowser;
        private readonly bool show;

        public CefShowPopupEventArgs(CefBrowser parentBrowser, bool show)
        {
            this.parentBrowser = parentBrowser;
            this.show = show;
        }

        public CefBrowser ParentBrowser
        {
            get { return parentBrowser; }
        }

        public bool Show
        {
            get { return show; }
        }
    }
}