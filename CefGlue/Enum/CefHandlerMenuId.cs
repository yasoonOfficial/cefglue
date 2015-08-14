namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Supported menu ID values.
    /// </summary>
    public enum CefHandlerMenuId
    {
        NavBack = cef_menu_id_t.MENU_ID_NAV_BACK,
        NavForward = cef_menu_id_t.MENU_ID_NAV_FORWARD,
        NavReload = cef_menu_id_t.MENU_ID_NAV_RELOAD,
        NavReloadNoCache = cef_menu_id_t.MENU_ID_NAV_RELOAD_NOCACHE,
        NavStop = cef_menu_id_t.MENU_ID_NAV_STOP,
        Undo = cef_menu_id_t.MENU_ID_UNDO,
        Redo = cef_menu_id_t.MENU_ID_REDO,
        Cut = cef_menu_id_t.MENU_ID_CUT,
        Copy = cef_menu_id_t.MENU_ID_COPY,
        Paste = cef_menu_id_t.MENU_ID_PASTE,
        Delete = cef_menu_id_t.MENU_ID_DELETE,
        SelectAll = cef_menu_id_t.MENU_ID_SELECTALL,
        Print = cef_menu_id_t.MENU_ID_PRINT,
        ViewSource = cef_menu_id_t.MENU_ID_VIEWSOURCE,
    }
}
