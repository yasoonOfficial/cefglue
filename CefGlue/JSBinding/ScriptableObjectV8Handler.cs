namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;

    internal sealed class ScriptableObjectV8Handler : CefV8Handler
    {
        private DispatchTable<MethodDef> dispatchTable;
        private object instance;
        private bool v8Extension;

        public ScriptableObjectV8Handler(ScriptableObjectBinder binder)
            : this(binder, false)
        { }

        public ScriptableObjectV8Handler(ScriptableObjectBinder binder, bool v8Extension)
            : this(binder, null, v8Extension)
        { }

        public ScriptableObjectV8Handler(ScriptableObjectBinder binder, object instance)
            : this(binder, instance, false)
        { }

        /// <summary>
        /// If instance == null -> then object reference will be got from object's userdata.
        /// Otherwise userdata ignored.
        /// </summary>
        public ScriptableObjectV8Handler(ScriptableObjectBinder binder, object instance, bool v8Extension)
        {
            this.instance = instance;
            this.dispatchTable = binder.DispatchTable;
            this.v8Extension = v8Extension;
        }

        internal unsafe override int execute(cef_v8handler_t* self, cef_string_t* name, cef_v8value_t* @object, int argumentCount, cef_v8value_t** arguments, cef_v8value_t** retval, cef_string_t* exception)
        {
            var method = this.dispatchTable.GetOrDefault(name);
            if (method == null) return 0;

            // check method visibility
            var methodAttributes = method.Attributes;
            if ((methodAttributes & MethodDefAttributes.Hidden) != 0)
            {
                if ((methodAttributes & (MethodDefAttributes.Getter | MethodDefAttributes.Setter)) != 0)
                {
                    if (this.v8Extension)
                    {
                        // allow invoke of hidden property accessor's method for v8 extension handler
                    }
                    else
                    {
                        return 0;
                    }
                }
                else return 0;
            }

            var instance = this.instance;
            if (instance == null)
            {
                // TODO: self must be got from obj's userdata
                throw new NotImplementedException();
            }

            // TODO: invoke method
            try
            {
                method.Invoke(instance, argumentCount, arguments, retval);
            }
            catch (Exception ex)
            {
                // TODO: how exceptions must be formatted ?
                // TODO: message for invoke errors
                cef_string_t.Copy(ex.ToString(), exception);
            }

            // release v8 values
            // TODO: this method must be typed (i.e. cef_v8value_t.invoke_release(@object);)
            cef_v8value_t.invoke_release((cef_base_t*)@object);
            for (var i = 0; i < argumentCount; i++)
            {
                cef_v8value_t.invoke_release((cef_base_t*)arguments[i]);
            }

            return 1;
        }

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            throw new NotSupportedException();
        }
    }
}
