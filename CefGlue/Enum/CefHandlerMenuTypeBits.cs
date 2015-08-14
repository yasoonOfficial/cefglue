namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// The cef_menu_info_t typeFlags value will be a combination of the following values.
    /// </summary>
    [Flags]
    public enum CefHandlerMenuTypeBits
    {
        /// <summary>
        /// No node is selected
        /// </summary>
        None = cef_handler_menutypebits_t.MENUTYPE_NONE,

        /// <summary>
        /// The top page is selected
        /// </summary>
        Page = cef_handler_menutypebits_t.MENUTYPE_PAGE,

        /// <summary>
        /// A subframe page is selected
        /// </summary>
        Frame = cef_handler_menutypebits_t.MENUTYPE_FRAME,

        /// <summary>
        /// A link is selected
        /// </summary>
        Link = cef_handler_menutypebits_t.MENUTYPE_LINK,

        /// <summary>
        /// An image is selected
        /// </summary>
        Image = cef_handler_menutypebits_t.MENUTYPE_IMAGE,

        /// <summary>
        /// There is a textual or mixed selection that is selected
        /// </summary>
        Selection = cef_handler_menutypebits_t.MENUTYPE_SELECTION,

        /// <summary>
        /// An editable element is selected
        /// </summary>
        Editable = cef_handler_menutypebits_t.MENUTYPE_EDITABLE,

        /// <summary>
        /// A misspelled word is selected
        /// </summary>
        MisspelledWord = cef_handler_menutypebits_t.MENUTYPE_MISSPELLED_WORD,

        /// <summary>
        /// A video node is selected
        /// </summary>
        Video = cef_handler_menutypebits_t.MENUTYPE_VIDEO,

        /// <summary>
        /// A video node is selected
        /// </summary>
        Audio = cef_handler_menutypebits_t.MENUTYPE_AUDIO,
    }
}
