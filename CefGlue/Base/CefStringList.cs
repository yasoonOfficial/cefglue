namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CefGlue.Interop;

    public sealed unsafe class CefStringList : IDisposable
    {
        internal static cef_string_list* CreateHandle()
        {
            return NativeMethods.cef_string_list_alloc();
        }

        internal static cef_string_list* CreateHandle(IEnumerable<string> collection)
        {
            var handle = CreateHandle();
            foreach (var value in collection)
            {
                fixed (char* str = value)
                {
                    cef_string_t nValue = new cef_string_t(str, value != null ? value.Length : 0);
                    NativeMethods.cef_string_list_append(handle, &nValue);
                }
            }
            return handle;
        }

        internal static void DestroyHandle(cef_string_list* handle)
        {
            NativeMethods.cef_string_list_free(handle);
        }

        internal static CefStringList From(cef_string_list* handle, bool ownsHandle = false)
        {
            return new CefStringList(handle, ownsHandle);
        }

        private cef_string_list* handle;
        private bool ownsHandle;

        /// <summary>
        /// Create new empty string list.
        /// </summary>
        public CefStringList()
        {
            this.handle = CreateHandle();
            this.ownsHandle = true;
        }

        /// <summary>
        /// Create new string list filled with values from collection.
        /// </summary>
        public CefStringList(IEnumerable<string> collection)
        {
            this.handle = CreateHandle(collection);
            this.ownsHandle = true;
        }

        /// <summary>
        /// Create string list wrapper.
        /// </summary>
        private CefStringList(cef_string_list* list, bool ownsHandle = false)
        {
            this.handle = list;
            this.ownsHandle = ownsHandle;
        }

        ~CefStringList()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.handle != null)
            {
                if (this.ownsHandle)
                {
                    DestroyHandle(this.handle);
                }
                this.handle = null;
            }
        }

        internal cef_string_list* Handle
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
                // TODO: debug checks
                return NativeMethods.cef_string_list_size(this.handle);
            }
        }

        public bool TryGetValue(int index, out string value)
        {
            cef_string_t nValue = new cef_string_t();
            var result = NativeMethods.cef_string_list_value(this.handle, index, ref nValue) != 0;
            value = result ? cef_string_t.ToString(&nValue) : null;
            cef_string_t.Clear(&nValue);
            return result;
        }

        public string this[int index]
        {
            get
            {
                string value;
                if (this.TryGetValue(index, out value))
                {
                    return value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public void Add(string value)
        {
            // TODO: debug checks
            fixed (char* str = value)
            {
                cef_string_t nValue = new cef_string_t(str, value != null ? value.Length : 0);
                NativeMethods.cef_string_list_append(this.handle, &nValue);
            }
        }

        public void Clear()
        {
            NativeMethods.cef_string_list_clear(this.handle);
        }

        public CefStringList Clone()
        {
            return new CefStringList(
                NativeMethods.cef_string_list_copy(this.handle)
                );
        }

        private static IEnumerable<string> emptySequence = new List<string>();

        public IEnumerable<string> AsEnumerable()
        {
            // TODO: CefStringList must be IEnumerable<string>

            var count = this.Count;
            if (count == 0)
            {
                return emptySequence;
            }
            else return AsEnumerableCore(count);
        }

        private IEnumerable<string> AsEnumerableCore(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return this[i];
            }
        }
    }
}
