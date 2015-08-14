namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefV8Context
    {
        /// <summary>
        /// Returns the current (top) context object in the V8 context stack.
        /// </summary>
        public static CefV8Context GetCurrentContext()
        {
            return CefV8Context.From(
                NativeMethods.cef_v8context_get_current_context()
                );
        }

        /// <summary>
        /// Returns the entered (bottom) context object in the V8 context stack.
        /// </summary>
        public static CefV8Context GetEnteredContext()
        {
            return CefV8Context.From(
                NativeMethods.cef_v8context_get_entered_context()
                );
        }

        /// <summary>
        /// Returns true if V8 is currently inside a context.
        /// </summary>
        public static bool InContext()
        {
            return NativeMethods.cef_v8context_in_context() != 0;
        }

        /// <summary>
        /// Returns the browser for this context.
        /// </summary>
        public CefBrowser GetBrowser()
        {
            return CefBrowser.From(
                cef_v8context_t.invoke_get_browser(this.ptr)
                );
        }

        /// <summary>
        /// Returns the frame for this context.
        /// </summary>
        public CefFrame GetFrame()
        {
            return CefFrame.From(
                cef_v8context_t.invoke_get_frame(this.ptr)
                );
        }

        /// <summary>
        /// Returns the global object for this context. The context must be entered
        /// before calling this method.
        /// </summary>
        public CefV8Value GetGlobal()
        {
            return CefV8Value.From(
                cef_v8context_t.invoke_get_global(this.ptr)
                );
        }

        /// <summary>
        /// Enter this context. A context must be explicitly entered before creating a
        /// V8 Object, Array, Function or Date asynchronously. Exit() must be called
        /// the same number of times as Enter() before releasing this context. V8
        /// objects belong to the context in which they are created. Returns true if
        /// the scope was entered successfully.
        /// </summary>
        public bool Enter()
        {
            return cef_v8context_t.invoke_enter(this.ptr) != 0;
        }

        /// <summary>
        /// Exit this context.
        /// Call this method only after calling Enter().
        /// Returns true if the scope was exited successfully.
        /// </summary>
        public bool Exit()
        {
            return cef_v8context_t.invoke_exit(this.ptr) != 0;
        }

        /// <summary>
        /// Returns true if this object is pointing to the same handle as |that|
        /// object.
        /// </summary>
        public bool IsSame(CefV8Context that)
        {
            return cef_v8context_t.invoke_is_same(this.ptr, that.ptr) != 0;
        }

        /// <summary>
        /// Evaluates the specified JavaScript code using this context's global object.
        /// On success |retval| will be set to the return value, if any, and the
        /// function will return true. On failure |exception| will be set to the
        /// exception, if any, and the function will return false.
        /// </summary>
        public bool Eval(string code, out CefV8Value retval, out CefV8Exception exception)
        {
            cef_v8value_t* n_retval = null;
            cef_v8exception_t* n_exception = null;
            fixed (char* code_str = code)
            {
                var n_code = new cef_string_t(code_str, code != null ? code.Length : 0);

                var n_result = cef_v8context_t.invoke_eval(this.ptr, &n_code, &n_retval, &n_exception);
                if (n_result != 0)
                {
                    retval = CefV8Value.From(n_retval);
                    exception = null;
                    return true;
                }
                else
                {
                    retval = null;
                    exception = CefV8Exception.From(n_exception);
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns true if this object is valid. Do not call any other methods if this
        /// method returns false.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return cef_v8context_t.invoke_is_valid(this.ptr) != 0;
            }
        }
    }
}
