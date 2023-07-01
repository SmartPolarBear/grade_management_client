using GradeManagement.View.Student;

namespace GradeManagement.Service.Student;

using Student = Data.Model.Student;

public sealed class StudentViewService
{
    private readonly Student _student;

    public StudentViewService(Student student)
    {
        _student = student;
    }

    public void ShowChangePasswordDialog()
    {
        var dialog = new ChangePasswordDialog(_student);
        dialog.ShowDialog();
    }
}