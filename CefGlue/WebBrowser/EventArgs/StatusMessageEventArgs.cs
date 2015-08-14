namespace CefGlue.WebBrowser
{
    using System;

    public sealed class StatusMessageEventArgs : EventArgs
    {
        private readonly CefHandlerStatusType type;
        private readonly string value;

        public StatusMessageEventArgs(CefHandlerStatusType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public CefHandlerStatusType Type
        {
            get { return this.type; }
        }

        public string Value
        {
            get { return this.value; }
        }
    }
}
