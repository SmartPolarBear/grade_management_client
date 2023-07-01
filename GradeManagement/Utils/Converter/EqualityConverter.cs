using System;
using System.Globalization;
using System.Windows.Data;

namespace GradeManagement.Utils.Converter;

public class EqualityConverter
    : IValueConverter
{
    public bool Inverse { get; set; }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.Equals(parameter) != true ^ Inverse;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException("EqualityConverter is a OneWay converter.");
    }
}