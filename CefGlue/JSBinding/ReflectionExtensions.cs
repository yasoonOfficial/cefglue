namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class ReflectionExtensions
    {
        public static PropertyInfo GetDeclaringTypeProperty(this MethodInfo method)
        {
            // TODO: check this is broken? 'cause it is used to visibility checks, and reflected type property can override scriptable attribute 

            var takesArg = method.GetParameters().Length == 1;
            var hasReturn = method.ReturnType != typeof(void);

            if (takesArg == hasReturn) return null;

            var bindingAttr = (method.IsStatic ? BindingFlags.Static : BindingFlags.Instance)
                | (method.IsPublic ? BindingFlags.Public : BindingFlags.Public | BindingFlags.NonPublic)
                ;

            if (takesArg)
            {
                return method.DeclaringType.GetProperties(bindingAttr)
                    .Where(prop => prop.GetSetMethod(true).AreEqualForDeclaringType(method))
                    .FirstOrDefault();
            }
            else
            {
                return method.DeclaringType.GetProperties(bindingAttr)
                    .Where(_ => _.GetGetMethod(true).AreEqualForDeclaringType(method))
                    .FirstOrDefault();
            }
        }

        public static bool IsGetterLikeSignature(this MethodInfo method)
        {
            var takesArg = method.GetParameters().Length == 1;
            var hasReturn = method.ReturnType != typeof(void);
            return takesArg == false && hasReturn == true;
        }

        public static bool IsSetterLikeSignature(this MethodInfo method)
        {
            var takesArg = method.GetParameters().Length == 1;
            var hasReturn = method.ReturnType != typeof(void);
            return takesArg == true && hasReturn == false;
        }

        public static bool AreEqualForDeclaringType(this MemberInfo first, MemberInfo second)
        {
            if (first == null && second == null) return true;
            if (first == null || second == null) return false;
            return first.MetadataToken == second.MetadataToken && first.Module == second.Module;
        }

        /*
        public static bool AreMethodsEqualForDeclaringType(this MethodInfo first, MethodInfo second)
        {
            first = first.ReflectedType == first.DeclaringType ? first : first.DeclaringType.GetMethod(first.Name, first.GetParameters().Select(p => p.ParameterType).ToArray());
            second = second.ReflectedType == second.DeclaringType ? second : second.DeclaringType.GetMethod(second.Name, second.GetParameters().Select(p => p.ParameterType).ToArray());
            return first == second;
        }
        */
    }
}
