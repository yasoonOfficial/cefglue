namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class BrowserSettingsForm : Form
    {
        public BrowserSettingsForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.StartPosition = FormStartPosition.Manual;
        }

        public CefBrowserSettings BrowserSettings
        {
            get
            {
                return (CefBrowserSettings)this.propertyGrid.SelectedObject;
            }
            set
            {
                this.propertyGrid.SelectedObject = value;
            }
        }
    }
}
