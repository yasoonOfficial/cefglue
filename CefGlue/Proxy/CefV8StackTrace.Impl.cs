namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefV8StackTrace
    {
        /// <summary>
        /// Returns true if this object is valid. Do not call any other methods if this
        /// method returns false.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return cef_v8stack_trace_t.invoke_is_valid(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns the number of stack frames.
        /// </summary>
        public int FrameCount
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8stack_trace_t.invoke_get_frame_count(this.ptr);
            }
        }

        /// <summary>
        /// Returns the stack frame at the specified 0-based index.
        /// </summary>
        public CefV8StackFrame GetFrame(int index)
        {
            ThrowIfObjectIsInvalid();
            return CefV8StackFrame.FromOrDefault(
                cef_v8stack_trace_t.invoke_get_frame(this.ptr, index)
                );
        }

        private void ThrowIfObjectIsInvalid()
        {
            if (!this.IsValid)
                throw new InvalidOperationException();
        }
    }
}
