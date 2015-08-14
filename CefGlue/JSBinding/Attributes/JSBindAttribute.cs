namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JSBindAttribute : Attribute
    {
        private bool visible;
        private NamingConvention namingConvention;

        public JSBindAttribute()
            : this(true) { }

        public JSBindAttribute(bool visible)
            : this(visible, NamingConvention.Default) { }

        public JSBindAttribute(NamingConvention namingConvention)
            : this(true, namingConvention) { }

        public JSBindAttribute(bool visible, NamingConvention namingConvention)
        {
            this.visible = visible;
            this.namingConvention = namingConvention;
        }

        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        public NamingConvention NamingConvention
        {
            get { return this.namingConvention; }
            set { this.namingConvention = value; }
        }

    }
}
