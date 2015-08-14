namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe struct cef_base_t
    {
        /// <summary>
        /// Size of the data structure.
        /// </summary>
        public int size;

        /// <summary>
        /// Increment the reference count.
        /// </summary>
        /// <remarks>
        /// int (CEF_CALLBACK *add_ref)(struct _cef_base_t* self);
        /// </remarks>
        public IntPtr add_ref;

        [UnmanagedFunctionPointer(NativeMethods.CefCallback), SuppressUnmanagedCodeSecurity]
        public delegate int add_ref_delegate(cef_base_t* self);

        /// <summary>
        /// Decrement the reference count.  Delete this object when no references remain.
        /// </summary>
        /// <remarks>
        /// int (CEF_CALLBACK *release)(struct _cef_base_t* self);
        /// </remarks>
        public IntPtr release;

        [UnmanagedFunctionPointer(NativeMethods.CefCallback), SuppressUnmanagedCodeSecurity]
        public delegate int release_delegate(cef_base_t* self);

        /// <summary>
        /// Returns the current number of references.
        /// </summary>
        /// <remarks>
        /// int (CEF_CALLBACK *get_refct)(struct _cef_base_t* self);
        /// </remarks>
        public IntPtr get_refct;

        [UnmanagedFunctionPointer(NativeMethods.CefCallback), SuppressUnmanagedCodeSecurity]
        public delegate int get_refct_delegate(cef_base_t* self);
    }
}
