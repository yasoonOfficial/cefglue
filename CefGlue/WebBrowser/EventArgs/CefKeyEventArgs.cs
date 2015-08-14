namespace CefGlue.WebBrowser
{
    using System;
    using System.ComponentModel;

    public sealed class CefKeyEventArgs : HandledEventArgs
    {
        private readonly CefHandlerKeyEventType type;
        private readonly int code;
        private readonly CefHandlerKeyEventModifiers modifiers;
        private readonly bool isSystemKey;

        public CefKeyEventArgs(CefHandlerKeyEventType type, int code, CefHandlerKeyEventModifiers modifiers, bool isSystemKey)
        {
            this.type = type;
            this.code = code;
            this.modifiers = modifiers;
            this.isSystemKey = isSystemKey;
        }

        public CefHandlerKeyEventType Type
        {
            get { return this.type; }
        }

        public int Code
        {
            get { return this.code; }
        }

        public CefHandlerKeyEventModifiers Modifiers
        {
            get { return this.modifiers; }
        }

        public bool IsSystemKey
        {
            get { return this.isSystemKey; }
        }
    }
}
