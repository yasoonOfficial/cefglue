namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    public struct CefKeyInfo
    {
        private int key;
        private bool sysChar;
        private bool imeChar;

        internal unsafe static CefKeyInfo From(cef_key_info_t* ptr)
        {
            return new CefKeyInfo(ptr);
        }

        internal unsafe CefKeyInfo(cef_key_info_t* ptr)
        {
            this.key = ptr->key;
#if WINDOWS
            this.sysChar = ptr->sysChar != 0;
            this.imeChar = ptr->imeChar != 0;
#endif
        }

#if WINDOWS
        public CefKeyInfo(int key, bool sysChar, bool imeChar)
        {
            this.key = key;
            this.sysChar = sysChar;
            this.imeChar = imeChar;
        }
#endif

        public int Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

#if WINDOWS
        public bool SysChar
        {
            get { return this.sysChar; }
            set { this.sysChar = value; }
        }

        public bool ImeChar
        {
            get { return this.imeChar; }
            set { this.imeChar = value; }
        }
#endif

        internal unsafe void To(cef_key_info_t* ptr)
        {
            ptr->key = this.key;
#if WINDOWS
            ptr->sysChar = this.sysChar ? 1 : 0;
            ptr->imeChar = this.imeChar ? 1 : 0;
#endif
        }
    }
}
