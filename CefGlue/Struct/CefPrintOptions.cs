namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Printing options.
    /// </summary>
    public sealed unsafe class CefPrintOptions : IDisposable
    {
        internal static CefPrintOptions From(cef_print_options_t* ptr)
        {
            return new CefPrintOptions(ptr);
        }

        private cef_print_options_t* ptr;
        private CefPaperMetrics paperMetrics;
        private CefPrintMargins paperMargins;

        private CefPrintOptions(cef_print_options_t* ptr)
        {
            this.ptr = ptr;
        }

        ~CefPrintOptions()
        {
            this.ptr = null;
            this.paperMetrics = null;
            this.paperMargins = null;
        }

        public void Dispose()
        {
            this.ptr = null;

            if (this.paperMetrics != null) { this.paperMetrics.Dispose(); }
            this.paperMetrics = null;

            if (this.paperMargins != null) { this.paperMargins.Dispose(); }
            this.paperMargins = null;

            GC.SuppressFinalize(this);
        }

        public CefPageOrientation PageOrientation
        {
            get { return (CefPageOrientation)this.ptr->page_orientation; }
            set { this.ptr->page_orientation = (cef_page_orientation)value; }
        }

        public CefPaperMetrics PaperMetrics
        {
            get
            {
                if (this.paperMetrics != null)
                {
                    this.paperMetrics = CefPaperMetrics.From(&this.ptr->paper_metrics);
                }
                return this.paperMetrics;
            }
        }

        public CefPrintMargins PaperMargins
        {
            get
            {
                if (this.PaperMargins != null)
                {
                    this.paperMargins = CefPrintMargins.From(&this.ptr->paper_margins);
                }
                return this.paperMargins;
            }
        }
    }
}
