#if DIAGNOSTICS
namespace CefGlue.Diagnostics
{
    using System;
    using System.Threading;

    public class GCFinalizerThreadId
    {
        static GCFinalizerThreadId()
        {
            var obj = new GCFinalizerThreadId();
        }

        private static int value;

        public static int Value
        {
            get
            {
                if (value == 0) { WaitForFinalizer(); }
                return value;
            }
        }

        private static void WaitForFinalizer()
        {
            GC.Collect(0);
            GC.WaitForPendingFinalizers();
        }

        private GCFinalizerThreadId()
        {
            value = 0;
        }

        ~GCFinalizerThreadId()
        {
            value = Thread.CurrentThread.ManagedThreadId;
        }
    }
}
#endif
