namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    internal sealed partial class CefWebFrameCore : IDisposable
    {
        private readonly CefWebBrowserCore context;

        private CefFrame frame;
        private CefV8Context v8Context;

        public CefWebFrameCore(CefWebBrowserCore context, CefFrame frame)
        {
            this.context = context;
            this.frame = frame;
        }

        ~CefWebFrameCore()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (this.frame != null)
            {
                this.frame.Dispose();
                this.frame = null;
            }

            UnbindV8Context();
        }

        internal CefFrame CefFrame
        {
            get { return this.frame; }
        }

        [Obsolete("Remove it method, it is no more required.")]
        public void BindCurrentV8Context()
        {
            if (this.v8Context != null) throw new InvalidOperationException();

            var context = CefV8Context.GetCurrentContext();
            if (context == null) throw new InvalidOperationException();
            this.v8Context = context;
        }

        public void BindContext(CefV8Context v8Context)
        {
            if (this.v8Context != null)
                throw new InvalidOperationException();

            if (v8Context == null)
                throw new ArgumentNullException("v8Context");

            this.v8Context = v8Context;
        }

        public void UnbindV8Context()
        {
            if (this.v8Context != null)
            {
                this.v8Context.Dispose();
                this.v8Context = null;
            }
        }

        public CefV8Context V8Context
        {
            get
            {
                // TODO: CefV8Context can be obtained lazily if CEF Issue#344 will be resolved: http://code.google.com/p/chromiumembedded/issues/detail?id=344.
                return this.v8Context;
            }
        }

        private JSBindingContext JSBindingContext
        {
            get { return this.context.JSBindingContext; }
        }
    }
}
