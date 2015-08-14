namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Existing thread IDs.
    /// </summary>
    internal enum cef_thread_id_t : int
    {
        TID_UI = 0,
        TID_IO = 1,
        TID_FILE = 2,
    }
}
