namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using CefGlue.Interop;

    unsafe partial class CefCookieManager
    {
        /// <summary>
        /// Returns the global cookie manager. By default data will be stored at
        /// CefSettings.CachePath if specified or in memory otherwise.
        /// </summary>
        public static CefCookieManager GetGlobalManager()
        {
            return CefCookieManager.From(NativeMethods.cef_cookie_manager_get_global_manager());
        }

        /// <summary>
        /// Creates a new cookie manager. If |path| is empty data will be stored in
        /// memory only. Returns NULL if creation fails.
        /// </summary>
        public static CefCookieManager CreateManager(string path)
        {
            fixed (char* path_str = path)
            {
                var m_path = new cef_string_t(path_str, path != null ? path.Length : 0);
                var m_cookie_manager = NativeMethods.cef_cookie_manager_create_manager(&m_path);
                return CefCookieManager.FromOrDefault(m_cookie_manager);
            }
        }

        /// <summary>
        /// Set the schemes supported by this manager. By default only "http" and
        /// "https" schemes are supported. Must be called before any cookies are
        /// accessed.
        /// </summary>
        public void SetSupportedSchemes(IEnumerable<string> schemes)
        {
            cef_cookie_manager_t.invoke_set_supported_schemes(this.ptr, CefStringList.CreateHandle(schemes));
        }

        /// <summary>
        /// Visit all cookies. The returned cookies are ordered by longest path, then
        /// by earliest creation date. Returns false if cookies cannot be accessed.
        /// </summary>
        public bool VisitAllCookies(CefCookieVisitor visitor)
        {
            return cef_cookie_manager_t.invoke_visit_all_cookies(this.ptr, visitor.GetNativePointerAndAddRef()) != 0;
        }

        /// <summary>
        /// Visit a subset of cookies. The results are filtered by the given url
        /// scheme, host, domain and path. If |includeHttpOnly| is true HTTP-only
        /// cookies will also be included in the results. The returned cookies are
        /// ordered by longest path, then by earliest creation date. Returns false if
        /// cookies cannot be accessed.
        /// </summary>
        public bool VisitUrlCookies(string url, bool includeHttpOnly, CefCookieVisitor visitor)
        {
            fixed (char* url_str = url)
            {
                var m_url = new cef_string_t(url_str, url != null ? url.Length : 0);
                return cef_cookie_manager_t.invoke_visit_url_cookies(this.ptr, &m_url, includeHttpOnly ? 1 : 0, visitor.GetNativePointerAndAddRef()) != 0;
            }
        }

        /// <summary>
        /// Sets a cookie given a valid URL and explicit user-provided cookie
        /// attributes. This function expects each attribute to be well-formed. It will
        /// check for disallowed characters (e.g. the ';' character is disallowed
        /// within the cookie value attribute) and will return false without setting
        /// the cookie if such characters are found. This method must be called on the
        /// IO thread.
        /// </summary>
        public bool SetCookie(string url, CefCookie cookie)
        {
            fixed (char* url_str = url)
            {
                var m_url = new cef_string_t(url_str, url != null ? url.Length : 0);
                return cef_cookie_manager_t.invoke_set_cookie(this.ptr, &m_url, cookie.GetNativeHandle()) != 0;
            }
        }

        /// <summary>
        /// Delete all cookies that match the specified parameters. If both |url| and
        /// values |cookieName| are specified all host and domain cookies matching
        /// both will be deleted. If only |url| is specified all host cookies (but not
        /// domain cookies) irrespective of path will be deleted. If |url| is empty all
        /// cookies for all hosts and domains will be deleted. Returns false if a non-
        /// empty invalid URL is specified or if cookies cannot be accessed. This
        /// method must be called on the IO thread.
        /// </summary>
        public bool DeleteCookies(string url, string cookieName)
        {
            fixed (char* url_str = url)
            {
                var m_url = new cef_string_t(url_str, url != null ? url.Length : 0);
                fixed (char* cookieName_str = cookieName)
                {
                    var m_cookieName = new cef_string_t(cookieName_str, cookieName != null ? cookieName.Length : 0);
                    return cef_cookie_manager_t.invoke_delete_cookies(this.ptr, &m_url, &m_cookieName) != 0;
                }
            }
        }

        /// <summary>
        /// Sets the directory path that will be used for storing cookie data. If
        /// |path| is empty data will be stored in memory only. Returns false if
        /// cookies cannot be accessed.
        /// </summary>
        public bool SetStoragePath(string path)
        {
            fixed (char* path_str = path)
            {
                var m_path = new cef_string_t(path_str, path != null ? path.Length : 0);
                return cef_cookie_manager_t.invoke_set_storage_path(this.ptr, &m_path) != 0;
            }
        }
    }
}
