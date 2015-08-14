namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using CefGlue.Threading;

    public partial class AboutForm : Form, IAboutFormController
    {
        public AboutForm()
        {
            InitializeComponent();

            this.browser.BindJSObject<IAboutFormController>("controller", this);

            // bind to browser 'controller' object, which will be implemented in form, via interface IAboutFormController 
            // TODO: must show other version info... i.e. version info about modules (libcef version, etc...)

            // this operation require frame with completed document and executed on cef ui thread
            // var view = this.browser.GetJSObject<IAboutFormView>("view");
        }

        public void CloseView()
        {
            MainForm.SynchronizationContext.Post((_) => { this.Close(); }, null);
        }

        public void OpenUrl(string url)
        {
            Process.Start(url);
        }

        public string CefGlueVersion
        {
            get
            {
                // FIXME: use AssemblyInformationalVersion from CefGlue assembly
                return Application.ProductVersion;
            }
        }
    }
}
