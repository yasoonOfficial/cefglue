namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using CefGlue.JSBinding;
    using CefGlue.Threading;

    // TODO: what namespace naming for Interop/Core/WebBrowser
    // TODO: move it to Core namespace?
    // TODO: rename it to CefWebBrowserCore

    [JSObject(JSBindingOptions.Frames)]
    public sealed partial class CefWebBrowserCore
    {
        private readonly object owner;
        private readonly CefBrowserSettings settings;
        private string startUrl;
        private CefWebClient client;
        private CefBrowser browser;
        private IntPtr windowHandle;

        private JSBindingContext jsBindingContext;

        private CefWebFrameCore mainFrame;

        private CefReadyOptions readyOptions;
        private int readyIdleThreshold;

        public CefWebBrowserCore(object owner, CefBrowserSettings settings, string startUrl)
        {
            this.owner = owner;
            this.settings = settings;
            this.startUrl = startUrl;
            this.jsBindingContext = new JSBindingContext(this);
            this.jsBindingContext.BindJSObject("cefGlue._browser", this);

            this.readyOptions = CefReadyOptions.None;
        }

        // TODO: browser will have some states
        // beforecreated, created, loaded, etc...
        // many functionality depends from this state

        public JSBindingContext JSBindingContext
        {
            get { return this.jsBindingContext; }
        }

        // TODO: Use CefWindowHandle instead of IntPtr
        public IntPtr WindowHandle
        {
            get { return this.windowHandle; }
        }

        public void Create(CefWindowInfo windowInfo)
        {
            if (this.client == null)
            {
                this.client = CefWebClientFactory.Default.Create(this);
            }

            CefBrowser.Create(windowInfo, this.client, this.StartUrl, this.settings);
        }

        internal void Attach(CefBrowser browser)
        {
            if (this.browser != null)
            {
                throw new InvalidOperationException("Browser already attached.");
            }

            this.browser = browser;
            this.windowHandle = browser.WindowHandle;

            this.ClearFrames();
            this.AttachMainFrame(browser.GetMainFrame());

            this.OnCreated();
        }

        internal void Detach()
        {
            this.ClearFrames();
            if (this.browser != null)
            {
                this.browser.Dispose();
                this.browser = null;
            }
            this.windowHandle = IntPtr.Zero;
        }

        internal void ClearFrames()
        {
            ClearMainFrame();
            // ...
        }

        internal void AttachMainFrame(CefFrame frame)
        {
            ClearMainFrame();
            this.mainFrame = new CefWebFrameCore(this, frame);
        }

        internal CefWebFrameCore MainFrame
        {
            get { return this.mainFrame; }
        }

        private void ClearMainFrame()
        {
            if (this.mainFrame != null)
            {
                this.mainFrame.Dispose();
                this.mainFrame = null;
            }
        }

        internal void OnCreated()
        {
            var handler = this.Created;
            if (handler != null)
            {
                handler(this.owner, EventArgs.Empty);
            }
        }

        public void OnNavStateChanged(bool canGoBack, bool canGoForward)
        {
            var prevCanGoBack = this.canGoBack;
            var prevCanGoForward = this.canGoForward;

            if (prevCanGoBack != canGoBack || prevCanGoForward != canGoForward)
            {
                this.canGoBack = canGoBack;
                this.canGoForward = canGoForward;

                if (prevCanGoBack != canGoBack)
                {
                    var handler = this.CanGoBackChanged;
                    if (handler != null)
                    {
                        handler(this.owner, EventArgs.Empty);
                    }
                }

                if (prevCanGoForward != canGoForward)
                {
                    var handler = this.CanGoForwardChanged;
                    if (handler != null)
                    {
                        handler(this.owner, EventArgs.Empty);
                    }
                }
            }
        }

        public void OnAddressChanged(string url)
        {
            if (this.address != url)
            {
                this.address = url;

                var handler = this.AddressChanged;
                if (handler != null)
                {
                    handler(this.owner, EventArgs.Empty);
                }
            }
        }

        public void OnTitleChanged(string title)
        {
            if (this.title != title)
            {
                this.title = title;

                var handler = this.TitleChanged;
                if (handler != null)
                {
                    handler(this.owner, EventArgs.Empty);
                }
            }
        }

        public void OnConsoleMessage(ConsoleMessageEventArgs e)
        {
            var handler = this.ConsoleMessage;
            if (handler != null)
            {
                handler(this.owner, e);
            }
        }

        internal bool OnNavigating(CefFrame frame, CefRequest request, CefHandlerNavType navType, bool isRedirect)
        {
            var handler = this.Navigating;
            if (handler != null)
            {
                var e = new CefNavigatingEventArgs(frame, request, navType, isRedirect);
                handler(this.owner, e);
                return e.Cancel;
            }
            return false;
        }

        internal void OnNavigated(CefFrame frame)
        {
            var handler = this.Navigated;
            if (handler != null)
            {
                var e = new CefNavigatedEventArgs(frame);
                handler(this.owner, e);
            }
        }

        internal void OnDocumentCompleted(CefFrame frame, int httpStatusCode)
        {
            var handler = this.DocumentCompleted;
            if (handler != null)
            {
                var e = new CefDocumentCompletedEventArgs(frame, httpStatusCode);
                handler(this.owner, e);
            }
        }

        public void OnProgressChanged()
        {
            // TODO: ProgressChanged event
            /*
            var handler = this.ProgressChanged;
            if (handler != null)
            {
                handler(this.owner, this.CreateProgressChangedEventArgs());
            }
            */
        }

        public void OnReady()
        {
            var handler = this.Ready;
            if (handler != null)
            {
                handler(this.owner, EventArgs.Empty);
            }
        }

        public void OnBeforePopup(CefBrowser parentBrowser, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, string url, ref CefClient client, CefBrowserSettings settings)
        {
            var handler = this.BeforePopup;
            if (handler != null)
            {
                handler(this.owner, new CefBeforePopupEventArgs(parentBrowser, popupFeatures, windowInfo, url, ref client, settings));
            }
        }

        public void OnAfterCreated(CefBrowser browser)
        {
            var handler = this.AfterCreated;
            if (handler != null)
            {
                handler(this.owner, new CefAfterCreatedEventArgs(browser));
            }
        }

        public bool OnDragStart(CefBrowser browser, CefDragData dragData, CefDragOperations mask)        
        {
            var handler = this.DragStart;
            if (handler != null)
            {
                handler(this.owner, new CefDragEventArgs(browser, dragData, mask));
            }

            return false;
        }

        public bool OnDragEnter(CefBrowser browser, CefDragData dragData, CefDragOperations mask)
        {
            var handler = this.DragEnter;
            if (handler != null)
            {
                handler(this.owner, new CefDragEventArgs(browser, dragData, mask));
            }

            return false;
        }

        public void OnShowPopup(CefBrowser browser, bool show)
        {
            var handler = this.ShowPopup;
            if (handler != null)
            {
                handler(this.owner, new CefShowPopupEventArgs(browser, show));
            }
        }

        internal void OnUncaughtException(CefBrowser browser, CefFrame frame, CefV8Context context, CefV8Exception exception, CefV8StackTrace stackTrace)
        {
            var handler = this.UncaughtException;
            if (handler != null)
            {
                handler(this, new CefUncaughtExceptionEventArgs(browser, frame, context, exception, stackTrace));
            }
        }
    }
}
