namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Key types.
    /// </summary>
    public enum CefKeyType
    {
        KeyUp = cef_key_type_t.KT_KEYUP,
        KeyDown = cef_key_type_t.KT_KEYDOWN,
        Char = cef_key_type_t.KT_CHAR,
    }
}
