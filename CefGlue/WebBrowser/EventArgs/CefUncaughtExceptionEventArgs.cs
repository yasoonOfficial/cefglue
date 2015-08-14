using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefGlue.WebBrowser
{
    public class CefUncaughtExceptionEventArgs : EventArgs
    {
        private readonly CefBrowser browser;
        private readonly CefFrame frame;
        private readonly CefV8Context context;
        private readonly CefV8Exception exception;
        private readonly CefV8StackTrace stackTrace;

        public CefUncaughtExceptionEventArgs(CefBrowser browser, CefFrame frame, CefV8Context context, CefV8Exception exception, CefV8StackTrace stackTrace)
        {
            this.browser = browser;
            this.frame = frame;
            this.context = context;
            this.exception = exception;
            this.stackTrace = stackTrace;
        }

        public CefBrowser Browser
        {
            get { return browser; }
        }

        public CefFrame Frame
        {
            get { return frame; }
        }

        public CefV8Context Context
        {
            get { return context; }
        }

        public CefV8Exception Exception
        {
            get { return exception; }
        }

        public CefV8StackTrace StackTrace
        {
            get { return stackTrace; }
        } 
    }
}
