namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    public partial class CefWebBrowserCore : IWebFrame
    {
        public void LoadUrl(string url)
        {
            this.MainFrame.LoadUrl(url);
        }

        public void LoadUrl(Uri url)
        {
            this.MainFrame.LoadUrl(url);
        }

        public void ExecuteJavaScript(string jsCode, string scriptUrl, int startLine)
        {
            this.MainFrame.ExecuteJavaScript(jsCode, scriptUrl, startLine);
        }

        public void VisitDom(CefDomVisitor visitor)
        {
            this.MainFrame.VisitDom(visitor);
        }

        public object InvokeScript(string name, params object[] args)
        {
            return this.MainFrame.InvokeScript(name, args);
        }
    }
}
