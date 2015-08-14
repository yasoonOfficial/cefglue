namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Popup window features.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_popup_features_t
    {
        public int x;
        public bool_t xSet;
        public int y;
        public bool_t ySet;
        public int width;
        public bool_t widthSet;
        public int height;
        public bool_t heightSet;

        public bool_t menuBarVisible;
        public bool_t statusBarVisible;
        public bool_t toolBarVisible;
        public bool_t locationBarVisible;
        public bool_t scrollbarsVisible;
        public bool_t resizable;

        public bool_t fullscreen;
        public bool_t dialog;

        public cef_string_list* additionalFeatures;
    }
}
