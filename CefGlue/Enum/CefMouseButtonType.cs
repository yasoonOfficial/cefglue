namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Mouse button types.
    /// </summary>
    public enum CefMouseButtonType
    {
        Left = cef_mouse_button_type_t.MBT_LEFT,
        Middle = cef_mouse_button_type_t.MBT_MIDDLE,
        Right = cef_mouse_button_type_t.MBT_RIGHT,
    }
}
