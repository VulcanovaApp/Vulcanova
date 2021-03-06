using System;
using System.Globalization;
using Vulcanova.Uonet.Api.Schedule;
using Xamarin.Forms;

namespace Vulcanova.Features.Timetable;

public class TimetableChangeTextDecorationsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is ChangeType.Exemption or ChangeType.ClassAbsence
            ? TextDecorations.Strikethrough
            : TextDecorations.None;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}