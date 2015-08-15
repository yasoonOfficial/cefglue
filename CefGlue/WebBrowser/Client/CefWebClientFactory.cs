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

		private static CefWebClientFactory _default;
        public static CefWebClientFactory Default
        {
            get 
			{
				if (_default == null)
					_default = defaultFactory.Value;
				
				return _default;
			}
			set
			{
				_default = value;
			}
        }

        public virtual CefWebClient Create(CefWebBrowserCore context)
        {
            return new CefWebClient(context);
        }
    }
}
