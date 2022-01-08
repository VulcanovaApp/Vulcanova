using System;

namespace Vulcanova.Extensions
{
    public static class DateExtensions
    {
        public static DateTime LastMonday(this DateTime dt)
        {
            var delta = DayOfWeek.Monday - dt.DayOfWeek;

            if (delta > 0)
            {
                delta -= 7;
            }

            return dt.AddDays(delta);
        }

        public static DateTime NextSunday(this DateTime dt)
        {
            var delta = DayOfWeek.Sunday - dt.DayOfWeek;

            if (delta < 0)
            {
                delta += 7;
            }

            return dt.AddDays(delta);
        }
    }
}