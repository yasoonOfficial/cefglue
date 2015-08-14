namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Status message types.
    /// </summary>
    internal enum cef_handler_statustype_t : int
    {
        STATUSTYPE_TEXT = 0,
        STATUSTYPE_MOUSEOVER_URL,
        STATUSTYPE_KEYBOARD_FOCUS_URL,
    }
}
