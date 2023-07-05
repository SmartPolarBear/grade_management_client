using GradeManagement.Data.Model;
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

    public Student? ShowAddStudentDialog()
    {
        var dialog = new AddStudentDialog();
        dialog.ShowDialog();
        return dialog.DialogResult == true ? dialog.Student : null;
    }

    public Teacher? ShowAddTeacherDialog()
    {
        var dialog = new AddTeacherDialog();
        dialog.ShowDialog();
        return dialog.DialogResult == true ? dialog.Teacher : null;
    }

    public Course? ShowAddCourseDialog()
    {
        var dialog = new AddCourseDialog();
        dialog.ShowDialog();
        return dialog.DialogResult == true ? dialog.Course : null;
    }

   
}