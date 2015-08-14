namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefSchemeHandlerCallback
    {
        /// <summary>
        /// Notify that header information is now available for retrieval.
        /// </summary>
        public void HeadersAvailable()
        {
            cef_scheme_handler_callback_t.invoke_headers_available(this.ptr);
        }

        /// <summary>
        /// Notify that response data is now available for reading.
        /// </summary>
        public void BytesAvailable()
        {
            cef_scheme_handler_callback_t.invoke_bytes_available(this.ptr);
        }

        /// <summary>
        /// Cancel processing of the request.
        /// </summary>
        public void Cancel()
        {
            cef_scheme_handler_callback_t.invoke_cancel(this.ptr);
        }

    }
}
