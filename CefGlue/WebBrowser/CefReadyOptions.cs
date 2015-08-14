namespace CefGlue.WebBrowser
{
    using System;

    [Flags]
    public enum CefReadyOptions : int
    {
        None = 0x00,
        Idle = 0x01,
        Frames = 0x02,

        // TODO: resources
        _Resources = 0x04,

        // TODO: observe timeouts
        _Timeouts = 0x08,

        XmlHttpRequest = 0x10,

        All = int.MaxValue
    }
}
