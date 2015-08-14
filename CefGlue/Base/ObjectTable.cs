namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal sealed class ObjectTable<T> where T : class
    {
        private Dictionary<IntPtr, T> table = new Dictionary<IntPtr, T>();

        public ObjectTable()
        {
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool TryGetValue(IntPtr key, out T value)
        {
            return table.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Add(IntPtr key, T value)
        {
            this.table.Add(key, value);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Remove(IntPtr key)
        {
            this.table.Remove(key);
        }
    }
}
