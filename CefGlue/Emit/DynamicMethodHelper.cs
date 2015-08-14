namespace CefGlue.Emit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Text;

    internal sealed class DynamicMethodHelper
    {
        private DynamicMethod method;
        private EmitHelper emit;

        public DynamicMethodHelper(string name, Type returnType, Type[] parameterTypes)
        {
            this.method = new DynamicMethod(name, returnType, parameterTypes,
                this.GetType(),
                true
                );
        }

        public EmitHelper Emitter
        {
            get
            {
                if (this.emit == null)
                {
                    this.emit = new EmitHelper(this, this.method.GetILGenerator());
                }
                return this.emit;
            }
        }

        public Delegate CreateDelegate(Type delegateType)
        {
            return this.method.CreateDelegate(delegateType);
        }
    }
}
