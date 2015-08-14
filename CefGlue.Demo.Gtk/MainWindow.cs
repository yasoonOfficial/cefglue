namespace CefGlue.Demo.GtkSharp
{
    using System;
    using Gtk;

    internal class MainWindow : Gtk.Window
    {
        public MainWindow ()
            : base(WindowType.Toplevel)
        {
            Console.WriteLine("MainWindow...");
            this.Title = "GefGlue Demo (Gtk#)";
            this.SetSizeRequest(800, 600);

            var vBox = new VBox();
            vBox.Visible = true;
            vBox.Homogeneous = false;

            var hBox = new HBox();
            hBox.Visible = true;

            var backButton = new Button();
            backButton.Visible = true;
            backButton.Label = "Back";

            var forwardButton = new Button();
            forwardButton.Visible = true;
            forwardButton.Label = "Forward";

            Console.WriteLine("new CefWebBrowserWidget()...");
            var browser = new CefGlue.GtkSharp.CefWebBrowserWidget();
            browser.Visible = true;
            Console.WriteLine("new CefWebBrowserWidget()... done");

            var statusBar = new Statusbar();
            statusBar.Visible = true;

            // Layout
            hBox.Add(backButton);
            hBox.Add(forwardButton);
            vBox.Add(hBox);
            vBox.Add(browser);
            vBox.Add(statusBar);
            this.Add(vBox);

            var vw1 = ((Box.BoxChild)(vBox[hBox]));
            vw1.Expand = false;
            vw1.Fill = false;

            var vw3 = ((Box.BoxChild)(vBox[statusBar]));
            vw3.Expand = false;
            vw3.Fill = false;

            //Show Everything
            Console.WriteLine("ShowAll()...");
            // this.ShowAll();
            Console.WriteLine("ShowAll()... done");
        }
    }
}

