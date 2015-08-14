namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefWebUrlRequestClient
    {
        /// <summary>
        /// Notifies the client that the request state has changed. State change
        /// notifications will always be sent before the below notification
        /// methods are called.
        /// </summary>
        private void on_state_change(cef_web_urlrequest_client_t* self, cef_web_urlrequest_t* requester, cef_weburlrequest_state_t state)
        {
            ThrowIfObjectDisposed();

            var m_requester = CefWebUrlRequest.From(requester);

            this.OnStateChange(m_requester, (CefWebUrlRequestState)state);
        }

        /// <summary>
        /// Notifies the client that the request state has changed.
        /// State change notifications will always be sent before the below notification methods are called.
        /// </summary>
        protected virtual void OnStateChange(CefWebUrlRequest requester, CefWebUrlRequestState state)
        {
        }


        /// <summary>
        /// Notifies the client that the request has been redirected and
        /// provides a chance to change the request parameters.
        /// </summary>
        private void on_redirect(cef_web_urlrequest_client_t* self, cef_web_urlrequest_t* requester, cef_request_t* request, cef_response_t* response)
        {
            ThrowIfObjectDisposed();

            var m_requester = CefWebUrlRequest.From(requester);
            var m_request = CefRequest.From(request);
            var m_response = CefResponse.From(response);

            this.OnRedirect(m_requester, m_request, m_response);
        }

        /// <summary>
        /// Notifies the client that the request has been redirected and provides a chance to change the request parameters.
        /// </summary>
        protected virtual void OnRedirect(CefWebUrlRequest requester, CefRequest request, CefResponse response)
        {
        }


        /// <summary>
        /// Notifies the client of the response data.
        /// </summary>
        private void on_headers_received(cef_web_urlrequest_client_t* self, cef_web_urlrequest_t* requester, cef_response_t* response)
        {
            ThrowIfObjectDisposed();

            var m_requester = CefWebUrlRequest.From(requester);
            var m_response = CefResponse.From(response);

            this.OnHeadersReceived(m_requester, m_response);
        }

        /// <summary>
        /// Notifies the client of the response data.
        /// </summary>
        protected virtual void OnHeadersReceived(CefWebUrlRequest requester, CefResponse response)
        {
        }


        /// <summary>
        /// Notifies the client of the upload progress.
        /// </summary>
        private void on_progress(cef_web_urlrequest_client_t* self, cef_web_urlrequest_t* requester, ulong bytesSent, ulong totalBytesToBeSent)
        {
            ThrowIfObjectDisposed();

            var m_requester = CefWebUrlRequest.From(requester);

            this.OnProgress(m_requester, bytesSent, totalBytesToBeSent);
        }

        /// <summary>
        /// Notifies the client of the upload progress.
        /// </summary>
        [CLSCompliant(false)]
        protected virtual void OnProgress(CefWebUrlRequest requester, ulong bytesSent, ulong totalBytesToBeSent)
        {
        }


        /// <summary>
        /// Notifies the client that content has been received.
        /// </summary>
        private void on_data(cef_web_urlrequest_client_t* self, cef_web_urlrequest_t* requester, /*const*/ void* data, int dataLength)
        {
            ThrowIfObjectDisposed();

            var m_requester = CefWebUrlRequest.From(requester);
            using (var m_stream = new UnmanagedMemoryStream((byte*)data, dataLength, dataLength, FileAccess.Read))
            {
                this.OnData(m_requester, m_stream, dataLength);
            }
        }

        /// <summary>
        /// Notifies the client that content has been received.
        /// </summary>
        protected virtual void OnData(CefWebUrlRequest requester, Stream data, int dataLength)
        {
        }


        /// <summary>
        /// Notifies the client that the request ended with an error.
        /// </summary>
        private void on_error(cef_web_urlrequest_client_t* self, cef_web_urlrequest_t* requester, cef_handler_errorcode_t errorCode)
        {
            ThrowIfObjectDisposed();

            var m_requester = CefWebUrlRequest.From(requester);

            this.OnError(m_requester, (CefHandlerErrorCode)errorCode);
        }

        /// <summary>
        /// Notifies the client that the request ended with an error.
        /// </summary>
        protected virtual void OnError(CefWebUrlRequest requester, CefHandlerErrorCode errorCode)
        {
        }

    }
}
