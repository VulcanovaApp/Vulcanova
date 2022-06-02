using System;
using System.Globalization;
using Vulcanova.Core.Layout;
using Vulcanova.Uonet.Api.Schedule;
using Xamarin.Forms;

namespace Vulcanova.Features.Timetable
{
    public class TimetableChangeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChangeType type)
            {
                var baseColor = type is ChangeType.Exemption or ChangeType.UnitExemption
                    ? "ErrorColor"
                    : "WarningColor";

                return ThemeUtility.GetThemedColorByResourceKey(baseColor);
            }

            return ThemeUtility.GetDefaultTextColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}