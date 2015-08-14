namespace CefGlue.Interop
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// CEF string list.
    /// </summary>
    /// <remarks>
    ///<c>cef_string_list*</c> === <c>cef_string_list_t</c>.
    /// </remarks>
    internal struct cef_string_list { }

    internal static unsafe partial class NativeMethods
    {
        /// <summary>
        /// Allocate a new string list.
        /// </summary>
        /// <returns></returns>
        // CEF_EXPORT cef_string_list_t cef_string_list_alloc();
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern cef_string_list* cef_string_list_alloc();

        /// <summary>
        /// Return the number of elements in the string list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        // CEF_EXPORT int cef_string_list_size(cef_string_list_t list);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_list_size(cef_string_list* list);

        /// <summary>
        /// Retrieve the value at the specified zero-based string list index.
        /// Returns true (1) if the value was successfully retrieved.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>This method return string by value (via cef_string_copy).</remarks>
        // CEF_EXPORT int cef_string_list_value(cef_string_list_t list,
        //                                      int index, cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern int cef_string_list_value(cef_string_list* list, int index, ref cef_string_t value);

        /// <summary>
        /// Append a new value at the end of the string list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        // CEF_EXPORT void cef_string_list_append(cef_string_list_t list,
        //                                       const cef_string_t* value);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_list_append(cef_string_list* list, cef_string_t* value);

        /// <summary>
        /// Clear the string list.
        /// </summary>
        /// <param name="list"></param>
        // CEF_EXPORT void cef_string_list_clear(cef_string_list_t list);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_list_clear(cef_string_list* list);

        /// <summary>
        /// Free the string list.
        /// </summary>
        /// <param name="list"></param>
        /// <remarks>Method doen't allow NULLs.</remarks>
        // CEF_EXPORT void cef_string_list_free(cef_string_list_t list);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern void cef_string_list_free(cef_string_list* list);

        /// <summary>
        /// Creates a copy of an existing string list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        // CEF_EXPORT cef_string_list_t cef_string_list_copy(cef_string_list_t list);
        [DllImport(CefDllName, CallingConvention = CefCall)]
        public static extern cef_string_list* cef_string_list_copy(cef_string_list* list);
    }
}
