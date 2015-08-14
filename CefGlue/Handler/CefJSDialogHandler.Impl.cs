namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefJSDialogHandler
    {
        /// <summary>
        /// Called  to run a JavaScript alert message. Return false to display
        /// the default alert or true if you displayed a custom alert.
        /// </summary>
        private int on_jsalert(cef_jsdialog_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, /*const*/ cef_string_t* message)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_message = cef_string_t.ToString(message);

            var handled = this.OnJSAlert(m_browser, m_frame, m_message);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to run a JavaScript alert message.
        /// Return false to display the default alert or true if you displayed a custom alert.
        /// </summary>
        protected virtual bool OnJSAlert(CefBrowser browser, CefFrame frame, string message)
        {
            return false;
        }

        /// <summary>
        /// Called to run a JavaScript confirm request. Return false to display
        /// the default alert or true if you displayed a custom alert. If you
        /// handled the alert set |retval| to true if the user accepted the
        /// confirmation.
        /// </summary>
        private int on_jsconfirm(cef_jsdialog_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, /*const*/ cef_string_t* message, int* retval)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_message = cef_string_t.ToString(message);
            bool m_retval;

            var handled = this.OnJSConfirm(m_browser, m_frame, m_message, out m_retval);

            if (handled)
            {
                *retval = m_retval ? 1 : 0;
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to run a JavaScript confirm request.
        /// Return false to display the default alert or true if you displayed a custom alert.
        /// If you handled the alert set |retval| to true if the user accepted the confirmation.
        /// </summary>
        protected virtual bool OnJSConfirm(CefBrowser browser, CefFrame frame, string message, out bool retval)
        {
            retval = false;
            return false;
        }


        /// <summary>
        /// Called to run a JavaScript prompt request. Return false to display
        /// the default prompt or true if you displayed a custom prompt. If you
        /// handled the prompt set |retval| to true if the user accepted the
        /// prompt and request and |result| to the resulting value.
        /// </summary>
        private int on_jsprompt(cef_jsdialog_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, /*const*/ cef_string_t* message, /*const*/ cef_string_t* defaultValue, int* retval, cef_string_t* result)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_frame = CefFrame.From(frame);
            var m_message = cef_string_t.ToString(message);
            var m_defaultValue = cef_string_t.ToString(defaultValue);
            bool m_retval;
            string m_result;

            var handled = this.OnJSPrompt(m_browser, m_frame, m_message, m_defaultValue, out m_retval, out m_result);

            if (handled)
            {
                *retval = m_retval ? 1 : 0;
                cef_string_t.Copy(m_result, result);
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to run a JavaScript prompt request.
        /// Return false to display the default prompt or true if you displayed a custom prompt.
        /// If you handled the prompt set |retval| to true if the user accepted the prompt and request and |result| to the resulting value.
        /// </summary>
        protected virtual bool OnJSPrompt(CefBrowser browser, CefFrame frame, string message, string defaultValue, out bool retval, out string result)
        {
            retval = false;
            result = null;
            return false;
        }

    }
}
