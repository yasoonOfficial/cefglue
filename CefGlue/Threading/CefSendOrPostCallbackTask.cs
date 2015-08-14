namespace CefGlue.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public sealed class CefSendOrPostCallbackTask : CefTask
    {
        private readonly SendOrPostCallback d;
        private readonly object state;
        private readonly EventWaitHandle waitHandle;

        public CefSendOrPostCallbackTask(SendOrPostCallback d, object state)
            : this(d, state, null)
        { }

        public CefSendOrPostCallbackTask(SendOrPostCallback d, object state, EventWaitHandle waitHandle)
        {
            this.d = d;
            this.state = state;
            this.waitHandle = waitHandle;
        }

        protected override void Execute(CefThreadId threadId)
        {
            try
            {
                this.d(this.state);
            }
            finally
            {
                if (this.waitHandle != null)
                {
                    this.waitHandle.Set();
                }
            }
        }
    }
}
