namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public sealed partial class CefWebBrowserCore
    {
        /*
        private CefProgressChangedEventArgs CreateProgressChangedEventArgs()
        {
            var progressPercentage = (int)(this.framesCompleted * 100.0 / this.framesTotal);
            if (progressPercentage > 100) progressPercentage = 100;

            return new CefProgressChangedEventArgs(progressPercentage);
        }
        */
    }
}
