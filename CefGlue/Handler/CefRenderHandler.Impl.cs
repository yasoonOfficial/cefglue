namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefRenderHandler
    {
        /// <summary>
        /// Called to retrieve the view rectangle which is relative to screen
        /// coordinates. Return true if the rectangle was provided.
        /// </summary>
        private int get_view_rect(cef_render_handler_t* self, cef_browser_t* browser, cef_rect_t* rect)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            CefRect m_rect;

            var handled = this.GetViewRect(m_browser, out m_rect);

            if (handled)
            {
                m_rect.To(rect);
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to retrieve the view rectangle which is relative to screen coordinates.
        /// Return true if the rectangle was provided.
        /// </summary>
        protected virtual bool GetViewRect(CefBrowser browser, out CefRect rect)
        {
            rect = new CefRect();
            return false;
        }


        /// <summary>
        /// Called to retrieve the simulated screen rectangle. Return true if the
        /// rectangle was provided.
        /// </summary>
        private int get_screen_rect(cef_render_handler_t* self, cef_browser_t* browser, cef_rect_t* rect)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            CefRect m_rect;

            var handled = this.GetScreenRect(m_browser, out m_rect);

            if (handled)
            {
                m_rect.To(rect);
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to retrieve the simulated screen rectangle.
        /// Return true if the rectangle was provided.
        /// </summary>
        protected virtual bool GetScreenRect(CefBrowser browser, out CefRect rect)
        {
            rect = new CefRect();
            return false;
        }


        /// <summary>
        /// Called to retrieve the translation from view coordinates to actual
        /// screen coordinates. Return true if the screen coordinates were
        /// provided.
        /// </summary>
        private int get_screen_point(cef_render_handler_t* self, cef_browser_t* browser, int viewX, int viewY, int* screenX, int* screenY)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            int m_screenX;
            int m_screenY;

            var handled = this.GetScreenPoint(m_browser, viewX, viewY, out m_screenX, out m_screenY);

            if (handled)
            {
                *screenX = m_screenX;
                *screenY = m_screenY;
            }

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called to retrieve the translation from view coordinates to actual screen coordinates.
        /// Return true if the screen coordinates were provided.
        /// </summary>
        protected virtual bool GetScreenPoint(CefBrowser browser, int viewX, int viewY, out int screenX, out int screenY)
        {
            screenX = 0;
            screenY = 0;
            return false;
        }


        /// <summary>
        /// Called when the browser wants to show or hide the popup widget. The
        /// popup should be shown if |show| is true and hidden if |show| is
        /// false.
        /// </summary>
        private void on_popup_show(cef_render_handler_t* self, cef_browser_t* browser, int show)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_show = show != 0;

            this.OnPopupShow(m_browser, m_show);
        }

        /// <summary>
        /// Called when the browser wants to show or hide the popup widget.
        /// The popup should be shown if |show| is true and hidden if |show| is false.
        /// </summary>
        protected virtual void OnPopupShow(CefBrowser browser, bool show)
        {
        }


        /// <summary>
        /// Called when the browser wants to move or resize the popup widget.
        /// |rect| contains the new location and size.
        /// </summary>
        private void on_popup_size(cef_render_handler_t* self, cef_browser_t* browser, /*const*/ cef_rect_t* rect)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_rect = CefRect.From(rect);

            this.OnPopupSize(m_browser, m_rect);
        }

        /// <summary>
        /// Called when the browser wants to move or resize the popup widget.
        /// |rect| contains the new location and size.
        /// </summary>
        protected virtual void OnPopupSize(CefBrowser browser, CefRect rect)
        {
        }


        /// <summary>
        /// Called when an element should be painted. |type| indicates whether
        /// the element is the view or the popup widget. |buffer| contains the
        /// pixel data for the whole image. |dirtyRects| contains the set of
        /// rectangles that need to be repainted. On Windows |buffer| will be
        /// width*height*4 bytes in size and represents a BGRA image with an
        /// upper-left origin.
        /// </summary>
        private void on_paint(cef_render_handler_t* self, cef_browser_t* browser, cef_paint_element_type_t type, int dirtyRectCount, cef_rect_t* dirtyRects, /*const*/ void* buffer)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_type = (CefPaintElementType)type;
            var m_dirtyRects = new CefRect[dirtyRectCount];
            for (int i = 0; i < m_dirtyRects.Length; i++) 
            {
                m_dirtyRects[i] = CefRect.From(dirtyRects);
                dirtyRects++;
            }
            var m_buffer = (IntPtr)buffer;

            this.OnPaint(m_browser, m_type, m_dirtyRects, m_buffer);
        }

        /// <summary>
        /// Called when an element should be painted.
        /// |type| indicates whether the element is the view or the popup widget.
        /// |buffer| contains the pixel data for the whole image.
        /// |dirtyRects| indicates the portions of the image that have been repainted.
        /// On Windows |buffer| will be width*height*4 bytes in size and represents a BGRA image with an upper-left origin.
        /// </summary>
        protected virtual void OnPaint(CefBrowser browser, CefPaintElementType type, CefRect[] dirtyRect, IntPtr buffer)
        {
        }


        /// <summary>
        /// Called when the browser window's cursor has changed.
        /// </summary>
        private void on_cursor_change(cef_render_handler_t* self, cef_browser_t* browser, cef_cursor_handle* cursor)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_cursor = (IntPtr)cursor;

            this.OnCursorChange(m_browser, m_cursor);
        }

        /// <summary>
        /// Called when the browser window's cursor has changed.
        /// </summary>
        protected virtual void OnCursorChange(CefBrowser browser, IntPtr cursorHandle)
        {
        }

    }
}
