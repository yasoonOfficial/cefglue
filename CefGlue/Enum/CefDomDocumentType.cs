namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// DOM document types.
    /// </summary>
    public enum CefDomDocumentType
    {
        Unknown = cef_dom_document_type_t.DOM_DOCUMENT_TYPE_UNKNOWN,
        Html = cef_dom_document_type_t.DOM_DOCUMENT_TYPE_HTML,
        XHtml = cef_dom_document_type_t.DOM_DOCUMENT_TYPE_XHTML,
        Plugin = cef_dom_document_type_t.DOM_DOCUMENT_TYPE_PLUGIN,
    }
}
