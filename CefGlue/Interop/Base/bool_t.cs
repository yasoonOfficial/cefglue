namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack, Size = 1)]
    internal unsafe struct bool_t
    {
        private byte value;

        public bool_t(bool value)
        {
            this.value = (byte)(value ? 1 : 0);
        }

        public static implicit operator bool(bool_t value)
        {
            return value.value != 0;
        }

        public static implicit operator bool_t(bool value)
        {
            return new bool_t(value);
        }
    }
}
