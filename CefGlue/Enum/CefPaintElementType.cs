namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    public enum CefPaintElementType
    {
        View = cef_paint_element_type_t.PET_VIEW,
        Popup = cef_paint_element_type_t.PET_POPUP,
    }
}
