using System;
using System.Globalization;
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
                var baseColor = type == ChangeType.Exemption ? "ErrorColor" : "WarningColor";
                
                var colorVariant = Application.Current.RequestedTheme == OSAppTheme.Dark
                    ? "Dark"
                    : "Light";

                if (Application.Current.Resources.TryGetValue($"{colorVariant}{baseColor}", out var color))
                {
                    return color;
                }
            }

            return Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}