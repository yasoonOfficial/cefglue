namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Key event types.
    /// </summary>
    internal enum cef_menu_id_t : int
    {
        MENU_ID_NAV_BACK = 10,
        MENU_ID_NAV_FORWARD = 11,
        MENU_ID_NAV_RELOAD = 12,
        MENU_ID_NAV_RELOAD_NOCACHE = 13,
        MENU_ID_NAV_STOP = 14,
        MENU_ID_UNDO = 20,
        MENU_ID_REDO = 21,
        MENU_ID_CUT = 22,
        MENU_ID_COPY = 23,
        MENU_ID_PASTE = 24,
        MENU_ID_DELETE = 25,
        MENU_ID_SELECTALL = 26,
        MENU_ID_PRINT = 30,
        MENU_ID_VIEWSOURCE = 31
    }
}