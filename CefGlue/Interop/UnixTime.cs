namespace CefGlue.Interop
{
    using System;

    internal static class UnixTime
    {
        public static DateTime ToDateTime(int unixTime)
        {
            return new DateTime(1970, 1, 1).AddSeconds(unixTime);
        }
    }
}

