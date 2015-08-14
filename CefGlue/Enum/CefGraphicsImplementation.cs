namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    public enum CefGraphicsImplementation
    {
        AngleInProcess = cef_graphics_implementation_t.ANGLE_IN_PROCESS,
        AngleInProcessCommandBuffer = cef_graphics_implementation_t.ANGLE_IN_PROCESS_COMMAND_BUFFER,
        DesktopInProcess = cef_graphics_implementation_t.DESKTOP_IN_PROCESS,
        DesktopInProcessCommandBuffer = cef_graphics_implementation_t.DESKTOP_IN_PROCESS_COMMAND_BUFFER,
    }
}
