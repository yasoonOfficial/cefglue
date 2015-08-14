namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);
    }
}
