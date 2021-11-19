using System;
using System.Globalization;
using Xamarin.Forms;

namespace Vulcanova.Core.Layout
{
    public class FallbackStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => value is string data && !string.IsNullOrEmpty(data) ? data : parameter;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}