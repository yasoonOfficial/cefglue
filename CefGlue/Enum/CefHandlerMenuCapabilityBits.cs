namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// The cef_handler_menuinfo_t editFlags value will be a combination of the following values.
    /// </summary>
    [Flags]
    public enum CefHandlerMenuCapabilityBits
    {
        // Values from WebContextMenuData::EditFlags in WebContextMenuData.h
        CanDoNone = cef_handler_menucapabilitybits_t.MENU_CAN_DO_NONE,
        CanUndo = cef_handler_menucapabilitybits_t.MENU_CAN_UNDO,
        CanRedo = cef_handler_menucapabilitybits_t.MENU_CAN_REDO,
        CanCut = cef_handler_menucapabilitybits_t.MENU_CAN_CUT,
        CanCopy = cef_handler_menucapabilitybits_t.MENU_CAN_COPY,
        CanPaste = cef_handler_menucapabilitybits_t.MENU_CAN_PASTE,
        CanDelete = cef_handler_menucapabilitybits_t.MENU_CAN_DELETE,
        CanSelectAll = cef_handler_menucapabilitybits_t.MENU_CAN_SELECT_ALL,
        CanTranslate = cef_handler_menucapabilitybits_t.MENU_CAN_TRANSLATE,
        // Values unique to CEF
        CanGoForward = cef_handler_menucapabilitybits_t.MENU_CAN_GO_FORWARD,
        CanGoBack = cef_handler_menucapabilitybits_t.MENU_CAN_GO_BACK,
    }
}
