namespace CefGlue.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public static class CefThread
    {
        private static SynchronizationContext ui;
        private static SynchronizationContext io;
        private static SynchronizationContext file;

        static CefThread()
        {
            ui = new CefThreadSynchronizationContext(CefThreadId.UI);
            io = new CefThreadSynchronizationContext(CefThreadId.IO);
            file = new CefThreadSynchronizationContext(CefThreadId.File);
        }

        public static SynchronizationContext UI
        {
            get { return ui; }
        }

        public static SynchronizationContext IO
        {
            get { return io; }
        }

        public static SynchronizationContext File
        {
            get { return file; }
        }
    }
}
