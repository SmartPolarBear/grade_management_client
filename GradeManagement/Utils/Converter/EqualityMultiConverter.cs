using System;
using System.Globalization;
using System.Windows.Data;

namespace GradeManagement.Utils.Converter;

public class EqualityMultiConverter
    : IMultiValueConverter
{
    public bool Inverse { get; set; }


    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 0)
        {
            return false;
        }

        var result = true;
        var first = values[0];

        for (var i = 1; i < values.Length; i++)
        {
            result &= first.Equals(values[i]);
        }

        return result ^ Inverse;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException("EqualityMultiConverter is a OneWay converter.");
    }
}