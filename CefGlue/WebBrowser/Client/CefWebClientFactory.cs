namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class CefWebClientFactory
    {
        private static Lazy<CefWebClientFactory> defaultFactory = new Lazy<CefWebClientFactory>(LazyThreadSafetyMode.PublicationOnly);

        public static CefWebClientFactory Default
        {
            get { return defaultFactory.Value; }
        }

        public virtual CefWebClient Create(CefWebBrowserCore context)
        {
            return new CefWebClient(context);
        }
    }
}
