namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// Cookie information.
    /// </summary>
    public sealed unsafe class CefCookie : IDisposable
    {
        // TODO: cookie can be writable

        internal static CefCookie From(cef_cookie_t* ptr)
        {
            return new CefCookie(ptr);
        }

        private cef_cookie_t* ptr;
        private bool owner;

        public CefCookie()
        {
            this.ptr = cef_cookie_t.Alloc();
            this.owner = true;
        }

        private CefCookie(cef_cookie_t* ptr)
        {
            this.ptr = ptr;
            this.owner = false;
        }

        ~CefCookie()
        {
            if (this.ptr != null && this.owner)
            {
                cef_cookie_t.Free(this.ptr);
            }
            this.ptr = null;
        }

        public void Dispose()
        {
            if (this.ptr != null && this.owner)
            {
                cef_cookie_t.Free(this.ptr);
            }
            this.ptr = null;
            GC.SuppressFinalize(this);
        }

        internal cef_cookie_t* GetNativeHandle()
        {
            return this.ptr;
        }

        /// <summary>
        /// The cookie name.
        /// </summary>
        public string Name
        {
            get
            {
                return cef_string_t.ToString(&this.ptr->name);
            }
            set
            {
                ThrowIfReadOnly();
                cef_string_t.Copy(value, &this.ptr->name);
            }
        }

        /// <summary>
        /// The cookie value.
        /// </summary>
        public string Value
        {
            get
            {
                return cef_string_t.ToString(&this.ptr->value);
            }
            set
            {
                ThrowIfReadOnly();
                cef_string_t.Copy(value, &this.ptr->value);
            }
        }

        /// <summary>
        /// If |domain| is empty a host cookie will be created instead of a domain cookie.
        /// Domain cookies are stored with a leading "." and are visible to sub-domains whereas host cookies are not.
        /// </summary>
        public string Domain
        {
            get
            {
                return cef_string_t.ToString(&this.ptr->value);
            }
            set
            {
                ThrowIfReadOnly();
                cef_string_t.Copy(value, &this.ptr->domain);
            }
        }

        /// <summary>
        /// If |path| is non-empty only URLs at or below the path will get the cookie value.
        /// </summary>
        public string Path
        {
            get
            {
                return cef_string_t.ToString(&this.ptr->path);
            }
            set
            {
                ThrowIfReadOnly();
                cef_string_t.Copy(value, &this.ptr->path);
            }
        }

        /// <summary>
        /// If |secure| is true the cookie will only be sent for HTTPS requests.
        /// </summary>
        public bool Secure
        {
            get
            {
                return this.ptr->secure;
            }
            set
            {
                ThrowIfReadOnly();
                this.ptr->secure = value;
            }
        }

        /// <summary>
        /// If |httponly| is true the cookie will only be sent for HTTP requests.
        /// </summary>
        public bool HttpOnly
        {
            get
            {
                return this.ptr->httponly;
            }
            set
            {
                ThrowIfReadOnly();
                this.ptr->secure = value;
            }
        }

        /// <summary>
        /// The cookie creation date.
        /// This is automatically populated by the system on cookie creation.
        /// </summary>
        public DateTime Creation
        {
            get
            {
                return this.ptr->creation.ToDateTime();
            }
            set
            {
                ThrowIfReadOnly();
                this.ptr->creation = new cef_time_t(value);
            }
        }

        /// <summary>
        /// The cookie last access date.
        /// This is automatically populated by the system on access.
        /// </summary>
        public DateTime LastAccess
        {
            get
            {
                return this.ptr->last_access.ToDateTime();
            }
            set
            {
                ThrowIfReadOnly();
                this.ptr->creation = new cef_time_t(value);
            }
        }

        /// <summary>
        /// The cookie expiration date is only valid if |has_expires| is true.
        /// </summary>
        public bool HasExpires
        {
            get
            {
                return this.ptr->has_expires;
            }
            set
            {
                ThrowIfReadOnly();
                this.ptr->secure = value;
            }
        }

        /// <summary>
        /// The cookie expiration date is only valid if |has_expires| is true.
        /// </summary>
        public DateTime Expires
        {
            get
            {
                // FIXME: check HasExpires value and throw if it is not set?
                return this.ptr->expires.ToDateTime();
            }
            set
            {
                // FIXME: HasExpires value ???
                ThrowIfReadOnly();
                this.ptr->creation = new cef_time_t(value);
            }
        }


        private void ThrowIfReadOnly()
        {
            if (!this.owner) throw new CefException("CefCookie object is in non editable state.");
        }
    }
}
