namespace CefGlue.WebBrowser
{
    public class CefWebDragHandler : CefDragHandler
    {
        private readonly CefWebBrowserCore context;

        public CefWebDragHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override bool OnDragStart(CefBrowser browser, CefDragData dragData, CefDragOperations mask)
        {
            return context.OnDragStart(browser, dragData, mask);
        }

        protected override bool OnDragEnter(CefBrowser browser, CefDragData dragData, CefDragOperations mask)
        {
            return context.OnDragEnter(browser, dragData, mask);
        }
    }
}