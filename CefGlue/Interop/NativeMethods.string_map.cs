namespace CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// CEF string map.
    /// </summary>
    /// <remarks>
    ///<c>cef_string_map*</c> === <c>cef_string_map_t</c>.
    /// </remarks>
    internal struct cef_string_map { }

    internal static unsafe partial class NativeMethods
    {
        /// <summary>
        /// Allocate a new string map.
        /// </summary>
        /// <returns></returns>
        // CEF_EXPORT cef_string_map_t cef_string_map_alloc();
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern cef_string_map* cef_string_map_alloc();

        ///
        /// Return the number of elements in the string map.
        ///
        // CEF_EXPORT int cef_string_map_size(cef_string_map_t map);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_map_size(cef_string_map* map);

        ///
        /// Return the value assigned to the specified key.
        ///
        // CEF_EXPORT int cef_string_map_find(cef_string_map_t map, const cef_string_t* key, cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_map_find(cef_string_map* map, /* const */ cef_string_t* key, cef_string_t* value);

        ///
        /// Return the key at the specified zero-based string map index.
        ///
        // CEF_EXPORT int cef_string_map_key(cef_string_map_t map, int index, cef_string_t* key);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_map_key(cef_string_map* map, int index, cef_string_t* key);

        ///
        /// Return the value at the specified zero-based string map index.
        ///
        // CEF_EXPORT int cef_string_map_value(cef_string_map_t map, int index, cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_map_value(cef_string_map* map, int index, cef_string_t* value);

        ///
        /// Append a new key/value pair at the end of the string map.
        ///
        // CEF_EXPORT int cef_string_map_append(cef_string_map_t map, const cef_string_t* key, const cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_map_append(cef_string_map* map, /* const */ cef_string_t* key, /* const */ cef_string_t* value);

        ///
        /// Clear the string map.
        ///
        // CEF_EXPORT void cef_string_map_clear(cef_string_map_t map);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_map_clear(cef_string_map* map);

        ///
        /// Free the string map.
        ///
        // CEF_EXPORT void cef_string_map_free(cef_string_map_t map);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_map_free(cef_string_map* map);
    }
}
