namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefPostDataElement
    {
        /// <summary>
        /// Create a new CefPostDataElement object.
        /// </summary>
        public static CefPostDataElement Create()
        {
            return CefPostDataElement.From(
                NativeMethods.cef_post_data_element_create()
                );
        }

        /// <summary>
        /// Remove all contents from the post data element.
        /// </summary>
        public void SetToEmpty()
        {
            cef_post_data_element_t.invoke_set_to_empty(this.ptr);
        }

        /// <summary>
        /// The post data element will represent a file.
        /// </summary>
        public void SetToFile(string fileName)
        {
            fixed (char* fileName_str = fileName)
            {
                var n_fileName = new cef_string_t(fileName_str, fileName != null ? fileName.Length : 0);
                cef_post_data_element_t.invoke_set_to_file(this.ptr, &n_fileName);
            }
        }

        /// <summary>
        /// The post data element will represent bytes.
        /// The bytes passed in will be copied.
        /// </summary>
        public void SetToBytes(int size, IntPtr bytes)
        {
            cef_post_data_element_t.invoke_set_to_bytes(this.ptr, size, (void*)bytes);

            // TODO: make usable method overrides (byte[] bytes, int offset, int length), (byte[] bytes, int length), (byte[] bytes)
        }

        /// <summary>
        /// Return the type of this post data element.
        /// </summary>
        public CefPostDataElementType Type
        {
            get
            {
                return (CefPostDataElementType)cef_post_data_element_t.invoke_get_type(this.ptr);
            }
        }

        /// <summary>
        /// Return the file name.
        /// </summary>
        public string GetFile()
        {
            var nResult = cef_post_data_element_t.invoke_get_file(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the number of bytes.
        /// </summary>
        public int GetBytesCount()
        {
            return cef_post_data_element_t.invoke_get_bytes_count(this.ptr);
        }

        /// <summary>
        /// Read up to |size| bytes into |bytes| and return the number of bytes actually read.
        /// </summary>
        public int GetBytes(int size, IntPtr bytes)
        {
            return cef_post_data_element_t.invoke_get_bytes(this.ptr, size, (void*)bytes);
            // TODO: make usable overrides
        }

    }
}
