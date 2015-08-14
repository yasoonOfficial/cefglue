namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;
    using CefGlue.Threading;

    internal partial class CefWebFrameCore : IWebFrame
    {
        public void LoadUrl(string url)
        {
            this.frame.LoadURL(url);
        }

        public void LoadUrl(Uri url)
        {
            this.LoadUrl(url.ToString());
        }

        public void ExecuteJavaScript(string jsCode, string scriptUrl, int startLine)
        {
            this.frame.ExecuteJavaScript(jsCode, scriptUrl, startLine);
        }

        public void VisitDom(CefDomVisitor visitor)
        {
            this.frame.VisitDom(visitor);
        }

        public object InvokeScript(string name, params object[] args)
        {
            object result = null;
            Exception exception = null;

            CefThread.UI.Send((_) =>
            {
                try
                {
                    // FIXME: CEF doesn't report that v8 context created for pages without script blocks so we need explicitedly get v8 context.
                    var context = V8Context ?? this.frame.GetV8Context();

                    result = InvokeScript(this.V8Context, name, args);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }, null);

            if (exception != null) throw exception;
            return result;
        }

        private static object InvokeScript(CefV8Context context, string memberName, params object[] args)
        {
            if (context == null) throw new ArgumentNullException("context");

            if (!context.Enter()) throw new CefException("Failed to enter V8 context.");
            try
            {
                // TODO: this list can be private list of context, 'cause we can invoke only one function at one time
                List<CefV8Value> proxies = new List<CefV8Value>(16);

                // javascript 'this' object
                CefV8Value obj = null;

                CefV8Value target = context.GetGlobal();
                proxies.Add(target);
                if (!memberName.Contains('.'))
                {
                    obj = target;
                    target = obj.GetValue(memberName);
                    proxies.Add(target);
                }
                else
                {
                    foreach (var member in memberName.Split('.'))
                    {
                        obj = target;
                        target = obj.GetValue(member); // TODO: do analysis of target - if it is not an object - throw
                        if (!target.IsObject) throw new CefException("Argument 'memberName' must be member access expression to a function. Invalid object in path.");
                        proxies.Add(target);
                    }
                }
                if (!target.IsFunction) throw new ArgumentException("Argument 'memberName' must be member access expression to a function.");

                CefV8Value[] v8Args;

                if (args.Length == 0) v8Args = null;
                else
                {
                    v8Args = new CefV8Value[args.Length]; // TODO: InvokeScript core can be optimized by prevent recreating arrays
                    for (var i = 0; i < args.Length; i++)
                    {
                        var value = CefConvert.ToV8Value(args[i]);
                        v8Args[i] = value;
                    }
                }

                var v8RetVal = target.ExecuteFunctionWithContext(context, obj, v8Args);

                // force destroing of proxies, to avoid unneccessary GC load (CLR and V8)
                foreach (var proxy in proxies) 
                    proxy.Dispose();

                proxies.Clear();

                // FIXME: not sure if exception CAN be null, this need to be checked
                if (v8RetVal.HasException)
                {
                    var exception = v8RetVal.GetException();
                    throw new JavaScriptException(exception.GetMessage());
                }

                //if (!string.IsNullOrEmpty(exception)) 
                    

                var result = CefConvert.ToObject(v8RetVal);
                v8RetVal.Dispose();
                return result;
            }
            finally
            {
                if (!context.Exit()) throw new CefException("Failed to exit V8 context.");
            }
        }
    }
}
