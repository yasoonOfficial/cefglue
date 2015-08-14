namespace CefGlue.JSBinding
{
    using System;

    [Flags]
    public enum JSBindingOptions
    {
        None = 0,

        /// <summary>
        /// Register object using v8 extension.
        /// </summary>
        /// <remarks>
        /// Objects registered as extension will be bound to any browser or frame, and not depended from <c>Popups</c> or <c>Frames</c> flag.
        /// </remarks>
        Extension = 0x1,

        /// <summary>
        /// Lazy compilation of method marshallers.
        /// </summary>
        /// <remarks>
        /// Use this with caution:
        /// 
        /// By default, all method marshallers will be compiled when you get object binder (register object).
        /// In this case if compilation fails - you got exception and binder will not be created (object will not be registered).
        /// 
        /// If you set LazyCompile options - method marshallers will be compiled, when actuall call performed.
        /// In this case if compilation fails - you got exception of method invokation (i.e. called method throw exception).
        /// </remarks>
        LazyCompile = 0x2,

        /// <summary>
        /// If this flag is set, then object will be bound to popup windows.
        /// </summary>
        Popups = 0x4,
        // TODO: JSBindingOptions.Popups

        /// <summary>
        /// If this flag is set, then object will be bound to all frames in browser.
        /// </summary>
        Frames = 0x8,
        // TODO: JSBindingOptions.Frames

        /// <summary>
        /// Bind public methods.
        /// </summary>
        Public = 0x10, // TODO: Rename it to Explicit - it means that only marked with JSBind methods/props will be collected

        // TODO: ScriptableObjectOptions.Popups support
        // TODO: ScriptableObjectOptions.Frames support
        // TODO: validating of ScriptableObjectOptions
    }
}
