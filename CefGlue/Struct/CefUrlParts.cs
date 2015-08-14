namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// URL component parts.
    /// </summary>
    public sealed class CefUrlParts
    {
        public static CefUrlParts Parse(string url)
        {
            return new CefUrlParts(url);
        }

        public CefUrlParts()
        {
        }

        public unsafe CefUrlParts(string url)
        {
            ParseUrl(url);
        }

        private unsafe void ParseUrl(string url)
        {
            if (url == null) throw new ArgumentNullException("url");

            cef_urlparts_t n_urlparts;
            try
            {
                fixed (char* url_str = url)
                {
                    var n_url = new cef_string_t(url_str, url.Length);

                    if (NativeMethods.cef_parse_url(&n_url, &n_urlparts) == 0)
                    {
                        throw new ArgumentException("Invalid url.");
                    }

                    this.Spec = cef_string_t.ToString(&n_urlparts.spec);
                    this.Scheme = cef_string_t.ToString(&n_urlparts.scheme);
                    this.UserName = cef_string_t.ToString(&n_urlparts.username);
                    this.Password = cef_string_t.ToString(&n_urlparts.password);
                    this.Host = cef_string_t.ToString(&n_urlparts.host);
                    this.Port = cef_string_t.ToString(&n_urlparts.port);
                    this.Path = cef_string_t.ToString(&n_urlparts.path);
                    this.Query = cef_string_t.ToString(&n_urlparts.query);
                }
            }
            finally
            {
                cef_urlparts_t.Clear(&n_urlparts);
            }
        }

        public override unsafe string ToString()
        {
            var spec = this.Spec;
            var scheme = this.Scheme;
            var username = this.UserName;
            var password = this.Password;
            var host = this.Host;
            var port = this.Port;
            var path = this.Path;
            var query = this.Query;

            fixed (char* spec_str = spec)
            fixed (char* scheme_str = scheme)
            fixed (char* username_str = username)
            fixed (char* password_str = password)
            fixed (char* host_str = host)
            fixed (char* port_str = port)
            fixed (char* path_str = path)
            fixed (char* query_str = query)
            {
                cef_urlparts_t n_urlparts;
                n_urlparts.spec = new cef_string_t(spec_str, spec != null ? spec.Length : 0);
                n_urlparts.scheme = new cef_string_t(scheme_str, scheme != null ? scheme.Length : 0);
                n_urlparts.username = new cef_string_t(username_str, username != null ? username.Length : 0);
                n_urlparts.password = new cef_string_t(password_str, password != null ? password.Length : 0);
                n_urlparts.host = new cef_string_t(host_str, host != null ? host.Length : 0);
                n_urlparts.port = new cef_string_t(port_str, port != null ? port.Length : 0);
                n_urlparts.path = new cef_string_t(path_str, path != null ? path.Length : 0);
                n_urlparts.query = new cef_string_t(query_str, query != null ? query.Length : 0);

                cef_string_t n_url;
                try
                {
                    if (NativeMethods.cef_create_url(&n_urlparts, &n_url) != 0)
                    {
                        return cef_string_t.ToString(&n_url);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
                finally
                {
                    cef_string_t.Clear(&n_url);
                }
            }
        }

        /// <summary>
        /// The complete URL specification.
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// Scheme component not including the colon (e.g., "http").
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// User name component.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password component.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Host component.
        /// This may be a hostname, an IPv4 address or an IPv6 literal surrounded by square brackets (e.g., "[2001:db8::1]").
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port number component.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Path component including the first slash following the host.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Query string component (i.e., everything following the '?').
        /// </summary>
        public string Query { get; set; }

    }
}
