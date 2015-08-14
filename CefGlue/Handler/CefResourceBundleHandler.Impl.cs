namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefResourceBundleHandler
    {
        /// <summary>
        /// Called to retrieve a localized translation for the string specified
        /// by |message_id|. To provide the translation set |string| to the
        /// translation string and return true (1). To use the default
        /// translation return false (0).
        ///
        /// WARNING: Be cautious when implementing this function. ID values are
        /// auto- generated when CEF is built and may change between versions.
        /// Existing ID values can be discovered by searching for *_strings.h in
        /// the "obj/global_intermediate" build output directory.
        /// </summary>
        private int get_localized_string(cef_resource_bundle_handler_t* self, int message_id, cef_string_t* @string)
        {
            ThrowIfObjectDisposed();

            var m_string = cef_string_t.ToString(@string);

            var localizedString = this.GetLocalizedString(message_id, m_string);

            if (localizedString != null)
            {
                cef_string_t.Set(localizedString, @string, true);
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Called to retrieve a localized translation for the string specified
        /// by |message_id|. Return null to use the default translation.
        ///
        /// WARNING: Be cautious when implementing this function. ID values are
        /// auto- generated when CEF is built and may change between versions.
        /// Existing ID values can be discovered by searching for *_strings.h in
        /// the "obj/global_intermediate" build output directory.
        /// </summary>
        protected virtual string GetLocalizedString(int messageId, string @string)
        {
            return null;
        }

        /// <summary>
        /// Called to retrieve data for the resource specified by |resource_id|.
        /// To provide the resource data set |data| and |data_size| to the data
        /// pointer and size respectively and return true (1). To use the default
        /// resource data return false (0). The resource data will not be copied
        /// and must remain resident in memory.
        ///
        /// WARNING: Be cautious when implementing this function. ID values are
        /// auto- generated when CEF is built and may change between versions.
        /// Existing ID values can be discovered by searching for *_resources.h
        /// in the "obj/global_intermediate" build output directory.
        /// </summary>
        private int get_data_resource(cef_resource_bundle_handler_t* self, int resource_id, void** data, int* data_size)
        {
            ThrowIfObjectDisposed();
            // TODO: Implement GetDataResource()
            throw new NotImplementedException();
        }
    }
}
