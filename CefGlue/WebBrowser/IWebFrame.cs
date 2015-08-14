namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    public interface IWebFrame : IJSBindingContext
    {
        void LoadUrl(string url);

        void LoadUrl(Uri url);

        void ExecuteJavaScript(string jsCode, string scriptUrl, int startLine);

        /// <summary>
        /// Visit the DOM document.
        /// </summary>
        void VisitDom(CefDomVisitor visitor);
        
        object InvokeScript(string name, params object[] args);
    }
}
