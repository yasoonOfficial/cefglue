namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;

    public sealed unsafe class CefPopupFeatures : IDisposable
    {
        internal static CefPopupFeatures From(cef_popup_features_t* ptr)
        {
            return new CefPopupFeatures(ptr);
        }

        private cef_popup_features_t* ptr;

        private CefPopupFeatures(cef_popup_features_t* ptr)
        {
            this.ptr = ptr;
        }

        ~CefPopupFeatures()
        {
            this.ptr = null;
        }

        public void Dispose()
        {
            this.ptr = null;
            GC.SuppressFinalize(this);
        }

        internal cef_popup_features_t* NativePointer
        {
            get
            {
                CheckNativePointer();
                return ptr;
            }
        }

        private void CheckNativePointer()
        {
            if (ptr == null) ThrowObjectDisposedException();
        }

        private void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException("{0} is disposed.", this.GetType().Name);
        }

        public int? X
        {
            get
            {
                return this.ptr->xSet ? (int?)this.ptr->x : null;
            }
            set
            {
                if (value != null)
                {
                    this.ptr->x = (int) value;
                    this.ptr->xSet = true;
                }
            }
        }

        public int? Y
        {
            get
            {
                return this.ptr->ySet ? (int?)this.ptr->y : null;
            }
        }

        public int? Width
        {
            get
            {
                return this.ptr->widthSet ? (int?)this.ptr->width : null;
            }
            set
            {
                if (value != null)
                {
                    this.ptr->width = (int) value;
                    this.ptr->widthSet = true;
                }
            }
        }

        public int? Height
        {
            get
            {
                return this.ptr->heightSet ? (int?)this.ptr->height : null;
            }
        }

        public bool MenuBarVisible
        {
            get
            {
                return this.ptr->menuBarVisible;
            }
        }

        public bool StatusBarVisible
        {
            get
            {
                return this.ptr->statusBarVisible;
            }
        }

        public bool ToolBarVisible
        {
            get
            {
                return this.ptr->toolBarVisible;
            }
        }

        public bool LocationBarVisible
        {
            get
            {
                return this.ptr->locationBarVisible;
            }
        }

        public bool ScrollbarsVisible
        {
            get
            {
                return this.ptr->scrollbarsVisible;
            }
        }

        public bool Resizable
        {
            get
            {
                return this.ptr->resizable;
            }
        }

        public bool Fullscreen
        {
            get
            {
                return this.ptr->fullscreen;
            }
        }

        public bool Dialog
        {
            get
            {
                return this.ptr->dialog;
            }
        }

        public IEnumerable<string> AdditionalFeatures
        {
            get
            {
                if (this.ptr->additionalFeatures != null)
                {
                    return CefStringList.From(this.ptr->additionalFeatures)
                        .AsEnumerable();
                }
                return Enumerable.Empty<string>();
            }
        }
    }
}
