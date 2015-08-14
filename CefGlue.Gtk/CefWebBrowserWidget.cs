namespace CefGlue.GtkSharp
{
    using System;
    using Gtk;
    using CefGlue;
    using CefGlue.WebBrowser;

    // TODO: Gtk.Box or Gtk.Bin ?
    public class CefWebBrowserWidget : Gtk.Box
    {
        private CefWebBrowserCore core;

        private int x;
        private int y;
        private int width;
        private int height;

        private bool created;

        public CefWebBrowserWidget()
        {
            Console.WriteLine("CefWebBrowserWidget() inside");
        }

        protected override void OnRealized ()
        {
            base.OnRealized ();

            Console.WriteLine("OnRealized2");

            this.core = new CefWebBrowserCore(this, new CefBrowserSettings(), "http://google.com");
            this.core.Created += HandleCoreCreated;
            this.ChildVisible = true;

            var windowInfo = new CefWindowInfo();

#if WINDOWS
#error WINDOWS is not supported currently.
            // TODO: Win32
            // windowInfo.SetAsChild(
            //    gdk_win32_drawable_get_handle(this.GdkWindow.Handle), this.x, this.y, this.width, this.height
            //    );
#elif LINUX
            windowInfo.SetAsChild(
                this.Raw
                );
#else
#error OS not supported.
#endif

            this.core.Create(windowInfo);
            windowInfo.Dispose();
        }

        void HandleCoreCreated (object sender, EventArgs e)
        {
            // this.ShowAll();
            Console.WriteLine("HandleCoreCreated");
            Console.WriteLine("Children.Length: {0}", this.Children.Length);
            this.Children[0].Visible = true;
        }

        /*

     protected override void OnSizeAllocated (Gdk.Rectangle allocation)
     {
         if (this.x != allocation.X
             || this.y != allocation.Y
             || this.width != allocation.Width
             || this.height != allocation.Height
             )
         {
             this.x = allocation.X;
             this.y = allocation.Y;
             this.width = allocation.Width;
             this.height = allocation.Height;
             Console.WriteLine("OnSizeAllocated {0}x{1}", this.width, this.height);
             if (this.created) {
                 ResizeWindow(this.core.WindowHandle, this.width, this.height);
             }
         }
     }
     
     protected override void OnActivate ()
     {
         Console.WriteLine("OnActivate");
         base.OnActivate ();
     }
     
     protected override void OnFocusGrabbed ()
     {
         Console.WriteLine("OnFocusGrabbed");
         base.OnFocusGrabbed ();
     }
     
     protected override void OnFocusChildSet (global::Gtk.Widget widget)
     {
         Console.WriteLine("OnFocusChildSet");
         base.OnFocusChildSet (widget);
     }
     
     protected override bool OnFocusInEvent (Gdk.EventFocus evnt)
     {
         Console.WriteLine("OnFocusInEvent");
         return base.OnFocusInEvent (evnt);
     }
     
     protected override bool OnFocusOutEvent (Gdk.EventFocus evnt)
     {
         Console.WriteLine("OnFocusOutEvent");
         return base.OnFocusOutEvent (evnt);
     }
     
         [DllImport("libgdk-win32-2.0-0.dll", CallingConvention = CallingConvention.Cdecl)]
     internal static extern IntPtr gdk_win32_drawable_get_handle(IntPtr raw);
     
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
     
     private void core_Created (object sender, EventArgs e)
     {
         this.created = true;
     }
     
        private static void ResizeWindow(IntPtr handle, int width, int height)
        {
            if (handle != IntPtr.Zero)
            {
                SetWindowPos(handle, IntPtr.Zero,
                    0, 0, width, height,
                    SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder
                    );
            }
        }
            */
    }
}

