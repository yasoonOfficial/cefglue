namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Various browser navigation types supported by chrome.
    /// </summary>
    public enum CefHandlerNavType
    {
        LinkClicked = cef_handler_navtype_t.NAVTYPE_LINKCLICKED,
        FormSubmitted = cef_handler_navtype_t.NAVTYPE_FORMSUBMITTED,
        BackForward = cef_handler_navtype_t.NAVTYPE_BACKFORWARD,
        Reload = cef_handler_navtype_t.NAVTYPE_RELOAD,
        FormResubmitted = cef_handler_navtype_t.NAVTYPE_FORMRESUBMITTED,
        Other = cef_handler_navtype_t.NAVTYPE_OTHER,
        LinkDropped = cef_handler_navtype_t.NAVTYPE_LINKDROPPED,
    }
}
