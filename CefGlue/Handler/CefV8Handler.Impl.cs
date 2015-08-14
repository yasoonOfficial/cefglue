namespace CefGlue
{
    using System;
    using System.Runtime.CompilerServices;
    using CefGlue.Interop;
    using JSBinding;

    unsafe partial class CefV8Handler
    {
        /// <summary>
        /// Execute with the specified argument list and return value. Return
        /// true if the method was handled. To invoke V8 callback functions
        /// outside the scope of this method you need to keep references to the
        /// current V8 context (CefV8Context) along with any necessary callback
        /// objects.
        /// </summary>
        internal virtual int execute(cef_v8handler_t* self, /*const*/ cef_string_t* name, cef_v8value_t* @object, int argumentCount, cef_v8value_t* /*const*/ * arguments, cef_v8value_t** retval, cef_string_t* exception)
        {
            ThrowIfObjectDisposed();

            var m_name = cef_string_t.ToString(name);
            var m_obj = CefV8Value.From(@object);
            CefV8Value[] m_arguments;
            if (argumentCount == 0) { m_arguments = null; }
            else
            {
                m_arguments = new CefV8Value[argumentCount];
                for (var i = 0; i < argumentCount; i++)
                {
                    m_arguments[i] = CefV8Value.From(arguments[i]);
                }
            }

            CefV8Value m_returnValue;
            string m_exception;

            var handled = this.Execute(m_name, m_obj, m_arguments, out m_returnValue, out m_exception);

            if (handled)
            {
                if (m_exception != null)
                {
                    cef_string_t.Copy(m_exception, exception);
                }
                else if (m_returnValue != null)
                {
                    *retval = m_returnValue.GetNativePointerAndAddRef();
                }
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Execute with the specified argument list and return value.
        /// Return true if the method was handled.
        /// To invoke V8 callback functions outside the scope of this method you need to keep references to the current V8 context (CefV8Context) along with any necessary callback objects.
        /// </summary>
        protected virtual bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            returnValue = null;
            exception = null;
            return false;
        }


    }
}
