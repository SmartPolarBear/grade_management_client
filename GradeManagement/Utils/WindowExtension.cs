using System;
using System.Windows;
using GradeManagement.Base.ViewModel;

namespace GradeManagement.Utils;

public static class WindowExtension
{
    public static T ViewModelOf<T>(this Window window)
        where T : ViewModelBase
    {
        return window.DataContext switch
        {
            T vm => vm,
            _ => throw new ArgumentException($"The window's DataContext is not a {typeof(T).ToString()}.")
        };
    }
}