namespace CefGlue
{
    /// <summary>
    /// Focus sources.
    /// </summary>
    public enum CefHandlerFocusSource : int
    {
        /// <summary>
        /// The source is explicit navigation via the API (LoadURL(), etc).
        /// </summary>
        Navigation = 0,

        /// <summary>
        /// The source is a system-generated focus event.
        /// </summary>
        System,

        /// <summary>
        /// The source is a child widget of the browser window requesting focus.
        /// </summary>
        Widget,
    }
}
