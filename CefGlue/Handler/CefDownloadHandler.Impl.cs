namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefDownloadHandler
    {
        /// <summary>
        /// A portion of the file contents have been received. This method will
        /// be called multiple times until the download is complete. Return
        /// |true| to continue receiving data and |false| to cancel.
        /// </summary>
        private int received_data(cef_download_handler_t* self, void* data, int data_size)
        {
            ThrowIfObjectDisposed();

            var m_stream = new UnmanagedMemoryStream((byte*)data, data_size, data_size, FileAccess.Read);

            var handled = this.ReceivedData(m_stream);

            m_stream.Dispose();

            return handled ? 1 : 0;
        }

        /// <summary>
        /// A portion of the file contents have been received.
        /// This method will be called multiple times until the download is complete.
        /// Return |true| to continue receiving data and |false| to cancel.
        /// </summary>
        protected abstract bool ReceivedData(Stream data);


        /// <summary>
        /// The download is complete.
        /// </summary>
        private void complete(cef_download_handler_t* self)
        {
            ThrowIfObjectDisposed();

            this.Complete();
        }

        /// <summary>
        /// The download is complete.
        /// </summary>
        protected abstract void Complete();

    }
}
