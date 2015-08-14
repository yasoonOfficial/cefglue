namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefV8StackFrame
    {
        /// <summary>
        /// Returns true if this object is valid. Do not call any other methods if this
        /// method returns false.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return cef_v8stack_frame_t.invoke_is_valid(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns the name of the resource script that contains the function.
        /// </summary>
        public string ScriptName
        {
            get
            {
                ThrowIfObjectIsInvalid();
                var nResult = cef_v8stack_frame_t.invoke_get_script_name(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the name of the resource script that contains the function or the
        /// sourceURL value if the script name is undefined and its source ends with
        /// a "//@ sourceURL=..." string.
        /// </summary>
        public string ScriptNameOrSourceURL
        {
            get
            {
                ThrowIfObjectIsInvalid();
                var nResult = cef_v8stack_frame_t.invoke_get_script_name_or_source_url(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the name of the function.
        /// </summary>
        public string FunctionName
        {
            get
            {
                ThrowIfObjectIsInvalid();
                var nResult = cef_v8stack_frame_t.invoke_get_function_name(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the 1-based line number for the function call or 0 if unknown.
        /// </summary>
        public int LineNumber
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8stack_frame_t.invoke_get_line_number(this.ptr);
            }
        }

        /// <summary>
        /// Returns the 1-based column offset on the line for the function call or 0 if
        /// unknown.
        /// </summary>
        public int Column
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8stack_frame_t.invoke_get_column(this.ptr);
            }
        }

        /// <summary>
        /// Returns true if the function was compiled using eval().
        /// </summary>
        public bool IsEval
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8stack_frame_t.invoke_is_eval(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if the function was called as a constructor via "new".
        /// </summary>
        public bool IsConstructor
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8stack_frame_t.invoke_is_constructor(this.ptr) != 0;
            }
        }

        private void ThrowIfObjectIsInvalid()
        {
            if (!this.IsValid)
                throw new InvalidOperationException();
        }
    }
}
