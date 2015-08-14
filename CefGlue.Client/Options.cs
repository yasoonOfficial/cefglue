namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal sealed class Options
    {
        public static Options Parse(string[] args)
        {
            return new Options(args);
        }

        public bool MultiThreadedMessageLoop { get; private set; }
        public bool CefMessageLoop { get; private set; }
        public bool Help { get; private set; }

        private Options(string[] args)
        {
            // apply defaults
            this.MultiThreadedMessageLoop = true;
            this.CefMessageLoop = true;
            this.Help = false;

            ParseArgs(args);
        }

        private void ParseArgs(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("--")) arg = arg.Substring(2);
                else if (arg.StartsWith("-") || arg.StartsWith("/")) arg = arg.Substring(1);
                else throw new ArgumentException("args");

                switch (arg)
                {
                    case "multi-threaded-message-loop":
                    case "mt":
                        this.MultiThreadedMessageLoop = true;
                        break;

                    case "single-threaded-message-loop":
                    case "st":
                        this.MultiThreadedMessageLoop = false;
                        break;

                    case "cef-message-loop":
                        this.CefMessageLoop = true;
                        break;

                    case "idle-loop":
                        this.CefMessageLoop = false;
                        break;

                    case "help":
                    case "?":
                        this.Help = true;
                        break;

                    default:
                        this.Help = true;
                        break;
                }
            }
        }

        public string GetHelpText()
        {
            return "CefGlue Client options:\n" +
                   "-mt, --multi-threaded-message-loop: use multi-threaded message loop.\n" +
                   "-st, --single-threaded-message-loop: use single-threaded message loop.\n" +
                   "--cef-message-loop: use cef message loop (in single-threaded message loop mode).\n" +
                   "--idle-loop: process cef messages on idle (in single-threaded message loop mode).\n" +
                   "--help: this help.";
        }
    }
}
