namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Paper metric information for printing.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_paper_metrics
    {
        public cef_paper_type_t paper_type;
        //Length and width needed if paper_type is custom_size
        //Units are in inches.
        public double length;
        public double width;
    }
}
