namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CefWebLoadHandler : CefLoadHandler
    {
        private readonly CefWebBrowserCore context;

        public CefWebLoadHandler(CefWebBrowserCore context)
        {
            this.context = context;
        }

        protected override void OnLoadStart(CefBrowser browser, CefFrame frame)
        {
            /*
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLoadHandler, "LoadHandler.OnLoadStart");
            #endif
            */

            if (frame.IsMain)
            {
                this.context.SetFrameReadyState(null, FrameReadyState.Navigated);
            }
            else if ((this.context.ReadyOptions & CefReadyOptions.Frames) != 0)
            {
                this.context.SetFrameReadyState(frame.GetName(), FrameReadyState.Navigated);
            }

            this.context.OnNavigated(frame);

            /*
            if (frame.GetURL() == "testshell-error:")
            {
                this.context.ProgressFrameBrowsing();
            }
            */

            /*
            var frames = browser.GetFrameNames();
            var framesCount = frames.Count;
            var url = frame.GetURL();
            */

            // FIXME: This is doesn't support popup windows. Now it changes IsLoading property even if popup loading.
            // It can be simple solved by checking browser.IsPopup, but it is good (hide popup's events)?
            // Also it can be solved (it is required by anyway) to create different Client to Popup window - so client can know how it must be reported.
        }

        protected override void OnLoadEnd(CefBrowser browser, CefFrame frame, int httpStatusCode)
        {
            /*
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLoadHandler, "LoadHandler.OnLoadEnd: HttpStatusCode=[{0}]", httpStatusCode);
            #endif
            */

            var completed = false;

            if (frame.IsMain)
            {
                if (this.context.GetFrameReadyState(null) == FrameReadyState.Navigated)
                {
                    this.context.SetFrameReadyState(null, FrameReadyState.Completed);
                    completed = true;
                }
            }
            else if ((this.context.ReadyOptions & CefReadyOptions.Frames) != 0)
            {
                var frameName = frame.GetName();
                if (this.context.GetFrameReadyState(frameName) == FrameReadyState.Navigated)
                {
                    this.context.SetFrameReadyState(frameName, FrameReadyState.Completed);
                    completed = true;
                }
            }

            if (completed)
            {
                this.context.OnDocumentCompleted(frame, httpStatusCode);
            }

            this.context.CheckReadiness();



            // TODO: DocumentLoading/DocumentLoaded event per LoadStart and LoadEnd/Error ?
            // TODO: DocumentCompleted event ?
            /*
            if (!browser.IsPopup && frame.IsMain)
            {
                // At this moment we know frames
                var frames = this.control.GetFrameNames(); // get frame names, not that it can be done only from UI thread, so better not block IO thread, and do required decision later.
                Cef.Logger.Info(LogTarget.CefWebBrowser, "Browser Have {0} frames: {1}", frames.Count(), string.Join(", ", frames.Select(_ => "\"" + _ + "\"")));
            }
            */

            /*
            if (framesLoading == 0)
            {
                // check that document are ready to interact
                // FIXME: now it is doesn't work as required, may be better use callback from js code, 
                // and post it via ExecuteJavaScript, which internally queue via timeout request, and when it completed - fire callback.

                frame.ExecuteJavaScript("setTimeout(function(){cefGlue.client.log('this code must fire document interactive event.');},0);", "cefGlue", 0);
                // frame.ExecuteJavaScript("cefGlue.client.log('hello2');", "cefGlue", 0);
                // frame.ExecuteJavaScript("cefGlue.client.log('hello3');", "cefGlue", 0);
                CefTask.Post(CefThreadId.UI, () =>
                {
                    var success = (bool)this.control.InvokeScript("cefGlue.getDocumentState");
                    if (success) this.control.PostDocumentInteractive(EventArgs.Empty);
                });
                #if DIAGNOSTICS
                Cef.Logger.Info(LogTarget.Default, "GetDocumentState queued.");
                #endif
            }
            */
        }

        protected override bool OnLoadError(CefBrowser browser, CefFrame frame, CefHandlerErrorCode errorCode, string failedUrl, ref string errorText)
        {
            /*
            #if DIAGNOSTICS
            Cef.Logger.Trace(LogTarget.CefLoadHandler, "LoadHandler.OnLoadError: ErrorCode=[{0}] FailedUrl=[{1}] ErrorText=[{2}]", errorCode, failedUrl, errorText);
            #endif
            */
            Console.WriteLine("OnLoadError: Name=[{0}] Url=[{1}] ",
                frame.IsMain ? "MAIN" : frame.GetName(),
                frame.GetURL()
                );

            return false;
            // errorText = "OnLoadError: ErrorCode=[" + errorCode.ToString() + "], URL=[" + failedUrl + "].";
            // return true;
        }
    }
}
