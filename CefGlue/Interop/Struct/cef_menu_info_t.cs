namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Structure representing menu information.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_menu_info_t
    {
        ///
        // Values from the cef_handler_menutypebits_t enumeration.
        ///
        public cef_handler_menutypebits_t typeFlags;

        ///
        // If window rendering is enabled |x| and |y| will be in screen coordinates.
        // Otherwise, |x| and |y| will be in view coordinates.
        ///
        public int x;
        public int y;

        public cef_string_t linkUrl;
        public cef_string_t imageUrl;
        public cef_string_t pageUrl;
        public cef_string_t frameUrl;
        public cef_string_t selectionText;
        public cef_string_t misspelledWord;

        ///
        // Values from the cef_handler_menucapabilitybits_t enumeration.
        ///
        public cef_handler_menucapabilitybits_t editFlags;

        public cef_string_t securityInfo;
    }
}
