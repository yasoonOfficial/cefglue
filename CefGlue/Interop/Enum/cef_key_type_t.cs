namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Key types.
    /// </summary>
    internal enum cef_key_type_t : int
    {
        KT_KEYUP = 0,
        KT_KEYDOWN,
        KT_CHAR,
    }
}
