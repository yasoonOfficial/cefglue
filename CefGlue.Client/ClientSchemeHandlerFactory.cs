namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue;

    internal sealed class ClientSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        protected override CefSchemeHandler Create(CefBrowser browser, string schemeName, CefRequest request)
        {
            return new ClientSchemeHandler();
        }
    }
}
