namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // Geoposition error codes.
    ///
    internal enum cef_geoposition_error_code_t : int
    {
        GEOPOSITON_ERROR_NONE = 0,
        GEOPOSITON_ERROR_PERMISSION_DENIED,
        GEOPOSITON_ERROR_POSITION_UNAVAILABLE,
        GEOPOSITON_ERROR_TIMEOUT,
    }
}
