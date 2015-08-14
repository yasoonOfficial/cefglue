namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDomDocument
    {
        /// <summary>
        /// Returns the document type.
        /// </summary>
        public CefDomDocumentType Type
        {
            get
            {
                return (CefDomDocumentType)cef_domdocument_t.invoke_get_type(this.ptr);
            }
        }

        /// <summary>
        /// Returns the root document node.
        /// </summary>
        public CefDomNode GetDocument()
        {
            return CefDomNode.From(
                cef_domdocument_t.invoke_get_document(this.ptr)
                );
        }

        /// <summary>
        /// Returns the BODY node of an HTML document.
        /// </summary>
        public CefDomNode GetBody()
        {
            return CefDomNode.From(
                cef_domdocument_t.invoke_get_body(this.ptr)
                );
        }

        /// <summary>
        /// Returns the HEAD node of an HTML document.
        /// </summary>
        public CefDomNode GetHead()
        {
            return CefDomNode.From(
                cef_domdocument_t.invoke_get_head(this.ptr)
                );
        }

        /// <summary>
        /// Returns the title of an HTML document.
        /// </summary>
        public string GetTitle()
        {
            var nResult = cef_domdocument_t.invoke_get_title(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the document element with the specified ID value.
        /// </summary>
        public CefDomNode GetElementById(string id)
        {
            fixed (char* id_str = id)
            {
                var n_id = new cef_string_t(id_str, id != null ? id.Length : 0);
                return CefDomNode.From(
                    cef_domdocument_t.invoke_get_element_by_id(this.ptr, &n_id)
                    );
            }
        }

        /// <summary>
        /// Returns the node that currently has keyboard focus.
        /// </summary>
        public CefDomNode GetFocusedNode()
        {
            return CefDomNode.From(
                cef_domdocument_t.invoke_get_focused_node(this.ptr)
                );
        }

        /// <summary>
        /// Returns true if a portion of the document is selected.
        /// </summary>
        public bool HasSelection
        {
            get
            {
                return cef_domdocument_t.invoke_has_selection(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns the selection start node.
        /// </summary>
        public CefDomNode GetSelectionStartNode()
        {
            return CefDomNode.From(
                cef_domdocument_t.invoke_get_selection_start_node(this.ptr)
                );
        }

        /// <summary>
        /// Returns the selection offset within the start node.
        /// </summary>
        public int GetSelectionStartOffset()
        {
            return cef_domdocument_t.invoke_get_selection_start_offset(this.ptr);
        }

        /// <summary>
        /// Returns the selection end node.
        /// </summary>
        public CefDomNode GetSelectionEndNode()
        {
            return CefDomNode.From(
                cef_domdocument_t.invoke_get_selection_end_node(this.ptr)
                );
        }

        /// <summary>
        /// Returns the selection offset within the end node.
        /// </summary>
        public int GetSelectionEndOffset()
        {
            return cef_domdocument_t.invoke_get_selection_end_offset(this.ptr);
        }

        /// <summary>
        /// Returns the contents of this selection as markup.
        /// </summary>
        public string GetSelectionAsMarkup()
        {
            var nResult = cef_domdocument_t.invoke_get_selection_as_markup(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the contents of this selection as text.
        /// </summary>
        public string GetSelectionAsText()
        {
            var nResult = cef_domdocument_t.invoke_get_selection_as_text(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the base URL for the document.
        /// </summary>
        public string GetBaseURL()
        {
            var nResult = cef_domdocument_t.invoke_get_base_url(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns a complete URL based on the document base URL and the specified partial URL.
        /// </summary>
        public string GetCompleteURL(string partialUrl)
        {
            fixed (char* partialUrl_str = partialUrl)
            {
                var n_partialUrl = new cef_string_t(partialUrl_str, partialUrl != null ? partialUrl.Length : 0);
                var nResult = cef_domdocument_t.invoke_get_complete_url(this.ptr, &n_partialUrl);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

    }
}
