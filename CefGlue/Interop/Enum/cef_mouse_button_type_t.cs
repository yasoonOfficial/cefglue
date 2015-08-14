namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Mouse button types.
    ///
    internal enum cef_mouse_button_type_t : int
    {
        MBT_LEFT = 0,
        MBT_MIDDLE,
        MBT_RIGHT,
    }
}
