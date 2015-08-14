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
    using CefGlue.Windows.Forms;
    using CefGlue.WebBrowser;

    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
        }

        public void BindData(BindingList<ConsoleMessageEventArgs> data)
        {
            this.dataGridView.DataSource = data;
        }
    }
}
