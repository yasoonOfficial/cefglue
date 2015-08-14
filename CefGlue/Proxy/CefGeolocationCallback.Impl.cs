namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefGeolocationCallback
    {
        /// <summary>
        /// Call to allow or deny geolocation access.
        /// </summary>
        public void Continue(int allow)
        {
            cef_geolocation_callback_t.invoke_cont(this.ptr, allow);
        }
    }
}
