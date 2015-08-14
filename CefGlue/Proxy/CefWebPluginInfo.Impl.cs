namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefWebPluginInfo
    {
        /// <summary>
        /// Returns the plugin name (i.e. Flash).
        /// </summary>
        public string Name
        {
            get
            {
                var nResult = cef_web_plugin_info_t.invoke_get_name(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the plugin file path (DLL/bundle/library).
        /// </summary>
        public string Path
        {
            get
            {
                var nResult = cef_web_plugin_info_t.invoke_get_path(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the version of the plugin (may be OS-specific).
        /// </summary>
        public string Version
        {
            get
            {
                var nResult = cef_web_plugin_info_t.invoke_get_version(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns a description of the plugin from the version information.
        /// </summary>
        public string Description
        {
            get
            {
                var nResult = cef_web_plugin_info_t.invoke_get_description(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }
    }
}