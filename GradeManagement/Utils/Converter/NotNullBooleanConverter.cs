using System;
using System.Globalization;
using System.Windows.Data;

namespace GradeManagement.Utils.Converter;

public class NotNullBooleanConverter
    : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException("This method should not be called.");
    }
}