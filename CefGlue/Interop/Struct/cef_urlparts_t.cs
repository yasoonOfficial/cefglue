namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// URL component parts.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_urlparts_t
    {
        ///
        // The complete URL specification.
        ///
        public cef_string_t spec;

        ///
        // Scheme component not including the colon (e.g., "http").
        ///
        public cef_string_t scheme;

        ///
        // User name component.
        ///
        public cef_string_t username;

        ///
        // Password component.
        ///
        public cef_string_t password;

        ///
        // Host component. This may be a hostname, an IPv4 address or an IPv6 literal
        // surrounded by square brackets (e.g., "[2001:db8::1]").
        ///
        public cef_string_t host;

        ///
        // Port number component.
        ///
        public cef_string_t port;

        ///
        // Path component including the first slash following the host.
        ///
        public cef_string_t path;

        ///
        // Query string component (i.e., everything following the '?').
        ///
        public cef_string_t query;


        public static void Clear(cef_urlparts_t* self)
        {
            cef_string_t.Clear(&self->spec);
            cef_string_t.Clear(&self->scheme);
            cef_string_t.Clear(&self->username);
            cef_string_t.Clear(&self->password);
            cef_string_t.Clear(&self->host);
            cef_string_t.Clear(&self->port);
            cef_string_t.Clear(&self->path);
            cef_string_t.Clear(&self->query);
        }
    }
}
