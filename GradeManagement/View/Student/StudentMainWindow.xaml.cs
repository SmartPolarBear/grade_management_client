using System.Windows;
using GradeManagement.Data;
using GradeManagement.ViewModel.Student;

namespace GradeManagement.View.Student;

public partial class StudentMainWindow : Window
{
    public StudentMainWindow(StudentUser user)
    {
        InitializeComponent();
        this.DataContext = new StudentMainViewModel(user);
    }

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}