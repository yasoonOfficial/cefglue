namespace CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cefglue_userdata_t
    {
        /// <summary>
        /// Base structure.
        /// </summary>
        public cef_base_t @base;

    };
}
