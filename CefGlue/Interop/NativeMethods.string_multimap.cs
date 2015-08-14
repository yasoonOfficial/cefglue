namespace CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// CEF string map.
    /// </summary>
    /// <remarks>
    ///<c>cef_string_multimap*</c> === <c>cef_string_multimap_t</c>.
    /// </remarks>
    internal struct cef_string_multimap { }

    internal static unsafe partial class NativeMethods
    {
        /// <summary>
        /// Allocate a new string multimap.
        /// </summary>
        // CEF_EXPORT cef_string_multimap_t cef_string_multimap_alloc();
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern cef_string_multimap* cef_string_multimap_alloc();

        /// <summary>
        /// Return the number of elements in the string multimap.
        /// </summary>
        // CEF_EXPORT int cef_string_multimap_size(cef_string_multimap_t map);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_multimap_size(cef_string_multimap* map);

        /// <summary>
        /// Return the number of values with the specified key.
        /// </summary>
        // CEF_EXPORT int cef_string_multimap_find_count(cef_string_multimap_t map,
        //                                               const cef_string_t* key);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_multimap_find_count(cef_string_multimap* map, /* const */ cef_string_t* key);

        /// <summary>
        /// Return the value_index-th value with the specified key.
        /// </summary>
        // CEF_EXPORT int cef_string_multimap_enumerate(cef_string_multimap_t map,
        //                                              const cef_string_t* key,
        //                                              int value_index,
        //                                              cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_multimap_enumerate(cef_string_multimap* map, /* const */ cef_string_t* key, int value_index, cef_string_t* value);

        /// <summary>
        /// Return the key at the specified zero-based string multimap index.
        /// </summary>
        // CEF_EXPORT int cef_string_multimap_key(cef_string_multimap_t map, int index,
        //                                        cef_string_t* key);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_multimap_key(cef_string_multimap* map, int index, cef_string_t* key);

        /// <summary>
        /// Return the value at the specified zero-based string multimap index.
        /// </summary>
        // CEF_EXPORT int cef_string_multimap_value(cef_string_multimap_t map, int index,
        //                                          cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_multimap_value(cef_string_multimap* map, int index, cef_string_t* value);

        /// <summary>
        /// Append a new key/value pair at the end of the string multimap.
        /// </summary>
        // CEF_EXPORT int cef_string_multimap_append(cef_string_multimap_t map,
        //                                           const cef_string_t* key,
        //                                           const cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_multimap_append(cef_string_multimap* map, /* const */ cef_string_t* key, /* const */ cef_string_t* value);

        /// <summary>
        /// Clear the string multimap.
        /// </summary>
        // CEF_EXPORT void cef_string_multimap_clear(cef_string_multimap_t map);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_multimap_clear(cef_string_multimap* map);

        /// <summary>
        /// Free the string multimap.
        /// </summary>
        // CEF_EXPORT void cef_string_multimap_free(cef_string_multimap_t map);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_multimap_free(cef_string_multimap* map);
    }
}

