namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefPrintHandler
    {
        /// <summary>
        /// Called to allow customization of standard print options before the
        /// print dialog is displayed. |printOptions| allows specification of
        /// paper size, orientation and margins. Note that the specified margins
        /// may be adjusted if they are outside the range supported by the
        /// printer. All units are in inches. Return false to display the default
        /// print options or true to display the modified |printOptions|.
        /// </summary>
        private int get_print_options(cef_print_handler_t* self, cef_browser_t* browser, cef_print_options_t* printOptions)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_printOptions = CefPrintOptions.From(printOptions);

            var handled = this.GetPrintOptions(m_browser, m_printOptions);

            m_printOptions.Dispose();

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to allow customization of standard print options before the print dialog is displayed.
        /// |printOptions| allows specification of paper size, orientation and margins.
        /// Note that the specified margins may be adjusted if they are outside the range supported by the printer.
        /// All units are in inches.
        /// Return false to display the default print options 
        /// or true to display the modified |printOptions|.
        /// </summary>
        protected virtual bool GetPrintOptions(CefBrowser browser, CefPrintOptions printOptions)
        {
            return false;
        }

        /// <summary>
        /// Called to format print headers and footers. |printInfo| contains
        /// platform- specific information about the printer context. |url| is
        /// the URL if the currently printing page, |title| is the title of the
        /// currently printing page, |currentPage| is the current page number and
        /// |maxPages| is the total number of pages. Six default header locations
        /// are provided by the implementation: top left, top center, top right,
        /// bottom left, bottom center and bottom right. To use one of these
        /// default locations just assign a string to the appropriate variable.
        /// To draw the header and footer yourself return true. Otherwise,
        /// populate the approprate variables and return false.
        /// </summary>
        private int get_print_header_footer(cef_print_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, /*const*/ cef_print_info_t* printInfo, /*const*/ cef_string_t* url, /*const*/ cef_string_t* title, int currentPage, int maxPages, cef_string_t* topLeft, cef_string_t* topCenter, cef_string_t* topRight, cef_string_t* bottomLeft, cef_string_t* bottomCenter, cef_string_t* bottomRight)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_printInfo = CefPrintInfo.From(printInfo);
            var m_url = cef_string_t.ToString(url);
            var m_title = cef_string_t.ToString(title);
            
            var m_topLeft = cef_string_t.ToString(topLeft);
            var m_topCenter = cef_string_t.ToString(topCenter);
            var m_topRight = cef_string_t.ToString(topRight);
            var m_bottomLeft = cef_string_t.ToString(bottomLeft);
            var m_bottomCenter = cef_string_t.ToString(bottomCenter);
            var m_bottomRight = cef_string_t.ToString(bottomRight);

            var o_topLeft = m_topLeft;
            var o_topCenter = m_topCenter;
            var o_topRight = m_topRight;
            var o_bottomLeft = m_bottomLeft;
            var o_bottomCenter = m_bottomCenter;
            var o_bottomRight = m_bottomRight;

            var handled = this.GetPrintHeaderFooter(m_browser, m_frame,
                m_printInfo, m_url, m_title, currentPage, maxPages,
                ref m_topLeft, ref m_topCenter, ref m_topRight,
                ref m_bottomLeft, ref m_bottomCenter, ref m_bottomRight);

            m_printInfo.Dispose();

            if (!handled)
            {
                if ((object)m_topLeft != (object)o_topLeft) cef_string_t.Copy(m_topLeft, topLeft);
                if ((object)m_topCenter != (object)o_topCenter) cef_string_t.Copy(m_topCenter, topCenter);
                if ((object)m_topRight != (object)o_topRight) cef_string_t.Copy(m_topRight, topRight);
                if ((object)m_bottomLeft != (object)o_bottomLeft) cef_string_t.Copy(m_bottomLeft, bottomLeft);
                if ((object)m_bottomCenter != (object)o_bottomCenter) cef_string_t.Copy(m_bottomCenter, bottomCenter);
                if ((object)m_bottomRight != (object)o_bottomRight) cef_string_t.Copy(m_bottomRight, bottomRight);
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to format print headers and footers.
        /// |printInfo| contains platform- specific information about the printer context.
        /// |url| is the URL if the currently printing page,
        /// |title| is the title of the currently printing page,
        /// |currentPage| is the current page number
        /// and |maxPages| is the total number of pages.
        /// Six default header locations are provided by the implementation: top left, top center, top right, bottom left, bottom center and bottom right.
        /// To use one of these default locations just assign a string to the appropriate variable.
        /// To draw the header and footer yourself return true.
        /// Otherwise, populate the approprate variables and return false.
        /// </summary>
        protected virtual bool GetPrintHeaderFooter(CefBrowser browser, CefFrame frame,
            CefPrintInfo printInfo, string url, string title, int currentPage, int maxPages,
            ref string topLeft, 
            ref string topCenter, 
            ref string topRight, 
            ref string bottomLeft, 
            ref string bottomCenter, 
            ref string bottomRight)
        {
            return false;
        }

    }
}
