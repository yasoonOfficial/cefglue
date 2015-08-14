namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// V8 property attribute values.
    /// </summary>
    [Flags]
    public enum CefV8PropertyAttribute
    {
        /// <summary>
        /// Writeable and enumerable.
        /// </summary>
        None = cef_v8_propertyattribute_t.V8_PROPERTY_ATTRIBUTE_NONE,

        /// <summary>
        /// Configurable, not writeable.
        /// </summary>
        ReadOnly = cef_v8_propertyattribute_t.V8_PROPERTY_ATTRIBUTE_READONLY,

        /// <summary>
        /// Configurable, not enumerable.
        /// </summary>
        DontEnum = cef_v8_propertyattribute_t.V8_PROPERTY_ATTRIBUTE_DONTENUM,

        /// <summary>
        /// Not configurable.
        /// </summary>
        DontDelete = cef_v8_propertyattribute_t.V8_PROPERTY_ATTRIBUTE_DONTDELETE,
    }
}
