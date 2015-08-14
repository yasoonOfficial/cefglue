namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Paper print margins.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_print_margins
    {
        //Margin size in inches for left/right/top/bottom (this is content margins).
        public double left;
        public double right;
        public double top;
        public double bottom;
        //Margin size (top/bottom) in inches for header/footer.
        public double header;
        public double footer;
    }
}
