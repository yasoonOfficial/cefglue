namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using CefGlue.Interop;

    /// <summary>
    /// Initialization settings.
    /// Specify <c>null</c> or <c>0</c> to get the recommended default values.
    /// </summary>
    public sealed class CefSettings
    {
        private bool _isReadOnly;

        private bool _multiThreadedMessageLoop;
        private string _cachePath;
        private string _userAgent;
        private string _productVersion;
        private string _locale;
        private IList<string> _extraPluginPaths;
        private string _logFile;
        private CefLogSeverity _logSeverity;
        private bool release_dcheck_enabled;
        private CefGraphicsImplementation graphicsImplementation;
        private uint localStorageQuota;
        private uint sessionStorageQuota;
        private string javaScriptFlags;
#if WINDOWS
        private bool autoDetectProxySettingsEnabled;
#endif
        private string resourcesDirPath;
        private string localesDirPath;
        private bool packLoadingDisabled;
        private int uncaught_exception_stack_size;
        private int context_safety_implementation;

        public CefSettings()
        {
            _extraPluginPaths = new List<string>();
        }

        /// <summary>
        /// Set to true to have the message loop run in a separate thread.
        /// If false than the Cef.DoMessageLoopWork() function must be called from your application message loop.
        /// </summary>
        public bool MultiThreadedMessageLoop
        {
            get { return _multiThreadedMessageLoop; }
            set
            {
                ThrowIfReadOnly();
                _multiThreadedMessageLoop = value;
            }
        }

        /// <summary>
        /// The location where cache data will be stored on disk.
        /// If empty an in-memory cache will be used.
        /// HTML5 databases such as localStorage will only persist across sessions if a cache path is specified.
        /// </summary>
        public string CachePath
        {
            get { return _cachePath; }
            set
            {
                ThrowIfReadOnly();
                _cachePath = value;
            }
        }

        /// <summary>
        /// Value that will be returned as the User-Agent HTTP header.
        /// If empty the default User-Agent string will be used.
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set
            {
                ThrowIfReadOnly();
                _userAgent = value;
            }
        }

        /// <summary>
        /// Value that will be inserted as the product portion of the default
        /// User-Agent string. If empty the Chromium product version will be used. If
        /// |userAgent| is specified this value will be ignored.
        /// </summary>
        public string ProductVersion
        {
            get { return _productVersion; }
            set
            {
                ThrowIfReadOnly();
                _productVersion = value;
            }
        }

        /// <summary>
        /// The locale string that will be passed to WebKit. If empty the default
        /// locale of "en-US" will be used.
        /// </summary>
        public string Locale
        {
            get { return _locale; }
            set
            {
                ThrowIfReadOnly();
                _locale = value;
            }
        }

        /// <summary>
        /// List of fully qualified paths to plugins (including plugin name) that will
        /// be loaded in addition to any plugins found in the default search paths.
        /// </summary>
        public IList<string> ExtraPluginPaths
        {
            get
            {
                if (!IsReadOnly) { return _extraPluginPaths; }
                else
                {
                    return new ReadOnlyCollection<string>(_extraPluginPaths);
                }
            }
        }

        /// <summary>
        /// The directory and file name to use for the debug log.
        /// If empty, the default name of "debug.log" will be used 
        /// and the file will be written to the application directory.
        /// </summary>
        public string LogFile
        {
            get { return _logFile; }
            set
            {
                ThrowIfReadOnly();
                _logFile = value;
            }
        }

        /// <summary>
        /// The log severity. Only messages of this severity level or higher will be logged.
        /// </summary>
        public CefLogSeverity LogSeverity
        {
            get { return _logSeverity; }
            set
            {
                ThrowIfReadOnly();
                _logSeverity = value;
            }
        }

        /// <summary>
        /// Enable DCHECK in release mode to ease debugging.
        /// </summary>
        public bool ReleaseDCheckEnabled
        {
            get { return this.release_dcheck_enabled; }
            set
            {
                ThrowIfReadOnly();
                this.release_dcheck_enabled = value;
            }
        }

        /// <summary>
        /// The graphics implementation that CEF will use for rendering GPU accelerated
        /// content like WebGL, accelerated layers and 3D CSS.
        /// </summary>
        public CefGraphicsImplementation GraphicsImplementation
        {
            get { return this.graphicsImplementation; }
            set
            {
                ThrowIfReadOnly();
                this.graphicsImplementation = value;
            }
        }


        /// <summary>
        /// Quota limit for localStorage data across all origins. Default size is 5MB.
        /// </summary>
        [CLSCompliant(false)]
        public uint LocalStorageQuota
        {
            get { return this.localStorageQuota; }
            set
            {
                ThrowIfReadOnly();
                this.localStorageQuota = value;
            }
        }

        /// <summary>
        /// Quota limit for sessionStorage data per namespace. Default size is 5MB.
        /// </summary>
        [CLSCompliant(false)]
        public uint SessionStorageQuota
        {
            get { return this.sessionStorageQuota; }
            set
            {
                ThrowIfReadOnly();
                this.sessionStorageQuota = value;
            }
        }

        /// <summary>
        /// Custom flags that will be used when initializing the V8 JavaScript engine.
        /// The consequences of using custom flags may not be well tested.
        /// </summary>
        public string JavaScriptFlags
        {
            get { return this.javaScriptFlags; }
            set
            {
                ThrowIfReadOnly();
                this.javaScriptFlags = value;
            }
        }

