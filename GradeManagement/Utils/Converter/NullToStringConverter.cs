using System;
using System.Globalization;
using System.Windows.Data;

namespace GradeManagement.Utils.Converter;

public class NullToStringConverter
    : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            null => parameter ?? "",
            _ => value
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException("Cannot convert back");
    }
}