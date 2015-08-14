namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Class representing print context information.
    /// </summary>
    public sealed unsafe class CefPrintInfo : IDisposable
    {
        internal static CefPrintInfo From(cef_print_info_t* ptr)
        {
            return new CefPrintInfo(ptr);
        }

        private cef_print_info_t* ptr;

        private CefPrintInfo(cef_print_info_t* ptr)
        {
            this.ptr = ptr;
        }

        ~CefPrintInfo()
        {
            this.ptr = null;
        }

        public void Dispose()
        {
            this.ptr = null;
        }

        public IntPtr DeviceContext { get { return this.ptr->m_hDC; } }

        // TODO: CefPrintInfo.Rectangle / return rectangle from this.ptr->m_Rect;

        public double Scale { get { return this.ptr->m_Scale; } }
    }
}
