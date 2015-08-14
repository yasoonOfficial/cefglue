namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefFrame
    {
        /// <summary>
        /// Execute undo in this frame.
        /// </summary>
        public void Undo()
        {
            cef_frame_t.invoke_undo(this.ptr);
        }

        /// <summary>
        /// Execute redo in this frame.
        /// </summary>
        public void Redo()
        {
            cef_frame_t.invoke_redo(this.ptr);
        }

        /// <summary>
        /// Execute cut in this frame.
        /// </summary>
        public void Cut()
        {
            cef_frame_t.invoke_cut(this.ptr);
        }

        /// <summary>
        /// Execute copy in this frame.
        /// </summary>
        public void Copy()
        {
            cef_frame_t.invoke_copy(this.ptr);
        }

        /// <summary>
        /// Execute paste in this frame.
        /// </summary>
        public void Paste()
        {
            cef_frame_t.invoke_paste(this.ptr);
        }

        /// <summary>
        /// Execute delete in this frame.
        /// </summary>
        public void Delete()
        {
            cef_frame_t.invoke_del(this.ptr);
        }

        /// <summary>
        /// Execute select all in this frame.
        /// </summary>
        public void SelectAll()
        {
            cef_frame_t.invoke_select_all(this.ptr);
        }

        /// <summary>
        /// Execute printing in the this frame.
        /// The user will be prompted with the print dialog appropriate to the operating system.
        /// </summary>
        public void Print()
        {
            cef_frame_t.invoke_print(this.ptr);
        }

        /// <summary>
        /// Save this frame's HTML source to a temporary file and open it in the default text viewing application.
        /// </summary>
        public void ViewSource()
        {
            cef_frame_t.invoke_view_source(this.ptr);
        }

        /// <summary>
        /// Returns this frame's HTML source as a string.
        /// This method should only be called on the UI thread.
        /// </summary>
        public string GetSource()
        {
            var nResult = cef_frame_t.invoke_get_source(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns this frame's display text as a string.
        /// This method should only be called on the UI thread.
        /// </summary>
        public string GetText()
        {
            var nResult = cef_frame_t.invoke_get_text(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Load the request represented by the |request| object.
        /// </summary>
        public void LoadRequest(CefRequest request)
        {
            cef_frame_t.invoke_load_request(this.ptr, request.GetNativePointerAndAddRef());
        }

        /// <summary>
        /// Load the specified |url|.
        /// </summary>
        public void LoadURL(string url)
        {
            fixed (char* str = url)
            {
                cef_string_t n_url = new cef_string_t(str, url != null ? url.Length : 0);
                cef_frame_t.invoke_load_url(this.ptr, &n_url);
            }
        }

        /// <summary>
        /// Load the contents of |string| with the optional dummy target |url|.
        /// </summary>
        public void LoadString(string content, string url)
        {
            fixed (char* content_str = content)
            fixed (char* url_str = url)
            {
                var n_content = new cef_string_t(content_str, content != null ? content.Length : 0);
                var n_url = new cef_string_t(url_str, url != null ? url.Length : 0);

                cef_frame_t.invoke_load_string(this.ptr, &n_content, &n_url);
            }
        }

        /// <summary>
        /// Load the contents of |stream| with the optional dummy target |url|.
        /// </summary>
        public void LoadStream(CefStreamReader stream, string url)
        {
            fixed (char* url_str = url)
            {
                var n_url = new cef_string_t(url_str, url != null ? url.Length : 0);

                cef_frame_t.invoke_load_stream(this.ptr, stream.GetNativePointerAndAddRef(), &n_url);
            }
        }

        /// <summary>
        /// Execute a string of JavaScript code in this frame.
        /// The |script_url| parameter is the URL where the script in question can be found, if any.
        /// The renderer may request this URL to show the developer the source of the error.
        /// The |start_line| parameter is the base line number to use for error reporting.
        /// </summary>
        public void ExecuteJavaScript(string jsCode, string scriptUrl, int startLine)
        {
            fixed (char* jsCode_str = jsCode)
            fixed (char* scriptUrl_str = scriptUrl)
            {
                var n_jsCode = new cef_string_t(jsCode_str, jsCode != null ? jsCode.Length : 0);
                var n_scriptUrl = new cef_string_t(scriptUrl_str, scriptUrl != null ? scriptUrl.Length : 0);

                cef_frame_t.invoke_execute_java_script(this.ptr, &n_jsCode, &n_scriptUrl, startLine);
            }
        }

        /// <summary>
        /// Returns true if this is the main frame.
        /// </summary>
        public bool IsMain
        {
            get
            {
                return cef_frame_t.invoke_is_main(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if this is the focused frame. This method should only be
        /// called on the UI thread.
        /// </summary>
        public bool IsFocused
        {
            get
            {
                return cef_frame_t.invoke_is_focused(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns this frame's name.
        /// </summary>
        public string GetName()
        {
            var nResult = cef_frame_t.invoke_get_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the URL currently loaded in this frame.
        /// This method should only be called on the UI thread.
        /// </summary>
        public string GetURL()
        {
            var nResult = cef_frame_t.invoke_get_url(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the browser that this frame belongs to.
        /// </summary>
        public CefBrowser GetBrowser()
        {
            return CefBrowser.From(
                cef_frame_t.invoke_get_browser(this.ptr)
                );
        }

        /// <summary>
        /// Visit the DOM document.
        /// </summary>
        public void VisitDom(CefDomVisitor visitor)
        {
            cef_frame_t.invoke_visit_dom(this.ptr, visitor.GetNativePointerAndAddRef());
        }

        /// <summary>
        /// Get the V8 context associated with the frame.
        /// This method should only be called on the UI thread.
        /// </summary>
        public CefV8Context GetV8Context()
        {
            // TODO: check that we are on UI thread in debug mode?
            return CefV8Context.FromOrDefault(
                cef_frame_t.invoke_get_v8context(this.ptr)
                );
        }

    }
}
