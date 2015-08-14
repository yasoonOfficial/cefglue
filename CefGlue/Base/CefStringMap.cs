namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CefGlue.Interop;

    public sealed unsafe class CefStringMap
    {
        private cef_string_map* handle;

        public CefStringMap()
        {
            this.handle = NativeMethods.cef_string_map_alloc();
        }

        ~CefStringMap()
        {
            Dispose(false);
        }

        internal void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.handle != null)
            {
                NativeMethods.cef_string_map_free(this.handle);
                this.handle = null;
            }
        }

        internal cef_string_map* Handle
        {
            get
            {
                return this.handle;
            }
        }

        public int Count
        {
            get
            {
                return NativeMethods.cef_string_map_size(this.handle);
            }
        }

        public bool TryGetValue(string key, out string value)
        {
            fixed (char* key_str = key)
            {
                var nKey = new cef_string_t(key_str, key != null ? key.Length : 0);
                cef_string_t nValue = new cef_string_t();

                var result = NativeMethods.cef_string_map_find(this.handle, &nKey, &nValue) != 0;

                value = result ? cef_string_t.ToString(&nValue) : null;

                cef_string_t.Clear(&nValue);
                return result;
            }
        }

        public string GetValue(string key)
        {
            string value;
            if (this.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public string this[string key]
        {
            get
            {
                return this.GetValue(key);
            }
        }

        public bool TryGetKey(int index, out string key)
        {
            cef_string_t nKey = new cef_string_t();
            var result = NativeMethods.cef_string_map_key(this.handle, index, &nKey) != 0;
            key = result ? cef_string_t.ToString(&nKey) : null;
            cef_string_t.Clear(&nKey);
            return result;
        }

        public bool TryGetValue(int index, out string value)
        {
            cef_string_t nValue = new cef_string_t();
            var result = NativeMethods.cef_string_map_value(this.handle, index, &nValue) != 0;
            value = result ? cef_string_t.ToString(&nValue) : null;
            cef_string_t.Clear(&nValue);
            return result;
        }

        public bool Append(string key, string value)
        {
            fixed (char* key_str = key)
            fixed (char* value_str = value)
            {
                var nKey = new cef_string_t(key_str, key != null ? key.Length : 0);
                var nValue = new cef_string_t(value_str, value != null ? value.Length : 0);

                return NativeMethods.cef_string_map_append(this.handle, &nKey, &nValue) != 0;
            }
        }

        public void Clear()
        {
            NativeMethods.cef_string_map_clear(this.handle);
        }
    }
}
