namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefV8Accessor
    {
        /// <summary>
        /// Called to get an accessor value. |name| is the name of the property
        /// being accessed. |object| is the This() object from V8's AccessorInfo
        /// structure. |retval| is the value to return for this property. Return
        /// true if handled.
        /// </summary>
        internal virtual int get(cef_v8accessor_t* self, /*const*/ cef_string_t* name, cef_v8value_t* @object, cef_v8value_t** retval, cef_string_t* exception)
        {
            ThrowIfObjectDisposed();

            var m_name = cef_string_t.ToString(name);
            var m_obj = CefV8Value.From(@object);
            CefV8Value m_returnValue;
            string mException;

            var handled = this.Get(m_name, m_obj, out m_returnValue, out mException);

            if (handled)
            {
                if (mException != null)
                {
                    cef_string_t.Copy(mException, exception);
                }
                else if (m_returnValue != null)
                {
                    *retval = m_returnValue.GetNativePointerAndAddRef();
                }
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to get an accessor value.
        /// |name| is the name of the property being accessed.
        /// |object| is the This() object from V8's AccessorInfo structure.
        /// |retval| is the value to return for this property.
        /// Return true if handled.
        /// </summary>
        protected virtual bool Get(string name, CefV8Value obj, out CefV8Value returnValue, out string exception)
        {
            returnValue = null;
            exception = null;
            return false;
        }


        /// <summary>
        /// Called to set an accessor value. |name| is the name of the property
        /// being accessed. |value| is the new value being assigned to this
        /// property. |object| is the This() object from V8's AccessorInfo
        /// structure. Return true if handled.
        /// </summary>
        internal virtual int set(cef_v8accessor_t* self, /*const*/ cef_string_t* name, cef_v8value_t* @object, cef_v8value_t* value, cef_string_t* exception)
        {
            ThrowIfObjectDisposed();

            var m_name = cef_string_t.ToString(name);
            var m_obj = CefV8Value.From(@object);
            var m_value = CefV8Value.From(value);
            string mException;

            var handled = this.Set(m_name, m_obj, m_value, out mException);

            if (handled)
            {
                if (mException != null)
                {
                    cef_string_t.Copy(mException, exception);
                }
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to set an accessor value.
        /// |name| is the name of the property being accessed.
        /// |value| is the new value being assigned to this property.
        /// |object| is the This() object from V8's AccessorInfo structure.
        /// Return true if handled.
        /// </summary>
        protected virtual bool Set(string name, CefV8Value obj, CefV8Value value, out string exception)
        {
            exception = null;
            return false;
        }


    }
}
