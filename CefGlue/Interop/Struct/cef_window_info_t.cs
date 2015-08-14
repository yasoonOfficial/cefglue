namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class representing window information.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_window_info_t
    {
#if WINDOWS
        // Standard parameters required by CreateWindowEx()
        public WindowStylesEx m_dwExStyle;
        public cef_string_t m_windowName;
        public WindowStyles m_dwStyle;
        public int m_x;
        public int m_y;
        public int m_nWidth;
        public int m_nHeight;
        public IntPtr m_hWndParent;
        public IntPtr m_hMenu;

        // If window rendering is disabled no browser window will be created. Set
        // |m_hWndParent| to the window that will act as the parent for popup menus,
        // dialog boxes, etc.
        //BOOL m_bWindowRenderingDisabled;
        public int m_bWindowRenderingDisabled;

        // Set to true to enable transparent painting.
        //BOOL m_bTransparentPainting;
        public int m_bTransparentPainting;

        // Handle for the new browser window.
        public IntPtr m_hWnd;
#elif LINUX
        // Pointer for the parent GtkBox widget.
        public cef_window_handle* m_ParentWidget;

        // Pointer for the new browser widget.
        public cef_window_handle* m_Widget;
#else
#error cef_window_info_t not supported current OS.
#endif

        public static void Clear(cef_window_info_t* self)
        {
#if WINDOWS
            cef_string_t.Clear(&self->m_windowName);
#endif
        }
    }
}
