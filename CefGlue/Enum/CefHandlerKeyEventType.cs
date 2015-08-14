namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Key event types.
    /// </summary>
    public enum CefHandlerKeyEventType
    {
        RawKeyDown = cef_handler_keyevent_type_t.KEYEVENT_RAWKEYDOWN,
        KeyDown = cef_handler_keyevent_type_t.KEYEVENT_KEYDOWN,
        KeyUp = cef_handler_keyevent_type_t.KEYEVENT_KEYUP,
        Char = cef_handler_keyevent_type_t.KEYEVENT_CHAR,
    }
}
