namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    public struct CefRect
    {
        private int x;
        private int y;
        private int width;
        private int height;

        internal unsafe static CefRect From(cef_rect_t* ptr)
        {
            return new CefRect(ptr);
        }

        internal unsafe CefRect(cef_rect_t* ptr)
        {
            this.x = ptr->x;
            this.y = ptr->y;
            this.width = ptr->width;
            this.height = ptr->height;
        }

        public CefRect(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        internal unsafe void To(cef_rect_t* ptr)
        {
            ptr->x = this.x;
            ptr->y = this.y;
            ptr->width = this.width;
            ptr->height = this.height;
        }
    }
}
