namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class JSObjectAttribute : Attribute
    {
        public static readonly JSObjectAttribute Default = new JSObjectAttribute();

        private readonly JSBindingOptions options;

        public JSObjectAttribute()
            : this(JSBindingOptions.Public)
        { }

        public JSObjectAttribute(JSBindingOptions bindingFlags)
        {
            this.options = bindingFlags;
        }

        public JSBindingOptions Options
        {
            get { return this.options; }
        }
    }
}
