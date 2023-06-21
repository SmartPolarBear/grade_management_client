using System.Collections.Generic;
using System.Linq;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data.Model;
using GradeManagement.Service.Teacher;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using Student = Data.Model.Student;

public record StudentWithGrade(Student Student, decimal? Grade);

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

    public string WindowTitle
        => $"Grading {CourseData.Id.Trim()}:{CourseData.Name.Trim()} of {TeacherData.Name.Trim()}";

    public IEnumerable<StudentWithGrade> Students =>
        from s in _gradingService.StudentsWithGrades
        select new StudentWithGrade(s.Student, s.Grade);


    public int StudentCount =>
        Students.Count();

    public int GradedStudentCount =>
        _gradingService.GradedStudents.Count();

    public int UngradedStudentCount =>
        _gradingService.UngradedStudents.Count();
}