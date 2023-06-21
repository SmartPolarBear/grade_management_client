using System.Windows;
using GradeManagement.Data.Model;
using GradeManagement.ViewModel.Teacher;

namespace GradeManagement.View.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

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
}