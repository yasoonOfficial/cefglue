namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Printing options.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_print_options_t
    {
        public cef_page_orientation page_orientation;
        public cef_paper_metrics paper_metrics;
        public cef_print_margins paper_margins;
    }
}
