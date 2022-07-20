using System;
using System.Globalization;
using Vulcanova.Core.Layout;
using Xamarin.Forms;

namespace Vulcanova.Features.Attendance.Converters;

public class AttendanceColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is PresenceType type)
        {
            if (type.AbsenceJustified)
            {
                return ThemeUtility.GetDefaultTextColor();
            }

            var baseColor = type switch
            {
                {AbsenceJustified: true} => "PrimaryColor",
                {Absence: true} => "ErrorColor",
                {Late: true} => "WarningColor",
                _ => null
            };

            if (baseColor == null)
            {
                return ThemeUtility.GetDefaultTextColor();
            }

            return ThemeUtility.GetThemedColorByResourceKey(baseColor);
        }

        return ThemeUtility.GetDefaultTextColor();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}