namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// V8 access control values.
    /// </summary>
    [Flags]
    public enum CefV8AccessControl
    {
        Default = cef_v8_accesscontrol_t.V8_ACCESS_CONTROL_DEFAULT,
        AllCanRead = cef_v8_accesscontrol_t.V8_ACCESS_CONTROL_ALL_CAN_READ,
        AllCanWrite = cef_v8_accesscontrol_t.V8_ACCESS_CONTROL_ALL_CAN_WRITE,
        ProhibitsOverwriting = cef_v8_accesscontrol_t.V8_ACCESS_CONTROL_PROHIBITS_OVERWRITING,
    }
}
