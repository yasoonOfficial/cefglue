namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Paper metric information for printing.
    /// </summary>
    public sealed unsafe class CefPaperMetrics
    {
        internal static CefPaperMetrics From(cef_paper_metrics* ptr)
        {
            return new CefPaperMetrics(ptr);
        }

        private cef_paper_metrics* ptr;

        private CefPaperMetrics(cef_paper_metrics* ptr)
        {
            this.ptr = ptr;
        }

        ~CefPaperMetrics()
        {
            this.ptr = null;
        }

        public void Dispose()
        {
            this.ptr = null;
        }

        public CefPaperType Type
        {
            get { return (CefPaperType)this.ptr->paper_type; }
            set { this.ptr->paper_type = (cef_paper_type_t)value; }
        }

        /// <summary>
        /// Length needed if Type is Custom. Units are in inches.
        /// </summary>
        public double Length
        {
            get { return this.ptr->length; }
            set { this.ptr->length = value; }
        }

        /// <summary>
        /// Width needed if Type is Custom. Units are in inches.
        /// </summary>
        public double Width
        {
            get { return this.ptr->width; }
            set { this.ptr->width = value; }
        }

    }
}
