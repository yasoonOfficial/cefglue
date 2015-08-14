namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal interface IAboutFormController
    {
        void CloseView();

        void OpenUrl(string url);
        string CefGlueVersion { get; }
    }
}
