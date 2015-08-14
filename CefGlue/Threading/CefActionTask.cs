namespace CefGlue.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public sealed class CefActionTask : CefTask
    {
        private Action action;

        public CefActionTask(Action action)
        {
            this.action = action;
        }

        protected override void Execute(CefThreadId threadId)
        {
            this.action();
        }
    }
}
