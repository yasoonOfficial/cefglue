namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Structure representing geoposition information. The properties of this
    // structure correspond to those of the JavaScript Position object although
    // their types may differ.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_geoposition_t
    {
        ///
        // Latitude in decimal degrees north (WGS84 coordinate frame).
        ///
        double latitude;

        ///
        // Longitude in decimal degrees west (WGS84 coordinate frame).
        ///
        double longitude;

        ///
        // Altitude in meters (above WGS84 datum).
        ///
        double altitude;

        ///
        // Accuracy of horizontal position in meters.
        ///
        double accuracy;

        ///
        // Accuracy of altitude in meters.
        ///
        double altitude_accuracy;

        ///
        // Heading in decimal degrees clockwise from true north.
        ///
        double heading;

        ///
        // Horizontal component of device velocity in meters per second.
        ///
        double speed;

        ///
        // Time of position measurement in miliseconds since Epoch in UTC time. This
        // is taken from the host computer's system clock.
        ///
        cef_time_t timestamp;

        ///
        // Error code, see enum above.
        ///
        cef_geoposition_error_code_t error_code;

        ///
        // Human-readable error message.
        ///
        cef_string_t error_message;

        public static void Clear(cef_geoposition_t* self)
        {
            cef_string_t.Clear(&self->error_message);
        }
    }
}
