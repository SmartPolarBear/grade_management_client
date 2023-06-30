using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using GradeManagement.Data;
using GradeManagement.Utils;
using GradeManagement.ViewModel.Teacher;

namespace GradeManagement.View.Teacher;

public partial class TeacherMainWindow : Window
{
    public TeacherMainWindow(TeacherUser user)
    {
        InitializeComponent();
        this.DataContext = new TeacherMainViewModel(user);
    }

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void MainDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var dgr = (sender as DataGridRow)!;
        var course = (dgr.DataContext as Data.Model.Course)!;

        var gradingWindow = new GradingWindow(this.ViewModelOf<TeacherMainViewModel>().TeacherData,
            course);
        gradingWindow.Show();

        gradingWindow.Closed += (o, args) =>
        {
            this.ViewModelOf<TeacherMainViewModel>().FilterResultsCommand.Execute(
                this.FilterToolBar);
        };
    }

    private void StudentDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var dgr = (sender as DataGridRow)!;
        var stc = (dgr.DataContext as Data.Model.Stc)!;
        var teacher = this.ViewModelOf<TeacherMainViewModel>().TeacherData;
        
        var grade = stc.Course.Scs.FirstOrDefault(g =>
            g.CourseId == stc.CourseId && g.StudentId == stc.StudentId);
        
        if (grade == null)
        {
            MessageBox.Show($"Student {stc.Student.Name} is not graded yet.", "Grade",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
            MessageBox.Show($"Student {stc.Student.Name} in course {stc.Course.Name} gets {grade.Score:F2}.", "Grade",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}