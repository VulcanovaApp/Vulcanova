using System;
using System.Globalization;
using Xamarin.Forms;

namespace Vulcanova.Core.Layout.Converters;

public class NegateBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is bool b ? !b : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}