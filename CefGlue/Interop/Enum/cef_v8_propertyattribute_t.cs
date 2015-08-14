namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // V8 property attribute values.
    ///
    internal enum cef_v8_propertyattribute_t : int
    {
        V8_PROPERTY_ATTRIBUTE_NONE = 0,       // Writeable, Enumerable, 
        //   Configurable
        V8_PROPERTY_ATTRIBUTE_READONLY = 1 << 0,  // Not writeable
        V8_PROPERTY_ATTRIBUTE_DONTENUM = 1 << 1,  // Not enumerable
        V8_PROPERTY_ATTRIBUTE_DONTDELETE = 1 << 2   // Not configurable
    }
}
