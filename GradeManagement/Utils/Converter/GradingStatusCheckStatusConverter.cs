using System;
using System.Globalization;
using System.Windows.Data;
using GradeManagement.Service.Login;
using GradeManagement.Service.Teacher;

namespace GradeManagement.Utils.Converter;

public class GradingStatusCheckStatusConverter
    : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        GradingStatus gs = (GradingStatus)value;
        return gs switch
        {
            GradingStatus.None => false,
            GradingStatus.Partial => null,
            GradingStatus.All => true,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}