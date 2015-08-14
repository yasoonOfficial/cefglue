namespace CefGlue.Demo.GtkSharp
{
    using System;
    using Gtk;

    class Program
    {
        public static void Main (string[] args)
        {
            Application.Init();

            Cef.Initialize(new CefSettings() {
                MultiThreadedMessageLoop = false,
                LogSeverity = CefLogSeverity.Error,
                LogFile = AppDomain.CurrentDomain.BaseDirectory + "/debug.log",
            });
            CefTask.Post(CefThreadId.IO, () => {
                Console.WriteLine("Hello from IO thread!");
            });

            var main = new MainWindow();
            main.Destroyed += (sender, e) => Application.Quit();

            main.Show();
            Cef.RunMessageLoop();

            Cef.Shutdown();
        }
    }
}
