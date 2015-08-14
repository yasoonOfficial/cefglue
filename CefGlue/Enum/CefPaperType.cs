namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Paper type for printing.
    /// </summary>
    public enum CefPaperType
    {
        Letter = cef_paper_type_t.PT_LETTER,
        Legal = cef_paper_type_t.PT_LEGAL,
        Executive = cef_paper_type_t.PT_EXECUTIVE,
        A3 = cef_paper_type_t.PT_A3,
        A4 = cef_paper_type_t.PT_A4,
        Custom = cef_paper_type_t.PT_CUSTOM
    }
}
