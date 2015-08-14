namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    public sealed class CefProgressChangedEventArgs : ProgressChangedEventArgs
    {
        private readonly bool exact;

        public CefProgressChangedEventArgs(int progressPercentage)
            : this(progressPercentage, false)
        { }

        public CefProgressChangedEventArgs(int progressPercentage, bool exact)
            : base(progressPercentage, null)
        {
            this.exact = exact;
        }

        public bool Exact
        {
            get { return this.exact; }
        }
    }
}
