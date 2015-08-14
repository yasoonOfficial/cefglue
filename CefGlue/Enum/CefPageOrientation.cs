namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Page orientation for printing.
    /// </summary>
    public enum CefPageOrientation
    {
        Portrait = cef_page_orientation.PORTRAIT,
        Landscape = cef_page_orientation.LANDSCAPE,
    }
}
