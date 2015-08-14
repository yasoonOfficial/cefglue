namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Threading;

    public partial class CefWebBrowserCore : IWebBrowser
    {
        private bool canGoBack;
        private bool canGoForward;
        private string address;
        private string title;

        public event EventHandler Created;

        public event EventHandler CanGoBackChanged;

        public event EventHandler CanGoForwardChanged;

        public event EventHandler AddressChanged;

        public event EventHandler TitleChanged;

        public event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

        public event EventHandler<CefNavigatingEventArgs> Navigating;

        public event EventHandler<CefNavigatedEventArgs> Navigated;

        public event EventHandler<CefDocumentCompletedEventArgs> DocumentCompleted;

        public event EventHandler<CefProgressChangedEventArgs> ProgressChanged;

        public event EventHandler Ready;

        public event EventHandler<CefBeforePopupEventArgs> BeforePopup;

        public event EventHandler<CefAfterCreatedEventArgs> AfterCreated;

        public event EventHandler<CefDragEventArgs> DragStart;

        public event EventHandler<CefDragEventArgs> DragEnter;

        public event EventHandler<CefShowPopupEventArgs> ShowPopup;

        public event EventHandler<CefUncaughtExceptionEventArgs> UncaughtException;

        public CefReadyOptions ReadyOptions
        {
            get { return this.readyOptions; }
            set { this.readyOptions = value; }
        }

        public int ReadyIdleThreshold
        {
            get { return this.readyIdleThreshold; }
            set {
                if (value < 0) throw new ArgumentOutOfRangeException();
                this.readyIdleThreshold = value;
            }
        }

        public CefBrowserSettings Settings
        {
            get { return this.settings; }
        }

        public string StartUrl
        {
            get { return this.startUrl; }
            set { this.startUrl = value; }
        }

        public bool CanGoBack
        {
            get { return this.canGoBack; }
        }

        public bool CanGoForward
        {
            get { return this.canGoForward; }
        }

        public string Address
        {
            get { return this.address; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public double ZoomLevel
        {
            get
            {
                // FIXME: require valid state (browser will be attached and context is not dropped)
                return this.browser.ZoomLevel;
            }
            set
            {
                // FIXME: require valid state (browser will be attached and context is not dropped)
                this.browser.ZoomLevel = value;
            }
        }


        public void Close()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            this.browser.Close();
        }

        public void GoBack()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            this.browser.GoBack();
        }

        public void GoForward()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            this.browser.GoForward();
        }

        public void Reload()
        {
            this.Reload(false);
        }

        public void Reload(bool ignoreCache)
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            if (ignoreCache)
            {
                this.browser.ReloadIgnoreCache();
            }
            else
            {
                this.browser.Reload();
            }
        }

        public void StopLoad()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            this.browser.StopLoad();
        }

        public void ShowDevTools()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            this.browser.ShowDevTools();
        }

        public void CloseDevTools()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            this.browser.CloseDevTools();
        }

        public IEnumerable<string> GetFrameNames()
        {
            // FIXME: require valid state (browser will be attached and context is not dropped)
            CefStringList frames = null;

            CefThread.UI.Send((_) =>
            {
                frames = this.browser.GetFrameNames();
            }, null);

            return frames.AsEnumerable();
        }
    }
}
