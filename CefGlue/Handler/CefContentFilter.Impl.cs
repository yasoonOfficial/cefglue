namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefContentFilter
    {
        /// <summary>
        /// Set |substitute_data| to the replacement for the data in |data| if
        /// data should be modified.
        /// </summary>
        private void process_data(cef_content_filter_t* self, /*const*/ void* data, int data_size, cef_stream_reader_t** substitute_data)
        {
            ThrowIfObjectDisposed();

            var m_stream = new UnmanagedMemoryStream((byte*)data, data_size, data_size, FileAccess.Read);
            CefStreamReader m_substitute_data;

            this.ProcessData(m_stream, out m_substitute_data);

            if (m_substitute_data != null)
            {
                *substitute_data = m_substitute_data.GetNativePointerAndAddRef();
            }

            m_stream.Dispose();
        }

        /// <summary>
        /// Set |substitute_data| to the replacement for the data in |data| if data should be modified.
        /// </summary>
        protected virtual void ProcessData(Stream data, out CefStreamReader substituteData)
        {
            substituteData = null;
        }


        /// <summary>
        /// Called when there is no more data to be processed. It is expected
        /// that whatever data was retained in the last ProcessData() call, it
        /// should be returned now by setting |remainder| if appropriate.
        /// </summary>
        private void drain(cef_content_filter_t* self, cef_stream_reader_t** remainder)
        {
            ThrowIfObjectDisposed();

            CefStreamReader m_remainder;

            this.Drain(out m_remainder);

            if (m_remainder != null)
            {
                *remainder = m_remainder.GetNativePointerAndAddRef();
            }
        }

        /// <summary>
        /// Called when there is no more data to be processed.
        /// It is expected that whatever data was retained in the last ProcessData() call,
        /// it should be returned now by setting |remainder| if appropriate.
        /// </summary>
        protected virtual void Drain(out CefStreamReader remainder)
        {
            remainder = null;
        }

    }
}
