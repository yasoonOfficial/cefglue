namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using CefGlue;

    internal sealed class ClientSchemeHandler : CefSchemeHandler
    {
        private Stream stream;

        private long responseLength;
        private int status;
        private string statusText;
        private string mimeType;


        private void Close()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
                this.stream = null;
            }
            this.responseLength = 0;
            this.status = 0;
            this.statusText = null;
            this.mimeType = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) Close();
            base.Dispose(disposing);
        }

        protected override bool ProcessRequest(CefRequest request, CefSchemeHandlerCallback callback)
        {
            var urlString = request.GetURL();

            string errorMessage = null;
            int errorStatus = 0;
            string errorStatusText = null;

            try
            {
                var uri = new Uri(urlString);
                var path = uri.Host + uri.AbsolutePath; // ignore host

                var asm = typeof(ClientSchemeHandler).Assembly;
                var resPrefix = "CefGlue.Client.Resources.";

                // convert path to resource name
                var parts = path.Split('/');
                for (var i = 0; i < parts.Length-1; i++)
                {
                    var filename = Path.GetFileNameWithoutExtension(parts[i]);
                    var extension = Path.GetExtension(parts[i]);

                    parts[i] = filename.Replace(".", "._").Replace('-', '_') + extension;
                }

                var resName = resPrefix + string.Join(".", parts);
                this.stream = asm.GetManifestResourceStream(resName);

                if (this.stream != null)
                {
                    // found
                    this.responseLength = -1;
                    this.status = 200;
                    this.statusText = "OK";
                    this.mimeType = GetMimeTypeFromUriSuffix(path);
                    callback.HeadersAvailable();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorStatus = 500;
                errorStatusText = "Internal Error";
                errorMessage = "<!doctype html><html><body><h1>Internal Error!</h1><pre>" + ex.ToString() + "</pre></body></html>";
            }

            // not found or error while processing request
            errorMessage = errorMessage ?? "<!doctype html><html><body><h1>Not Found!</h1><p>The requested url [" + urlString + "] not found!</p></body></html>";
            var bytes = Encoding.UTF8.GetBytes(errorMessage);
            this.stream = new MemoryStream(bytes, false);

            this.responseLength = -1;
            this.status = errorStatus != 0 ? errorStatus : 404;
            this.statusText = errorStatusText ?? "Not Found";
            this.mimeType = "text/html";
            callback.HeadersAvailable();
            return true;
        }

        protected override void Cancel()
        {
            this.Close();
        }

        protected override void GetResponseHeaders(CefResponse response, out long responseLength, ref string redirectUrl)
        {
            responseLength = this.responseLength;
            
            if (responseLength != -1)
            {
                var headers = new CefStringMultiMap();
                headers.Append("Content-Length", responseLength.ToString());
                response.SetHeaderMap(headers);
            }

            response.SetStatus(this.status);
            response.SetStatusText(this.statusText);
            response.SetMimeType(this.mimeType);
        }

        protected override bool ReadResponse(Stream stream, int bytesToRead, out int bytesRead, CefSchemeHandlerCallback callback)
        {
            byte[] buffer = new byte[bytesToRead];
            var readed = this.stream.Read(buffer, 0, buffer.Length);
            if (readed > 0)
            {
                stream.Write(buffer, 0, readed);
                bytesRead = readed;
                return true;
            }
            else
            {
                this.Close();
                bytesRead = 0;
                return false;
            }
        }


        private static string GetMimeTypeFromUriSuffix(string value)
        {
            if (value.EndsWith(".html")) return "text/html";
            else if (value.EndsWith(".js")) return "application/javascript";
            else if (value.EndsWith(".png")) return "image/png";
            else if (value.EndsWith(".jpg") || value.EndsWith(".jpeg")) return "image/jpeg";
            else if (value.EndsWith(".gif")) return "image/gif";
            else if (value.EndsWith(".json")) return "application/json";
            else if (value.EndsWith(".css")) return "text/css";
            else if (value.EndsWith(".txt")) return "text/plain";
            else return "binary/octet-stream";
        }
    }
}
