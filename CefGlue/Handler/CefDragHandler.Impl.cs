namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDragHandler
    {
        /// <summary>
        /// Called when the browser window initiates a drag event. |dragData|
        /// contains the drag event data and |mask| represents the type of drag
        /// operation. Return false for default drag handling behavior or true to
        /// cancel the drag event.
        /// </summary>
        private int on_drag_start(cef_drag_handler_t* self, cef_browser_t* browser, cef_drag_data_t* dragData, cef_drag_operations_mask_t mask)
        {
            ThrowIfObjectDisposed();

            var mBrowser = CefBrowser.From(browser);
            var mDragData = CefDragData.From(dragData);

            var handled = this.OnDragStart(mBrowser, mDragData, (CefDragOperations)mask);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called when the browser window initiates a drag event.
        /// |dragData| contains the drag event data and |mask| represents the type of drag
        /// operation.
        /// Return false for default drag handling behavior 
        /// or true to cancel the drag event.
        /// </summary>
        [CLSCompliant(false)]
        protected virtual bool OnDragStart(CefBrowser browser, CefDragData dragData, CefDragOperations mask)
        {
            return false;
        }


        /// <summary>
        /// Called when an external drag event enters the browser window.
        /// |dragData| contains the drag event data and |mask| represents the
        /// type of drag operation. Return false for default drag handling
        /// behavior or true to cancel the drag event.
        /// </summary>
        private int on_drag_enter(cef_drag_handler_t* self, cef_browser_t* browser, cef_drag_data_t* dragData, cef_drag_operations_mask_t mask)
        {
            ThrowIfObjectDisposed();

            var mBrowser = CefBrowser.From(browser);
            var mDragData = CefDragData.From(dragData);

            var handled = this.OnDragEnter(mBrowser, mDragData, (CefDragOperations)mask);

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Called when an external drag event enters the browser window.
        /// |dragData| contains the drag event data 
        /// and |mask| represents the type of drag operation.
        /// Return false for default drag handling behavior
        /// or true to cancel the drag event.
        /// </summary>
        [CLSCompliant(false)]
        protected virtual bool OnDragEnter(CefBrowser browser, CefDragData dragData, CefDragOperations mask)
        {
            return false;
        }
    }
}
