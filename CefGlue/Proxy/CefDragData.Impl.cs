namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDragData
    {
        /// <summary>
        /// Returns true if the drag data is a link.
        /// </summary>
        public bool IsLink
        {
            get
            {
                return cef_drag_data_t.invoke_is_link(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if the drag data is a text or html fragment.
        /// </summary>
        public bool IsFragment
        {
            get
            {
                return cef_drag_data_t.invoke_is_fragment(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if the drag data is a file.
        /// </summary>
        public bool IsFile
        {
            get
            {
                return cef_drag_data_t.invoke_is_file(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Return the link URL that is being dragged.
        /// </summary>
        public string GetLinkURL()
        {
            var nResult = cef_drag_data_t.invoke_get_link_url(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the title associated with the link being dragged.
        /// </summary>
        public string GetLinkTitle()
        {
            var nResult = cef_drag_data_t.invoke_get_link_title(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the metadata, if any, associated with the link being dragged.
        /// </summary>
        public string GetLinkMetadata()
        {
            var nResult = cef_drag_data_t.invoke_get_link_metadata(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the plain text fragment that is being dragged.
        /// </summary>
        public string GetFragmentText()
        {
            var nResult = cef_drag_data_t.invoke_get_fragment_text(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the text/html fragment that is being dragged.
        /// </summary>
        public string GetFragmentHtml()
        {
            var nResult = cef_drag_data_t.invoke_get_fragment_html(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the base URL that the fragment came from. This value is used
        /// for resolving relative URLs and may be empty.
        /// </summary>
        public string GetFragmentBaseURL()
        {
            var nResult = cef_drag_data_t.invoke_get_fragment_base_url(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Return the name of the file being dragged out of the browser window.
        /// </summary>
        public string GetFileName()
        {
            var nResult = cef_drag_data_t.invoke_get_file_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Retrieve the list of file names that are being dragged into the
        /// browser window.
        /// </summary>
        public CefStringList GetFileNames()
        {
            var nList = CefStringList.CreateHandle();

            var success = cef_drag_data_t.invoke_get_file_names(this.ptr, nList) != 0;

            if (success)
            {
                return CefStringList.From(nList, true);
            }
            else
            {
                CefStringList.DestroyHandle(nList);
                return null;
            }
        }
    }
}
