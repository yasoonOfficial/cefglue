namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    public partial class CefWebBrowserCore : IJSBindingContext
    {
        public void BindJSObject<T>(string name, T obj, JSBindingOptions options) where T : class
        {
            this.JSBindingContext.BindJSObject<T>(name, obj, options);
        }

        public void BindJSObject<T>(string name, T obj) where T : class
        {
            this.JSBindingContext.BindJSObject<T>(name, obj);
        }

        public void BindJSObject(string name, object obj)
        {
            this.JSBindingContext.BindJSObject(name, obj);
        }

        public void BindJSObject(string name, object obj, Type type)
        {
            this.JSBindingContext.BindJSObject(name, obj, type);
        }

        public void BindJSObject(string name, object obj, JSBindingOptions options)
        {
            this.JSBindingContext.BindJSObject(name, obj, options);
        }

        public void BindJSObject(string name, object obj, Type type, JSBindingOptions options)
        {
            this.JSBindingContext.BindJSObject(name, obj, type, options);
        }

        public void BindJSObject<T>(T obj) where T : class
        {
            this.JSBindingContext.BindJSObject<T>(obj);
        }

        public void BindJSObject<T>(T obj, JSBindingOptions options) where T : class
        {
            this.JSBindingContext.BindJSObject<T>(obj, options);
        }

        public void BindJSObject(object obj)
        {
            this.JSBindingContext.BindJSObject(obj);
        }

        public void BindJSObject(object obj, Type type)
        {
            this.JSBindingContext.BindJSObject(obj, type);
        }

        public void BindJSObject(object obj, JSBindingOptions options)
        {
            this.JSBindingContext.BindJSObject(obj, options);
        }

        public void BindJSObject(object obj, Type type, JSBindingOptions options)
        {
            this.JSBindingContext.BindJSObject(obj, type, options);
        }

        public T GetJSObject<T>(string name) where T : class
        {
            // FIXME: this is MainFrame's method
            return this.JSBindingContext.GetJSObject<T>(name);
        }

        public object GetJSObject(string name, Type type)
        {
            // FIXME: this is MainFrame's method
            return this.JSBindingContext.GetJSObject(name, type);
        }
    }
}
