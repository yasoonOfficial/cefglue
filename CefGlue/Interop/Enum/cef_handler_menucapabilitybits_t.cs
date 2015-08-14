namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // The cef_menu_info_t editFlags value will be a combination of the
    // following values.
    ///
    internal enum cef_handler_menucapabilitybits_t : int
    {
        // Values from WebContextMenuData::EditFlags in WebContextMenuData.h
        MENU_CAN_DO_NONE = 0x0,
        MENU_CAN_UNDO = 0x1,
        MENU_CAN_REDO = 0x2,
        MENU_CAN_CUT = 0x4,
        MENU_CAN_COPY = 0x8,
        MENU_CAN_PASTE = 0x10,
        MENU_CAN_DELETE = 0x20,
        MENU_CAN_SELECT_ALL = 0x40,
        MENU_CAN_TRANSLATE = 0x80,
        // Values unique to CEF
        MENU_CAN_GO_FORWARD = 0x10000000,
        MENU_CAN_GO_BACK = 0x20000000,
    }
}
