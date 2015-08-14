namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    public enum CefWebUrlRequestState
    {
        Unsent = cef_weburlrequest_state_t.WUR_STATE_UNSENT,
        Started = cef_weburlrequest_state_t.WUR_STATE_STARTED,
        HeadersReceived = cef_weburlrequest_state_t.WUR_STATE_HEADERS_RECEIVED,
        Loading = cef_weburlrequest_state_t.WUR_STATE_LOADING,
        Done = cef_weburlrequest_state_t.WUR_STATE_DONE,
        Error = cef_weburlrequest_state_t.WUR_STATE_ERROR,
        Abort = cef_weburlrequest_state_t.WUR_STATE_ABORT,
    }
}
