using System;
using System.Globalization;
using Vulcanova.Uonet.Api.Lessons;
using Xamarin.Forms;

namespace Vulcanova.Features.Attendance.Converters;

public class JustificationStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is JustificationStatus status)
        {
            return status switch
            {
                JustificationStatus.Requested => "resource://Vulcanova.Resources.Icons.hourglass.svg",
                JustificationStatus.Accepted => "resource://Vulcanova.Resources.Icons.checkmark-circle.svg",
                JustificationStatus.Rejected => "resource://Vulcanova.Resources.Icons.close-circle.svg",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}