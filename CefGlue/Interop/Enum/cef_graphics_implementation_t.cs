namespace CefGlue.Interop
{
    using System;

    internal enum cef_graphics_implementation_t : int
    {
        ANGLE_IN_PROCESS = 0,
        ANGLE_IN_PROCESS_COMMAND_BUFFER,
        DESKTOP_IN_PROCESS,
        DESKTOP_IN_PROCESS_COMMAND_BUFFER,
    }
}
