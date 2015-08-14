namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Various browser navigation types supported by chrome.
    ///
    internal enum cef_handler_navtype_t : int
    {
        NAVTYPE_LINKCLICKED = 0,
        NAVTYPE_FORMSUBMITTED,
        NAVTYPE_BACKFORWARD,
        NAVTYPE_RELOAD,
        NAVTYPE_FORMRESUBMITTED,
        NAVTYPE_OTHER,
        NAVTYPE_LINKDROPPED,
    }
}
