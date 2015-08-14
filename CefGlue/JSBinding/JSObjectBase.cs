namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.WebBrowser;

    public abstract class JSObjectBase : IDisposable
    {
        private readonly CefWebFrameCore context;
        private CefV8Value obj;

        internal JSObjectBase(CefWebFrameCore context)
        {
            this.context = context;

            // FIXME: for safety we will subscribe here to context event, and when frame context is cleared/disposed
            // then we unbind this object from V8
        }

        ~JSObjectBase()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (this.obj != null)
            {
                this.obj.Dispose();
                this.obj = null;
            }
        }

        public void Bind(CefV8Value obj)
        {
            if (this.obj != null) throw new InvalidOperationException();

            this.obj = obj;
            BindCore();
        }

        protected abstract void BindCore();
    }
}
