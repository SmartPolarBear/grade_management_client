using GradeManagement.View.Admin;

namespace GradeManagement.Service.Admin;

using Admin = Data.Model.Admin;
using Student = Data.Model.Student;
using Teacher = Data.Model.Teacher;

public sealed class AdminViewService
{
    private readonly Admin _admin;

    public AdminViewService(Admin admin)
    {
        _admin = admin;
    }

    public void ShowChangePasswordDialog()
    {
        var dialog = new ChangePasswordDialog(_admin);
        dialog.ShowDialog();
    }
}