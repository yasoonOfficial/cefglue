namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefXmlReader
    {
        /// <summary>
        /// Create a new CefXmlReader object.
        /// The returned object's methods can only be called from the thread that created the object.
        /// </summary>
        public static CefXmlReader Create(CefStreamReader stream, CefXmlEncodingType encodingType, string uri)
        {
            fixed (char* uri_str = uri)
            {
                var n_uri = new cef_string_t(uri_str, uri != null ? uri.Length : 0);

                return CefXmlReader.From(
                    NativeMethods.cef_xml_reader_create(
                        stream.GetNativePointerAndAddRef(),
                        (cef_xml_encoding_type_t)encodingType,
                        &n_uri)
                        );
            }
        }

        /// <summary>
        /// Create a new CefXmlReader object.
        /// The returned object's methods can only be called from the thread that created the object.
        /// </summary>
        public static CefXmlReader Create(CefStreamReader stream, CefXmlEncodingType encodingType, Uri uri)
        {
            return Create(stream, encodingType, uri.ToString());
        }

        /// <summary>
        /// Moves the cursor to the next node in the document.
        /// This method must be called at least once to set the current cursor position.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToNextNode()
        {
            return cef_xml_reader_t.invoke_move_to_next_node(this.ptr) != 0;
        }

        /// <summary>
        /// Close the document.
        /// This should be called directly to ensure that cleanup occurs on the correct thread.
        /// </summary>
        public bool Close()
        {
            return cef_xml_reader_t.invoke_close(this.ptr) != 0;
        }

        /// <summary>
        /// Returns true if an error has been reported by the XML parser.
        /// </summary>
        public bool HasError()
        {
            return cef_xml_reader_t.invoke_has_error(this.ptr) != 0;
        }

        /// <summary>
        /// Returns the error string.
        /// </summary>
        public string GetError()
        {
            var nResult = cef_xml_reader_t.invoke_get_error(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }


        // The below methods retrieve data for the node at the current cursor position.

        /// <summary>
        /// Returns the node type.
        /// </summary>
        public CefXmlNodeType Type
        {
            get
            {
                return (CefXmlNodeType)cef_xml_reader_t.invoke_get_type(this.ptr);
            }
        }

        /// <summary>
        /// Returns the node depth. Depth starts at 0 for the root node.
        /// </summary>
        public int GetDepth()
        {
            return cef_xml_reader_t.invoke_get_depth(this.ptr);
        }

        /// <summary>
        /// Returns the local name.
        /// See http://www.w3.org/TR/REC-xml-names/#NT-LocalPart for additional details.
        /// </summary>
        public string GetLocalName()
        {
            var nResult = cef_xml_reader_t.invoke_get_local_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the namespace prefix.
        /// See http://www.w3.org/TR/REC-xml-names/ for additional details.
        /// </summary>
        public string GetPrefix()
        {
            var nResult = cef_xml_reader_t.invoke_get_prefix(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the qualified name, equal to (Prefix:)LocalName.
        /// See http://www.w3.org/TR/REC-xml-names/#ns-qualnames for additional details.
        /// </summary>
        public string GetQualifiedName()
        {
            var nResult = cef_xml_reader_t.invoke_get_qualified_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the URI defining the namespace associated with the node.
        /// See http://www.w3.org/TR/REC-xml-names/ for additional details.
        /// </summary>
        public string GetNamespaceURI()
        {
            var nResult = cef_xml_reader_t.invoke_get_namespace_uri(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the base URI of the node.
        /// See http://www.w3.org/TR/xmlbase/ for additional details.
        /// </summary>
        public string GetBaseURI()
        {
            var nResult = cef_xml_reader_t.invoke_get_base_uri(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the xml:lang scope within which the node resides.
        /// See http://www.w3.org/TR/REC-xml/#sec-lang-tag for additional details.
        /// </summary>
        public string GetXmlLang()
        {
            var nResult = cef_xml_reader_t.invoke_get_xml_lang(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns true if the node represents an empty element.
        /// &lt;a/&gt; is considered empty but &lt;a&gt;&lt;/a&gt; is not.
        /// </summary>
        public bool IsEmptyElement()
        {
            return cef_xml_reader_t.invoke_is_empty_element(this.ptr) != 0;
        }

        /// <summary>
        /// Returns true if the node has a text value.
        /// </summary>
        public bool HasValue()
        {
            return cef_xml_reader_t.invoke_has_value(this.ptr) != 0;
        }

        /// <summary>
        /// Returns the text value.
        /// </summary>
        public string GetValue()
        {
            var nResult = cef_xml_reader_t.invoke_get_value(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns true if the node has attributes.
        /// </summary>
        public bool HasAttributes()
        {
            return cef_xml_reader_t.invoke_has_attributes(this.ptr) != 0;
        }

        /// <summary>
        /// Returns the number of attributes.
        /// </summary>
        public int GetAttributeCount()
        {
            return cef_xml_reader_t.invoke_get_attribute_count(this.ptr);
        }

        /// <summary>
        /// Returns the value of the attribute at the specified 0-based index.
        /// </summary>
        public string GetAttribute(int index)
        {
            var nResult = cef_xml_reader_t.invoke_get_attribute_byindex(this.ptr, index);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the value of the attribute with the specified qualified name.
        /// </summary>
        public string GetAttribute(string qualifiedName)
        {
            fixed (char* qualifiedName_str = qualifiedName)
            {
                var n_qualifiedName = new cef_string_t(qualifiedName_str, qualifiedName != null ? qualifiedName.Length : 0);

                var nResult = cef_xml_reader_t.invoke_get_attribute_byqname(this.ptr, &n_qualifiedName);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the value of the attribute with the specified local name and namespace URI.
        /// </summary>
        public string GetAttribute(string localName, string namespaceUri)
        {
            fixed (char* localName_str = localName)
            fixed (char* namespaceUri_str = namespaceUri)
            {
                var n_localName = new cef_string_t(localName_str, localName != null ? localName.Length : 0);
                var n_namespaceUri = new cef_string_t(namespaceUri_str, namespaceUri != null ? namespaceUri.Length : 0);

                var nResult = cef_xml_reader_t.invoke_get_attribute_bylname(this.ptr, &n_localName, &n_namespaceUri);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns an XML representation of the current node's children.
        /// </summary>
        public string GetInnerXml()
        {
            var nResult = cef_xml_reader_t.invoke_get_inner_xml(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns an XML representation of the current node including its children.
        /// </summary>
        public string GetOuterXml()
        {
            var nResult = cef_xml_reader_t.invoke_get_outer_xml(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the line number for the current node.
        /// </summary>
        public int GetLineNumber()
        {
            return cef_xml_reader_t.invoke_get_line_number(this.ptr);
        }


        // Attribute nodes are not traversed by default. The below methods can
        // be used to move the cursor to an attribute node.
        // MoveToCarryingElement() can be called afterwards to return the cursor
        // to the carrying element. The depth of an attribute node will be 1 +
        // the depth of the carrying element.

        /// <summary>
        /// Moves the cursor to the attribute at the specified 0-based index.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToAttribute(int index)
        {
            return cef_xml_reader_t.invoke_move_to_attribute_byindex(this.ptr, index) != 0;
        }

        /// <summary>
        /// Moves the cursor to the attribute with the specified qualified name.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToAttribute(string qualifiedName)
        {
            fixed (char* qualifiedName_str = qualifiedName)
            {
                var n_qualifiedName = new cef_string_t(qualifiedName_str, qualifiedName != null ? qualifiedName.Length : 0);

                return cef_xml_reader_t.invoke_move_to_attribute_byqname(this.ptr, &n_qualifiedName) != 0;
            }
        }

        /// <summary>
        /// Moves the cursor to the attribute with the specified local name and namespace URI.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToAttribute(string localName, string namespaceUri)
        {
            fixed (char* localName_str = localName)
            fixed (char* namespaceUri_str = namespaceUri)
            {
                var n_localName = new cef_string_t(localName_str, localName != null ? localName.Length : 0);
                var n_namespaceUri = new cef_string_t(namespaceUri_str, namespaceUri != null ? namespaceUri.Length : 0);

                return cef_xml_reader_t.invoke_move_to_attribute_bylname(this.ptr, &n_localName, &n_namespaceUri) != 0;
            }
        }

        /// <summary>
        /// Moves the cursor to the first attribute in the current element.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToFirstAttribute()
        {
            return cef_xml_reader_t.invoke_move_to_first_attribute(this.ptr) != 0;
        }

        /// <summary>
        /// Moves the cursor to the next attribute in the current element.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToNextAttribute()
        {
            return cef_xml_reader_t.invoke_move_to_next_attribute(this.ptr) != 0;
        }

        /// <summary>
        /// Moves the cursor back to the carrying element.
        /// Returns true if the cursor position was set successfully.
        /// </summary>
        public bool MoveToCarryingElement()
        {
            return cef_xml_reader_t.invoke_move_to_carrying_element(this.ptr) != 0;
        }

    }
}
