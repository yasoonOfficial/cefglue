namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Browser initialization settings. Specify NULL or 0 to get the recommended
    // default values. The consequences of using custom values may not be well
    // tested.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_browser_settings_t
    {
        /// <summary>
        /// Size of this structure.
        /// </summary>
        public int size;

        ///
        // Disable drag & drop of URLs from other windows.
        ///
        public bool_t drag_drop_disabled;

        ///
        // Disable default navigation resulting from drag & drop of URLs.
        ///
        public bool_t load_drops_disabled;

        ///
        // Disable history back/forward navigation.
        ///
        public bool_t history_disabled;

        ///
        // The number of frames per second (fps) for animation and windowless
        // rendering. When window rendering is enabled and the JavaScript
        // requestAnimationFrame method is used the browser client area will be
        // invalidated at the rate specified. When window rendering is disabled the
        // CefRenderHandler::OnPaint() method will be called at the rate specified.
        // This value must be between 0 and 90. Specify a value of zero for the
        // default frame rate of 30 fps. Changing this value may affect display
        // performance and/or CPU usage.
        ///
        public int animation_frame_rate;


        // The below values map to WebPreferences settings.

        ///
        // Font settings.
        ///
        public cef_string_t standard_font_family;
        public cef_string_t fixed_font_family;
        public cef_string_t serif_font_family;
        public cef_string_t sans_serif_font_family;
        public cef_string_t cursive_font_family;
        public cef_string_t fantasy_font_family;
        public Int32 default_font_size;
        public Int32 default_fixed_font_size;
        public Int32 minimum_font_size;
        public Int32 minimum_logical_font_size;

        ///
        // Set to true (1) to disable loading of fonts from remote sources.
        ///
        public bool_t remote_fonts_disabled;

        ///
        // Default encoding for Web content. If empty "ISO-8859-1" will be used.
        ///
        public cef_string_t default_encoding;

        ///
        // Set to true (1) to attempt automatic detection of content encoding.
        ///
        public bool_t encoding_detector_enabled;

        ///
        // Set to true (1) to disable JavaScript.
        ///
        public bool_t javascript_disabled;

        ///
        // Set to true (1) to disallow JavaScript from opening windows.
        ///
        public bool_t javascript_open_windows_disallowed;

        ///
        // Set to true (1) to disallow JavaScript from closing windows.
        ///
        public bool_t javascript_close_windows_disallowed;

        ///
        // Set to true (1) to disallow JavaScript from accessing the clipboard.
        ///
        public bool_t javascript_access_clipboard_disallowed;

        ///
        // Set to true (1) to disable DOM pasting in the editor. DOM pasting also
        // depends on |javascript_cannot_access_clipboard| being false (0).
        ///
        public bool_t dom_paste_disabled;

        ///
        // Set to true (1) to enable drawing of the caret position.
        ///
        public bool_t caret_browsing_enabled;

        ///
        // Set to true (1) to disable Java.
        ///
        public bool_t java_disabled;

        ///
        // Set to true (1) to disable plugins.
        ///
        public bool_t plugins_disabled;

        ///
        // Set to true (1) to allow access to all URLs from file URLs.
        ///
        public bool_t universal_access_from_file_urls_allowed;

        ///
        // Set to true (1) to allow access to file URLs from other file URLs.
        ///
        public bool_t file_access_from_file_urls_allowed;

        ///
        // Set to true (1) to allow risky security behavior such as cross-site
        // scripting (XSS). Use with extreme care.
        ///
        public bool_t web_security_disabled;

        ///
        // Set to true (1) to enable console warnings about XSS attempts.
        ///
        public bool_t xss_auditor_enabled;

        ///
        // Set to true (1) to suppress the network load of image URLs.  A cached
        // image will still be rendered if requested.
        ///
        public bool_t image_load_disabled;

        ///
        // Set to true (1) to shrink standalone images to fit the page.
        ///
        public bool_t shrink_standalone_images_to_fit;

        ///
        // Set to true (1) to disable browser backwards compatibility features.
        ///
        public bool_t site_specific_quirks_disabled;

        ///
        // Set to true (1) to disable resize of text areas.
        ///
        public bool_t text_area_resize_disabled;

        ///
        // Set to true (1) to disable use of the page cache.
        ///
        public bool_t page_cache_disabled;

        ///
        // Set to true (1) to not have the tab key advance focus to links.
        ///
        public bool_t tab_to_links_disabled;

        ///
        // Set to true (1) to disable hyperlink pings (<a ping> and window.sendPing).
        ///
        public bool_t hyperlink_auditing_disabled;

        ///
        // Set to true (1) to enable the user style sheet for all pages.
        // |user_style_sheet_location| must be set to the style sheet URL.
        ///
        public bool_t user_style_sheet_enabled;

        ///
        // Location of the user style sheet. This must be a data URL of the form
        // "data:text/css;charset=utf-8;base64,csscontent" where "csscontent" is the
        // base64 encoded contents of the CSS file.
        ///
        public cef_string_t user_style_sheet_location;

        ///
        // Set to true (1) to disable style sheets.
        ///
        public bool_t author_and_user_styles_disabled;

        ///
        // Set to true (1) to disable local storage.
        ///
        public bool_t local_storage_disabled;

        ///
        // Set to true (1) to disable databases.
        ///
        public bool_t databases_disabled;

        ///
        // Set to true (1) to disable application cache.
        ///
        public bool_t application_cache_disabled;

        ///
        // Set to true (1) to disable WebGL.
        ///
        public bool_t webgl_disabled;

        ///
        // Set to true (1) to enable accelerated compositing. This is turned off by
        // default because the current in-process GPU implementation does not
        // support it correctly.
        ///
        public bool_t accelerated_compositing_enabled;

        ///
        // Set to true (1) to disable accelerated layers. This affects features like
        // 3D CSS transforms.
        ///
        public bool_t accelerated_layers_disabled;

        ///
        // Set to true (1) to disable accelerated video.
        ///
        public bool_t accelerated_video_disabled;

        ///
        // Set to true (1) to disable accelerated 2d canvas.
        ///
        public bool_t accelerated_2d_canvas_disabled;

        ///
        // Set to true (1) to disable accelerated painting.
        ///
        public bool_t accelerated_painting_disabled;

        ///
        // Set to true (1) to disable accelerated filters.
        ///
        public bool_t accelerated_filters_disabled;

        ///
        // Set to true (1) to disable accelerated plugins.
        ///
        public bool_t accelerated_plugins_disabled;

        ///
        // Set to true (1) to disable developer tools (WebKit inspector).
        ///
        public bool_t developer_tools_disabled;

        ///
        // Set to true (1) to enable fullscreen mode.
        ///
        public bool_t fullscreen_enabled;
    }
}
