namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Paper type for printing.
    ///
    internal enum cef_paper_type_t : int
    {
        PT_LETTER = 0,
        PT_LEGAL,
        PT_EXECUTIVE,
        PT_A3,
        PT_A4,
        PT_CUSTOM
    }
}
