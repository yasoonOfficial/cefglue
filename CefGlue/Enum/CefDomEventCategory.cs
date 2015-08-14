namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    /// <summary>
    /// DOM event category flags.
    /// </summary>
    [Flags]
    public enum CefDomEventCategory
    {
        Unknown = cef_dom_event_category_t.DOM_EVENT_CATEGORY_UNKNOWN,
        UI = cef_dom_event_category_t.DOM_EVENT_CATEGORY_UI,
        Mouse = cef_dom_event_category_t.DOM_EVENT_CATEGORY_MOUSE,
        Mutation = cef_dom_event_category_t.DOM_EVENT_CATEGORY_MUTATION,
        Keyboard = cef_dom_event_category_t.DOM_EVENT_CATEGORY_KEYBOARD,
        Text = cef_dom_event_category_t.DOM_EVENT_CATEGORY_TEXT,
        Composition = cef_dom_event_category_t.DOM_EVENT_CATEGORY_COMPOSITION,
        Drag = cef_dom_event_category_t.DOM_EVENT_CATEGORY_DRAG,
        Clipboard = cef_dom_event_category_t.DOM_EVENT_CATEGORY_CLIPBOARD,
        Message = cef_dom_event_category_t.DOM_EVENT_CATEGORY_MESSAGE,
        Wheel = cef_dom_event_category_t.DOM_EVENT_CATEGORY_WHEEL,
        BeforeTextInserted = cef_dom_event_category_t.DOM_EVENT_CATEGORY_BEFORE_TEXT_INSERTED,
        Overflow = cef_dom_event_category_t.DOM_EVENT_CATEGORY_OVERFLOW,
        Transition = cef_dom_event_category_t.DOM_EVENT_CATEGORY_PAGE_TRANSITION,
        PopState = cef_dom_event_category_t.DOM_EVENT_CATEGORY_POPSTATE,
        Progress = cef_dom_event_category_t.DOM_EVENT_CATEGORY_PROGRESS,
        XmlHttpRequestProgress = cef_dom_event_category_t.DOM_EVENT_CATEGORY_XMLHTTPREQUEST_PROGRESS,
        WebKitAnimation = cef_dom_event_category_t.DOM_EVENT_CATEGORY_WEBKIT_ANIMATION,
        WebKitTransition = cef_dom_event_category_t.DOM_EVENT_CATEGORY_WEBKIT_TRANSITION,
        BeforeLoad = cef_dom_event_category_t.DOM_EVENT_CATEGORY_BEFORE_LOAD,
    }
}
