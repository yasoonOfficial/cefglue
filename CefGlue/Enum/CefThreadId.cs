namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Existing thread IDs.
    /// </summary>
    public enum CefThreadId
    {
        UI = cef_thread_id_t.TID_UI,
        IO = cef_thread_id_t.TID_IO,
        File = cef_thread_id_t.TID_FILE
    }
}
