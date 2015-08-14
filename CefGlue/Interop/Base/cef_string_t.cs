namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    /// <summary>
    /// CEF string type.
    ///
    /// Whomever allocates |str| is responsible for providing an appropriate |dtor| implementation that will free the string in the same memory space.
    /// When reusing an existing string structure make sure to call |dtor| for the old value before assigning new |str| and |dtor| values.
    /// Static strings will have a NULL |dtor| value.
    /// </summary>
    /// <remarks>
    /// This is assumes that CEF builded with the UTF16 string type as default (CEF_STRING_TYPE_UTF16).
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_string_t
    {
        /// <summary></summary>
        /// <remarks>char16_t* str;</remarks>
        internal char* str;

        /// <summary></summary>
        /// <remarks>size_t length;</remarks>
        internal int length;

        /// <summary></summary>
        /// <remarks>void (*dtor)(char16_t* str);</remarks>
        internal IntPtr dtor;

        [UnmanagedFunctionPointer(NativeMethods.CefCall), SuppressUnmanagedCodeSecurity]
        public delegate void dtor_delegate(char* str);

        /// <summary>
        /// This is useful to pass string without copying.
        /// </summary>
        public cef_string_t(char* str, int length)
        {
            this.str = str;
            this.length = length;
            this.dtor = IntPtr.Zero;
        }

#if DIAGNOSTICS
        private static readonly bool diagnostics = true;

        private static dtor_delegate bs_dtor_impl;
        private static IntPtr bs_dtor_impl_fp;

        internal static Action<IntPtr, string> OnCreate;
        internal static Action<IntPtr> OnDispose;

        static cef_string_t()
        {
            bs_dtor_impl = new dtor_delegate(dtor_impl);
            bs_dtor_impl_fp = Marshal.GetFunctionPointerForDelegate(bs_dtor_impl);
        }

        private static void dtor_impl(char* str)
        {
            if (OnDispose != null) OnDispose((IntPtr)str);
            Marshal.FreeHGlobal((IntPtr)str);
        }

        private static void copy_impl(string src, cef_string_t* dst)
        {
            NativeMethods.cef_string_clear(dst);

            if (string.IsNullOrEmpty(src))
            {
                if (OnCreate != null) OnCreate(IntPtr.Zero, null);
                dst->str = null;
                dst->length = 0;
                dst->dtor = IntPtr.Zero;
            }
            else
            {
                var ptr = Marshal.StringToHGlobalUni(src);
                if (OnCreate != null) OnCreate((IntPtr)ptr, src);
                dst->str = (char*)ptr;
                dst->length = src.Length;
                dst->dtor = bs_dtor_impl_fp;
            }
        }
#endif

        public static string ToString(cef_string_t* ptr)
        {
            if (ptr == null)
            {
                return string.Empty;
            }

            return new string(ptr->str, 0, ptr->length);
        }

        public static void Set(string src, cef_string_t* dst, bool copy)
        {
            fixed (char* ptr = src)
            {
#if DIAGNOSTICS
                if (copy)
                {
                    copy_impl(src, dst);
                }
                else
#endif
                NativeMethods.cef_string_set(ptr, src == null ? 0 : src.Length, dst, copy ? 1 : 0);
            }
        }

        public static void Copy(string src, cef_string_t* dst)
        {
            fixed (char* ptr = src)
            {
#if DIAGNOSTICS
                if (diagnostics)
                {
                    copy_impl(src, dst);
                }
                else
#endif
                NativeMethods.cef_string_set(ptr, src == null ? 0 : src.Length, dst, 1);
            }
        }

        public static void Clear(cef_string_t* str)
        {
            NativeMethods.cef_string_clear(str);
        }
    }
}