#if DIAGNOSTICS
namespace CefGlue.Diagnostics
{
    using System;

    public enum LogOperation
    {
        None = 0,

        AddRef,
        ReleaseRef,
        RefCount,

        Create,
        Dispose
    }
}
#endif
