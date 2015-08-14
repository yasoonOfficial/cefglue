namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDomEvent
    {
        /// <summary>
        /// Returns the event type.
        /// </summary>
        public string Type
        {
            get
            {
                var nResult = cef_domevent_t.invoke_get_type(this.ptr);
                return cef_string_userfree.GetStringAndFree(nResult);
            }
        }

        /// <summary>
        /// Returns the event category.
        /// </summary>
        public CefDomEventCategory Category
        {
            get
            {
                return (CefDomEventCategory)cef_domevent_t.invoke_get_category(this.ptr);
            }
        }

        /// <summary>
        /// Returns the event processing phase.
        /// </summary>
        public CefDomEventPhase Phase
        {
            get
            {
                return (CefDomEventPhase)cef_domevent_t.invoke_get_phase(this.ptr);
            }
        }

        /// <summary>
        /// Returns true if the event can bubble up the tree.
        /// </summary>
        public bool CanBubble
        {
            get
            {
                return cef_domevent_t.invoke_can_bubble(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns true if the event can be canceled.
        /// </summary>
        public bool CanCancel
        {
            get
            {
                return cef_domevent_t.invoke_can_cancel(this.ptr) != 0;
            }
        }

        /// <summary>
        /// Returns the document associated with this event.
        /// </summary>
        public CefDomDocument GetDocument()
        {
            return CefDomDocument.From(
                cef_domevent_t.invoke_get_document(this.ptr)
                );
        }

        /// <summary>
        /// Returns the target of the event.
        /// </summary>
        public CefDomNode GetTarget()
        {
            return CefDomNode.From(
                cef_domevent_t.invoke_get_target(this.ptr)
                );
        }

        /// <summary>
        /// Returns the current target of the event.
        /// </summary>
        public CefDomNode GetCurrentTarget()
        {
            return CefDomNode.From(
                cef_domevent_t.invoke_get_current_target(this.ptr)
                );
        }


    }
}
