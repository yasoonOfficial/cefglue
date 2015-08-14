namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using CefGlue.Interop;
    using Diagnostics;

    unsafe partial class CefApp
    {
        /// <summary>
        /// Provides an opportunity to register custom schemes. Do not keep a
        /// reference to the |registrar| object. This function is called on the
        /// UI thread.
        /// </summary>
        private void on_register_custom_schemes(cef_app_t* self, cef_scheme_registrar_t* registrar)
        {
            ThrowIfObjectDisposed();

            using (var m_registrar = CefSchemeRegistrar.From(registrar))
            {
                this.OnRegisterCustomSchemes(m_registrar);
            }
        }

        /// <summary>
        /// Provides an opportunity to register custom schemes. Do not keep a reference
        /// to the |registrar| object. This method is called on the UI thread.
        /// </summary>
        protected virtual void OnRegisterCustomSchemes(CefSchemeRegistrar registrar)
        {
        }

        /// <summary>
        /// Return the handler for resource bundle events. If
        /// CefSettings.pack_loading_disabled is true (1) a handler must be
        /// returned. If no handler is returned resources will be loaded from
        /// pack files. This function is called on multiple threads.
        /// </summary>
        private cef_resource_bundle_handler_t* get_resource_bundle_handler(cef_app_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetResourceBundleHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for resource bundle events. If
        /// CefSettings.pack_loading_disabled is true (1) a handler must be
        /// returned. If no handler is returned resources will be loaded from
        /// pack files. This function is called on multiple threads.
        /// </summary>
        protected virtual CefResourceBundleHandler GetResourceBundleHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for proxy events. If not handler is returned the
        /// default system handler will be used. This function is called on the
        /// IO thread.
        /// </summary>
        private cef_proxy_handler_t* get_proxy_handler(cef_app_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetProxyHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        protected virtual CefProxyHandler GetProxyHandler()
        {
            return null;
        }
    }
}
