using System.Windows;
using GradeManagement.Data;
using GradeManagement.ViewModel.Admin;

namespace GradeManagement.View.Admin;

public partial class AdminMainWindow : Window
{
    public AdminMainWindow(AdminUser user)
    {
        InitializeComponent();
        this.DataContext = new AdminMainViewModel(user);
    }

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}