using System.Windows;
using System.Windows.Controls;
using GradeManagement.Data;
using GradeManagement.Utils;
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

    private void EmailTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        this.DataContextOf<StudentMainViewModel>().UpdateEmailCommand.RaiseCanExecuteChanged();
    }
}