namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using CefGlue.Interop;

    internal sealed class MethodDef : DispatchTableEntry
    {
        private MethodDefAttributes attributes;
        private MethodInvoker invoker;
        private object methods;

        public MethodDef(string name, MethodDefAttributes attributes)
            : base(name)
        {
            this.attributes = attributes;

            if (this.Getter && this.Setter)
            {
                throw new ArgumentException("Property accessor can be getter or setter, but not both.", "attributes");
            }

            if ((this.Getter || this.Setter) && !this.Hidden)
            {
                throw new ArgumentException("Property accessor methods must be hidden.", "attributes");
            }

            if (this.HasOverloads || this.Compiled)
            {
                throw new ArgumentException("HasOverloads, Compiled attribute can't be set directly.", "attributes");
            }
        }

        public MethodDefAttributes Attributes { get { return this.attributes; } }

        public bool Hidden { get { return (this.attributes & MethodDefAttributes.Hidden) != 0; } }
        public bool Getter { get { return (this.attributes & MethodDefAttributes.Getter) != 0; } }
        public bool Setter { get { return (this.attributes & MethodDefAttributes.Setter) != 0; } }
        public bool Static { get { return (this.attributes & MethodDefAttributes.Static) != 0; } }
        public bool HasOverloads { get { return (this.attributes & MethodDefAttributes.HasOverloads) != 0; } }
        public bool Compiled { get { return (this.attributes & MethodDefAttributes.Compiled) != 0; } }

        public void Add(MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException("method");

            if (this.methods == null)
            {
                this.methods = method;
                return;
            }

            if ((this.Attributes & (MethodDefAttributes.Getter | MethodDefAttributes.Setter)) != 0)
            {
                throw new JSBindingException("Property accessor methods can't have overloads.");
            }

            if (this.methods is MethodInfo)
            {
                this.methods = new MethodInfo[] { (MethodInfo)this.methods, method };
                this.attributes |= MethodDefAttributes.HasOverloads;
                return;
            }

            var methods = this.methods as MethodInfo[];
            Array.Resize<MethodInfo>(ref methods, methods.Length + 1);
            methods[methods.Length - 1] = method;
            this.methods = methods;
        }

        public IEnumerable<MethodInfo> GetMethods()
        {
            if (!this.HasOverloads)
            {
                yield return (MethodInfo)this.methods;
            }
            else
            {
                var methods = (MethodInfo[])this.methods;
                for (var i = 0; i < methods.Length; i++)
                {
                    yield return methods[i];
                }
            }
        }

        public string[] GetArgumentNames()
        {
            // TODO: get common arg names?
            var method = GetMethods().First();
            return method.GetParameters().Select(_ => _.Name).ToArray();
        }

        public bool ReturnTypeIsVoid
        {
            get
            {
                return GetMethods().All(_ => _.ReturnType == typeof(void));
            }
        }

        public void Compile()
        {
            if (!this.Compiled)
            {
                this.invoker = MethodInvokerBuilder.Create(this);

                this.attributes |= MethodDefAttributes.Compiled;
            }
        }

        internal unsafe void Invoke(object instance, int argumentsCount, cef_v8value_t** arguments, cef_v8value_t** retval)
        {
            if (!this.Compiled)
            {
                this.Compile();
            }

            this.invoker(instance, argumentsCount, arguments, retval);
        }

    }
}
