namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Log severity levels.
    ///
    internal enum cef_log_severity_t : int
    {
        LOGSEVERITY_VERBOSE = -1,
        LOGSEVERITY_INFO,
        LOGSEVERITY_WARNING,
        LOGSEVERITY_ERROR,
        LOGSEVERITY_ERROR_REPORT,
        // Disables logging completely.
        LOGSEVERITY_DISABLE = 99
    }
}
