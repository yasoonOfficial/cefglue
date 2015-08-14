namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    public interface IWebBrowser : IWebFrame
    {
        event EventHandler Created;

        // 
        event EventHandler CanGoBackChanged;

        event EventHandler CanGoForwardChanged;

        event EventHandler AddressChanged;

        event EventHandler TitleChanged;

        //
        event EventHandler<ConsoleMessageEventArgs> ConsoleMessage;

        //
        event EventHandler<CefNavigatingEventArgs> Navigating;

        event EventHandler<CefNavigatedEventArgs> Navigated;

        event EventHandler<CefDocumentCompletedEventArgs> DocumentCompleted;

        event EventHandler<CefProgressChangedEventArgs> ProgressChanged;

        //
        event EventHandler Ready;

        event EventHandler<CefBeforePopupEventArgs> BeforePopup;

        event EventHandler<CefAfterCreatedEventArgs> AfterCreated;

        event EventHandler<CefDragEventArgs> DragStart;

        event EventHandler<CefDragEventArgs> DragEnter;

        event EventHandler<CefShowPopupEventArgs> ShowPopup;

        event EventHandler<CefUncaughtExceptionEventArgs> UncaughtException;

        CefReadyOptions ReadyOptions { get; set; }

        int ReadyIdleThreshold { get; set; }

        //
        string StartUrl { get; set; }

        [Browsable(false)]
        CefBrowserSettings Settings { get; }

        [Browsable(false)]
        bool CanGoBack { get; }

        [Browsable(false)]
        bool CanGoForward { get; }

        [Browsable(false)]
        string Address { get; }

        [Browsable(false)]
        string Title { get; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        double ZoomLevel { get; set; }

        /// <summary>
        /// Closes this browser window. This method will do not block the calling thread.
        /// </summary>
        void Close();

        /// <summary>
        /// Navigate backwards.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Navigate backwards.
        /// </summary>
        void GoForward();

        /// <summary>
        /// Reload the current page.
        /// </summary>
        void Reload();

        /// <summary>
        /// Reload the current page.
        /// </summary>
        /// <param name="ignoreCache">If true - ignoring any cached data.</param>
        void Reload(bool ignoreCache);

        /// <summary>
        /// Stop loading the page.
        /// </summary>
        void StopLoad();

        // SetFocus
        // IsPopup
        // HasDocument
        // MainFrame
        // FocusedFrame
        // GetFrame (by name)

        /// <summary>
        /// Returns the names of all existing frames.
        /// </summary>
        IEnumerable<string> GetFrameNames();

        // Find
        // StopFinding
        // ClearHistory

        /// <summary>
        /// Open developer tools in its own window.
        /// </summary>
        void ShowDevTools();

        /// <summary>
        /// Explicitly close the developer tools window if one exists for this browser instance.
        /// </summary>
        void CloseDevTools();

        // TODO: etc... from CefBrowser
    }
}
