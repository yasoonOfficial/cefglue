namespace CefGlue.WebBrowser
{
    using System;
    using System.ComponentModel;

    public sealed class CefNavigatingEventArgs : CancelEventArgs
    {
        private readonly CefFrame frame;
        private readonly CefRequest request;
        private readonly CefHandlerNavType navType;
        private readonly bool isRedirect;

        public CefNavigatingEventArgs(CefFrame frame, CefRequest request, CefHandlerNavType navType, bool isRedirect)
        {
            this.frame = frame;
            this.request = request;
            this.navType = navType;
            this.isRedirect = isRedirect;
        }

        public CefFrame Frame
        {
            get { return this.frame; }
        }

        public CefRequest Request
        {
            get { return this.request; }
        }

        public CefHandlerNavType NavType
        {
            get { return this.navType; }
        }

        public bool IsRedirect
        {
            get { return this.isRedirect; }
        }
    }
}
