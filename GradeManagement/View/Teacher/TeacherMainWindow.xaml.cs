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
    public TeacherMainWindow(TeacherUser? user)
    {
        InitializeComponent();
        this.DataContext = new TeacherMainViewModel(user!);
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
            this.ViewModelOf<TeacherMainViewModel>().UpdateCoursesCommand.Execute(
                this.FilterToolBar);
        };
    }
}