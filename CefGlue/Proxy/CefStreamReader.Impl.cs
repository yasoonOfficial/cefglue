namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefStreamReader
    {
        /// <summary>
        /// Create a new CefStreamReader object from a file.
        /// </summary>
        public static CefStreamReader Create(string fileName)
        {
            fixed (char* fileName_str = fileName)
            {
                var n_fileName = new cef_string_t(fileName_str, fileName != null ? fileName.Length : 0);

                return CefStreamReader.From(
                    NativeMethods.cef_stream_reader_create_for_file(&n_fileName)
                    );
            }
        }

        /// <summary>
        /// Create a new CefStreamReader object from data.
        /// </summary>
        /// <remarks>It will copy data.</remarks>
        [CLSCompliant(false)]
        public static unsafe CefStreamReader Create(void* data, int size)
        {
            return CefStreamReader.From(
                NativeMethods.cef_stream_reader_create_for_data(data, size)
                );
        }

        /// <summary>
        /// Create a new CefStreamReader object from a custom handler.
        /// </summary>
        public static CefStreamReader Create(CefReadHandler handler)
        {
            return CefStreamReader.From(
                NativeMethods.cef_stream_reader_create_for_handler(handler.GetNativePointerAndAddRef())
                );
        }

        /// <summary>
        /// Read raw binary data.
        /// </summary>
        internal int Read(void* ptr, int size, int count)
        {
            return cef_stream_reader_t.invoke_read(this.ptr, ptr, size, count);
        }

        /// <summary>
        /// Read raw binary data.
        /// </summary>
        public int Read(byte[] buffer, int offset, int length)
        {
            if (buffer.Length - offset < length) throw new ArgumentOutOfRangeException();

            fixed (byte* ptr = &buffer[offset])
            {
                return Read(ptr, 1, length);
            }
        }


        /// <summary>
        /// Seek to the specified offset position.
        /// |whence| may be any one of SEEK_CUR, SEEK_END or SEEK_SET.
        /// Returns zero on success and non-zero on failure.
        /// </summary>
        public bool Seek(long offset, SeekOrigin whence)
        {
            return cef_stream_reader_t.invoke_seek(this.ptr, offset, (int)whence) == 0;
        }

        /// <summary>
        /// Return the current offset position.
        /// </summary>
        public long Tell()
        {
            return cef_stream_reader_t.invoke_tell(this.ptr);
        }

        /// <summary>
        /// Return non-zero if at end of file.
        /// </summary>
        public bool Eof()
        {
            return cef_stream_reader_t.invoke_eof(this.ptr) != 0;
        }


    }
}
