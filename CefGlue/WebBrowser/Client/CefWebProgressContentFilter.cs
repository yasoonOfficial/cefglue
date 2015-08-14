namespace CefGlue.WebBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class CefWebProgressContentFilter : CefContentFilter
    {
        private static int contentNo;
        private FileStream fileStream;

        public CefWebProgressContentFilter()
        {
            this.fileStream = File.OpenWrite("content." + contentNo++ + ".bin");
        }

        protected override void Dispose(bool disposing)
        {
            if (this.fileStream != null)
            {
                this.fileStream.Close();
                this.fileStream = null;
            }
        }

        protected override void ProcessData(Stream data, out CefStreamReader substituteData)
        {
            var bytes = new byte[(int)data.Length];
            data.Read(bytes, 0, (int)data.Length);
            fileStream.Write(bytes, 0, (int)data.Length);

            substituteData = null;
            // Console.WriteLine("ProcessData: {0}", data.Length);
        }

        protected override void Drain(out CefStreamReader remainder)
        {
            if (this.fileStream != null)
            {
                this.fileStream.Close();
                this.fileStream = null;
            }

            remainder = null;
            Console.WriteLine("Drain");
        }
    }
}
