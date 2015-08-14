namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    ///
    // Class representing print context information.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_print_info_t
    {
        // HDC m_hDC;
        public IntPtr m_hDC;
        //RECT m_Rect;
        public RECT m_Rect;
        public double m_Scale;
    }
}
