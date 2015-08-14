namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // TODO: this interface will be implemented by browser (BrowserContext) and operate on main frame
    // and this is will be implemented by FrameContext and operate on concrete frame
    // how are objects must be bound to one-shot option, and/or for "global" and/or for any document in browser ?
    // i.e. once browser control created we may be want to register object to any document or to loaded document.
    // reslove it via options?

    /// <summary>
    /// Defines methods that provide access to JS binding feature.
    /// </summary>
    public interface IJSBindingContext
    {
        // CLR->JS
        void BindJSObject<T>(string name, T obj, JSBindingOptions options) where T : class;
        void BindJSObject<T>(string name, T obj) where T : class;
        void BindJSObject(string name, object obj);
        void BindJSObject(string name, object obj, Type type);
        void BindJSObject(string name, object obj, JSBindingOptions options);
        void BindJSObject(string name, object obj, Type type, JSBindingOptions options);
        void BindJSObject<T>(T obj) where T : class;
        void BindJSObject<T>(T obj, JSBindingOptions options) where T : class;
        void BindJSObject(object obj);
        void BindJSObject(object obj, Type type);
        void BindJSObject(object obj, JSBindingOptions options);
        void BindJSObject(object obj, Type type, JSBindingOptions options);

        // TODO: UnbindJSObject

        // JS->CLR
        T GetJSObject<T>(string name) where T : class;
        object GetJSObject(string name, Type type);
    }
}
