namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// XML node types.
    /// </summary>
    public enum CefXmlNodeType
    {
        Unsupported = cef_xml_node_type_t.XML_NODE_UNSUPPORTED,
        ProcessingInstruction = cef_xml_node_type_t.XML_NODE_PROCESSING_INSTRUCTION,
        DocumentType = cef_xml_node_type_t.XML_NODE_DOCUMENT_TYPE,
        ElementStart = cef_xml_node_type_t.XML_NODE_ELEMENT_START,
        ElementEnd = cef_xml_node_type_t.XML_NODE_ELEMENT_END,
        Attribute = cef_xml_node_type_t.XML_NODE_ATTRIBUTE,
        Text = cef_xml_node_type_t.XML_NODE_TEXT,
        CData = cef_xml_node_type_t.XML_NODE_CDATA,
        EntityReference = cef_xml_node_type_t.XML_NODE_ENTITY_REFERENCE,
        Whitespace = cef_xml_node_type_t.XML_NODE_WHITESPACE,
        Comment = cef_xml_node_type_t.XML_NODE_COMMENT,
    }
}
