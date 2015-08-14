namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Key event modifiers.
    /// </summary>
    internal enum cef_handler_keyevent_modifiers_t
    {
        KEY_SHIFT = 1 << 0,
        KEY_CTRL = 1 << 1,
        KEY_ALT = 1 << 2,
        KEY_META = 1 << 3,
        KEY_KEYPAD = 1 << 4,  // Only used on Mac OS-X
    }
}
