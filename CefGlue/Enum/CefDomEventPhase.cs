namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// DOM event processing phases.
    /// </summary>
    public enum CefDomEventPhase
    {
        Unknown = cef_dom_event_phase_t.DOM_EVENT_PHASE_UNKNOWN,
        Capturing = cef_dom_event_phase_t.DOM_EVENT_PHASE_CAPTURING,
        AtTarget = cef_dom_event_phase_t.DOM_EVENT_PHASE_AT_TARGET,
        Bubbling = cef_dom_event_phase_t.DOM_EVENT_PHASE_BUBBLING,
    }
}
