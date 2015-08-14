namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal enum cef_weburlrequest_state_t : int
    {
        WUR_STATE_UNSENT = 0,
        WUR_STATE_STARTED = 1,
        WUR_STATE_HEADERS_RECEIVED = 2,
        WUR_STATE_LOADING = 3,
        WUR_STATE_DONE = 4,
        WUR_STATE_ERROR = 5,
        WUR_STATE_ABORT = 6,
    }
}
