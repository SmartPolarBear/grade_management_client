using System.Windows;
using GradeManagement.Data;
using GradeManagement.Service.Admin;
using GradeManagement.Utils;
using GradeManagement.ViewModel.Admin;

namespace GradeManagement.View.Admin;

using Admin = Data.Model.Admin;

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

    private void ChangePasswordMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var service = new AdminViewService(this.DataContextOf<AdminMainViewModel>().AdminData);
        service.ShowChangePasswordDialog();
    }
}