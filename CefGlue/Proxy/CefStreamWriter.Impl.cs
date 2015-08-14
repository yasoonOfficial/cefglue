namespace CefGlue
{
    using System;
    using CefGlue.Interop;
    using System.IO;

    unsafe partial class CefStreamWriter
    {
        /// <summary>
        /// Create a new CefStreamWriter object for a file.
        /// </summary>
        public static CefStreamWriter Create(string fileName)
        {
            fixed (char* fileName_str = fileName)
            {
                var n_fileName = new cef_string_t(fileName_str, fileName != null ? fileName.Length : 0);
                return CefStreamWriter.From(
                    NativeMethods.cef_stream_writer_create_for_file(&n_fileName)
                    );
            }
        }

        /// <summary>
        /// Create a new CefStreamWriter object for a custom handler.
        /// </summary>
        public static CefStreamWriter Create(CefWriteHandler handler)
        {
            return CefStreamWriter.From(
                NativeMethods.cef_stream_writer_create_for_handler(handler.GetNativePointerAndAddRef())
                );
        }

        /// <summary>
        /// Write raw binary data.
        /// </summary>
        internal int Write(/*const*/ void* ptr, int size, int count)
        {
            return cef_stream_writer_t.invoke_write(this.ptr, ptr, size, count);
        }

        /// <summary>
        /// Write raw binary data.
        /// </summary>
        public int Write(byte[] buffer, int offset, int length)
        {
            if (buffer.Length - offset < length) throw new ArgumentOutOfRangeException();

            fixed (byte* ptr = &buffer[offset])
            {
                return Write(ptr, 1, length);
            }
        }


        /// <summary>
        /// Seek to the specified offset position.
        /// |whence| may be any one of SEEK_CUR, SEEK_END or SEEK_SET.
        /// </summary>
        public bool Seek(long offset, SeekOrigin whence)
        {
            return cef_stream_writer_t.invoke_seek(this.ptr, offset, (int)whence) == 0;
        }

        /// <summary>
        /// Return the current offset position.
        /// </summary>
        public long Tell()
        {
            return cef_stream_writer_t.invoke_tell(this.ptr);
        }

        /// <summary>
        /// Flush the stream.
        /// </summary>
        public bool Flush()
        {
            return cef_stream_writer_t.invoke_flush(this.ptr) == 0;
        }

    }
}
