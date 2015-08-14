namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal enum cef_weburlrequest_flags_t : int
    {
        WUR_FLAG_NONE = 0,
        WUR_FLAG_SKIP_CACHE = 0x1,
        WUR_FLAG_ALLOW_CACHED_CREDENTIALS = 0x2,
        WUR_FLAG_ALLOW_COOKIES = 0x4,
        WUR_FLAG_REPORT_UPLOAD_PROGRESS = 0x8,
        WUR_FLAG_REPORT_LOAD_TIMING = 0x10,
        WUR_FLAG_REPORT_RAW_HEADERS = 0x20
    }
}
