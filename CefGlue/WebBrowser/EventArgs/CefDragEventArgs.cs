using System;

namespace CefGlue.WebBrowser
{
    public class CefDragEventArgs : EventArgs
    {
        private readonly CefBrowser browser;
        private readonly CefDragData dragData;
        private readonly CefDragOperations mask;

        public CefDragEventArgs(CefBrowser browser, CefDragData dragData, CefDragOperations mask)
        {
            this.browser = browser;
            this.dragData = dragData;
            this.mask = mask;
        }

        public CefBrowser Browser
        {
            get { return browser; }
        }

        public CefDragData DragData
        {
            get { return dragData; }
        }

        public CefDragOperations Mask
        {
            get { return mask; }
        }
    }
}