namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefWriteHandler
    {
        /// <summary>
        /// Write raw binary data.
        /// </summary>
        private int write(cef_write_handler_t* self, /*const*/ void* ptr, int size, int n)
        {
            ThrowIfObjectDisposed();

            long length = size * n;
            using (var m_stream = new UnmanagedMemoryStream((byte*)ptr, size * n, size * n, FileAccess.Read))
            {
                return this.Write(m_stream, size, n);
            }
        }

        protected abstract int Write(Stream stream, int size, int count);

        /// <summary>
        /// Seek to the specified offset position. |whence| may be any one of
        /// SEEK_CUR, SEEK_END or SEEK_SET.
        /// </summary>
        private int seek(cef_write_handler_t* self, long offset, int whence)
        {
            ThrowIfObjectDisposed();

            return this.Seek(offset, (SeekOrigin)whence) ? 0 : 1;
        }

        /// <summary>
        /// Seek to the specified offset position.
        /// |whence| may be any one of SEEK_CUR, SEEK_END or SEEK_SET.
        /// </summary>
        /// <returns>If successfull - returns true. If fails - returns false.</returns>
        protected abstract bool Seek(long offset, SeekOrigin whence);


        /// <summary>
        /// Return the current offset position.
        /// </summary>
        private long tell(cef_write_handler_t* self)
        {
            ThrowIfObjectDisposed();

            return this.Tell();
        }

        /// <summary>
        /// Return the current offset position.
        /// </summary>
        protected abstract long Tell();


        /// <summary>
        /// Flush the stream.
        /// </summary>
        private int flush(cef_write_handler_t* self)
        {
            ThrowIfObjectDisposed();

            return this.Flush() ? 0 : 1;
        }

        /// <summary>
        /// Flush the stream.
        /// </summary>
        /// <returns>If successfull - returns true. If fails - returns false.</returns>
        protected abstract bool Flush();


    }
}
