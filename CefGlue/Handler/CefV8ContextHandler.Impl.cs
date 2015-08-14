namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Implement this class to handle V8 context events. The functions of
    /// this structure will be called on the UI thread.
    /// </summary>
    unsafe partial class CefV8ContextHandler
    {
        /// <summary>
        /// Called immediately after the V8 context for a frame has been created.
        /// To retrieve the JavaScript 'window' object use the
        /// cef_v8context_t::get_global() function.
        /// </summary>
        private void on_context_created(cef_v8context_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_v8context_t* context)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_context = CefV8Context.From(context);
            
            this.OnContextCreated(m_browser, m_frame, m_context);
        }

        /// <summary>
        /// Called immediately before the V8 context for a frame is released. No
        /// references to the context should be kept after this function is
        /// called.
        /// </summary>
        private void on_context_released(cef_v8context_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_v8context_t* context)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_context = CefV8Context.From(context);

            this.OnContextReleased(m_browser, m_frame, m_context);
        }

        /// <summary>
        /// Called for global uncaught exceptions. Execution of this callback is
        /// disabled by default. To enable set
        /// CefSettings.uncaught_exception_stack_size &gt; 0.
        /// </summary>
        private void on_uncaught_exception(cef_v8context_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_v8context_t* context, cef_v8exception_t* exception, cef_v8stack_trace_t* stackTrace)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_context = CefV8Context.From(context);
            var m_exception = CefV8Exception.From(exception);
            var m_stackTrace = CefV8StackTrace.From(stackTrace);

            this.OnUncaughtException(m_browser, m_frame, m_context, m_exception, m_stackTrace);
        }

        /// <summary>
        /// Called immediately after the V8 context for a frame has been created.
        /// To retrieve the JavaScript 'window' object use the
        /// cef_v8context_t::get_global() function.
        /// </summary>
        protected abstract void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context);

        /// <summary>
        /// Called immediately before the V8 context for a frame is released. No
        /// references to the context should be kept after this function is
        /// called.
        /// </summary>
        protected abstract void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context);

        /// <summary>
        /// Called for global uncaught exceptions. Execution of this callback is
        /// disabled by default. To enable set
        /// CefSettings.uncaught_exception_stack_size &gt; 0.
        /// </summary>
        protected abstract void OnUncaughtException(CefBrowser browser, CefFrame frame, CefV8Context context, CefV8Exception exception, CefV8StackTrace stackTrace);
    }
}