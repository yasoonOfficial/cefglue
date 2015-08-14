namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefFocusHandler
    {
        /// <summary>
        /// Called when the browser component is about to loose focus. For
        /// instance, if focus was on the last HTML element and the user pressed
        /// the TAB key. |next| will be true if the browser is giving focus to
        /// the next component and false if the browser is giving focus to the
        /// previous component.
        /// </summary>
        private void on_take_focus(cef_focus_handler_t* self, cef_browser_t* browser, int next)
        {
            ThrowIfObjectDisposed();

            var m_browser = CefBrowser.From(browser);

            this.OnTakeFocus(m_browser, next != 0);
        }

        /// <summary>
        /// Called when the browser component is about to loose focus.
        /// For instance, if focus was on the last HTML element and the user pressed the TAB key.
        /// |next| will be true if the browser is giving focus to the next component and false if the browser is giving focus to the previous component.
        /// </summary>
        protected virtual void OnTakeFocus(CefBrowser browser, bool next)
        {
        }


        /// <summary>
        /// Called when the browser component is requesting focus. |source| indicates
        /// where the focus request is originating from. Return false to allow the
        /// focus to be set or true to cancel setting the focus.
        /// </summary>
        private int on_set_focus(cef_focus_handler_t* self, cef_browser_t* browser, cef_handler_focus_source_t source)
        {
            ThrowIfObjectDisposed();

            var mBrowser = CefBrowser.From(browser);
            var handled = this.OnSetFocus(mBrowser, (CefHandlerFocusSource)source);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called when the browser component is requesting focus.
        /// |source| indicates where the focus request is originating from.
        /// Return false to allow the focus to be set or true to cancel setting the focus.
        /// </summary>
        protected virtual bool OnSetFocus(CefBrowser browser, CefHandlerFocusSource source)
        {
            return false;
        }

        /// <summary>
        /// Called when a new node in the the browser gets focus. The |node|
        /// value may be empty if no specific node has gained focus. The node
        /// object passed to this method represents a snapshot of the DOM at the
        /// time this method is executed. DOM objects are only valid for the
        /// scope of this method. Do not keep references to or attempt to access
        /// any DOM objects outside the scope of this method.
        /// </summary>
        private void on_focused_node_changed(cef_focus_handler_t* self, cef_browser_t* browser, cef_frame_t* frame, cef_domnode_t* node)
        {
            ThrowIfObjectDisposed();

            var mBrowser = CefBrowser.From(browser);
            var mFrame = CefFrame.From(frame);
            var mNode = CefDomNode.FromOrDefault(node);

            // TODO: DOM nodes context
            this.OnFocusedNodeChanged(mBrowser, mFrame, mNode);

            if (mNode != null) mNode.Dispose();
        }

        /// <summary>
        /// Called when a new node in the the browser gets focus.
        /// The |node| value may be empty if no specific node has gained focus.
        /// The node object passed to this method represents a snapshot of the DOM at the time this method is executed.
        /// DOM objects are only valid for the scope of this method.
        /// Do not keep references to or attempt to access any DOM objects outside the scope of this method.
        /// </summary>
        protected virtual void OnFocusedNodeChanged(CefBrowser browser, CefFrame frame, CefDomNode node)
        {
        }

    }
}
