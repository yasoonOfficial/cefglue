namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // The cef_handler_menuinfo_t typeFlags value will be a combination of the
    // following values.
    ///
    internal enum cef_handler_menutypebits_t : int
    {
        ///
        // No node is selected
        ///
        MENUTYPE_NONE = 0x0,
        ///
        // The top page is selected
        ///
        MENUTYPE_PAGE = 0x1,
        ///
        // A subframe page is selected
        ///
        MENUTYPE_FRAME = 0x2,
        ///
        // A link is selected
        ///
        MENUTYPE_LINK = 0x4,
        ///
        // An image is selected
        ///
        MENUTYPE_IMAGE = 0x8,
        ///
        // There is a textual or mixed selection that is selected
        ///
        MENUTYPE_SELECTION = 0x10,
        ///
        // An editable element is selected
        ///
        MENUTYPE_EDITABLE = 0x20,
        ///
        // A misspelled word is selected
        ///
        MENUTYPE_MISSPELLED_WORD = 0x40,
        ///
        // A video node is selected
        ///
        MENUTYPE_VIDEO = 0x80,
        ///
        // A video node is selected
        ///
        MENUTYPE_AUDIO = 0x100,
    }
}
