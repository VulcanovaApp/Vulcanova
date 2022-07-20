using System;
using System.Globalization;
using Vulcanova.Core.Layout;
using Vulcanova.Uonet.Api.Lessons;
using Xamarin.Forms;

namespace Vulcanova.Features.Attendance.Converters;

public class JustificationColorConverter : IValueConverter
{
    private static readonly AttendanceColorConverter AttendanceColorConverter = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Lesson lesson)
        {
            if (lesson.PresenceType == null)
            {
                return ThemeUtility.GetDefaultTextColor();
            }

            string baseColor;

            if (lesson.PresenceType.AbsenceJustified)
            {
                baseColor = "PrimaryColor";
            }
            else
            {
                baseColor = lesson.JustificationStatus switch
                {
                    JustificationStatus.Accepted => "PrimaryColor",
                    JustificationStatus.Rejected => "ErrorColor",
                    JustificationStatus.Requested => "WarningColor",
                    _ => null
                };
            }

            if (baseColor == null)
            {
                return AttendanceColorConverter.Convert(lesson.PresenceType, targetType, parameter, culture);
            }

            return ThemeUtility.GetThemedColorByResourceKey(baseColor);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}