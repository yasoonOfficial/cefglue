namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDomNode
    {
        /// <summary>
        /// Returns the type for this node.
        /// </summary>
        public CefDomNodeType Type
        {
            get
            {
                return (CefDomNodeType)cef_domnode_t.invoke_get_type(this.ptr);
            }
        }

        /// <summary>
        /// Returns true if this is a text node.
        /// </summary>
        public bool IsText
        {
            get
            {
                return cef_domnode_t.invoke_is_text(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if this is an element node.
        /// </summary>
        public bool IsElement
        {
            get
            {
                return cef_domnode_t.invoke_is_element(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if this is a form control element node.
        /// </summary>
        public bool IsFormControlElement
        {
            get
            {
                return cef_domnode_t.invoke_is_form_control_element(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns the type of this form control element node.
        /// </summary>
        public string FormControlElementType
        {
            get
            {
                var nResult = cef_domnode_t.invoke_get_form_control_element_type(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns true if this object is pointing to the same handle as |that| object.
        /// </summary>
        public bool IsSame(CefDomNode that)
        {
            return cef_domnode_t.invoke_is_same(this.ptr, that.GetNativePointerAndAddRef()) != 0;
        }

        /// <summary>
        /// Returns the name of this node.
        /// </summary>
        public string GetName()
        {
            var nResult = cef_domnode_t.invoke_get_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the value of this node.
        /// </summary>
        public string GetValue()
        {
            var nResult = cef_domnode_t.invoke_get_value(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Set the value of this node.
        /// Returns true on success.
        /// </summary>
        public bool SetValue(string value)
        {
            fixed (char* value_str = value)
            {
                var n_value = new cef_string_t(value_str, value != null ? value.Length : 0);
                return cef_domnode_t.invoke_set_value(this.ptr, &n_value) != 0;
            }
        }

        /// <summary>
        /// Returns the contents of this node as markup.
        /// </summary>
        public string GetAsMarkup()
        {
            var nResult = cef_domnode_t.invoke_get_as_markup(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the document associated with this node.
        /// </summary>
        public CefDomDocument GetDocument()
        {
            var n_document = cef_domnode_t.invoke_get_document(this.ptr);
            return CefDomDocument.From(n_document);
        }

        /// <summary>
        /// Returns the parent node.
        /// </summary>
        public CefDomNode GetParent()
        {
            var n_parent = cef_domnode_t.invoke_get_parent(this.ptr);
            return CefDomNode.From(n_parent);
        }

        /// <summary>
        /// Returns the previous sibling node.
        /// </summary>
        public CefDomNode GetPreviousSibling()
        {
            var n_result = cef_domnode_t.invoke_get_previous_sibling(this.ptr);
            return CefDomNode.From(n_result);
        }

        /// <summary>
        /// Returns the next sibling node.
        /// </summary>
        public CefDomNode GetNextSibling()
        {
            var n_result = cef_domnode_t.invoke_get_next_sibling(this.ptr);
            return CefDomNode.From(n_result);
        }

        /// <summary>
        /// Returns true if this node has child nodes.
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return cef_domnode_t.invoke_has_children(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Return the first child node.
        /// </summary>
        public CefDomNode GetFirstChild()
        {
            return CefDomNode.From(
                cef_domnode_t.invoke_get_first_child(this.ptr)
                );
        }

        /// <summary>
        /// Returns the last child node.
        /// </summary>
        public CefDomNode GetLastChild()
        {
            return CefDomNode.From(
                cef_domnode_t.invoke_get_last_child(this.ptr)
                );
        }

        /// <summary>
        /// Add an event listener to this node for the specified event type.
        /// If |useCapture| is true then this listener will be considered a capturing listener.
        /// Capturing listeners will recieve all events of the specified type before the events are dispatched to any other event targets beneath the current node in the tree.
        /// Events which are bubbling upwards through the tree will not trigger a capturing listener.
        /// Separate calls to this method can be used to register the same listener with and without capture.
        /// See WebCore/dom/EventNames.h for the list of supported event types.
        /// </summary>
        public void AddEventListener(string eventType, CefDomEventListener listener, bool useCapture)
        {
            fixed (char* eventType_str = eventType)
            {
                var n_eventType = new cef_string_t(eventType_str, eventType != null ? eventType.Length : 0);
                cef_domnode_t.invoke_add_event_listener(this.ptr, &n_eventType, listener.GetNativePointerAndAddRef(), useCapture ? 1 : 0);
            }
        }


        // The following methods are valid only for element nodes.

        /// <summary>
        /// Returns the tag name of this element.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public string GetElementTagName()
        {
            var nResult = cef_domnode_t.invoke_get_element_tag_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns true if this element has attributes.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public bool HasElementAttributes()
        {
            return cef_domnode_t.invoke_has_element_attributes(this.ptr) != 0;
        }

        /// <summary>
        /// Returns true if this element has an attribute named |attrName|.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public bool HasElementAttribute(string attrName)
        {
            fixed (char* attrName_str = attrName)
            {
                var n_attrName = new cef_string_t(attrName_str, attrName != null ? attrName.Length : 0);
                return cef_domnode_t.invoke_has_element_attribute(this.ptr, &n_attrName) != 0;
            }
        }

        /// <summary>
        /// Returns the element attribute named |attrName|.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public string GetElementAttribute(string attrName)
        {
            fixed (char* attrName_str = attrName)
            {
                var nAttrName = new cef_string_t(attrName_str, attrName != null ? attrName.Length : 0);
                var nResult = cef_domnode_t.invoke_get_element_attribute(this.ptr, &nAttrName);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns a map of all element attributes.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public CefStringMap GetElementAttributes()
        {
            var result = new CefStringMap();
            cef_domnode_t.invoke_get_element_attributes(this.ptr, result.Handle);
            return result;
        }

        /// <summary>
        /// Set the value for the element attribute named |attrName|.
        /// Returns true on success.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public bool SetElementAttribute(string attrName, string value)
        {
            fixed (char* attrName_str = attrName)
            fixed (char* value_str = value)
            {
                var n_attrName = new cef_string_t(attrName_str, attrName != null ? attrName.Length : 0);
                var n_value = new cef_string_t(value_str, value != null ? value.Length : 0);

                return cef_domnode_t.invoke_set_element_attribute(this.ptr, &n_attrName, &n_value) != 0;
            }
        }

        /// <summary>
        /// Returns the inner text of the element.
        /// </summary>
        /// <remarks>
        /// This method are valid only for element node.
        /// </remarks>
        public string GetElementInnerText()
        {
            var nResult = cef_domnode_t.invoke_get_element_inner_text(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }


    }
}
