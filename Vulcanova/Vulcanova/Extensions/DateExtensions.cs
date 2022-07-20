using System;

namespace Vulcanova.Extensions;

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

    public static (DateTime Monday, DateTime Sunday) GetMondayOfFirstWeekAndSundayOfLastWeekOfMonth(this DateTime dt)
    {
        var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
        var lastDayOfMonth = new DateTime(dt.Year, dt.Month,
            DateTime.DaysInMonth(dt.Year, dt.Month));

        var firstDay = firstDayOfMonth.LastMonday();
        var lastDay = lastDayOfMonth.NextSunday();

        return (firstDay, lastDay);
    }
}