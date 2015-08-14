namespace CefGlue.JSBinding
{
    using System;

    // TODO: rename it to Methods ?

    [Flags]
    internal enum MethodDefAttributes
    {
        None = 0x0,

        /// <summary>
        /// Hidden method.
        /// Hidden methods can't be invoked from script directly.
        /// </summary>
        /// <remarks>
        /// Property accessors (Getter, Setter) must have this flag set.
        /// Note, that V8 extensions ignore this flag if Getter or Setter flag is set.
        /// </remarks>
        Hidden = 0x1,

        /// <summary>
        /// Property getter method.
        /// </summary>
        Getter = 0x2,

        /// <summary>
        /// Property setter method.
        /// </summary>
        Setter = 0x4,

        /// <summary>
        /// Static method.
        /// </summary>
        Static = 0x8,

        /// <summary>
        /// Method has overloads.
        /// </summary>
        HasOverloads = 0x10,

        /// <summary>
        /// Method compiled.
        /// </summary>
        Compiled = 0x20,
    }
}
