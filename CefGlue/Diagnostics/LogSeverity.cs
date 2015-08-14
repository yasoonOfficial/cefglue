#if DIAGNOSTICS
namespace CefGlue.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum LogSeverity
    {
        Trace = 0,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
#endif
