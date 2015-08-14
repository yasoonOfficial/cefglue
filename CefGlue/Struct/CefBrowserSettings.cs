namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;

    public sealed unsafe class CefBrowserSettings
    {
        internal static CefBrowserSettings From(cef_browser_settings_t* pointer)
        {
            return new CefBrowserSettings(pointer);
        }

        private cef_browser_settings_t* _ptr;
        private bool _owner;

        public CefBrowserSettings()
        {
            _ptr = cef_browser_settings_t.Alloc();
            _owner = true;
        }

        private CefBrowserSettings(cef_browser_settings_t* pointer)
        {
            _ptr = pointer;
            _owner = false;
        }

        #region IDisposable
        ~CefBrowserSettings()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_ptr != null)
            {
                if (_owner)
                {
                    cef_browser_settings_t.Free(_ptr);
                }
                _ptr = null;
            }
        }
        #endregion

        internal cef_browser_settings_t* NativePointer
        {
            get
            {
                CheckNativePointer();
                return _ptr;
            }
        }

        private void CheckNativePointer()
        {
            if (_ptr == null) ThrowObjectDisposedException();
        }

        private void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException("{0} is disposed.", this.GetType().Name);
        }

        /// <summary>
        /// Disable drag &amp; drop of URLs from other windows.
        /// </summary>
        public bool DragDropDisabled
        {
            get
            {
                return this._ptr->drag_drop_disabled;
            }
            set
            {
                this._ptr->drag_drop_disabled = value;
            }
        }

        /// <summary>
        /// Disable default navigation resulting from drag &amp; drop of URLs.
        /// </summary>
        public bool LoadDropsDisabled
        {
            get
            {
                return this._ptr->load_drops_disabled;
            }
            set
            {
                this._ptr->load_drops_disabled = value;
            }
        }

        // The below values map to WebPreferences settings.

        //
        // Font settings.
        //
        public string StandardFontFamily
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->standard_font_family);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->standard_font_family);
            }
        }

        public string FixedFontFamily
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->fixed_font_family);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->fixed_font_family);
            }
        }

        public string SerifFontFamily
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->serif_font_family);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->serif_font_family);
            }
        }

        public string SansSerifFontFamily
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->sans_serif_font_family);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->sans_serif_font_family);
            }
        }

        public string CursiveFontFamily
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->cursive_font_family);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->cursive_font_family);
            }
        }

        public string FantasyFontFamily
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->fantasy_font_family);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->fantasy_font_family);
            }
        }

        public int DefaultFontSize
        {
            get
            {
                return this._ptr->default_font_size;
            }
            set
            {
                this._ptr->default_font_size = value;
            }
        }

        public int DefaultFixedFontSize
        {
            get
            {
                return this._ptr->default_fixed_font_size;
            }
            set
            {
                this._ptr->default_fixed_font_size = value;
            }
        }

        public int MinimumFontSize
        {
            get
            {
                return this._ptr->minimum_font_size;
            }
            set
            {
                this._ptr->minimum_font_size = value;
            }
        }

        public int MinimumLogicalFontSize
        {
            get
            {
                return this._ptr->minimum_logical_font_size;
            }
            set
            {
                this._ptr->minimum_logical_font_size = value;
            }
        }

        /// <summary>
        /// Set to true to disable loading of fonts from remote sources.
        /// </summary>
        public bool RemoteFontsDisabled
        {
            get
            {
                return this._ptr->remote_fonts_disabled;
            }
            set
            {
                this._ptr->remote_fonts_disabled = value;
            }
        }

        /// <summary>
        /// Default encoding for Web content. If empty "ISO-8859-1" will be used.
        /// </summary>
        public string DefaultEncoding
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->default_encoding);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->default_encoding);
            }
        }

        /// <summary>
        /// Set to true to attempt automatic detection of content encoding.
        /// </summary>
        public bool EncodingDetectorEnabled
        {
            get
            {
                return this._ptr->encoding_detector_enabled;
            }
            set
            {
                this._ptr->encoding_detector_enabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable JavaScript.
        /// </summary>
        public bool JavaScriptDisabled
        {
            get
            {
                return this._ptr->javascript_disabled;
            }
            set
            {
                this._ptr->javascript_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disallow JavaScript from opening windows.
        /// </summary>
        public bool JavaScriptOpenWindowsDisallowed
        {
            get
            {
                return this._ptr->javascript_open_windows_disallowed;
            }
            set
            {
                this._ptr->javascript_open_windows_disallowed = value;
            }
        }

        /// <summary>
        /// Set to true to disallow JavaScript from closing windows.
        /// </summary>
        public bool JavaScriptCloseWindowsDisallowed
        {
            get
            {
                return this._ptr->javascript_close_windows_disallowed;
            }
            set
            {
                this._ptr->javascript_close_windows_disallowed = value;
            }
        }

        /// <summary>
        /// Set to true to disallow JavaScript from accessing the clipboard.
        /// </summary>
        public bool JavaScriptAccessClipboardDisallowed
        {
            get
            {
                return this._ptr->javascript_access_clipboard_disallowed;
            }
            set
            {
                this._ptr->javascript_access_clipboard_disallowed = value;
            }
        }

        /// <summary>
        /// Set to true to disable DOM pasting in the editor. DOM pasting also
        /// depends on |javascript_cannot_access_clipboard| being false (0).
        /// </summary>
        public bool DomPasteDisabled
        {
            get
            {
                return this._ptr->dom_paste_disabled;
            }
            set
            {
                this._ptr->dom_paste_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to enable drawing of the caret position.
        /// </summary>
        public bool CaretBrowsingEnabled
        {
            get
            {
                return this._ptr->caret_browsing_enabled;
            }
            set
            {
                this._ptr->caret_browsing_enabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable Java.
        /// </summary>
        public bool JavaDisabled
        {
            get
            {
                return this._ptr->java_disabled;
            }
            set
            {
                this._ptr->java_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable plugins.
        /// </summary>
        public bool PluginsDisabled
        {
            get
            {
                return this._ptr->plugins_disabled;
            }
            set
            {
                this._ptr->plugins_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to allow access to all URLs from file URLs.
        /// </summary>
        public bool UniversalAccessFromFileUrlsAllowed
        {
            get
            {
                return this._ptr->universal_access_from_file_urls_allowed;
            }
            set
            {
                this._ptr->universal_access_from_file_urls_allowed = value;
            }
        }

        /// <summary>
        /// Set to true to allow access to file URLs from other file URLs.
        /// </summary>
        public bool FileAccessFromFileUrlsAllowed
        {
            get
            {
                return this._ptr->file_access_from_file_urls_allowed;
            }
            set
            {
                this._ptr->file_access_from_file_urls_allowed = value;
            }
        }

        /// <summary>
        /// Set to true to allow risky security behavior such as cross-site scripting (XSS).
        /// Use with extreme care.
        /// </summary>
        public bool WebSecurityDisabled
        {
            get
            {
                return this._ptr->web_security_disabled;
            }
            set
            {
                this._ptr->web_security_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to enable console warnings about XSS attempts.
        /// </summary>
        public bool XssAuditorEnabled
        {
            get
            {
                return this._ptr->xss_auditor_enabled;
            }
            set
            {
                this._ptr->xss_auditor_enabled = value;
            }
        }

        /// <summary>
        /// Set to true to suppress the network load of image URLs.
        /// A cached image will still be rendered if requested.
        /// </summary>
        public bool ImageLoadDisabled
        {
            get
            {
                return this._ptr->image_load_disabled;
            }
            set
            {
                this._ptr->image_load_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to shrink standalone images to fit the page.
        /// </summary>
        public bool ShrinkStandaloneImagesToFit
        {
            get
            {
                return this._ptr->shrink_standalone_images_to_fit;
            }
            set
            {
                this._ptr->shrink_standalone_images_to_fit = value;
            }
        }

        /// <summary>
        /// Set to true to disable browser backwards compatibility features.
        /// </summary>
        public bool SiteSpecificQuirksDisabled
        {
            get
            {
                return this._ptr->site_specific_quirks_disabled;
            }
            set
            {
                this._ptr->site_specific_quirks_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable resize of text areas.
        /// </summary>
        public bool TextAreaResizeDisabled
        {
            get
            {
                return this._ptr->text_area_resize_disabled;
            }
            set
            {
                this._ptr->text_area_resize_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable use of the page cache.
        /// </summary>
        public bool PageCacheDisabled
        {
            get
            {
                return this._ptr->page_cache_disabled;
            }
            set
            {
                this._ptr->page_cache_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to not have the tab key advance focus to links.
        /// </summary>
        public bool TabToLinksDisabled
        {
            get
            {
                return this._ptr->tab_to_links_disabled;
            }
            set
            {
                this._ptr->tab_to_links_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable hyperlink pings (&lt;a ping&gt; and window.sendPing).
        /// </summary>
        public bool HyperlinkAuditingDisabled
        {
            get
            {
                return this._ptr->hyperlink_auditing_disabled;
            }
            set
            {
                this._ptr->hyperlink_auditing_disabled = value;
            }
        }

        /// <summary>
        /// Location of the user style sheet. This must be a data URL of the form
        /// "data:text/css;charset=utf-8;base64,csscontent" where "csscontent" is the
        /// base64 encoded contents of the CSS file.
        /// </summary>
        public bool UserStyleSheetEnabled
        {
            get
            {
                return this._ptr->user_style_sheet_enabled;
            }
            set
            {
                this._ptr->user_style_sheet_enabled = value;
            }
        }

        public string UserStyleSheetLocation
        {
            get
            {
                return cef_string_t.ToString(&this._ptr->user_style_sheet_location);
            }
            set
            {
                cef_string_t.Copy(value, &this._ptr->user_style_sheet_location);
            }
        }

        /// <summary>
        /// Set to true to disable style sheets.
        /// </summary>
        public bool AuthorAndUserStylesDisabled
        {
            get
            {
                return this._ptr->author_and_user_styles_disabled;
            }
            set
            {
                this._ptr->author_and_user_styles_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable local storage.
        /// </summary>
        public bool LocalStorageDisabled
        {
            get
            {
                return this._ptr->local_storage_disabled;
            }
            set
            {
                this._ptr->local_storage_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable databases.
        /// </summary>
        public bool DatabasesDisabled
        {
            get
            {
                return this._ptr->databases_disabled;
            }
            set
            {
                this._ptr->databases_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable application cache.
        /// </summary>
        public bool ApplicationCacheDisabled
        {
            get
            {
                return this._ptr->application_cache_disabled;
            }
            set
            {
                this._ptr->application_cache_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable WebGL.
        /// </summary>
        public bool WebGLDisabled
        {
            get
            {
                return this._ptr->webgl_disabled;
            }
            set
            {
                this._ptr->webgl_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to enable accelerated compositing.
        /// </summary>
        public bool AcceleratedCompositingEnabled
        {
            get
            {
                return this._ptr->accelerated_compositing_enabled;
            }
            set
            {
                this._ptr->accelerated_compositing_enabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable accelerated layers.
        /// This affects features like 3D CSS transforms.
        /// </summary>
        public bool AcceleratedLayersDisabled
        {
            get
            {
                return this._ptr->accelerated_layers_disabled;
            }
            set
            {
                this._ptr->accelerated_layers_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable accelerated video.
        /// </summary>
        public bool AcceleratedVideoDisabled
        {
            get
            {
                return this._ptr->accelerated_video_disabled;
            }
            set
            {
                this._ptr->accelerated_video_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable accelerated 2d canvas.
        /// </summary>
        public bool Accelerated2DCanvasDisabled
        {
            get
            {
                return this._ptr->accelerated_2d_canvas_disabled;
            }
            set
            {
                this._ptr->accelerated_2d_canvas_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable accelerated plugins.
        /// </summary>
        public bool AcceleratedPluginsDisabled
        {
            get
            {
                return this._ptr->accelerated_plugins_disabled;
            }
            set
            {
                this._ptr->accelerated_plugins_disabled = value;
            }
        }

        /// <summary>
        /// Set to true to disable developer tools (WebKit inspector).
        /// </summary>
        public bool DeveloperToolsDisabled
        {
            get
            {
                return this._ptr->developer_tools_disabled;
            }
            set
            {
                this._ptr->developer_tools_disabled = value;
            }
        }
    }
}
