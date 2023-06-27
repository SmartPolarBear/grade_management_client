using GradeManagement.View.Teacher;

namespace GradeManagement.Service.Teacher;

using Teacher = Data.Model.Teacher;

public sealed class TeacherViewService
{
    private readonly Teacher _teacher;

    public TeacherViewService(Teacher teacher)
    {
        _teacher = teacher;
    }

    public void ShowChangePasswordDialog()
    {
        var dialog = new ChangePasswordDialog(_teacher);
        dialog.ShowDialog();
    }
    
    public void ShowChangeAccountInfoDialog()
    {
        var dialog = new ChangeAccountInfoDialog(_teacher);
        dialog.ShowDialog();
    }
    
    
}