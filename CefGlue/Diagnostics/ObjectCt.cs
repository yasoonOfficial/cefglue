#if DIAGNOSTICS
namespace CefGlue.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ObjectCt
    {
        public static void WriteDump()
        {
            // TODO: generate it from ProxySchema and HandlerSchema

            Cef.Logger.Trace(LogTarget.ObjectCt, "ObjectCt Dump For Handlers:");
            Cef.Logger.Trace(LogTarget.CefClient, "ObjectCt=[{0}]", CefClient.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefContentFilter, "ObjectCt=[{0}]", CefContentFilter.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefCookieVisitor, "ObjectCt=[{0}]", CefCookieVisitor.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDisplayHandler, "ObjectCt=[{0}]", CefDisplayHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDomEventListener, "ObjectCt=[{0}]", CefDomEventListener.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDomVisitor, "ObjectCt=[{0}]", CefDomVisitor.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDownloadHandler, "ObjectCt=[{0}]", CefDownloadHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefFindHandler, "ObjectCt=[{0}]", CefFindHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefFocusHandler, "ObjectCt=[{0}]", CefFocusHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefJSDialogHandler, "ObjectCt=[{0}]", CefJSDialogHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefKeyboardHandler, "ObjectCt=[{0}]", CefKeyboardHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefLifeSpanHandler, "ObjectCt=[{0}]", CefLifeSpanHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefLoadHandler, "ObjectCt=[{0}]", CefLoadHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefMenuHandler, "ObjectCt=[{0}]", CefMenuHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefPrintHandler, "ObjectCt=[{0}]", CefPrintHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefPermissionHandler, "ObjectCt=[{0}]", CefPermissionHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefReadHandler, "ObjectCt=[{0}]", CefReadHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefRenderHandler, "ObjectCt=[{0}]", CefRenderHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefRequestHandler, "ObjectCt=[{0}]", CefRequestHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefResourceBundleHandler, "ObjectCt=[{0}]", CefResourceBundleHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefSchemeHandler, "ObjectCt=[{0}]", CefSchemeHandler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefSchemeHandlerFactory, "ObjectCt=[{0}]", CefSchemeHandlerFactory.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefTask, "ObjectCt=[{0}]", CefTask.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefV8Accessor, "ObjectCt=[{0}]", CefV8Accessor.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefV8Handler, "ObjectCt=[{0}]", CefV8Handler.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefWriteHandler, "ObjectCt=[{0}]", CefWriteHandler.ObjectCt);

            Cef.Logger.Trace(LogTarget.ObjectCt, "ObjectCt Dump For Proxies:");
            Cef.Logger.Trace(LogTarget.CefBrowser, "ObjectCt=[{0}]", CefBrowser.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDomDocument, "ObjectCt=[{0}]", CefDomDocument.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDomEvent, "ObjectCt=[{0}]", CefDomEvent.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefDomNode, "ObjectCt=[{0}]", CefDomNode.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefFrame, "ObjectCt=[{0}]", CefFrame.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefPostData, "ObjectCt=[{0}]", CefPostData.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefPostDataElement, "ObjectCt=[{0}]", CefPostDataElement.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefRequest, "ObjectCt=[{0}]", CefRequest.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefResponse, "ObjectCt=[{0}]", CefResponse.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefStreamReader, "ObjectCt=[{0}]", CefStreamReader.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefStreamWriter, "ObjectCt=[{0}]", CefStreamWriter.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefV8Context, "ObjectCt=[{0}]", CefV8Context.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefV8Exception, "ObjectCt=[{0}]", CefV8Exception.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefV8Value, "ObjectCt=[{0}]", CefV8Value.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefWebPluginInfo, "ObjectCt=[{0}]", CefWebPluginInfo.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefWebUrlRequest, "ObjectCt=[{0}]", CefWebUrlRequest.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefWebUrlRequestClient, "ObjectCt=[{0}]", CefWebUrlRequestClient.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefXmlReader, "ObjectCt=[{0}]", CefXmlReader.ObjectCt);
            Cef.Logger.Trace(LogTarget.CefZipReader, "ObjectCt=[{0}]", CefZipReader.ObjectCt);
        }
    }
}
#endif
