namespace CefGlue
{
    using System;
    using CefGlue.Interop;
    using CefGlue.Diagnostics;
    using CefGlue.Threading;

    unsafe partial class CefTask
    {
        /// <summary>
        /// Post a task for execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public static void Post(CefThreadId threadId, CefTask task)
        {
            Cef.PostTask(threadId, task);
        }

        /// <summary>
        /// Post a task for delayed execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public static void Post(CefThreadId threadId, CefTask task, long delayMs)
        {
            Cef.PostTask(threadId, task, delayMs);
        }

        /// <summary>
        /// Post a task for execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public static void Post(CefThreadId threadId, Action action)
        {
            Cef.PostTask(threadId, new CefActionTask(action));
        }

        /// <summary>
        /// Post a task for delayed execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public static void Post(CefThreadId threadId, Action action, long delayMs)
        {
            Cef.PostTask(threadId, new CefActionTask(action), delayMs);
        }

        /// <summary>
        /// Post a task for execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public void Post(CefThreadId threadId)
        {
            Cef.PostTask(threadId, this);
        }

        /// <summary>
        /// Post a task for delayed execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public void Post(CefThreadId threadId, long delayMs)
        {
            Cef.PostTask(threadId, this, delayMs);
        }

        /// <summary>
        /// Method that will be executed.
        /// |threadId| is the thread executing the call.
        /// </summary>
        private void execute(cef_task_t* self, cef_thread_id_t threadId)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefTask, self, "Execute: ThreadId=[{0}]", threadId);
#endif

            this.Execute((CefThreadId)threadId);
        }

        /// <summary>
        /// Method that will be executed.
        /// </summary>
        /// <param name="threadId">Thread executing the call.</param>
        protected abstract void Execute(CefThreadId threadId);
    }
}
