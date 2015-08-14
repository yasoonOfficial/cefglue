namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text;
    using Emit;

    internal static class JSObjectTypeBuilder
    {
        private const string assemblyName = "CefGlue.JSObject.g";
        private const string moduleName = assemblyName + ".dll";
        private const string namespaceName = assemblyName;

        private static bool initialized;

        private static AssemblyBuilder assembly;
        private static ModuleBuilder module;

        private static void Initialize()
        {
            if (initialized) return;

            // create a dynamic assembly
            assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                    new AssemblyName(assemblyName),
                    AssemblyBuilderAccess.RunAndSave // FIXME: in release it will be only Run
                    );

            // create a main module
            module = assembly.DefineDynamicModule(moduleName);

            initialized = true;
        }


        public static Type Create(Type targetType, JSBindingOptions options, NamingConvention namingConvention)
        {
            Initialize();

            // create the type
            TypeBuilder type = module.DefineType(
                string.Format("{0}.{1}Proxy", namespaceName, targetType.FullName),
                TypeAttributes.Public | TypeAttributes.Sealed,
                typeof(JSObjectBase),
                new Type[] { targetType }
                );

            // TODO: properties

            // create members
            foreach (var method in targetType.GetMethods())
            {
                // explicit interface implementation
                MethodBuilder mb = type.DefineMethod(
                        targetType.Name + "." + method.Name,
                        MethodAttributes.Private | MethodAttributes.HideBySig |
                        MethodAttributes.NewSlot | MethodAttributes.Virtual |
                        MethodAttributes.Final,
                        method.ReturnType,
                        method.GetParameters().Select(_ => _.ParameterType).ToArray()
                        );
                type.DefineMethodOverride(mb, method);

                var emit = new EmitHelper(mb.GetILGenerator());
                emit.LdArg(1)
                    .Ret()
                    ;
            }

            return type.CreateType();
        }


    }
}
