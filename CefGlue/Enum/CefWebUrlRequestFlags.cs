namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    [Flags]
    public enum CefWebUrlRequestFlags
    {
        None = cef_weburlrequest_flags_t.WUR_FLAG_NONE,
        SkipCache = cef_weburlrequest_flags_t.WUR_FLAG_SKIP_CACHE,
        AllowCachedCredentials = cef_weburlrequest_flags_t.WUR_FLAG_ALLOW_CACHED_CREDENTIALS,
        AllowCookies = cef_weburlrequest_flags_t.WUR_FLAG_ALLOW_COOKIES,
        ReportUploadProgress = cef_weburlrequest_flags_t.WUR_FLAG_REPORT_UPLOAD_PROGRESS,
        ReportLoadTiming = cef_weburlrequest_flags_t.WUR_FLAG_REPORT_LOAD_TIMING,
        ReportRawHeaders = cef_weburlrequest_flags_t.WUR_FLAG_REPORT_RAW_HEADERS,
    }
}
