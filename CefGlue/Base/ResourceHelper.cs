namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class ResourceHelper
    {
        private static string core;
        private static string xmlHttpRequest;

        public static string Core
        {
            get
            {
                if (core == null)
                {
                    core = GetStringResource("Core.js");
                }
                return core;
            }
        }

        public static string XmlHttpRequest
        {
            get
            {
                if (xmlHttpRequest == null)
                {
                    xmlHttpRequest = GetStringResource("XMLHttpRequest.js");
                }
                return xmlHttpRequest;
            }
        }

        private static string GetStringResource(string name)
        {
            var asm = typeof(ResourceHelper).Assembly;
            using (var stream = asm.GetManifestResourceStream("CefGlue.Resources." + name))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
