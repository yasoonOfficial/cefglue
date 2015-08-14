namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.CompilerServices;

    public sealed class JSBindingContext : IJSBindingContext
    {
        public const JSBindingOptions DefaultOptions = JSBindingOptions.Public;

        private readonly object owner;
        private Dictionary<string, JSObjectDef> objects;

        public JSBindingContext(object owner)
        {
            this.owner = owner;
            this.objects = new Dictionary<string, JSObjectDef>();
        }

        public void BindJSObject<T>(string name, T obj, JSBindingOptions options) where T : class
        {
            this.BindJSObject(name, obj, typeof(T), options);
        }

        public void BindJSObject<T>(string name, T obj) where T : class
        {
            this.BindJSObject(name, obj, typeof(T), DefaultOptions);
        }

        public void BindJSObject(string name, object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            this.BindJSObject(name, obj, obj.GetType(), DefaultOptions);
        }

        public void BindJSObject(string name, object obj, Type type)
        {
            this.BindJSObject(name, obj, type, DefaultOptions);
        }

        public void BindJSObject(string name, object obj, JSBindingOptions options)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            this.BindJSObject(name, obj, obj.GetType(), DefaultOptions);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void BindJSObject(string name, object obj, Type type, JSBindingOptions options)
        {
            if (this.owner != null && (options & JSBindingOptions.Extension) != 0)
            {
                throw new NotSupportedException("Extension can be bound only on global context. Use Cef.JSBinding instead.");
            }

            var def = new JSObjectDef(obj, type, name, options);

            if (this.objects.ContainsKey(def.MemberName))
            {
                throw new JSBindingException(string.Format(
                    "JSObject with name '{0}' already registered.", name
                    ));
            }

            // force binder creation
            var binder = ScriptableObjectBinder.Get(def.TargetType, options);

            if (def.Extension)
            {
                // Register V8 extension
                var javaScriptCode = binder.CreateJavaScriptExtension(def.MemberName);
                var handler = new ScriptableObjectV8Handler(binder, def.Target, true);

                if (!Cef.RegisterExtension(def.ExtensionName, javaScriptCode, (CefV8Handler)handler))
                {
                    throw new InvalidOperationException("Cef.RegisterExtension failed.");
                }
            }

            this.objects.Add(def.MemberName, def);
        }

        public void BindJSObject<T>(T obj) where T : class
        {
            this.BindJSObject(obj, typeof(T));
        }

        public void BindJSObject<T>(T obj, JSBindingOptions options) where T : class
        {
            this.BindJSObject(obj, typeof(T), options);
        }

        public void BindJSObject(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            this.BindJSObject(obj, obj.GetType());
        }

        public void BindJSObject(object obj, Type type)
        {
            this.BindJSObject(obj, type, DefaultOptions);
        }

        public void BindJSObject(object obj, JSBindingOptions options)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            this.BindJSObject(obj, obj.GetType(), options);
        }

        public void BindJSObject(object obj, Type type, JSBindingOptions options)
        {
            this.BindJSObject(GetMemberNameForType(type), obj, type, options);
        }

        public T GetJSObject<T>(string name) where T : class
        {
            return (T)this.GetJSObject(name, typeof(T));
        }

        public object GetJSObject(string name, Type type)
        {
            if (this.owner == null) throw new NotSupportedException();

            throw new NotImplementedException();
        }

        public void BindObjects(CefV8Value target)
        {
            foreach (var def in this.objects.Values.Where(_ => !_.Extension))
            {
                var binder = ScriptableObjectBinder.Get(def.TargetType, def.Options);

                List<CefV8Value> proxies = null;
                var name = def.MemberName;
                var obj = target;

                if (name.Contains('.'))
                {
                    proxies = new List<CefV8Value>();

                    var parts = name.Split('.');
                    for (var i = 0; i < parts.Length - 1; i++)
                    {
                        name = parts[i];

                        var nsObj = obj.GetValue(name);
                        proxies.Add(nsObj);

                        if (nsObj.IsUndefined)
                        {
                            var newObj = CefV8Value.CreateObject();
                            proxies.Add(newObj);
                            obj.SetValue(name, newObj);
                            obj = newObj;
                            continue;
                        }
                        else if (nsObj.IsObject)
                        {
                            obj = nsObj;
                            continue;
                        }
                        else throw new JSBindingException("Invalid member access expression. Invalid object in path.");
                    }

                    name = parts[parts.Length - 1];
                }

                var boundObject = binder.CreateScriptableObject(def.Target);
                obj.SetValue(name, boundObject);
                boundObject.Dispose();

                if (proxies != null)
                {
                    foreach (var proxy in proxies) proxy.Dispose();
                }
            }
        }

        private static string GetMemberNameForType(Type type)
        {
            // TODO: use JSObject attribute, if it set
            return NameHelper.ConvertMemberName(type.FullName, NamingConvention.Default);
        }
    }
}
