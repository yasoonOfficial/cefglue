namespace CefGlue.JSBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal sealed class NameHelper
    {
        public static string ConvertIdentifier(string name, NamingConvention namingConvention)
        {
            // TODO: complete this...

            if (namingConvention == NamingConvention.None) return name;

            if (char.IsUpper(name[0]))
            {
                return char.ToLowerInvariant(name[0]) + name.Substring(1);
            }
            return name;
        }

        public static string ConvertMemberName(string name, NamingConvention namingConvention)
        {
            // it allow names like Some.Member.Access
            // TODO: check for incorrect symbols

            if (!name.Contains('.')) return ConvertIdentifier(name, namingConvention);

            var parts = name.Split('.');

            for (var i = 0; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(parts[i])) throw new ArgumentException("name");
                parts[i] = ConvertIdentifier(parts[i], namingConvention);
            }

            return string.Join(".", parts);
        }

    }
}
