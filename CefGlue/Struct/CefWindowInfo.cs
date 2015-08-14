namespace CefGlue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.Interop;

    public sealed unsafe class CefWindowInfo : IDisposable
    {
        internal static CefWindowInfo From(cef_window_info_t* pointer)
        {
            return new CefWindowInfo(pointer);
        }

        private cef_window_info_t* _ptr;
        private bool _owner;

        public CefWindowInfo()
        {
            _ptr = cef_window_info_t.Alloc();
            _owner = true;
        }

        private CefWindowInfo(cef_window_info_t* pointer)
        {
            _ptr = pointer;
            _owner = false;
        }

        #region IDisposable
        ~CefWindowInfo()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_ptr != null)
            {
                if (_owner)
                {
                    cef_window_info_t.Free(_ptr);
                }
                _ptr = null;
            }
        }
        #endregion

        internal cef_window_info_t* NativePointer
        {
            get
            {
                CheckNativePointer();
                return _ptr;
            }
        }

        private void CheckNativePointer()
        {
            if (_ptr == null) ThrowObjectDisposedException();
        }

        private void ThrowObjectDisposedException()
        {
            throw new ObjectDisposedException("{0} is disposed.", this.GetType().Name);
        }

#if WINDOWS
        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        internal WindowStylesEx ExStyle
        {
            get { return NativePointer->m_dwExStyle; }
            set { NativePointer->m_dwExStyle = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public string WindowName
        {
            get { return cef_string_t.ToString(&NativePointer->m_windowName); }
            set { cef_string_t.Copy(value, &NativePointer->m_windowName); }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        internal WindowStyles Style
        {
            get { return NativePointer->m_dwStyle; }
            set { NativePointer->m_dwStyle = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public int X
        {
            get { return NativePointer->m_x; }
            set { NativePointer->m_x = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public int Y
        {
            get { return NativePointer->m_y; }
            set { NativePointer->m_y = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public int Width
        {
            get { return NativePointer->m_nWidth; }
            set { NativePointer->m_nWidth = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public int Height
        {
            get { return NativePointer->m_nHeight; }
            set { NativePointer->m_nHeight = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public IntPtr ParentWindowHandle
        {
            get { return NativePointer->m_hWndParent; }
            set { NativePointer->m_hWndParent = value; }
        }

        /// <summary>
        /// Standard parameter required by CreateWindowEx().
        /// </summary>
        public IntPtr MenuHandle
        {
            get { return NativePointer->m_hMenu; }
            set { NativePointer->m_hMenu = value; }
        }

        /// <summary>
        /// If window rendering is disabled no browser window will be created.
        /// Set |m_hWndParent| to the window that will act as the parent for popup menus, dialog boxes, etc.
        /// </summary>
        public bool WindowRenderingDisabled
        {
            get { return NativePointer->m_bWindowRenderingDisabled != 0; }
            set { NativePointer->m_bWindowRenderingDisabled = value ? 1 : 0; }
        }

        /// <summary>
        /// Set to true to enable transparent painting.
        /// </summary>
        public bool TransparentPainting
        {
            get { return NativePointer->m_bTransparentPainting != 0; }
            set { NativePointer->m_bTransparentPainting = value ? 1 : 0; }
        }

        /// <summary>
        /// Handle for the new browser window.
        /// </summary>
        public IntPtr WindowHandle
        {
            get { return NativePointer->m_hWnd; }
            set { NativePointer->m_hWnd = value; }
        }

        public void SetAsChild(IntPtr parentWindowHandle, int x, int y, int width, int height)
        {
            Style = WindowStyles.WS_CHILD
                | WindowStyles.WS_CLIPCHILDREN
                | WindowStyles.WS_CLIPSIBLINGS
                | WindowStyles.WS_TABSTOP
                | WindowStyles.WS_VISIBLE;
            ParentWindowHandle = parentWindowHandle;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void SetAsPopup(IntPtr parentWindowHandle, string windowName)
        {
            Style = WindowStyles.WS_OVERLAPPEDWINDOW
                | WindowStyles.WS_CLIPCHILDREN
                | WindowStyles.WS_CLIPSIBLINGS
                | WindowStyles.WS_VISIBLE;
            ParentWindowHandle = parentWindowHandle;
            X = NativeMethods.CW_USEDEFAULT;
            Y = NativeMethods.CW_USEDEFAULT;
            Width = NativeMethods.CW_USEDEFAULT;
            Height = NativeMethods.CW_USEDEFAULT;

            WindowName = windowName;
        }

        public void SetAsOffScreen(IntPtr parentWindowHandle)
        {
            WindowRenderingDisabled = true;
            ParentWindowHandle = parentWindowHandle;
        }
#elif LINUX
        public void SetAsChild(IntPtr parentWindowHandle)
        {
            this._ptr->m_ParentWidget = (cef_window_handle*)parentWindowHandle;
        }
#endif

    }
}
