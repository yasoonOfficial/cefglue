namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// DOM node types.
    /// </summary>
    public enum CefDomNodeType
    {
        Unsupported = cef_dom_node_type_t.DOM_NODE_TYPE_UNSUPPORTED,
        Element = cef_dom_node_type_t.DOM_NODE_TYPE_ELEMENT,
        Attribute = cef_dom_node_type_t.DOM_NODE_TYPE_ATTRIBUTE,
        Text = cef_dom_node_type_t.DOM_NODE_TYPE_TEXT,
        CDataSection = cef_dom_node_type_t.DOM_NODE_TYPE_CDATA_SECTION,
        EntityReference = cef_dom_node_type_t.DOM_NODE_TYPE_ENTITY_REFERENCE,
        Entity = cef_dom_node_type_t.DOM_NODE_TYPE_ENTITY,
        ProcessingInstruction = cef_dom_node_type_t.DOM_NODE_TYPE_PROCESSING_INSTRUCTIONS,
        Comment = cef_dom_node_type_t.DOM_NODE_TYPE_COMMENT,
        Document = cef_dom_node_type_t.DOM_NODE_TYPE_DOCUMENT,
        DocumentType = cef_dom_node_type_t.DOM_NODE_TYPE_DOCUMENT_TYPE,
        DocumentFragment = cef_dom_node_type_t.DOM_NODE_TYPE_DOCUMENT_FRAGMENT,
        Notation = cef_dom_node_type_t.DOM_NODE_TYPE_NOTATION,
        XPathNamespace = cef_dom_node_type_t.DOM_NODE_TYPE_XPATH_NAMESPACE,
    }
}
