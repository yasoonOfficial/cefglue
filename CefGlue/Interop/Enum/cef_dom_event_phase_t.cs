namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    ///
    // DOM event processing phases.
    ///
    internal enum cef_dom_event_phase_t : int
    {
        DOM_EVENT_PHASE_UNKNOWN = 0,
        DOM_EVENT_PHASE_CAPTURING,
        DOM_EVENT_PHASE_AT_TARGET,
        DOM_EVENT_PHASE_BUBBLING,
    }
}
