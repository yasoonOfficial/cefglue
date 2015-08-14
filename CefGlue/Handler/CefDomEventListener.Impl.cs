namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDomEventListener
    {
        /// <summary>
        /// Called when an event is received. The event object passed to this
        /// method contains a snapshot of the DOM at the time this method is
        /// executed. DOM objects are only valid for the scope of this method. Do
        /// not keep references to or attempt to access any DOM objects outside
        /// the scope of this method.
        /// </summary>
        private void handle_event(cef_domevent_listener_t* self, cef_domevent_t* @event)
        {
            ThrowIfObjectDisposed();

            var m_domEvent = CefDomEvent.From(@event);

            this.HandleEvent(m_domEvent);

            m_domEvent.Dispose();
        }

        /// <summary>
        /// Called when an event is received.
        /// The event object passed to this method contains a snapshot of the DOM at the time this method is executed.
        /// DOM objects are only valid for the scope of this method.
        /// Do not keep references to or attempt to access any DOM objects outside the scope of this method.
        /// </summary>
        protected abstract void HandleEvent(CefDomEvent e);

    }
}
