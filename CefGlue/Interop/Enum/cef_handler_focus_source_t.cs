namespace CefGlue.Interop
{
    /// <summary>
    /// Focus sources.
    /// </summary>
    internal enum cef_handler_focus_source_t : int
    {
        /// <summary>
        /// The source is explicit navigation via the API (LoadURL(), etc).
        /// </summary>
        FOCUS_SOURCE_NAVIGATION = 0,

        /// <summary>
        /// The source is a system-generated focus event.
        /// </summary>
        FOCUS_SOURCE_SYSTEM,

        /// <summary>
        /// The source is a child widget of the browser window requesting focus.
        /// </summary>
        FOCUS_SOURCE_WIDGET,
    }
}
