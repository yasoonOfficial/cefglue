using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefGlue.Client
{
    class App : CefApp
    {
        protected override void OnRegisterCustomSchemes(CefSchemeRegistrar registrar)
        {
            registrar.AddCustomScheme("res", true, true, false);
        }
    }
}
