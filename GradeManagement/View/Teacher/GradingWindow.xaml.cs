using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using GradeManagement.Utils;
using GradeManagement.ViewModel.Teacher;

namespace GradeManagement.View.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

internal class GradingMethodVisibilityConverter
    : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (parameter == null)
        {
            return (CourseGradingMethod)System.Convert.ToInt32(value) switch
            {
                CourseGradingMethod.Score100 => Visibility.Visible,
                CourseGradingMethod.Score5 => Visibility.Collapsed,
                CourseGradingMethod.PF => Visibility.Collapsed,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
        else
        {
            return (CourseGradingMethod)System.Convert.ToInt32(value) switch
            {
                CourseGradingMethod.Score100 => Visibility.Collapsed,
                CourseGradingMethod.Score5 => Visibility.Visible,
                CourseGradingMethod.PF => Visibility.Visible,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public partial class GradingWindow : Window
{
    private readonly Teacher _teacher;
    private readonly Course _course;


    public GradingWindow(Teacher teacher, Course course)
    {
        _teacher = teacher;
        _course = course;
        InitializeComponent();
        this.DataContext = new GradingViewModel(_teacher, _course);
    }

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void MainDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var student = (sender as Control)!.DataContextOf<StudentWithGrade>();
        var vm = this.ViewModelOf<GradingViewModel>()!;
        Window gradingWin = (CourseGradingMethod)_course.GradingMethod switch
        {
            CourseGradingMethod.Score5 or CourseGradingMethod.PF => new SelectionGradingDialog(
                (CourseGradingMethod)_course.GradingMethod, student.Grade),
            CourseGradingMethod.Score100 => throw new NotImplementedException(),
            _ => throw new ArgumentException("Invalid grading method!")
        };

        if (gradingWin.ShowDialog() == true)
        {
            vm.GradeStudent((gradingWin as IGradingDialog)!.GradeResult,
                student);
        }
    }
}