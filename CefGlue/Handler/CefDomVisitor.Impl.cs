namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class CefDomVisitor
    {
        /// <summary>
        /// Method executed for visiting the DOM. The document object passed to
        /// this method represents a snapshot of the DOM at the time this method
        /// is executed. DOM objects are only valid for the scope of this method.
        /// Do not keep references to or attempt to access any DOM objects
        /// outside the scope of this method.
        /// </summary>
        private void visit(cef_domvisitor_t* self, cef_domdocument_t* document)
        {
            ThrowIfObjectDisposed();

            var m_document = CefDomDocument.From(document);

            this.Visit(m_document);

            m_document.Dispose();
        }

        /// <summary>
        /// Method executed for visiting the DOM.
        /// The document object passed to this method represents a snapshot of the DOM at the time this method is executed.
        /// DOM objects are only valid for the scope of this method.
        /// Do not keep references to or attempt to access any DOM objects outside the scope of this method.
        /// </summary>
        protected abstract void Visit(CefDomDocument document);

    }
}
