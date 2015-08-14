namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    public sealed partial class CefWebBrowserCore
    {
        private FrameReadyState mainFrameReadyState;
        private Dictionary<string, FrameReadyState> framesReadyState;
        private int mainFrameIdleRequestNo;
        private int mainFrameIdleResponseNo;
        private int? mainFrameActiveRequests;
        
        internal void ClearReadyState()
        {
            this.mainFrameReadyState = FrameReadyState.None;
            this.mainFrameIdleRequestNo = 0;
            this.mainFrameIdleResponseNo = 0;
            this.mainFrameActiveRequests = null;

            if (this.framesReadyState != null)
            {
                this.framesReadyState.Clear();
            }
        }

        internal void CheckReadiness()
        {
            // TODO: ReadyOptions = Idle | Frames -> it must execute idle requests on each frame, instead of executing only on main frame.

            if (!Cef.CurrentlyOn(CefThreadId.UI)) throw new InvalidOperationException("Require UI thread.");

            if (this.mainFrameReadyState != FrameReadyState.Completed) return;

            // idle request are sent
            if (this.mainFrameIdleRequestNo > 0)
            {
                if (this.mainFrameIdleRequestNo == this.mainFrameIdleResponseNo)
                {
                    Console.WriteLine("IdleRequest completed.");
                }
                else
                {
                    Console.WriteLine("IdleRequest already sent.");
                    return;
                }
            }
            else if ((this.ReadyOptions & CefReadyOptions.Idle) != 0)
            {
                this.mainFrameIdleRequestNo++;
                this.MainFrame.ExecuteJavaScript(
                    "setTimeout(function(){cefGlue._browser.notifyIdle(" + this.mainFrameIdleRequestNo + ")}, " + this.readyIdleThreshold + ")",
                    "cefGlue",
                    1);
                return;
            }

            // check frames
            if (this.framesReadyState != null && this.framesReadyState.Values.Any(_ => _ != FrameReadyState.Completed))
            {
                Console.WriteLine("Not all frames completed.");
                return;
            }

            // check resources

            // check XmlHttpRequest
            if ((this.ReadyOptions & CefReadyOptions.XmlHttpRequest) != 0)
            {
                if (this.mainFrameActiveRequests == null)
                {
                    this.MainFrame.ExecuteJavaScript(
                        "setTimeout(function(){cefGlue._browser.notifyActiveRequests(cefGlue.activeRequests)}, 0)",
                        "cefGlue",
                        1);
                    return;
                }
                else if (this.mainFrameActiveRequests != 0) return;
            }

            this.mainFrameActiveRequests = null;
            this.OnReady();
        }

        [JSBind]
        private void NotifyIdle(int requestNo)
        {
            Console.WriteLine("NotifyIdle");
            if (this.mainFrameIdleRequestNo < requestNo || requestNo < this.mainFrameIdleResponseNo)
            {
                throw new InvalidOperationException();
            }

            this.mainFrameIdleResponseNo = requestNo;
            CefTask.Post(CefThreadId.UI, CheckReadiness);
        }

        [JSBind]
        private void NotifyActiveRequests(int count)
        {
            Console.WriteLine("NotifyActiveRequests");
            this.mainFrameActiveRequests = count;
            CefTask.Post(CefThreadId.UI, CheckReadiness);
        }

        internal void SetFrameReadyState(string name, FrameReadyState state)
        {
            if (name == null)
            {
                this.mainFrameReadyState = state;
            }
            else
            {
                if (this.framesReadyState == null)
                {
                    this.framesReadyState = new Dictionary<string, FrameReadyState>();
                }

                this.framesReadyState[name] = state;
            }
        }

        internal FrameReadyState GetFrameReadyState(string name)
        {
            if (name == null) return this.mainFrameReadyState;

            FrameReadyState result;
            if (this.framesReadyState.TryGetValue(name, out result)) return result;

            return FrameReadyState.None;
        }

    }
}
