namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Initialization settings. Specify NULL or 0 to get the recommended default
    // values.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_settings_t
    {
        ///
        // Size of this structure.
        ///
        // size_t size;
        public int size;

        ///
        // Set to true (1) to have the message loop run in a separate thread. If
        // false (0) than the CefDoMessageLoopWork() function must be called from
        // your application message loop.
        ///
        // bool multi_threaded_message_loop;
        public bool_t multi_threaded_message_loop;

        ///
        // The location where cache data will be stored on disk. If empty an
        // in-memory cache will be used. HTML5 databases such as localStorage will
        // only persist across sessions if a cache path is specified.
        ///
        public cef_string_t cache_path;

        ///
        // Value that will be returned as the User-Agent HTTP header. If empty the
        // default User-Agent string will be used.
        ///
        public cef_string_t user_agent;

        ///
        // Value that will be inserted as the product portion of the default
        // User-Agent string. If empty the Chromium product version will be used. If
        // |userAgent| is specified this value will be ignored.
        ///
        public cef_string_t product_version;

        ///
        // The locale string that will be passed to WebKit. If empty the default
        // locale of "en-US" will be used.
        ///
        public cef_string_t locale;

        ///
        // List of fully qualified paths to plugins (including plugin name) that will
        // be loaded in addition to any plugins found in the default search paths.
        ///
        public cef_string_list* extra_plugin_paths;

        ///
        // The directory and file name to use for the debug log. If empty, the
        // default name of "debug.log" will be used and the file will be written
        // to the application directory.
        ///
        public cef_string_t log_file;

        ///
        // The log severity. Only messages of this severity level or higher will be
        // logged.
        ///
        public cef_log_severity_t log_severity;

        ///
        // Enable DCHECK in release mode to ease debugging.
        ///
        public bool_t release_dcheck_enabled;
        
        ///
        // The graphics implementation that CEF will use for rendering GPU accelerated
        // content like WebGL, accelerated layers and 3D CSS.
        ///
        public cef_graphics_implementation_t graphics_implementation;

        ///
        // Quota limit for localStorage data across all origins. Default size is 5MB.
        ///
        public uint local_storage_quota;
        
        ///
        // Quota limit for sessionStorage data per namespace. Default size is 5MB.
        ///
        public uint session_storage_quota;
		
		///
		// Custom flags that will be used when initializing the V8 JavaScript engine.
		// The consequences of using custom flags may not be well tested.
		///
		public cef_string_t javascript_flags;

#if WINDOWS
        ///
        // Set to true (1) to use the system proxy resolver on Windows when 
        // "Automatically detect settings" is checked. This setting is disabled
        // by default for performance reasons.
        ///
        public bool_t auto_detect_proxy_settings_enabled;
#endif

        ///
        // The fully qualified path for the resources directory. If this value is
        // empty the chrome.pak and/or devtools_resources.pak files must be located in
        // the module directory on Windows/Linux or the app bundle Resources directory
        // on Mac OS X.
        ///
        public cef_string_t resources_dir_path;

        ///
        // The fully qualified path for the locales directory. If this value is empty
        // the locales directory must be located in the module directory. This value
        // is ignored on Mac OS X where pack files are always loaded from the app
        // bundle resource directory.
        ///
        public cef_string_t locales_dir_path;

        ///
        // Set to true (1) to disable loading of pack files for resources and locales.
        // A resource bundle handler must be provided for the browser and renderer
        // processes via CefApp::GetResourceBundleHandler() if loading of pack files
        // is disabled.
        ///
        public bool_t pack_loading_disabled;

        ///
        // The number of stack trace frames to capture for uncaught exceptions.
        // Specify a positive value to enable the CefV8ContextHandler::
        // OnUncaughtException() callback. Specify 0 (default value) and
        // OnUncaughtException() will not be called.
        ///
        public int uncaught_exception_stack_size;

        ///
        // By default CEF V8 references will be invalidated (the IsValid() method will
        // return false) after the owning context has been released. This reduces the
        // need for external record keeping and avoids crashes due to the use of V8
        // references after the associated context has been released.
        //
        // CEF currently offers two context safety implementations with different
        // performance characteristics. The default implementation (value of 0) uses a
        // map of hash values and should provide better performance in situations with
        // a small number contexts. The alternate implementation (value of 1) uses a
        // hidden value attached to each context and should provide better performance
        // in situations with a large number of contexts.
        //
        // If you need better performance in the creation of V8 references and you
        // plan to manually track context lifespan you can disable context safety by
        // specifying a value of -1.
        ///
        public int context_safety_implementation;

        public static void Clear(cef_settings_t* self)
        {
            cef_string_t.Clear(&self->cache_path);
            cef_string_t.Clear(&self->user_agent);
            cef_string_t.Clear(&self->product_version);
            cef_string_t.Clear(&self->locale);
            // FIXME: null check?
            CefStringList.DestroyHandle(self->extra_plugin_paths);
            cef_string_t.Clear(&self->log_file);
            cef_string_t.Clear(&self->javascript_flags);
            cef_string_t.Clear(&self->resources_dir_path);
            cef_string_t.Clear(&self->locales_dir_path);
        }
    }
}
