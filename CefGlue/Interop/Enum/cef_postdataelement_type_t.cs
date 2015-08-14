namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Post data elements may represent either bytes or files.
    ///
    internal enum cef_postdataelement_type_t : int
    {
        PDE_TYPE_EMPTY = 0,
        PDE_TYPE_BYTES,
        PDE_TYPE_FILE,
    }
}
