using System;
using System.Globalization;
using Vulcanova.Resources;
using Xamarin.Forms;

namespace Vulcanova.Features.Homework.HomeworkDetails;

public class BooleanToYesNoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool) value switch
        {
            true => Strings.CommonYes,
            false => Strings.CommonNo
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}