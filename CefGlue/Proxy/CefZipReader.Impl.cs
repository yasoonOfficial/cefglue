namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefZipReader
    {
        /// <summary>
        /// Create a new CefZipReader object.
        /// The returned object's methods can only be called from the thread that created the object.
        /// </summary>
        public static CefZipReader Create(CefStreamReader stream)
        {
            return CefZipReader.From(
                NativeMethods.cef_zip_reader_create(stream.GetNativePointerAndAddRef())
                );
        }

        /// <summary>
        /// Moves the cursor to the first file in the archive.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToFirstFile()
        {
            return cef_zip_reader_t.invoke_move_to_first_file(this.ptr) != 0;
        }

        /// <summary>
        /// Moves the cursor to the next file in the archive.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToNextFile()
        {
            return cef_zip_reader_t.invoke_move_to_next_file(this.ptr) != 0;
        }

        /// <summary>
        /// Moves the cursor to the specified file in the archive.
        /// If |caseSensitive| is true then the search will be case sensitive.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToFile(string fileName, bool caseSensitive)
        {
            fixed (char* fileName_str = fileName)
            {
                var n_fileName = new cef_string_t(fileName_str, fileName != null ? fileName.Length : 0);

                return cef_zip_reader_t.invoke_move_to_file(this.ptr, &n_fileName, caseSensitive ? 1 : 0) != 0;
            }
        }

        /// <summary>
        /// Closes the archive.
        /// This should be called directly to ensure that cleanup occurs on the correct thread.
        /// </summary>
        public bool Close()
        {
            return cef_zip_reader_t.invoke_close(this.ptr) != 0;
        }


        // The below methods act on the file at the current cursor position.

        /// <summary>
        /// Returns the name of the file.
        /// </summary>
        public string GetFileName()
        {
            var nResult = cef_zip_reader_t.invoke_get_file_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the uncompressed size of the file.
        /// </summary>
        public long GetFileSize()
        {
            return cef_zip_reader_t.invoke_get_file_size(this.ptr);
        }

        /// <summary>
        /// Returns the last modified timestamp for the file.
        /// </summary>
        public DateTime GetFileLastModified()
        {
            var nResult = cef_zip_reader_t.invoke_get_file_last_modified(this.ptr);
            return UnixTime.ToDateTime(nResult);
        }

        /// <summary>
        /// Opens the file for reading of uncompressed data.
        /// A read password may optionally be specified.
        /// </summary>
        public bool OpenFile(string password = null)
        {
            fixed (char* password_str = password)
            {
                var n_password = new cef_string_t(password_str, password != null ? password.Length : 0);

                return cef_zip_reader_t.invoke_open_file(this.ptr, &n_password) != 0;
            }
        }

        /// <summary>
        /// Closes the file.
        /// </summary>
        public bool CloseFile()
        {
            return cef_zip_reader_t.invoke_close_file(this.ptr) != 0;
        }

        /// <summary>
        /// Read uncompressed file contents into the specified buffer.
        /// Returns &amp; 0 if an error occurred, 0 if at the end of file, or the number of bytes read.
        /// </summary>
        internal unsafe int ReadFile(void* buffer, int bufferSize)
        {
            return cef_zip_reader_t.invoke_read_file(this.ptr, buffer, bufferSize);
        }

        // TODO: CefZipReader.ReadFile overloads

        /// <summary>
        /// Returns the current offset in the uncompressed file contents.
        /// </summary>
        public long Tell()
        {
            return cef_zip_reader_t.invoke_tell(this.ptr);
        }

        /// <summary>
        /// Returns true if at end of the file contents.
        /// </summary>
        public bool Eof()
        {
            return cef_zip_reader_t.invoke_eof(this.ptr) != 0;
        }


    }
}
