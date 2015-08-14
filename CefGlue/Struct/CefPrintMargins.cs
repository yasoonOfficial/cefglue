namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Paper print margins.
    /// </summary>
    public sealed unsafe class CefPrintMargins : IDisposable
    {
        internal static CefPrintMargins From(cef_print_margins* ptr)
        {
            return new CefPrintMargins(ptr);
        }

        private cef_print_margins* ptr;

        private CefPrintMargins(cef_print_margins* ptr)
        {
            this.ptr = ptr;
        }

        ~CefPrintMargins()
        {
            this.ptr = null;
        }

        public void Dispose()
        {
            this.ptr = null;
        }

        /// <summary>
        /// Content left margin size in inches.
        /// </summary>
        public double Left
        {
            get { return this.ptr->left; }
            set { this.ptr->left = value; }
        }

        /// <summary>
        /// Content right margin size in inches.
        /// </summary>
        public double Right
        {
            get { return this.ptr->right; }
            set { this.ptr->right = value; }
        }

        /// <summary>
        /// Content top margin size in inches.
        /// </summary>
        public double Top
        {
            get { return this.ptr->top; }
            set { this.ptr->top = value; }
        }

        /// <summary>
        /// Content bottom margin size in inches.
        /// </summary>
        public double Bottom
        {
            get { return this.ptr->bottom; }
            set { this.ptr->bottom = value; }
        }

        /// <summary>
        /// Margin size (top) in inches for header.
        /// </summary>
        public double Header
        {
            get { return this.ptr->header; }
            set { this.ptr->header = value; }
        }

        /// <summary>
        /// Margin size (bottom) in inches for footer.
        /// </summary>
        public double Footer
        {
            get { return this.ptr->footer; }
            set { this.ptr->footer = value; }
        }
    }
}
