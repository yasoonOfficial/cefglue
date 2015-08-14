namespace CefGlue.TestShell
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using CefGlue.Threading;
    using CefGlue.WebBrowser;

    public partial class MainForm : Form
    {
        // TODO: move it out to Program/Main class.
        public static SynchronizationContext SynchronizationContext { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            // Get current synchronization context to interact with WinForms UI thread.
            MainForm.SynchronizationContext = SynchronizationContext.Current;

            var browser = (IWebBrowser)this.browser;

            browser.StartUrl = "http://www.google.com";
            //browser.StartUrl = "http://www.google.comqwe";
            //browser.StartUrl = "http://news.bbc.co.uk";
            //browser.StartUrl = "http://cefglue.dmitriid.com/doc/";
            //browser.StartUrl = "file://" + Environment.CurrentDirectory + "/event_flow.html";
            //browser.StartUrl = "file://" + Environment.CurrentDirectory + "/subframe_loading.html";
            //browser.StartUrl = "file://" + Environment.CurrentDirectory + "/window_location.html";
            //browser.StartUrl = "file://" + Environment.CurrentDirectory + "/xhr_test.html";
            //browser.StartUrl = "about:blank";
            //browser.StartUrl = "http://www.quackit.com/html/templates/frames/frames_example_5.html";
            //browser.StartUrl = "http://www.quackit.com/html/templates/download/grunge/03/";
            //browser.StartUrl = "http://localhost:12312";

            browser.Created += new EventHandler(browser_Created);
            browser.AddressChanged += new EventHandler(browser_AddressChanged);
            browser.TitleChanged += new EventHandler(browser_TitleChanged);
            browser.CanGoBackChanged += new EventHandler(browser_CanGoBackChanged);
            browser.CanGoForwardChanged += new EventHandler(browser_CanGoForwardChanged);
            browser.ConsoleMessage += new EventHandler<ConsoleMessageEventArgs>(browser_ConsoleMessage);
            browser.Navigating += new EventHandler<CefNavigatingEventArgs>(browser_Navigating);
            browser.Navigated += new EventHandler<CefNavigatedEventArgs>(browser_Navigated);
            browser.DocumentCompleted += new EventHandler<CefDocumentCompletedEventArgs>(browser_DocumentCompleted);
            browser.ProgressChanged += new EventHandler<CefProgressChangedEventArgs>(browser_ProgressChanged);

            browser.Ready += new EventHandler(browser_DocumentReady);
            browser.ReadyOptions = CefReadyOptions.All; // & ~CefReadyOptions.XmlHttpRequest;
            browser.ReadyIdleThreshold = 0;
        }

        private void Write(string eventName, string format, params object[] args)
        {
            lock (this)
            {
                var prevColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}: {1}",
                    eventName.PadRight(24),
                    string.Format(format, args)
                    );
                Console.ForegroundColor = prevColor;
            }
        }

        private void browser_Created(object sender, EventArgs e)
        {
            Write("Created", "");
        }

        private void browser_CanGoBackChanged(object sender, EventArgs e)
        {
            Write("CanGoBackChanged", "{0}", ((IWebBrowser)sender).CanGoBack);
        }

        private void browser_CanGoForwardChanged(object sender, EventArgs e)
        {
            Write("CanGoForwardChanged", "{0}", ((IWebBrowser)sender).CanGoForward);
        }

        private void browser_AddressChanged(object sender, EventArgs e)
        {
            Write("AddressChanged", "{0}", ((IWebBrowser)sender).Address);
        }

        private void browser_TitleChanged(object sender, EventArgs e)
        {
            MainForm.SynchronizationContext.Post((_) =>
            {
                this.Text = ((IWebBrowser)sender).Title;
            }, null);
            Write("TitleChanged", "{0}", ((IWebBrowser)sender).Title);
        }

        private void browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            Write("ConsoleMessage", "{0} ({1}:{2})", e.Message, e.Source, e.Line);
        }

        private void browser_Navigating(object sender, CefNavigatingEventArgs e)
        {
            Write("Navigating", "TargetFrameName=[{0}] Url=[{1}]", e.Frame.GetName(), e.Request.GetURL());
        }

        private void browser_Navigated(object sender, CefNavigatedEventArgs e)
        {
            Write("Navigated", "TargetFrameName=[{0}] Url=[{1}]", e.Frame.GetName(), e.Frame.GetURL());
        }

        private void browser_DocumentCompleted(object sender, CefDocumentCompletedEventArgs e)
        {
            Write("DocumentCompleted", "TargetFrameName=[{0}] HttpStatusCode=[{1}]", e.Frame.GetName(), e.HttpStatusCode);
        }

        private void browser_ProgressChanged(object sender, CefProgressChangedEventArgs e)
        {
            Write("ProgressChanged", "{0}%", e.ProgressPercentage);
        }

        private void browser_DocumentReady(object sender, EventArgs e)
        {
            Write("Ready", "");
        }
    }
}
