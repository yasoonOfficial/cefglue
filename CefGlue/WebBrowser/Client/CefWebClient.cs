namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;

    public class CefWebClient : CefClient
    {
        private readonly CefWebBrowserCore context;

        private readonly CefLifeSpanHandler lifeSpanHandler;
        private readonly CefLoadHandler loadHandler;
        private readonly CefRequestHandler requestHandler;
        private readonly CefDisplayHandler displayHandler;
        private readonly CefFocusHandler focusHandler;
        private readonly CefKeyboardHandler keyboardHandler;
        private readonly CefMenuHandler menuHandler;
        private readonly CefPrintHandler printHandler;
        private readonly CefFindHandler findHandler;
        private readonly CefJSDialogHandler jsDialogHandler;
        private readonly CefV8ContextHandler v8ContextHandler;
        private readonly CefRenderHandler renderHandler;
        private readonly CefDragHandler dragHandler;

        public CefWebClient(CefWebBrowserCore context)
        {
            this.context = context;
            this.lifeSpanHandler = new CefWebLifeSpanHandler(context);
            this.loadHandler = new CefWebLoadHandler(context);
            this.requestHandler = new CefWebRequestHandler(context);
            this.displayHandler = new CefWebDisplayHandler(context);
            this.focusHandler = null; // new CefWebFocusHandler(context);
            this.keyboardHandler = new CefWebKeyboardHandler(context);
            this.menuHandler = null; // new CefWebMenuHandler(context);
            this.printHandler = null; // new CefWebPrintHandler(context);
            this.findHandler = null; // new CefWebFindHandler(context);
            this.jsDialogHandler = new CefWebJSDialogHandler(context);
            this.v8ContextHandler = new CefWebV8ContextHandler(context);
            this.renderHandler = null; // new CefWebRenderHandler(context);
            this.dragHandler = null; // new CefWebDragHandler(context);
        }

        protected override CefLifeSpanHandler GetLifeSpanHandler()
        {
            return this.lifeSpanHandler;
        }

        protected override CefLoadHandler GetLoadHandler()
        {
            return this.loadHandler;
        }

        protected override CefRequestHandler GetRequestHandler()
        {
            return this.requestHandler;
        }

        protected override CefDisplayHandler GetDisplayHandler()
        {
            return this.displayHandler;
        }

        protected override CefFocusHandler GetFocusHandler()
        {
            return this.focusHandler;
        }

        protected override CefKeyboardHandler GetKeyboardHandler()
        {
            return this.keyboardHandler;
        }

        protected override CefMenuHandler GetMenuHandler()
        {
            return this.menuHandler;
        }

        protected override CefPrintHandler GetPrintHandler()
        {
            return this.printHandler;
        }

        protected override CefFindHandler GetFindHandler()
        {
            return this.findHandler;
        }

        protected override CefJSDialogHandler GetJSDialogHandler()
        {
            return this.jsDialogHandler;
        }

        protected override CefV8ContextHandler GetV8ContextHandler()
        {
            return this.v8ContextHandler;
        }

        protected override CefRenderHandler GetRenderHandler()
        {
            return this.renderHandler;
        }

        protected override CefDragHandler GetDragHandler()
        {
            return this.dragHandler;
        }
    }
}
