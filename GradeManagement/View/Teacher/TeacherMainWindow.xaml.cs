using System.Windows;
using System.Windows.Controls.Ribbon;
using GradeManagement.Data;
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
}