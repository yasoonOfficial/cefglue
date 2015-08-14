namespace CefGlue
{
    using System;
    using System.IO;
    using CefGlue.Interop;

    unsafe partial class CefReadHandler
    {
        /// <summary>
        /// Read raw binary data.
        /// </summary>
        private int read(cef_read_handler_t* self, void* ptr, int size, int n)
        {
            ThrowIfObjectDisposed();

            long length = size * n;
            using (var m_stream = new UnmanagedMemoryStream((byte*)ptr, length, length, FileAccess.Write))
            {
                return this.Read(m_stream, size, n);
            }
        }

        /// <summary>
        /// Read raw binary data.
        /// </summary>
        protected abstract int Read(Stream stream, int size, int count);


        /// <summary>
        /// Seek to the specified offset position. |whence| may be any one of
        /// SEEK_CUR, SEEK_END or SEEK_SET.
        /// </summary>
        private int seek(cef_read_handler_t* self, long offset, int whence)
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
        private long tell(cef_read_handler_t* self)
        {
            ThrowIfObjectDisposed();

            return this.Tell();
        }

        /// <summary>
        /// Return the current offset position.
        /// </summary>
        protected abstract long Tell();


        /// <summary>
        /// Return non-zero if at end of file.
        /// </summary>
        private int eof(cef_read_handler_t* self)
        {
            ThrowIfObjectDisposed();

            return this.Eof() ? 1 : 0;
        }

        /// <summary>
        /// Return non-zero if at end of file.
        /// </summary>
        protected abstract bool Eof();
    }
}
