namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Diagnostics;
    using CefGlue.JSBinding;

    public class CefWebV8ContextHandler : CefV8ContextHandler
    {
        private readonly CefWebBrowserCore _context;

        public CefWebV8ContextHandler(CefWebBrowserCore context)
        {
            _context = context;
        }

        protected override unsafe void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            if (frame.IsMain)
            {
                _context.MainFrame.BindContext(context);
            }

            var obj = context.GetGlobal();
            Cef.JSBindingContext.BindObjects(obj);
            _context.JSBindingContext.BindObjects(obj);
            obj.Dispose();
        }

        protected override unsafe void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            if (_context.MainFrame != null)
            {
                _context.MainFrame.UnbindV8Context();
            }
        }

        protected override void OnUncaughtException(CefBrowser browser, CefFrame frame, CefV8Context context, CefV8Exception exception, CefV8StackTrace stackTrace)
        {
            _context.OnUncaughtException(browser, frame, context, exception, stackTrace);
        }
    }
}
