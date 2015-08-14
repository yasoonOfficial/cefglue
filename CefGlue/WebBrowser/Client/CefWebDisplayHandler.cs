namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using Diagnostics;

    public class CefWebDisplayHandler : CefDisplayHandler
    {
        private readonly CefWebBrowserCore context;
        // private Dictionary<CefHandlerStatusType, string> statusMessages = new Dictionary<CefHandlerStatusType, string>();

        public CefWebDisplayHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override void OnNavStateChange(CefBrowser browser, bool canGoBack, bool canGoForward)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "OnNavStateChange: CanGoBack=[{0}] CanGoForward=[{1}]", canGoBack, canGoForward);
#endif
            this.context.OnNavStateChanged(canGoBack, canGoForward);
        }

        protected override void OnAddressChange(CefBrowser browser, CefFrame frame, string url)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "OnAddressChange: URL=[{0}] Frame.IsMain=[{1}]", url, frame.IsMain);
#endif
            if (frame.IsMain)
            {
                this.context.OnAddressChanged(url);
            }
        }

        protected override void OnTitleChange(CefBrowser browser, string title)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "OnTitleChange: Title=[{0}]", title);
#endif

            this.context.OnTitleChanged(title);
        }

        protected override bool OnTooltip(CefBrowser browser, ref string text)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "OnTooltip: Text=[{0}]", text);
#endif

            return false;
        }

        protected override void OnStatusMessage(CefBrowser browser, string value, CefHandlerStatusType type)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "OnStatusMessage: Type=[{0}] Value=[{1}]", type, value);
#endif

            /*
            // TODO: popups support...

            if (value.Length == 0)
            {
                this.statusMessages.Remove(type);
                GetMostImportantStatusMessage(this.statusMessages, out type, out value);
            }
            else
            {
                this.statusMessages[type] = value;
            }

            this.context.PostStatusMessage(new StatusMessageEventArgs(type, value ?? ""));
            */
        }

        protected override bool OnConsoleMessage(CefBrowser browser, string message, string source, int line)
        {
#if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "OnConsoleMessage: Message=[{0}] Source=[{1}] Line=[{2}]", message, source, line);
#endif
            // FIXME: check context state? if (this.context.IsDisposed) return false;

            this.context.OnConsoleMessage(new ConsoleMessageEventArgs(message, source, line));

            return true;
        }

        /*
        private static void GetMostImportantStatusMessage(Dictionary<CefHandlerStatusType, string> messages, out CefHandlerStatusType type, out string value)
        {
            type = CefHandlerStatusType.MouseOverUrl;
            if (messages.TryGetValue(type, out value)) return;

            type = CefHandlerStatusType.KeyboardFocusUrl;
            if (messages.TryGetValue(type, out value)) return;

            type = CefHandlerStatusType.Text;
            if (messages.TryGetValue(type, out value)) return;
        }
        */
    }
}
