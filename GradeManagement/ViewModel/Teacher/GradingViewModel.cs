using System.Linq;
using GradeManagement.Base.ViewModel;
using GradeManagement.Service.Teacher;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

public class GradingViewModel
    : ViewModelBase
{
    private CourseGradingService _gradingService;
    private TeacherService _teacherService;

    public GradingViewModel(Teacher teacher, Course course)
    {
        TeacherData = teacher;
        CourseData = course;

        _teacherService = new TeacherService(TeacherData);
        _gradingService = new CourseGradingService(TeacherData, CourseData);
    }

    public Teacher TeacherData { get; }

    public Course CourseData { get; }

    public int GradedStudentCount => _gradingService.GradedStudents.Count();

    public int UngradedStudentCount => _gradingService.UngradedStudents.Count();
}