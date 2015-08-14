namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using Diagnostics;

    public class CefWebLifeSpanHandler : CefLifeSpanHandler
    {
        private readonly CefWebBrowserCore context;

        public CefWebLifeSpanHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override bool OnBeforePopup(CefBrowser parentBrowser, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, string url, ref CefClient client, CefBrowserSettings settings)
        {
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLifeSpanHandler, "OnBeforePopup");
            #endif

            /*
                #if DEBUG
                string message = string.Format(
                    "LifeSpanHandler:OnBeforePopup\n"
                    + "               URL: {0}\n"
                    + "                 X: {1}\n"
                    + "                 Y: {2}\n"
                    + "             Width: {3}\n"
                    + "            Height: {4}\n"
                    + "    MenuBarVisible: {5}\n"
                    + "  StatusBarVisible: {6}\n"
                    + "    ToolBarVisible: {7}\n"
                    + "LocationBarVisible: {8}\n"
                    + " ScrollbarsVisible: {9}\n"
                    + "         Resizable: {10}\n"
                    + "        Fullscreen: {11}\n"
                    + "            Dialog: {12}\n",
                    url,
                    popupFeatures.X.HasValue ? popupFeatures.X.ToString() : "not set",
                    popupFeatures.Y.HasValue ? popupFeatures.Y.ToString() : "not set",
                    popupFeatures.Width.HasValue ? popupFeatures.Width.ToString() : "not set",
                    popupFeatures.Height.HasValue ? popupFeatures.Height.ToString() : "not set",
                    popupFeatures.MenuBarVisible,
                    popupFeatures.StatusBarVisible,
                    popupFeatures.ToolBarVisible,
                    popupFeatures.LocationBarVisible,
                    popupFeatures.ScrollbarsVisible,
                    popupFeatures.Resizable,
                    popupFeatures.Fullscreen,
                    popupFeatures.Dialog
                    );
                #endif
                */

            this.context.OnBeforePopup(parentBrowser, popupFeatures, windowInfo, url, ref client, settings);

            // TODO: create webview here
            // TODO: do not inherit browser settings for devtools
            client = CefWebClientFactory.Default.Create(new CefWebBrowserCore(null, this.context.Settings, url));

            // TODO: create new browsercontext for popup windows

            return false;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLifeSpanHandler, "OnAfterCreated");
            #endif

            this.context.OnAfterCreated(browser);

            // TODO: check context state, it can be already closed
            this.context.Attach(browser);

            /*
            if (!browser.IsPopup)
            {
                if (this.context.IsDisposed)
                {
                    browser.Close();
                    return;
                }

                this.context.browser = browser;
                this.context.browserWindowHandle = browser.WindowHandle;

                this.context.SetStyle(ControlStyles.Opaque, true);

                this.context.ResizeBrowserWindow();

                // FIXME: this is invalid - can't be accessed from another thread
                //if (this.control.Focused)
                //{
                //    this.control.browser.SetFocus(true);
                //}
            }
            */
        }

        protected override bool RunModal(CefBrowser browser)
        {
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLifeSpanHandler, "RunModal");
            #endif

            return false;
        }

        protected override bool DoClose(CefBrowser browser)
        {
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLifeSpanHandler, "DoClose");
            #endif

            return false;
        }

        protected override void OnBeforeClose(CefBrowser browser)
        {
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLifeSpanHandler, "OnBeforeClose");
            #endif

            // TODO: raise Closing event ?
            this.context.Detach();
            browser.Dispose();

            /*
            if (!browser.IsPopup)
            {
                browser.Dispose();

                if (this.context.browser != null)
                {
                    this.context.browser.Dispose();
                    this.context.browser = null;
                    this.context.browserWindowHandle = IntPtr.Zero;
                }

                if (!this.context.IsDisposed)
                {
                    if (this.context.InvokeRequired)
                    {
                        this.context.BeginInvoke(new Action(this.context.DestroyHandle));
                    }
                    else
                    {
                        this.context.DestroyHandle();
                    }
                }
            }
            else
            {
                browser.Dispose();
            }
            */
        }
    }
}
