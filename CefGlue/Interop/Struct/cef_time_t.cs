namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.InteropServices;

    ///
    // Time information. Values should always be in UTC.
    ///
    [StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]
    internal unsafe partial struct cef_time_t
    {
        public static cef_time_t From(DateTime value)
        {
            return new cef_time_t(value);
        }

        int year;          // Four digit year "2007"
        int month;         // 1-based month (values 1 = January, etc.)
        int day_of_week;   // 0-based day of week (0 = Sunday, etc.)
        int day_of_month;  // 1-based day of month (1-31)
        int hour;          // Hour within the current day (0-23)
        int minute;        // Minute within the current hour (0-59)
        int second;        // Second within the current minute (0-59 plus leap seconds which may take it up to 60).
        int millisecond;   // Milliseconds within the current second (0-999)

        public cef_time_t(DateTime value)
        {
            value = value.ToUniversalTime();

            this.year = value.Year;
            this.month = value.Month;
            this.day_of_week = (int)value.DayOfWeek;
            this.day_of_month = value.Day;
            this.hour = value.Hour;
            this.minute = value.Minute;
            this.second = value.Second;
            this.millisecond = value.Millisecond;
        }

        private cef_time_t(int year, int month, int day_of_week, int day_of_month, int hour, int minute, int second, int millisecond)
        {
            this.year = year;
            this.month = month;
            this.day_of_week = day_of_week;
            this.day_of_month = day_of_month;
            this.hour = hour;
            this.minute = minute;
            this.second = second;
            this.millisecond = millisecond;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(
                this.year,
                this.month,
                this.day_of_month,
                this.hour,
                this.minute,
                this.second,
                this.millisecond,
                DateTimeKind.Utc
                );
        }
    }
}
