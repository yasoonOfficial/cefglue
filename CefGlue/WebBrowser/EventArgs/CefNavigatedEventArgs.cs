namespace CefGlue.WebBrowser
{
    using System;
    using System.ComponentModel;

    public sealed class CefNavigatedEventArgs : EventArgs
    {
        private readonly CefFrame frame;

        public CefNavigatedEventArgs(CefFrame frame)
        {
            this.frame = frame;
        }

        public CefFrame Frame
        {
            get { return this.frame; }
        }
    }
}
