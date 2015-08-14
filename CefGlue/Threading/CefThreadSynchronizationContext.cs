namespace CefGlue.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    internal sealed class CefThreadSynchronizationContext : SynchronizationContext
    {
        private readonly CefThreadId threadId;
        private readonly AutoResetEvent waitHandle;

        public CefThreadSynchronizationContext(CefThreadId threadId)
        {
            this.threadId = threadId;
            this.waitHandle = new AutoResetEvent(false);
        }

        public override SynchronizationContext CreateCopy()
        {
            return this;
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            new CefSendOrPostCallbackTask(d, state).Post(this.threadId);
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            if (Cef.CurrentlyOn(this.threadId))
            {
                d(state);
            }
            else
            {
                lock (this.waitHandle)
                {
                    new CefSendOrPostCallbackTask(d, state, this.waitHandle).Post(this.threadId);
                    waitHandle.WaitOne();
                }
            }
        }
    }
}
