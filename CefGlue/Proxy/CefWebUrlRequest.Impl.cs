namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefWebUrlRequest
    {
        /// <summary>
        /// Create a new CefWebUrlRequest object.
        /// </summary>
        public static CefWebUrlRequest Create(CefRequest request, CefWebUrlRequestClient client)
        {
            return CefWebUrlRequest.From(
                NativeMethods.cef_web_urlrequest_create(
                    request.GetNativePointerAndAddRef(),
                    client.GetNativePointerAndAddRef()
                    )
                );
        }

        /// <summary>
        /// Cancels the request.
        /// </summary>
        public void Cancel()
        {
            cef_web_urlrequest_t.invoke_cancel(this.ptr);
        }

        /// <summary>
        /// Returns the current ready state of the request.
        /// </summary>
        public CefWebUrlRequestState GetState()
        {
            return (CefWebUrlRequestState)cef_web_urlrequest_t.invoke_get_state(this.ptr);
        }

    }
}
