namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using CefGlue.Interop;
    using Diagnostics;

    // TODO: all handlers require optimization like this:
    // now we implement all client properties, it means that native side will always do native callback to query attached handlers
    // but we can omit this by analysing descendant classes for property overrides
    // or analyse property results if we assume that client have const handlers
    // and put NULL to field callback
    // or simply pass to base constructor enumeration with enabled features

    unsafe partial class CefClient
    {
        /// <summary>
        /// Return the handler for browser life span events.
        /// </summary>
        private cef_life_span_handler_t* get_life_span_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetLifeSpanHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for browser life span events.
        /// </summary>
        protected virtual CefLifeSpanHandler GetLifeSpanHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for browser load status events.
        /// </summary>
        private cef_load_handler_t* get_load_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetLoadHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for browser load status events.
        /// </summary>
        protected virtual CefLoadHandler GetLoadHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for browser request events.
        /// </summary>
        private cef_request_handler_t* get_request_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetRequestHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for browser request events.
        /// </summary>
        protected virtual CefRequestHandler GetRequestHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for browser display state events.
        /// </summary>
        private cef_display_handler_t* get_display_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetDisplayHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for browser display state events.
        /// </summary>
        protected virtual CefDisplayHandler GetDisplayHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for focus events.
        /// </summary>
        private cef_focus_handler_t* get_focus_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetFocusHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for focus events.
        /// </summary>
        protected virtual CefFocusHandler GetFocusHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for keyboard events.
        /// </summary>
        private cef_keyboard_handler_t* get_keyboard_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetKeyboardHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for keyboard events.
        /// </summary>
        protected virtual CefKeyboardHandler GetKeyboardHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for context menu events.
        /// </summary>
        private cef_menu_handler_t* get_menu_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetMenuHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for context menu events.
        /// </summary>
        protected virtual CefMenuHandler GetMenuHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for printing events.
        /// </summary>
        private cef_print_handler_t* get_print_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetPrintHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for printing events.
        /// </summary>
        protected virtual CefPrintHandler GetPrintHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for permission events.
        /// </summary>
        private cef_permission_handler_t* get_permission_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetPermissionHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for permission events.
        /// </summary>
        protected virtual CefPermissionHandler GetPermissionHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for find result events.
        /// </summary>
        private cef_find_handler_t* get_find_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetFindHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for find result events.
        /// </summary>
        protected virtual CefFindHandler GetFindHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for JavaScript dialog events.
        /// </summary>
        private cef_jsdialog_handler_t* get_jsdialog_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetJSDialogHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for JavaScript dialog events.
        /// </summary>
        protected virtual CefJSDialogHandler GetJSDialogHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for JavaScript binding events.
        /// </summary>
        private cef_v8context_handler_t* get_v8context_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetV8ContextHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for JavaScript binding events.
        /// </summary>
        protected virtual CefV8ContextHandler GetV8ContextHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for off-screen rendering events.
        /// </summary>
        private cef_render_handler_t* get_render_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetRenderHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for off-screen rendering events.
        /// </summary>
        protected virtual CefRenderHandler GetRenderHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for drag events.
        /// </summary>
        private cef_drag_handler_t* get_drag_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetDragHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for drag events.
        /// </summary>
        protected virtual CefDragHandler GetDragHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for geolocation permissions requests. If no handler is
        /// provided geolocation access will be denied by default.
        /// </summary>
        private cef_geolocation_handler_t* get_geolocation_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetGeolocationHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for geolocation permissions requests. If no handler is
        /// provided geolocation access will be denied by default.
        /// </summary>
        protected virtual CefGeolocationHandler GetGeolocationHandler()
        {
            return null;
        }

        /// <summary>
        /// Return the handler for zoom events. If no handler is provided the default
        /// zoom behavior will be used.
        /// </summary>
        private cef_zoom_handler_t* get_zoom_handler(cef_client_t* self)
        {
            ThrowIfObjectDisposed();

            var handler = GetZoomHandler();
            return handler == null ? null : handler.GetNativePointerAndAddRef();
        }

        /// <summary>
        /// Return the handler for zoom events. If no handler is provided the default
        /// zoom behavior will be used.
        /// </summary>
        protected virtual CefZoomHandler GetZoomHandler()
        {
            return null;
        }
    }
}
