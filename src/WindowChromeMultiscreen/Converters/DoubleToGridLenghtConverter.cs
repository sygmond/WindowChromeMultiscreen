using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WindowChromeMultiscreen.Converters;

public class DoubleToGridLenghtConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            throw new ArgumentException($"Can't convert null to {nameof(GridLength)}.", nameof(value));

        if (value is not double inputValue)
            throw new ArgumentException($"Input value must be of type double.", nameof(value));

        return new GridLength(inputValue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}