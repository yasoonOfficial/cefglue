namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal sealed class JSObjectBinder
    {
        private static readonly Dictionary<Type, JSObjectBinder> binders = new Dictionary<Type, JSObjectBinder>();

        public static JSObjectBinder Get(Type type, JSBindingOptions options)
        {
            lock (binders)
            {
                JSObjectBinder value;
                if (binders.TryGetValue(type, out value))
                {
                    return value;
                }
                else
                {
                    var binder = new JSObjectBinder(type, options);
                    binders[type] = binder;
                    return binder;
                }
            }
        }

        private readonly Type type;
        private readonly JSBindingOptions options;
        private readonly NamingConvention namingConvention;
        private readonly Type proxyType;

        private JSObjectBinder(Type type, JSBindingOptions options)
            : this(type, options, NamingConvention.Default)
        { }

        private JSObjectBinder(Type type, JSBindingOptions options, NamingConvention namingConvention)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (!type.IsInterface) throw new ArgumentException("Only interface type allowed.", "type");

            this.type = type;
            this.options = options;
            this.namingConvention = namingConvention;

            this.proxyType = JSObjectTypeBuilder.Create(type, options, namingConvention);
        }

        public object Bind(CefV8Value obj)
        {
            // TODO: bind proxy to v8 value, it will be inspect obj and cache v8 functions to access them
            // TODO: also proxy object must reference FrameContext, 'cause it require to know a v8context
            return Activator.CreateInstance(proxyType, null);
        }

    }
}
