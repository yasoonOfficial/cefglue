namespace CefGlue.WebBrowser
{
    using System;
    using System.ComponentModel;

    public sealed class CefDocumentCompletedEventArgs : EventArgs
    {
        private readonly CefFrame frame;
        private readonly int httpStatusCode;

        public CefDocumentCompletedEventArgs(CefFrame frame, int httpStatusCode)
        {
            this.frame = frame;
            this.httpStatusCode = httpStatusCode;
        }

        public CefFrame Frame
        {
            get { return this.frame; }
        }

        public int HttpStatusCode
        {
            get { return this.httpStatusCode; }
        }
    }
}
