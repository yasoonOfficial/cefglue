namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using CefGlue.Diagnostics;

    public class CefWebRequestHandler : CefRequestHandler
    {
        private CefWebBrowserCore context;

        public CefWebRequestHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override bool OnBeforeBrowse(CefBrowser browser, CefFrame frame, CefRequest request, CefHandlerNavType navType, bool isRedirect)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefRequestHandler, "OnBeforeBrowse: Method=[{0}] Url=[{1}] NavType=[{2}] IsRedirect=[{3}]", request.GetMethod(), request.GetURL(), navType, isRedirect);
#endif

            var cancel = this.context.OnNavigating(frame, request, navType, isRedirect);
            if (cancel) return true;

            if (frame.IsMain)
            {
                // browser navigating to a new page
                this.context.ClearFrames();
                this.context.AttachMainFrame(frame);

                this.context.ClearReadyState();
                this.context.SetFrameReadyState(null, FrameReadyState.Navigating);
            }
            else if ((this.context.ReadyOptions & CefReadyOptions.Frames) != 0)
            {
                this.context.SetFrameReadyState(frame.GetName(), FrameReadyState.Navigating);
            }

            return false;
        }

        // TODO: use OnBeforeResourceLoad to OnResourceResponse to detect resource loading, and waiting when all resources will be loaded
        protected override bool OnBeforeResourceLoad(CefBrowser browser, CefRequest request, out string redirectUrl, out CefStreamReader resourceStream, CefResponse response, int loadFlags)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("OnBeforeResourceLoad: {0}", request.GetURL());
            Console.ForegroundColor = prevColor;

            redirectUrl = null;
            resourceStream = null;
            return false;
        }

        protected override void OnResourceResponse(CefBrowser browser, string url, CefResponse response, out CefContentFilter filter)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("OnResourceResponse: {0} ({1})", url, response.GetHeader("Content-Length"));
            Console.ForegroundColor = prevColor;

            response.GetHeaderMap().Append("Access-Control-Allow-Origin", "*");

            filter = null;
            // filter = new CefWebProgressContentFilter();
        }
    }
}
