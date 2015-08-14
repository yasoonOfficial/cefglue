namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefSchemeHandler
    {
        /// <summary>
        /// Begin processing the request. To handle the request return true and
        /// call HeadersAvailable() once the response header information is
        /// available (HeadersAvailable() can also be called from inside this
        /// method if header information is available immediately). To cancel the request return false.
        /// </summary>
        private int process_request(cef_scheme_handler_t* self, cef_request_t* request, cef_scheme_handler_callback_t* callback)
        {
            ThrowIfObjectDisposed();

            var mRequest = CefRequest.From(request);
            var mCallback = CefSchemeHandlerCallback.From(callback);

            var handled = this.ProcessRequest(mRequest, mCallback);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Begin processing the request.
        /// 
        /// To handle the request return true and call HeadersAvailable() once the response header information is
        /// available (HeadersAvailable() can also be called from inside this
        /// method if header information is available immediately).
        /// 
        /// To cancel the request return false.
        /// </summary>
        protected abstract bool ProcessRequest(CefRequest request, CefSchemeHandlerCallback callback);


        /// <summary>
        /// Retrieve response header information. If the response length is not
        /// known set |response_length| to -1 and ReadResponse() will be called
        /// until it returns false. If the response length is known set
        /// |response_length| to a positive value and ReadResponse() will be
        /// called until it returns false or the specified number of bytes have
        /// been read. Use the |response| object to set the mime type, http
        /// status code and other optional header values.
        /// To redirect the request to a new URL set |redirectUrl| to the new URL.
        /// </summary>
        private void get_response_headers(cef_scheme_handler_t* self, cef_response_t* response, long* response_length, cef_string_t* redirectUrl)
        {
            ThrowIfObjectDisposed();

            var mResponse = CefResponse.From(response);
            long mResponseLength;
            string mRedirectUrl = null;

            this.GetResponseHeaders(mResponse, out mResponseLength, ref mRedirectUrl);

            *response_length = mResponseLength;
            if (mRedirectUrl != null) {
                cef_string_t.Copy(mRedirectUrl, redirectUrl);
            }
        }

        /// <summary>
        /// Retrieve response header information.
        /// If the response length is not known set |response_length| to -1 and ReadResponse() will be called
        /// until it returns false.
        /// 
        /// If the response length is known set |response_length| to a positive value and ReadResponse() will be
        /// called until it returns false or the specified number of bytes have
        /// been read.
        /// 
        /// Use the |response| object to set the mime type, http status code and other optional header values.
        ///
        /// To redirect the request to a new URL set |redirectUrl| to the new URL.
        /// </summary>
        protected abstract void GetResponseHeaders(CefResponse response, out long responseLength, ref string redirectUrl);


        /// <summary>
        /// Read response data. If data is available immediately copy up to
        /// |bytes_to_read| bytes into |data_out|, set |bytes_read| to the number
        /// of bytes copied, and return true. To read the data at a later time
        /// set |bytes_read| to 0, return true and call BytesAvailable() when the
        /// data is available. To indicate response completion return false.
        /// </summary>
        private int read_response(cef_scheme_handler_t* self, void* data_out, int bytes_to_read, int* bytes_read, cef_scheme_handler_callback_t* callback)
        {
            ThrowIfObjectDisposed();

            var mCallback = CefSchemeHandlerCallback.From(callback);

            using (var mStream = new UnmanagedMemoryStream((byte*)data_out, bytes_to_read, bytes_to_read, FileAccess.Write))
            {
                int mBytesRead;
                var handled = this.ReadResponse(mStream, bytes_to_read, out mBytesRead, mCallback);
                *bytes_read = mBytesRead;
                return handled ? 1 : 0;
            }
        }

        /// <summary>
        /// Read response data.
        /// If data is available immediately copy up to |bytes_to_read| bytes into |data_out|, set |bytes_read| to the number
        /// of bytes copied, and return true.
        /// 
        /// To read the data at a later time set |bytes_read| to 0, return true and call BytesAvailable() when the
        /// data is available.
        /// 
        /// To indicate response completion return false.
        /// </summary>
        protected abstract bool ReadResponse(Stream stream, int bytesToRead, out int bytesRead, CefSchemeHandlerCallback callback);


        /// <summary>
        /// Cancel processing of the request.
        /// </summary>
        private void cancel(cef_scheme_handler_t* self)
        {
            ThrowIfObjectDisposed();

            this.Cancel();
        }

        /// <summary>
        /// Cancel processing of the request.
        /// </summary>
        protected abstract void Cancel();


    }
}
