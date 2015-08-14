namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefCookieVisitor
    {
        /// <summary>
        /// Method that will be called once for each cookie. |count| is the
        /// 0-based index for the current cookie. |total| is the total number of
        /// cookies. Set |deleteCookie| to true to delete the cookie currently
        /// being visited. Return false to stop visiting cookies. This method may
        /// never be called if no cookies are found.
        /// </summary>
        private int visit(cef_cookie_visitor_t* self, /*const*/ cef_cookie_t* cookie, int count, int total, int* deleteCookie)
        {
            ThrowIfObjectDisposed();

            var m_cookie = CefCookie.From(cookie);
            bool m_deleteCookie;

            var handled = this.Visit(m_cookie, count, total, out m_deleteCookie);

            m_cookie.Dispose();

            *deleteCookie = m_deleteCookie ? 1 : 0;

            return handled ? 1 : 0;
        }

        /// <summary>
        /// Method that will be called once for each cookie.
        /// |count| is the 0-based index for the current cookie.
        /// |total| is the total number of cookies.
        /// Set |deleteCookie| to true to delete the cookie currently being visited.
        /// Return false to stop visiting cookies.
        /// This method may never be called if no cookies are found.
        /// </summary>
        protected virtual bool Visit(CefCookie cookie, int count, int total, out bool deleteCookie)
        {
            deleteCookie = false;
            return false;
        }


    }
}
