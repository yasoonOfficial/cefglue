#if DIAGNOSTICS
namespace CefGlue.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [CLSCompliant(false)]
    public sealed class Logger
    {
        private bool disabled;
        private StreamWriter writer;
        private bool autoFlush;
        private LogSeverity severity;
        private bool[] targets;

        private static Logger s_logger;

        private static int targetCount;
        private static string[] severityNames;
        private static string[] targetNames;
        private static string[] operationNames;

        private static int gcFinalizerThreadId;

        static Logger()
        {
            severityNames = GetEnumNameValueMap(typeof(LogSeverity));
            targetNames = GetEnumNameValueMap(typeof(LogTarget));
            operationNames = GetEnumNameValueMap(typeof(LogOperation), false);

            targetCount = Enum.GetValues(typeof(LogTarget)).Length + 1;

            gcFinalizerThreadId = GCFinalizerThreadId.Value;
        }

        public static Logger Instance
        {
            get
            {
                if (s_logger == null)
                {
                    s_logger = new Logger();
                }
                return s_logger;
            }
        }

        internal Logger()
        {
            this.autoFlush = true;
            this.severity = LogSeverity.Trace;
            this.targets = new bool[targetCount];
        }

        public void Open()
        {
            if (this.disabled) return;

            if (this.writer == null)
            {
                try
                {
                    this.writer = File.AppendText("CefGlue.log");
                }
                catch (UnauthorizedAccessException)
                {
                    this.disabled = true;
                }
                catch (IOException)
                {
                    this.disabled = true;
                }
            }
        }

        public void Close()
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
        }

        public void SetTarget(LogTarget target, bool enabled)
        {
            this.targets[(int)target] = enabled;
        }

        public void SetAllTargets(bool enabled)
        {
            for (var i = 0; i < this.targets.Length; i++)
            {
                this.targets[i] = enabled;
            }
        }

        #region Log
        public void Log(LogSeverity severity, LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, objectId, operation, message);
        }

        public void Log(LogSeverity severity, LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Log(LogSeverity severity, LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, (IntPtr)objectId, operation, message);
        }

        public unsafe void Log(LogSeverity severity, LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, (IntPtr)objectId, operation, string.Format(format, args));
        }

        public void Log(LogSeverity severity, LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, objectId, operation, null);
        }

        public unsafe void Log(LogSeverity severity, LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, (IntPtr)objectId, operation, null);
        }

        public void Log(LogSeverity severity, LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, objectId, LogOperation.None, message);
        }

        public void Log(LogSeverity severity, LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, objectId, LogOperation.None, string.Format(format, args));
        }

        public unsafe void Log(LogSeverity severity, LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, (IntPtr)objectId, LogOperation.None, message);
        }

        public unsafe void Log(LogSeverity severity, LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, (IntPtr)objectId, LogOperation.None, string.Format(format, args));
        }

        public void Log(LogSeverity severity, LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, IntPtr.Zero, LogOperation.None, message);
        }

        public void Log(LogSeverity severity, LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > severity) return;
            Write(severity, target, IntPtr.Zero, LogOperation.None, string.Format(format, args));
        }
        #endregion

        #region Trace
        public void Trace(LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, operation, message);
        }

        public void Trace(LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Trace(LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, operation, message);
        }

        public unsafe void Trace(LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, operation, string.Format(format, args));
        }

        public void Trace(LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, operation);
        }

        public unsafe void Trace(LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, (IntPtr)objectId, operation);
        }

        public void Trace(LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, message);
        }

        public void Trace(LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, string.Format(format, args));
        }

        public unsafe void Trace(LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, message);
        }

        public unsafe void Trace(LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, objectId, string.Format(format, args));
        }

        public void Trace(LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, message);
        }

        public void Trace(LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Trace) return;
            Log(LogSeverity.Trace, target, string.Format(format, args));
        }
        #endregion

        #region Debug
        public void Debug(LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, operation, message);
        }

        public void Debug(LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Debug(LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, operation, message);
        }

        public unsafe void Debug(LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, operation, string.Format(format, args));
        }

        public void Debug(LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, operation);
        }

        public unsafe void Debug(LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, (IntPtr)objectId, operation);
        }

        public void Debug(LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, message);
        }

        public void Debug(LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, string.Format(format, args));
        }

        public unsafe void Debug(LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, message);
        }

        public unsafe void Debug(LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, objectId, string.Format(format, args));
        }

        public void Debug(LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, message);
        }

        public void Debug(LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Debug) return;
            Log(LogSeverity.Debug, target, string.Format(format, args));
        }
        #endregion

        #region Info
        public void Info(LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, operation, message);
        }

        public void Info(LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Info(LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, operation, message);
        }

        public unsafe void Info(LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, operation, string.Format(format, args));
        }

        public void Info(LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, operation);
        }

        public unsafe void Info(LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, (IntPtr)objectId, operation);
        }

        public void Info(LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, message);
        }

        public void Info(LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, string.Format(format, args));
        }

        public unsafe void Info(LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, message);
        }

        public unsafe void Info(LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, objectId, string.Format(format, args));
        }

        public void Info(LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, message);
        }

        public void Info(LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Info) return;
            Log(LogSeverity.Info, target, string.Format(format, args));
        }
        #endregion

        #region Warn
        public void Warn(LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, operation, message);
        }

        public void Warn(LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Warn(LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, operation, message);
        }

        public unsafe void Warn(LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, operation, string.Format(format, args));
        }

        public void Warn(LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, operation);
        }

        public unsafe void Warn(LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, (IntPtr)objectId, operation);
        }

        public void Warn(LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, message);
        }

        public void Warn(LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, string.Format(format, args));
        }

        public unsafe void Warn(LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, message);
        }

        public unsafe void Warn(LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, objectId, string.Format(format, args));
        }

        public void Warn(LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, message);
        }

        public void Warn(LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Warn) return;
            Log(LogSeverity.Warn, target, string.Format(format, args));
        }
        #endregion

        #region Error
        public void Error(LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, operation, message);
        }

        public void Error(LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Error(LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, operation, message);
        }

        public unsafe void Error(LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, operation, string.Format(format, args));
        }

        public void Error(LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, operation);
        }

        public unsafe void Error(LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, (IntPtr)objectId, operation);
        }

        public void Error(LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, message);
        }

        public void Error(LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, string.Format(format, args));
        }

        public unsafe void Error(LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, message);
        }

        public unsafe void Error(LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, objectId, string.Format(format, args));
        }

        public void Error(LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, message);
        }

        public void Error(LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Error) return;
            Log(LogSeverity.Error, target, string.Format(format, args));
        }
        #endregion

        #region Fatal
        public void Fatal(LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, operation, message);
        }

        public void Fatal(LogTarget target, IntPtr objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, operation, string.Format(format, args));
        }

        public unsafe void Fatal(LogTarget target, void* objectId, LogOperation operation, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, operation, message);
        }

        public unsafe void Fatal(LogTarget target, void* objectId, LogOperation operation, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, operation, string.Format(format, args));
        }

        public void Fatal(LogTarget target, IntPtr objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, operation);
        }

        public unsafe void Fatal(LogTarget target, void* objectId, LogOperation operation)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, (IntPtr)objectId, operation);
        }

        public void Fatal(LogTarget target, IntPtr objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, message);
        }

        public void Fatal(LogTarget target, IntPtr objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, string.Format(format, args));
        }

        public unsafe void Fatal(LogTarget target, void* objectId, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, message);
        }

        public unsafe void Fatal(LogTarget target, void* objectId, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, objectId, string.Format(format, args));
        }

        public void Fatal(LogTarget target, string message)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, message);
        }

        public void Fatal(LogTarget target, string format, params object[] args)
        {
            if (this.targets[(int)target] == false || this.severity > LogSeverity.Fatal) return;
            Log(LogSeverity.Fatal, target, string.Format(format, args));
        }
        #endregion


        private void Write(LogSeverity severity, LogTarget target, IntPtr objectId, LogOperation operation, string message)
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");

            lock (this)
            {
                if (this.writer != null)
                {
                    try
                    {
                        this.writer.Flush();
                    }
                    catch (ObjectDisposedException)
                    {
                        this.writer = null;
                    }
                }

                if (this.writer == null) Open();
                if (this.disabled) return;

                this.writer.Write(now);
                this.writer.Write('|');

                this.writer.Write("{0:X8}|", (uint)(Process.GetCurrentProcess().Id));

                var threadId = Thread.CurrentThread.ManagedThreadId;
                this.writer.Write(threadId.ToString().PadLeft(2));
                this.writer.Write('|');
                this.writer.Write("{0:X8}", GetCurrentThreadId());
                this.writer.Write('|');
                if (Cef.CurrentlyOn(CefThreadId.UI))
                {
                    this.writer.Write("CefUI  ");
                }
                else if (Cef.CurrentlyOn(CefThreadId.IO))
                {
                    this.writer.Write("CefIO  ");
                }
                else if (Cef.CurrentlyOn(CefThreadId.File))
                {
                    this.writer.Write("CefFile");
                }
                else if (threadId == gcFinalizerThreadId)
                {
                    this.writer.Write("GC     ");
                }
                else
                {
                    var threadName = Thread.CurrentThread.Name;
                    if (string.IsNullOrEmpty(threadName))
                    {
                        this.writer.Write("       ");
                    }
                    else
                    {
                        if (threadName.Length > 7) threadName = threadName.Substring(0, 7);
                        this.writer.Write(threadName.PadRight(7));
                    }
                }

                this.writer.Write('|');
                this.writer.Write(severityNames[(int)severity]);
                this.writer.Write('|');
                this.writer.Write(targetNames[(int)target]);
                this.writer.Write('|');
                if (objectId == IntPtr.Zero)
                {
                    this.writer.Write("        ");
                }
                else
                {
                    this.writer.Write("{0:X8}", (uint)objectId);
                }
                this.writer.Write('|');
                if (operation != LogOperation.None)
                {
                    this.writer.Write(operationNames[(int)operation]);
                    if (message != null) this.writer.Write(": ");
                }
                this.writer.WriteLine(message ?? string.Empty);

                if (this.autoFlush) writer.Flush();
            }
        }

        private static string[] GetEnumNameValueMap(Type type, bool autoPadding = true)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (!type.IsEnum) throw new ArgumentException("Enum type required.");

            // check that all values are zero-based and continious
            var values = Enum.GetValues(type);
            for (var i = 0; i < values.Length; i++)
            {
                var value = (int)values.GetValue(i);
                if (value != i) throw new InvalidOperationException(string.Format("Enum type [{0}] must have zero-based continious values."));
            }

            var names = Enum.GetNames(type);

            if (autoPadding)
            {
                var maxLength = names.Max(_ => _.Length);
                names = names.Select(_ => _.PadRight(maxLength)).ToArray();
            }

            return names;
        }

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();
    }
}
#endif
