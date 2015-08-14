namespace CefGlue.WebBrowser
{
    using System;

    public sealed class ConsoleMessageEventArgs : EventArgs
    {
        private readonly string message;
        private readonly string source;
        private readonly int line;

        public ConsoleMessageEventArgs(string message, string source, int line)
        {
            this.message = message;
            this.source = source;
            this.line = line;
        }

        public string Message
        {
            get { return this.message; }
        }

        public string Source
        {
            get { return this.source; }
        }

        public int Line
        {
            get { return this.line; }
        }
    }
}
