namespace CefGlue.Windows.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using CefGlue.WebBrowser;

    [ToolboxBitmap(typeof(CefWebBrowser))]
    public partial class CefWebBrowser : Control
    {
        private CefWebBrowserCore core;

        private bool handleCreatedHandled;

        public CefWebBrowser()
            : this(new CefBrowserSettings())
        {
        }

        public CefWebBrowser(CefBrowserSettings settings)
            : this(settings, null)
        {
        }

        public CefWebBrowser(string startUrl)
            : this(new CefBrowserSettings(), startUrl)
        {
        }

        public CefWebBrowser(CefBrowserSettings settings, string startUrl)
        {
            this.core = new CefWebBrowserCore(this, settings, startUrl);
            this.core.Created += new EventHandler(core_Created);

            this.SetStyle(
                ControlStyles.ContainerControl
                | ControlStyles.ResizeRedraw
                | ControlStyles.FixedWidth
                | ControlStyles.FixedHeight
                | ControlStyles.StandardClick
                | ControlStyles.UserMouse
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.StandardDoubleClick
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.CacheText
                | ControlStyles.EnableNotifyMessage
                | ControlStyles.DoubleBuffer
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UseTextForAccessibility
                | ControlStyles.Opaque,
                false);

            this.SetStyle(
                ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.Selectable,
                true);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (this.DesignMode)
            {
                if (!this.handleCreatedHandled)
                {
                    this.Paint += new PaintEventHandler(this.DesignModePaint);
                }
            }
            else
            {
                if (this.handleCreatedHandled)
                {
                    throw new InvalidOperationException("Handle already created.");
                }

                // TODO: fix when browser still creating, but control already disposed
                // it can be done via setting client to detached state, and onaftercreated
                // force browser to be closed.

                var windowInfo = new CefWindowInfo();
#if WINDOWS
                windowInfo.SetAsChild(this.Handle, 0, 0, this.Width, this.Height);
#else
#error Only windows supported.
#endif
                this.core.Create(windowInfo);

                windowInfo.Dispose();
            }

            this.handleCreatedHandled = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResizeWindow(this.core.WindowHandle, this.Width, this.Height);
        }

        private void core_Created(object sender, EventArgs e)
        {
            ResizeWindow(this.core.WindowHandle, this.Width, this.Height);
        }

        private void DesignModePaint(object sender, PaintEventArgs e)
        {
            var width = this.Width;
            var height = this.Height;
            if (width > 1 && height > 1)
            {
                var brush = new SolidBrush(this.ForeColor);
                var pen = new Pen(this.ForeColor);
                pen.DashStyle = DashStyle.Dash;

                e.Graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);

                var fontHeight = (int)(this.Font.GetHeight(e.Graphics) * 1.25);

                var x = 3;
                var y = 3;

                e.Graphics.DrawString("CefWebBrowser", this.Font, brush, x, y + (0 * fontHeight));
                e.Graphics.DrawString(string.Format("StartUrl: {0}", this.StartUrl), this.Font, brush, x, y + (1 * fontHeight));

                brush.Dispose();
                pen.Dispose();
            }
        }

        private static void ResizeWindow(IntPtr handle, int width, int height)
        {
            if (handle != IntPtr.Zero)
            {
                NativeMethods.SetWindowPos(handle, IntPtr.Zero,
                    0, 0, width, height,
                    SetWindowPosFlags.NoMove | SetWindowPosFlags.NoZOrder
                    );
            }
        }
    }
}
