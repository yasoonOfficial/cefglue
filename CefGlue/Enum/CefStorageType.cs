namespace CefGlue
{
    using CefGlue.Interop;

    /// <summary>
    /// Storage types.
    /// </summary>
    public enum CefStorageType : int
    {
        Local = cef_storage_type_t.ST_LOCALSTORAGE,
        Session = cef_storage_type_t.ST_SESSIONSTORAGE,
    }
}
