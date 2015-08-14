//
// Validate preprocessor directives.
//

#if OS_DEFINED
    #error OS_DEFINED must not be defined directly.
#endif

#if WINDOWS
    #if OS_DEFINED
        #error Only one target operation system can be defined in one time.
    #endif
    #define OS_DEFINED
#endif

#if LINUX
    #if OS_DEFINED
        #error Only one target operation system can be defined in one time.
    #endif
    #define OS_DEFINED
#endif

#if MACOSX
    #if OS_DEFINED
        #error Only one target operation system can be defined in one time.
    #endif
    #define OS_DEFINED
#endif

#if !OS_DEFINED
    #error Target operation system is not defined. Must be defined one of WINDOWS, LINUX or MACOSX.
#endif

