namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Supported XML encoding types. The parser supports ASCII, ISO-8859-1, and
    // UTF16 (LE and BE) by default. All other types must be translated to UTF8
    // before being passed to the parser. If a BOM is detected and the correct
    // decoder is available then that decoder will be used automatically.
    ///
    internal enum cef_xml_encoding_type_t : int
    {
        XML_ENCODING_NONE = 0,
        XML_ENCODING_UTF8,
        XML_ENCODING_UTF16LE,
        XML_ENCODING_UTF16BE,
        XML_ENCODING_ASCII,
    }
}
