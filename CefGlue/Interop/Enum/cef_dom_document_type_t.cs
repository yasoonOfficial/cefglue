namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // DOM document types.
    ///
    internal enum cef_dom_document_type_t : int
    {
        DOM_DOCUMENT_TYPE_UNKNOWN = 0,
        DOM_DOCUMENT_TYPE_HTML,
        DOM_DOCUMENT_TYPE_XHTML,
        DOM_DOCUMENT_TYPE_PLUGIN,
    }
}
