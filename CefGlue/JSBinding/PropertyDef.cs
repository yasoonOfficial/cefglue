namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal sealed class PropertyDef : DispatchTableEntry
    {
        private MethodDef getMethod;
        private MethodDef setMethod;

        public PropertyDef(string name)
            : base(name)
        {
        }

        public bool CanRead { get { return this.getMethod != null; } }
        public bool CanWrite { get { return this.setMethod != null; } }

        public MethodDef GetMethod { get { return this.getMethod; } }

        public MethodDef SetMethod { get { return this.setMethod; } }

        public void Add(MethodDef method)
        {
            if ((method.Attributes & MethodDefAttributes.Getter) != 0)
            {
                if (this.getMethod != null) throw new JSBindingException("Getter already defined.");
                this.getMethod = method;
            }
            else if ((method.Attributes & MethodDefAttributes.Setter) != 0)
            {
                if (this.setMethod != null) throw new JSBindingException("Setter already defined.");
                this.setMethod = method;
            }
            else throw new JSBindingException("Method must be a getter or setter.");
        }
    }
}
