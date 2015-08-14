namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefV8Value
    {
        /// <summary>
        /// Create a new CefV8Value object of type undefined.
        /// </summary>
        public static CefV8Value CreateUndefined()
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_undefined()
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type null.
        /// </summary>
        public static CefV8Value CreateNull()
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_null()
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type bool.
        /// </summary>
        public static CefV8Value CreateBool(bool value)
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_bool(value ? 1 : 0)
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type int.
        /// </summary>
        public static CefV8Value CreateInt(int value)
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_int(value)
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type unsigned int.
        /// </summary>
        public static CefV8Value CreateUInt(uint value)
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_uint(value)
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type double.
        /// </summary>
        public static CefV8Value CreateDouble(double value)
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_double(value)
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type Date.
        /// </summary>
        public static CefV8Value CreateDate(DateTime value)
        {
            cef_time_t n_date = new cef_time_t(value);
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_date(&n_date)
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type string.
        /// </summary>
        public static CefV8Value CreateString(string value)
        {
            fixed (char* value_str = value)
            {
                var n_value = new cef_string_t(value_str, value != null ? value.Length : 0);
                return CefV8Value.From(
                    NativeMethods.cef_v8value_create_string(&n_value)
                    );
            }
        }

        /// <summary>
        /// Create a new CefV8Value object of type object.
        /// </summary>
        public static CefV8Value CreateObject()
        {
            return CreateObject(null);
        }

        /// <summary>
        /// Create a new CefV8Value object of type object with accessors.
        /// </summary>
        public static CefV8Value CreateObject(CefV8Accessor accessor)
        {
            return CefV8Value.From(NativeMethods.cef_v8value_create_object(
                    accessor != null ? accessor.GetNativePointerAndAddRef() : null
                ));
        }

        /// <summary>
        /// Create a new CefV8Value object of type array.
        /// </summary>
        public static CefV8Value CreateArray(int length)
        {
            return CefV8Value.From(
                NativeMethods.cef_v8value_create_array(length)
                );
        }

        /// <summary>
        /// Create a new CefV8Value object of type function.
        /// </summary>
        public static CefV8Value CreateFunction(string name, CefV8Handler handler)
        {
            fixed (char* name_str = name)
            {
                var n_name = new cef_string_t(name_str, name != null ? name.Length : 0);

                return CefV8Value.From(
                    NativeMethods.cef_v8value_create_function(&n_name, handler.GetNativePointerAndAddRef())
                    );
            }
        }

        /// <summary>
        /// Returns true if this object is valid. Do not call any other methods if this
        /// method returns false.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return cef_v8value_t.invoke_is_valid(this.ptr) != 0;
            }
        }

        private void ThrowIfObjectIsInvalid()
        {
            if (!this.IsValid)
                throw new InvalidOperationException();
        }

        /// <summary>
        /// True if the value type is undefined.
        /// </summary>
        public bool IsUndefined
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_undefined(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is null.
        /// </summary>
        public bool IsNull
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_null(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is bool.
        /// </summary>
        public bool IsBool
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_bool(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is int.
        /// </summary>
        public bool IsInt
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_int(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is unsigned int.
        /// </summary>
        public bool IsUInt
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_uint(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is double.
        /// </summary>
        public bool IsDouble
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_double(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is Date.
        /// </summary>
        public bool IsDate
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_date(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is string.
        /// </summary>
        public bool IsString
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_string(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is object.
        /// </summary>
        public bool IsObject
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_object(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is array.
        /// </summary>
        public bool IsArray
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_array(this.ptr) != 0;
            }
        }

        /// <summary>
        /// True if the value type is function.
        /// </summary>
        public bool IsFunction
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_function(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if this is a user created object.
        /// </summary>
        public bool IsUserCreated
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_is_user_created(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if the last method call resulted in an exception. This
        /// attribute exists only in the scope of the current CEF value object.
        /// </summary>
        public bool HasException
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_has_exception(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if this object will re-throw future exceptions. This attribute
        /// exists only in the scope of the current CEF value object.
        /// </summary>
        public bool WillRethrowExceptions
        {
            get
            {
                ThrowIfObjectIsInvalid();
                return cef_v8value_t.invoke_will_rethrow_exceptions(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if this object is pointing to the same handle as |that| object.
        /// </summary>
        public bool IsSame(CefV8Value that)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_is_same(this.ptr, that.GetNativePointerAndAddRef()) != 0;
        }

        /// <summary>
        /// Return a bool value.
        /// The underlying data will be converted to if necessary.
        /// </summary>
        public bool GetBoolValue()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_get_bool_value(this.ptr) != 0;
        }

        /// <summary>
        /// Return an int value.
        /// The underlying data will be converted to if necessary.
        /// </summary>
        public int GetIntValue()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_get_int_value(this.ptr);
        }

        /// <summary>
        /// Return an unsigned int value.
        /// The underlying data will be converted to if necessary.
        /// </summary>
        public uint GetUIntValue()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_get_uint_value(this.ptr);
        }

        /// <summary>
        /// Return a double value.
        /// The underlying data will be converted to if necessary.
        /// </summary>
        public double GetDoubleValue()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_get_double_value(this.ptr);
        }

        /// <summary>
        /// Return a Date value.
        /// The underlying data will be converted to if necessary.
        /// </summary>
        public DateTime GetDateValue()
        {
            ThrowIfObjectIsInvalid();
            var n_result = cef_v8value_t.invoke_get_date_value(this.ptr);
            return n_result.ToDateTime();
        }

        /// <summary>
        /// Return a string value.
        /// The underlying data will be converted to if necessary.
        /// </summary>
        public string GetStringValue()
        {
            ThrowIfObjectIsInvalid();
            var nResult = cef_v8value_t.invoke_get_string_value(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }


        // OBJECT METHODS - These methods are only available on objects. Arrays
        // and functions are also objects. String- and integer-based keys can be
        // used interchangably with the framework converting between them as
        // necessary. Keys beginning with "Cef::" and "v8::" are reserved by the
        // system.

        /// <summary>
        /// Returns true if the object has a value with the specified identifier.
        /// </summary>
        public bool HasValue(string key)
        {
            ThrowIfObjectIsInvalid();
            fixed (char* key_str = key)
            {
                var n_key = new cef_string_t(key_str, key != null ? key.Length : 0);

                return cef_v8value_t.invoke_has_value_bykey(this.ptr, &n_key) != 0;
            }
        }

        /// <summary>
        /// Returns true if the object has a value with the specified identifier.
        /// </summary>
        public bool HasValue(int index)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_has_value_byindex(this.ptr, index) != 0;
        }

        /// <summary>
        /// Delete the value with the specified identifier and returns true on
        /// success. Returns false if this method is called incorrectly or an exception
        /// is thrown. For read-only and don't-delete values this method will return
        /// true even though deletion failed.
        /// </summary>
        public bool DeleteValue(string key)
        {
            ThrowIfObjectIsInvalid();
            fixed (char* key_str = key)
            {
                var n_key = new cef_string_t(key_str, key != null ? key.Length : 0);

                return cef_v8value_t.invoke_delete_value_bykey(this.ptr, &n_key) != 0;
            }
        }

        /// <summary>
        /// Delete the value with the specified identifier and returns true on
        /// success. Returns false if this method is called incorrectly, deletion fails
        /// or an exception is thrown. For read-only and don't-delete values this
        /// method will return true even though deletion failed.
        /// </summary>
        public bool DeleteValue(int index)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_delete_value_byindex(this.ptr, index) != 0;
        }

        /// <summary>
        /// Returns the value with the specified identifier on success. Returns null
        /// if this method is called incorrectly or an exception is thrown.
        /// </summary>
        public CefV8Value GetValue(string key)
        {
            ThrowIfObjectIsInvalid();
            fixed (char* key_str = key)
            {
                var n_key = new cef_string_t(key_str, key != null ? key.Length : 0);

                return CefV8Value.From(
                    cef_v8value_t.invoke_get_value_bykey(this.ptr, &n_key)
                    );
            }
        }

        /// <summary>
        /// Returns the value with the specified identifier on success. Returns null
        /// if this method is called incorrectly or an exception is thrown.
        /// </summary>
        public CefV8Value GetValue(int index)
        {
            ThrowIfObjectIsInvalid();
            return CefV8Value.From(
                   cef_v8value_t.invoke_get_value_byindex(this.ptr, index)
                   );
        }

        /// <summary>
        /// Associates a value with the specified identifier and returns true on
        /// success. Returns false if this method is called incorrectly or an exception
        /// is thrown. For read-only values this method will return true even though
        /// assignment failed.
        /// </summary>
        public bool SetValue(string key, CefV8Value value, CefV8PropertyAttribute attribute = CefV8PropertyAttribute.None)
        {
            ThrowIfObjectIsInvalid();
            fixed (char* key_str = key)
            {
                var n_key = new cef_string_t(key_str, key != null ? key.Length : 0);

                return cef_v8value_t.invoke_set_value_bykey(this.ptr, &n_key, value.GetNativePointerAndAddRef(), (cef_v8_propertyattribute_t)attribute) != 0;
            }
        }

        /// <summary>
        /// Associates a value with the specified identifier and returns true on
        /// success. Returns false if this method is called incorrectly or an exception
        /// is thrown. For read-only values this method will return true even though
        /// assignment failed.
        /// </summary>
        public bool SetValue(int index, CefV8Value value)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_set_value_byindex(this.ptr, index, value.GetNativePointerAndAddRef()) != 0;
        }

        /// <summary>
        /// Registers an identifier and returns true on success. Access to the
        /// identifier will be forwarded to the CefV8Accessor instance passed to
        /// CefV8Value::CreateObject(). Returns false if this method is called
        /// incorrectly or an exception is thrown. For read-only values this method
        /// will return true even though assignment failed.
        /// </summary>
        public bool SetValue(string key, CefV8AccessControl settings, CefV8PropertyAttribute attribute)
        {
            ThrowIfObjectIsInvalid();
            fixed (char* key_str = key)
            {
                var n_key = new cef_string_t(key_str, key != null ? key.Length : 0);

                return cef_v8value_t.invoke_set_value_byaccessor(this.ptr, &n_key, (cef_v8_accesscontrol_t)settings, (cef_v8_propertyattribute_t)attribute) != 0;
            }
        }

        /// <summary>
        /// Read the keys for the object's values into the specified vector.
        /// Integer- based keys will also be returned as strings.
        /// </summary>
        public bool TryGetKeys(out CefStringList keys)
        {
            ThrowIfObjectIsInvalid();
            var nList = CefStringList.CreateHandle();

            var success = cef_v8value_t.invoke_get_keys(this.ptr, nList) != 0;

            if (success)
            {
                keys = CefStringList.From(nList, true);
                return true;
            }
            else
            {
                CefStringList.DestroyHandle(nList);
                keys = null;
                return false;
            }
        }

        /// <summary>
        /// Read the keys for the object's values into the specified vector.
        /// Integer- based keys will also be returned as strings.
        /// </summary>
        public CefStringList GetKeys()
        {
            ThrowIfObjectIsInvalid();
            CefStringList keys;
            if (TryGetKeys(out keys))
            {
                return keys;
            }
            else throw new CefException("CefV8Value.GetKeys failed.");
        }

        /// <summary>
        /// Sets the user data for this object and returns true on success. Returns
        /// false if this method is called incorrectly. This method can only be called
        /// on user created objects.
        /// </summary>
        public bool SetUserData(CefUserData userData)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_set_user_data(
                this.ptr,
                (cef_base_t*)userData.GetNativePointerAndAddRef()
            ) != 0;
        }

        /// <summary>
        /// Returns the user data, if any, specified when the object was created.
        /// </summary>
        public CefUserData GetUserData()
        {
            ThrowIfObjectIsInvalid();
            var n_base = cef_v8value_t.invoke_get_user_data(this.ptr);
            if (n_base == null) return null;
            return CefUserData.FromOrDefault((cefglue_userdata_t*)n_base);
        }

        /// <summary>
        /// Returns the amount of externally allocated memory registered for the
        /// object.
        /// </summary>
        public int GetExternallyAllocatedMemory()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_get_externally_allocated_memory(this.ptr);
        }

        /// <summary>
        /// Adjusts the amount of registered external memory for the object. Used to
        /// give V8 an indication of the amount of externally allocated memory that is
        /// kept alive by JavaScript objects. V8 uses this information to decide when
        /// to perform global garbage collection. Each CefV8Value tracks the amount of
        /// external memory associated with it and automatically decreases the global
        /// total by the appropriate amount on its destruction. |change_in_bytes|
        /// specifies the number of bytes to adjust by. This method returns the number
        /// of bytes associated with the object after the adjustment. This method can
        /// only be called on user created objects.
        /// </summary>
        public int AdjustExternallyAllocatedMemory(int change_in_bytes)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_adjust_externally_allocated_memory(this.ptr, change_in_bytes);
        }


        // ARRAY METHODS - These methods are only available on arrays.

        /// <summary>
        /// Returns the number of elements in the array.
        /// </summary>
        public int GetArrayLength()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_get_array_length(this.ptr);
        }


        // FUNCTION METHODS - These methods are only available on functions.

        /// <summary>
        /// Returns the function name.
        /// </summary>
        public string GetFunctionName()
        {
            ThrowIfObjectIsInvalid();
            var nResult = cef_v8value_t.invoke_get_function_name(this.ptr);
            return cef_string_userfree.GetStringAndFree(nResult);
        }

        /// <summary>
        /// Returns the function handler or NULL if not a CEF-created function.
        /// </summary>
        public CefV8Handler GetFunctionHandler()
        {
            ThrowIfObjectIsInvalid();
            return CefV8Handler.FromOrDefault(
                cef_v8value_t.invoke_get_function_handler(this.ptr)
            );
        }

        /// <summary>
        /// Returns the exception resulting from the last method call. This attribute
        /// exists only in the scope of the current CEF value object.
        /// </summary>
        public CefV8Exception GetException()
        {
            ThrowIfObjectIsInvalid();
            return CefV8Exception.FromOrDefault(
                cef_v8value_t.invoke_get_exception(this.ptr)
            );
        }

        /// <summary>
        /// Clears the last exception and returns true on success.
        /// </summary>
        public bool ClearException()
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_clear_exception(this.ptr) != 0;
        }

        /// <summary>
        /// Set whether this object will re-throw future exceptions. By default
        /// exceptions are not re-thrown. If a exception is re-thrown the current
        /// context should not be accessed again until after the exception has been
        /// caught and not re-thrown. Returns true on success. This attribute exists
        /// only in the scope of the current CEF value object.
        /// </summary>
        public bool SetRethrowExceptions(bool rethrow)
        {
            ThrowIfObjectIsInvalid();
            return cef_v8value_t.invoke_set_rethrow_exceptions(this.ptr, rethrow ? 1 : 0) != 0;
        }

        /// <summary>
        /// Execute the function using the current V8 context. This method should only
        /// be called from within the scope of a CefV8Handler or CefV8Accessor
        /// callback, or in combination with calling Enter() and Exit() on a stored
        /// CefV8Context reference. |object| is the receiver ('this' object) of the
        /// function. If |object| is empty the current context's global object will be
        /// used. |arguments| is the list of arguments that will be passed to the
        /// function. Returns the function return value on success. Returns NULL if
        /// this method is called incorrectly or an exception is thrown.
        /// </summary>
        public CefV8Value ExecuteFunction(CefV8Value obj, CefV8Value[] arguments)
        {
            ThrowIfObjectIsInvalid();
            var n_arguments = CreateArgumentsArray(arguments);

            fixed (cef_v8value_t** n_arguments_ptr = n_arguments)
            {
                return CefV8Value.FromOrDefault(
                    cef_v8value_t.invoke_execute_function(
                        this.ptr,
                        obj.GetNativePointerAndAddRef(),
                        n_arguments != null ? n_arguments.Length : 0,
                        n_arguments_ptr
                    )
                );
            }
        }

        /// <summary>
        /// Execute the function using the specified V8 context. |object| is the
        /// receiver ('this' object) of the function. If |object| is empty the
        /// specified context's global object will be used. |arguments| is the list of
        /// arguments that will be passed to the function. Returns the function return
        /// value on success. Returns NULL if this method is called incorrectly or an
        /// exception is thrown.
        /// </summary>
        public CefV8Value ExecuteFunctionWithContext(CefV8Context context, CefV8Value obj, CefV8Value[] arguments)
        {
            ThrowIfObjectIsInvalid();
            var n_arguments = CreateArgumentsArray(arguments);

            fixed (cef_v8value_t** n_arguments_ptr = n_arguments)
            {
                return CefV8Value.FromOrDefault(
                    cef_v8value_t.invoke_execute_function_with_context(
                        this.ptr,
                        context.GetNativePointerAndAddRef(),
                        obj.GetNativePointerAndAddRef(),
                        n_arguments != null ? n_arguments.Length : 0,
                        n_arguments_ptr
                    )
                );
            }
        }

        private static cef_v8value_t*[] CreateArgumentsArray(CefV8Value[] arguments)
        {
            if (arguments == null) return null;

            var length = arguments.Length;
            if (length == 0) return null;

            var result = new cef_v8value_t*[arguments.Length];

            for (var i = 0; i < length; i++)
            {
                result[i] = arguments[i].GetNativePointerAndAddRef();
            }

            return result;
        }
    }
}
