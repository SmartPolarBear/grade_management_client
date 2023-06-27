using System;
using System.Windows.Controls;

namespace GradeManagement.Utils;

public static class ControlExtension
{
    public static T DataContextOf<T>(this Control control)
    {
        return control.DataContext switch
        {
            T vm => vm,
            _ => throw new ArgumentException($"The control's DataContext is not a {typeof(T).ToString()}.")
        };
    }
}