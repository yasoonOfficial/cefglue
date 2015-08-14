namespace CefGlue.Emit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text;

    internal sealed class EmitHelper
    {
        private readonly object parent;
        private readonly ILGenerator il;

        public EmitHelper(ILGenerator il)
            : this(null, il)
        { }

        public EmitHelper(object parent, ILGenerator il)
        {
            this.parent = parent;
            this.il = il;
        }

        public DynamicMethodHelper DynamicMethod
        {
            get { return (DynamicMethodHelper)this.parent; }
        }

        public Label DefineLabel()
        {
            return il.DefineLabel();
        }


        public EmitHelper MarkLabel(Label loc)
        {
            il.MarkLabel(loc);
            return this;
        }

        public EmitHelper LdArg(int index)
        {
            switch (index)
            {
                case 0: il.Emit(OpCodes.Ldarg_0); break;
                case 1: il.Emit(OpCodes.Ldarg_1); break;
                case 2: il.Emit(OpCodes.Ldarg_2); break;
                case 3: il.Emit(OpCodes.Ldarg_3); break;

                default:
                    if (index <= byte.MaxValue)
                    {
                        il.Emit(OpCodes.Ldarg_S, (byte)index);
                        break;
                    }
                    else if (index <= ushort.MaxValue)
                    {
                        il.Emit(OpCodes.Ldarg, (ushort)index);
                        break;
                    }
                    else throw new ArgumentOutOfRangeException("index");
            }

            return this;
        }

        public EmitHelper LdConstI4(int value)
        {
            switch (value)
            {
                case -1: il.Emit(OpCodes.Ldc_I4_M1); break;
                case 0: il.Emit(OpCodes.Ldc_I4_0); break;
                case 1: il.Emit(OpCodes.Ldc_I4_1); break;
                case 2: il.Emit(OpCodes.Ldc_I4_2); break;
                case 3: il.Emit(OpCodes.Ldc_I4_3); break;
                case 4: il.Emit(OpCodes.Ldc_I4_4); break;
                case 5: il.Emit(OpCodes.Ldc_I4_5); break;
                case 6: il.Emit(OpCodes.Ldc_I4_6); break;
                case 7: il.Emit(OpCodes.Ldc_I4_7); break;
                default:
                    if (sbyte.MinValue <= value && value <= sbyte.MaxValue)
                    {
                        il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4, value);
                    }
                    break;
            }
            return this;
        }

        public EmitHelper LdIndI()
        {
            il.Emit(OpCodes.Ldind_I);
            return this;
        }

        public EmitHelper StIndI()
        {
            il.Emit(OpCodes.Stind_I);
            return this;
        }

        public EmitHelper LdNull()
        {
            il.Emit(OpCodes.Ldnull);
            return this;
        }

        public EmitHelper LdStr(string value)
        {
            il.Emit(OpCodes.Ldstr, value);
            return this;
        }

        public EmitHelper Beq(Label label)
        {
            il.Emit(OpCodes.Beq, label);
            return this;
        }

        public EmitHelper BeqS(Label label)
        {
            il.Emit(OpCodes.Beq_S, label);
            return this;
        }

        public EmitHelper BrTrue(Label label)
        {
            il.Emit(OpCodes.Brtrue, label);
            return this;
        }

        public EmitHelper BrTrueS(Label label)
        {
            il.Emit(OpCodes.Brtrue_S, label);
            return this;
        }

        public EmitHelper Call(MethodInfo methodInfo)
        {
            il.EmitCall(OpCodes.Call, methodInfo, null);
            return this;
        }

        public EmitHelper CallVirt(MethodInfo methodInfo)
        {
            il.EmitCall(OpCodes.Callvirt, methodInfo, null);
            return this;
        }

        public EmitHelper Add()
        {
            il.Emit(OpCodes.Add);
            return this;
        }

        public EmitHelper Dup()
        {
            il.Emit(OpCodes.Dup);
            return this;
        }

        public EmitHelper Pop()
        {
            il.Emit(OpCodes.Pop);
            return this;
        }

        public EmitHelper Ret()
        {
            il.Emit(OpCodes.Ret);
            return this;
        }

        public EmitHelper Switch(Label[] jumpTable)
        {
            il.Emit(OpCodes.Switch, jumpTable);
            return this;
        }

    }
}
