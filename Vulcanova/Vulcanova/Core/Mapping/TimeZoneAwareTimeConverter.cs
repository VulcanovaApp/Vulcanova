using System;
using System.Globalization;
using AutoMapper;

namespace Vulcanova.Core.Mapping
{
    public class TimeZoneAwareTimeConverter : IValueConverter<string, DateTime>
    {
        private static readonly TimeZoneInfo Tz = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");

        public DateTime Convert(string sourceMember, ResolutionContext context) =>
            TimeZoneInfo.ConvertTimeToUtc(
                DateTime.ParseExact(sourceMember, "HH:mm", CultureInfo.InvariantCulture), Tz);

        public static readonly TimeZoneAwareTimeConverter Instance = new();
    }
}