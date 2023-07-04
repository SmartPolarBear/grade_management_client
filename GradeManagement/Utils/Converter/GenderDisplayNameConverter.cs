using System;
using System.Globalization;
using System.Windows.Data;
using GradeManagement.Data;

namespace GradeManagement.Utils.Converter;

public class GenderDisplayNameConverter
    : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        int ival = System.Convert.ToInt32(value);
        return ((Gender)ival).ToDisplayName();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException("ConvertBack can not be implemented.");
    }
}