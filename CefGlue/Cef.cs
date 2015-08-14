namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;
    using CefGlue.Diagnostics;
    using CefGlue.JSBinding;
    using CefGlue.Threading;

    // TODO: allow specify libcef.dll location (and other libraries?)
    // TODO: do exception checking in handler impls, and reporting about them

    public static unsafe partial class Cef
    {
        private static CefSettings s_currentSettings;
        private static bool s_multiThreadedMessageLoop;
        private static JSBindingContext jsBindingContext = new JSBindingContext(null);

        public static CefSettings CurrentSettings
        {
            get
            {
                ThrowIfNotInitialized();
                return s_currentSettings;
            }
            private set
            {
                if (s_currentSettings != null)
                {
                    s_currentSettings.IsReadOnly = false;
                    s_currentSettings = null;
                }
                if (value != null)
                {
                    value.IsReadOnly = true;
                    s_multiThreadedMessageLoop = value.MultiThreadedMessageLoop;
                }
                s_currentSettings = value;
            }
        }

        // TODO: rename property
        public static bool IsInitialized { get { return s_currentSettings != null; } }

        internal static bool MultiThreadedMessageLoop { get { return s_multiThreadedMessageLoop; } }

#if DIAGNOSTICS
        [CLSCompliant(false)]
        public static Logger Logger { get { return Logger.Instance; } }
#endif

        /// <summary>
        /// This function should be called on the main application thread to initialize
        /// CEF when the application is started.
        /// </summary>
        /// <param name="settings"></param>
        /// <exception cref="Exception"></exception>
        public static void Initialize(CefSettings settings, CefApp app = null)
        {
            if (IsInitialized) throw new CefException("CEF already initialized.");
#if DIAGNOSTICS
            cef_string_t.OnCreate = (ptr, str) =>
            {
                if (string.IsNullOrEmpty(str))
                {
                    Cef.Logger.Trace(LogTarget.CefString, IntPtr.Zero, LogOperation.Create, "Empty.");
                }
                else
                {
                    Cef.Logger.Trace(LogTarget.CefString, ptr, LogOperation.Create, "Value=[{0}].",
                        str.Length <= 1024 ? str : (str.Substring(0, 1024) + "...")
                        );
                }
            };
            cef_string_t.OnDispose = (ptr) =>
            {
                Cef.Logger.Trace(LogTarget.CefString, ptr, LogOperation.Dispose);
            };

            Logger.SetAllTargets(true);
            Logger.Trace(LogTarget.Default, "Initialize");
#endif

            var n_settings = settings.CreateNative();
            var n_app = app == null ? null : app.GetNativePointerAndAddRef();

            // FIXME: not sure if application should be null
            var initialized = NativeMethods.cef_initialize(n_settings, n_app) != 0;
            cef_settings_t.Free(n_settings);

            if (!initialized) throw new CefException("CEF failed to initialize.");

            CurrentSettings = settings;
        }

        /// <summary>
        /// This function should be called on the main application thread to shut down
        /// CEF before the application exits.
        /// </summary>
        public static void Shutdown()
        {
            if (!IsInitialized) return;

            CurrentSettings = null;

            // FIXME: force to be proxies will be collected, this is can be done without CG.Collect in Cef.Shutdown to implicit dispose live proxies and objects

#if DIAGNOSTICS
            ObjectCt.WriteDump();
#endif

            // all proxies must be collected -> then all cef resources will be freed
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

#if DIAGNOSTICS
            ObjectCt.WriteDump();
#endif

            NativeMethods.cef_shutdown();

#if DIAGNOSTICS
            Logger.Close();
#endif
        }

        internal static JSBindingContext JSBindingContext
        {
            get { return jsBindingContext; }
        }

        public static IJSBindingContext JSBinding
        {
            get { return jsBindingContext; }
        }

        /// <summary>
        /// Perform a single iteration of CEF message loop processing. This function is
        /// used to integrate the CEF message loop into an existing application message
        /// loop. Care must be taken to balance performance against excessive CPU usage.
        /// This function should only be called on the main application thread and only
        /// if cef_initialize() is called with a CefSettings.multi_threaded_message_loop
        /// value of false (0). This function will not block.
        /// </summary>
        public static void DoMessageLoopWork()
        {
            NativeMethods.cef_do_message_loop_work();
        }

        /// <summary>
        /// Run the CEF message loop. Use this function instead of an application-
        /// provided message loop to get the best balance between performance and CPU
        /// usage. This function should only be called on the main application thread and
        /// only if cef_initialize() is called with a
        /// CefSettings.multi_threaded_message_loop value of false (0). This function
        /// will block until a quit message is received by the system.
        /// </summary>
        public static void RunMessageLoop()
        {
            NativeMethods.cef_run_message_loop();
        }

        /// <summary>
        /// Set to true before calling Windows APIs like TrackPopupMenu that
        /// enter a modal message loop. Set to false after exiting the modal
        /// message loop.
        /// </summary>
        public static void SetOSModalLoop(bool osModalLoop)
        {
            NativeMethods.cef_set_osmodal_loop(osModalLoop ? 1 : 0);
        }

        /// <summary>
        /// Register a new V8 extension with the specified JavaScript extension
        /// code and handler. Functions implemented by the handler are prototyped
        /// using the keyword 'native'. The calling of a native function is
        /// restricted to the scope in which the prototype of the native function
        /// is defined. This function may be called on any thread.
        ///
        /// Example JavaScript extension code: &lt;pre&gt;
        ///   /// create the 'example' global object if it doesn't already exist.
        ///   if (!example)
        ///     example = {};
        ///   /// create the 'example.test' global object if it doesn't already exist.
        ///   if (!example.test)
        ///     example.test = {};
        ///   (function() {
        ///     /// Define the function 'example.test.myfunction'.
        ///     example.test.myfunction = function() {
        ///       /// Call CefV8Handler::Execute() with the function name 'MyFunction'
        ///       /// and no arguments.
        ///       native function MyFunction();
        ///       return MyFunction();
        ///     };
        ///     /// Define the getter function for parameter 'example.test.myparam'.
        ///     example.test.__defineGetter__('myparam', function() {
        ///       /// Call CefV8Handler::Execute() with the function name 'GetMyParam'
        ///       /// and no arguments.
        ///       native function GetMyParam();
        ///       return GetMyParam();
        ///     });
        ///     /// Define the setter function for parameter 'example.test.myparam'.
        ///     example.test.__defineSetter__('myparam', function(b) {
        ///       /// Call CefV8Handler::Execute() with the function name 'SetMyParam'
        ///       /// and a single argument.
        ///       native function SetMyParam();
        ///       if(b) SetMyParam(b);
        ///     });
        ///
        ///     /// Extension definitions can also contain normal JavaScript variables
        ///     /// and functions.
        ///     var myint = 0;
        ///     example.test.increment = function() {
        ///       myint += 1;
        ///       return myint;
        ///     };
        ///   })();
        /// &lt;/pre&gt; Example usage in the page: &lt;pre&gt;
        ///   /// Call the function.
        ///   example.test.myfunction();
        ///   /// Set the parameter.
        ///   example.test.myparam = value;
        ///   /// Get the parameter.
        ///   value = example.test.myparam;
        ///   /// Call another function.
        ///   example.test.increment();
        /// &lt;/pre&gt;
        /// </summary>
        public static bool RegisterExtension(string extensionName, string javascriptCode, CefV8Handler handler)
        {
            fixed (char* extensionName_str = extensionName)
            fixed (char* javasriptCode_str = javascriptCode)
            {
                var n_extensionName = new cef_string_t(extensionName_str, extensionName.Length);
                var n_javascriptCode = new cef_string_t(javasriptCode_str, javascriptCode.Length);

                return NativeMethods.cef_register_extension(
                    &n_extensionName,
                    &n_javascriptCode,
                    handler.GetNativePointerAndAddRef()
                    ) != 0;
            }
        }

        /// <summary>
        /// Register a scheme handler factory for the specified |scheme_name| and optional |domain_name|.
        /// An NULL |domain_name| value for a standard scheme will cause the factory to match all domain names.
        /// The |domain_name| value will be ignored for non-standard schemes.
        /// If |scheme_name| is a built-in scheme and no handler is returned by |factory| then the built-in scheme handler factory will be called.
        /// If |scheme_name| is a custom scheme the cef_register_custom_scheme() function should be called for that scheme.
        /// This function may be called multiple times to change or remove the factory that matches the specified |scheme_name| and optional |domain_name|.
        /// Returns false (0) if an error occurs.
        /// This function may be called on any thread.
        /// </summary>
        public static bool RegisterSchemeHandlerFactory(string schemeName, string domainName, CefSchemeHandlerFactory factory)
        {
            fixed (char* schemeName_str = schemeName)
            fixed (char* domainName_str = domainName)
            {
                var n_schemeName = new cef_string_t(schemeName_str, schemeName.Length);
                var n_domainName = new cef_string_t(domainName_str, domainName != null ? domainName.Length : 0);

                return NativeMethods.cef_register_scheme_handler_factory(
                    &n_schemeName,
                    &n_domainName,
                    factory.GetNativePointerAndAddRef()
                    ) != 0;
            }
        }

        /// <summary>
        /// Clear all registered scheme handler factories.
        /// Returns false (0) on error.
        /// This function may be called on any thread.
        /// </summary>
        public static bool ClearSchemeHandlerFactories()
        {
            return NativeMethods.cef_clear_scheme_handler_factories() != 0;
        }

        /// <summary>
        /// Add an entry to the cross-origin access whitelist.
        ///
        /// The same-origin policy restricts how scripts hosted from different
        /// origins (scheme + domain) can communicate. By default, scripts can
        /// only access resources with the same origin. Scripts hosted on the
        /// HTTP and HTTPS schemes (but no other schemes) can use the "Access-
        /// Control-Allow-Origin" header to allow cross-origin requests. For
        /// example, https://source.example.com can make XMLHttpRequest requests
        /// on http://target.example.com if the http://target.example.com request
        /// returns an "Access-Control-Allow-Origin: https://source.example.com"
        /// response header.
        ///
        /// Scripts in separate frames or iframes and hosted from the same
        /// protocol and domain suffix can execute cross-origin JavaScript if
        /// both pages set the document.domain value to the same domain suffix.
        /// For example, scheme://foo.example.com and scheme://bar.example.com
        /// can communicate using JavaScript if both domains set
        /// document.domain="example.com".
        ///
        /// This function is used to allow access to origins that would otherwise
        /// violate the same-origin policy. Scripts hosted underneath the fully
        /// qualified |source_origin| URL (like http://www.example.com) will be
        /// allowed access to all resources hosted on the specified
        /// |target_protocol| and |target_domain|. If |allow_target_subdomains|
        /// is true (1) access will also be allowed to all subdomains of the
        /// target domain. This function may be called on any thread. Returns
        /// false (0) if |source_origin| is invalid or the whitelist cannot be
        /// accessed.
        /// </summary>
        public static bool AddCrossOriginWhitelistEntry(string sourceOrigin, string targetProtocol, string targetDomain, bool allowTargetSubdomains)
        {
            fixed (char* sourceOrigin_str = sourceOrigin)
            fixed (char* targetProtocol_str = targetProtocol)
            fixed (char* targetDomain_str = targetDomain)
            {
                var n_sourceOrigin = new cef_string_t(sourceOrigin_str, sourceOrigin.Length);
                var n_targetProtocol = new cef_string_t(targetProtocol_str, targetProtocol.Length);
                var n_targetDomain = new cef_string_t(targetDomain_str, targetDomain.Length);

                return NativeMethods.cef_add_cross_origin_whitelist_entry(
                    &n_sourceOrigin,
                    &n_targetProtocol,
                    &n_targetDomain,
                    allowTargetSubdomains ? 1 : 0
                    ) != 0;
            }
        }

        /// <summary>
        /// Remove an entry from the cross-origin access whitelist.
        /// Returns false (0) if |source_origin| is invalid or the whitelist cannot be accessed.
        /// </summary>
        public static bool RemoveCrossOriginWhitelistEntry(string sourceOrigin, string targetProtocol, string targetDomain, bool allowTargetSubdomains)
        {
            fixed (char* sourceOrigin_str = sourceOrigin)
            fixed (char* targetProtocol_str = targetProtocol)
            fixed (char* targetDomain_str = targetDomain)
            {
                var n_sourceOrigin = new cef_string_t(sourceOrigin_str, sourceOrigin.Length);
                var n_targetProtocol = new cef_string_t(targetProtocol_str, targetProtocol.Length);
                var n_targetDomain = new cef_string_t(targetDomain_str, targetDomain.Length);

                return NativeMethods.cef_remove_cross_origin_whitelist_entry(
                    &n_sourceOrigin,
                    &n_targetProtocol,
                    &n_targetDomain,
                    allowTargetSubdomains ? 1 : 0
                    ) != 0;
            }
        }

        /// <summary>
        /// Remove all entries from the cross-origin access whitelist.
        /// Returns false (0) if the whitelist cannot be accessed.
        /// </summary>
        public static bool ClearCrossOriginWhitelist()
        {
            return NativeMethods.cef_clear_cross_origin_whitelist() != 0;
        }

        /// <summary>
        /// CEF maintains multiple internal threads that are used for handling
        /// different types of tasks.
        /// The UI thread creates the browser window and is used for all interaction with the WebKit rendering engine and V8 JavaScript engine
        /// (The UI thread will be the same as the main application thread if cef_initialize() is called with a CefSettings.multi_threaded_message_loop value of false (0).)
        /// The IO thread is used for handling schema and network requests.
        /// The FILE thread is used for the application cache and other miscellaneous activities.
        /// This function will return true (1) if called on the specified thread.
        /// </summary>
        public static bool CurrentlyOn(CefThreadId threadId)
        {
            return NativeMethods.cef_currently_on((cef_thread_id_t)threadId) != 0;
        }

        /// <summary>
        /// Post a task for execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public static void PostTask(CefThreadId threadId, CefTask task)
        {
            var result = NativeMethods.cef_post_task((cef_thread_id_t)threadId, task.GetNativePointerAndAddRef()) != 0;
            if (!result) ThrowPostTaskError();
        }

        /// <summary>
        /// Post a task for delayed execution on the specified thread.
        /// This function may be called on any thread.
        /// </summary>
        public static void PostTask(CefThreadId threadId, CefTask task, long delayMs)
        {
            var result = NativeMethods.cef_post_delayed_task((cef_thread_id_t)threadId, task.GetNativePointerAndAddRef(), delayMs) != 0;
            if (!result) ThrowPostTaskError();
        }

        /// <summary>
        /// Parse the specified |url| into its component parts.
        /// Returns false (0) if the URL is NULL or invalid.
        /// </summary>
        // TODO: CEF API
        //public static extern int parse_url(/*const*/ cef_string_t* url, cef_urlparts_t* parts);

        /// <summary>
        /// Creates a URL from the specified |parts|, which must contain a non-
        /// NULL spec or a non-NULL host and path (at a minimum), but not both.
        /// Returns false (0) if |parts| isn't initialized as described.
        /// </summary>
        // TODO: CEF API
        //public static extern int create_url(/*const*/ cef_urlparts_t* parts, cef_string_t* url);

        /// <summary>
        /// Returns the number of installed web plugins. This function must be
        /// called on the UI thread.
        /// </summary>
        public static int GetWebPluginCount()
        {
            return NativeMethods.cef_get_web_plugin_count();
        }

        /// <summary>
        /// Returns information for web plugin at the specified zero-based index.
        /// This function must be called on the UI thread.
        /// </summary>
        public static CefWebPluginInfo GetWebPluginInfo(int index)
        {
            var webPluginInfo = NativeMethods.cef_get_web_plugin_info(index);
            return CefWebPluginInfo.FromOrDefault(webPluginInfo);
        }

        /// <summary>
        /// Returns information for web plugin with the specified name. This
        /// function must be called on the UI thread.
        /// </summary>
        public static CefWebPluginInfo GetWebPluginInfo(string name)
        {
            fixed (char* str = name)
            {
                var nName = new cef_string_t(str, name != null ? name.Length : 0);
                var webPluginInfo = NativeMethods.cef_get_web_plugin_info_byname(&nName);
                return CefWebPluginInfo.FromOrDefault(webPluginInfo);
            }
        }

        private static void ThrowIfNotInitialized()
        {
            if (!IsInitialized) throw new CefException("CEF is not initialized.");
        }

        private static void ThrowPostTaskError()
        {
            throw new CefException("Post task error.");
        }
    }
}