#if WINDOWS
        /// <summary>
        /// Set to true to use the system proxy resolver on Windows when "Automatically detect settings" is checked.
        /// This setting is disabled by default for performance reasons.
        /// </summary>
        public bool AutoDetectProxySettingsEnabled
        {
            get { return this.autoDetectProxySettingsEnabled; }
            set
            {
                ThrowIfReadOnly();
                this.autoDetectProxySettingsEnabled = value;
            }
        }
#endif

        /// <summary>
        /// The fully qualified path for the resources directory. If this value is
        /// empty the chrome.pak and/or devtools_resources.pak files must be located in
        /// the module directory on Windows/Linux or the app bundle Resources directory
        /// on Mac OS X.
        /// </summary>
        public string ResourcesDirPath
        {
            get { return this.resourcesDirPath; }
            set
            {
                ThrowIfReadOnly();
                this.resourcesDirPath = value;
            }
        }

        /// <summary>
        /// The fully qualified path for the locales directory. If this value is empty
        /// the locales directory must be located in the module directory. This value
        /// is ignored on Mac OS X where pack files are always loaded from the app
        /// bundle resource directory.
        /// </summary>
        public string LocalesDirPath
        {
            get { return this.localesDirPath; }
            set
            {
                ThrowIfReadOnly();
                this.localesDirPath = value;
            }
        }

        /// <summary>
        /// Set to true to disable loading of pack files for resources and locales.
        /// A resource bundle handler must be provided for the browser and renderer
        /// processes via CefApp::GetResourceBundleHandler() if loading of pack files
        /// is disabled.
        /// </summary>
        public bool PackLoadingDisabled
        {
            get { return this.packLoadingDisabled; }
            set
            {
                ThrowIfReadOnly();
                this.packLoadingDisabled = value;
            }
        }

        ///
        // The number of stack trace frames to capture for uncaught exceptions.
        // Specify a positive value to enable the CefV8ContextHandler::
        // OnUncaughtException() callback. Specify 0 (default value) and
        // OnUncaughtException() will not be called.
        ///
        public int UncaughtExceptionStackSize
        {
            get { return this.uncaught_exception_stack_size; }
            set
            {
                ThrowIfReadOnly();
                this.uncaught_exception_stack_size = value;
            }
        }

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
        public int ContextSafetyImplementation
        {
            get { return this.context_safety_implementation; }
            set
            {
                ThrowIfReadOnly();
                this.context_safety_implementation = value;
            }
        }

        internal bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly) throw new InvalidOperationException("CefSettings object is read-only.");
        }


        internal unsafe cef_settings_t* CreateNative()
        {
            var ptr = cef_settings_t.Alloc();

            ptr->multi_threaded_message_loop = this.MultiThreadedMessageLoop;
            cef_string_t.Copy(this.CachePath, &ptr->cache_path);
            cef_string_t.Copy(this.UserAgent, &ptr->user_agent);
            cef_string_t.Copy(this.ProductVersion, &ptr->product_version);
            cef_string_t.Copy(this.Locale, &ptr->locale);
            ptr->extra_plugin_paths = CefStringList.CreateHandle(this.ExtraPluginPaths);
            cef_string_t.Copy(this.LogFile, &ptr->log_file);
            ptr->log_severity = (cef_log_severity_t)this.LogSeverity;
            ptr->release_dcheck_enabled = this.release_dcheck_enabled;
            ptr->graphics_implementation = (cef_graphics_implementation_t)this.GraphicsImplementation;
            ptr->local_storage_quota = this.LocalStorageQuota;
            ptr->session_storage_quota = this.SessionStorageQuota;
            cef_string_t.Copy(this.JavaScriptFlags, &ptr->javascript_flags);

#if WINDOWS
            ptr->auto_detect_proxy_settings_enabled = this.AutoDetectProxySettingsEnabled;
#endif

            cef_string_t.Copy(this.ResourcesDirPath, &ptr->resources_dir_path);
            cef_string_t.Copy(this.LocalesDirPath, &ptr->locales_dir_path);
            ptr->pack_loading_disabled = this.PackLoadingDisabled;
            ptr->uncaught_exception_stack_size = this.uncaught_exception_stack_size;
            ptr->context_safety_implementation = this.context_safety_implementation;

            return ptr;
        }

    }
}
