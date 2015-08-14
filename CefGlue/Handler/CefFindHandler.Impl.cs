namespace CefGlue
{
    using System;
    using CefGlue.Interop;
    using Diagnostics;

    unsafe partial class CefFindHandler
    {
        /// <summary>
        /// Called to report find results returned by CefBrowser::Find().
        /// |identifer| is the identifier passed to CefBrowser::Find(), |count|
        /// is the number of matches currently identified, |selectionRect| is the
        /// location of where the match was found (in window coordinates),
        /// |activeMatchOrdinal| is the current position in the search results,
        /// and |finalUpdate| is true if this is the last find notification.
        /// </summary>
        private void on_find_result(cef_find_handler_t* self, cef_browser_t* browser, int identifier, int count, /*const*/ cef_rect_t* selectionRect, int activeMatchOrdinal, int finalUpdate)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);
            var m_selectionRect = CefRect.From(selectionRect);
            var m_finalUpdate = finalUpdate != 0;

            this.OnFindResult(m_browser, identifier, count, m_selectionRect, activeMatchOrdinal, m_finalUpdate);
        }

        /// <summary>
        /// Called to report find results returned by CefBrowser::Find().
        /// |identifer| is the identifier passed to CefBrowser::Find(),
        /// |count| is the number of matches currently identified,
        /// |selectionRect| is the location of where the match was found (in window coordinates),
        /// |activeMatchOrdinal| is the current position in the search results,
        /// and |finalUpdate| is true if this is the last find notification.
        /// </summary>
        protected virtual void OnFindResult(CefBrowser browser, int identifier, int count, CefRect selectionRect, int activeMatchOrdinal, bool finalUpdate)
        {
        }

    }
}
