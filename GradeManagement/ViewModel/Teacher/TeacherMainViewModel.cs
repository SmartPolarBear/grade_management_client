using GradeManagement.Base.ViewModel;
using GradeManagement.Data;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;

public class TeacherMainViewModel
    : ViewModelBase
{
    private readonly Teacher _teacher;

    public TeacherMainViewModel(TeacherUser teacher)
    {
        _teacher = (teacher.Data as Teacher)!;
    }

    public Teacher TeacherData => _teacher;
    
    
    
}