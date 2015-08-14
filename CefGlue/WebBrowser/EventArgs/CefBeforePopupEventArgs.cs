using System;

namespace CefGlue.WebBrowser
{
    public class CefBeforePopupEventArgs : EventArgs
    {
        private readonly CefBrowser parentBrowser;
        private readonly CefPopupFeatures popupFeatures;
        private readonly CefWindowInfo windowInfo;
        private readonly CefClient client;
        private readonly CefBrowserSettings settings;
        private readonly string url;

        public CefBeforePopupEventArgs(CefBrowser parentBrowser, CefPopupFeatures popupFeatures, CefWindowInfo windowInfo, string url, ref CefClient client, CefBrowserSettings settings)
        {
            this.parentBrowser = parentBrowser;
            this.popupFeatures = popupFeatures;
            this.windowInfo = windowInfo;
            this.url = url;
            this.client = client;
            this.settings = settings;
        }

        public CefBrowser ParentBrowser
        {
            get { return parentBrowser; }
        }

        public CefPopupFeatures PopupFeatures
        {
            get { return popupFeatures; }
        }

        public CefWindowInfo WindowInfo
        {
            get { return windowInfo; }
        }

        public CefClient Client
        {
            get { return client; }
        }

        public CefBrowserSettings Settings
        {
            get { return settings; }
        }

        public string Url
        {
            get { return url; }
        }
    }
}