namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Supported XML encoding types. The parser supports ASCII, ISO-8859-1, and
    /// UTF16 (LE and BE) by default. All other types must be translated to UTF8
    /// before being passed to the parser. If a BOM is detected and the correct
    /// decoder is available then that decoder will be used automatically.
    /// </summary>
    public enum CefXmlEncodingType
    {
        None = cef_xml_encoding_type_t.XML_ENCODING_NONE,
        Utf8 = cef_xml_encoding_type_t.XML_ENCODING_UTF8,
        Utf16LE = cef_xml_encoding_type_t.XML_ENCODING_UTF16LE,
        Utf16BE = cef_xml_encoding_type_t.XML_ENCODING_UTF16BE,
        Ascii = cef_xml_encoding_type_t.XML_ENCODING_ASCII,
    }
}
