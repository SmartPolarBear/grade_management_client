using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using GradeManagement.Service.Teacher;
using GradeManagement.View.Teacher;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using Student = Data.Model.Student;

public record StudentWithGrade(Student Student, decimal? Grade, string DisplayGrade);

public class GradingViewModel
    : ViewModelBase
{
    private CourseGradingService _gradingService;
    private TeacherService _teacherService;
    private CourseGradingViewService _gradingViewService;

    public GradingViewModel(Teacher teacher, Course course)
    {
        TeacherData = teacher;
        CourseData = course;

        _teacherService = new TeacherService(TeacherData);
        _gradingService = new CourseGradingService(TeacherData, CourseData);
        _gradingViewService = new CourseGradingViewService(TeacherData, CourseData);
    }

    public async void GradeStudent(decimal gradeResult, StudentWithGrade s)
    {
        await _gradingService.GradeStudent(s.Student, CourseData, gradeResult);
        NotifyPropertyChanged(nameof(Students));
        NotifyPropertyChanged(nameof(GradedStudentCount));
        NotifyPropertyChanged(nameof(UngradedStudentCount));
    }

    private void EditGradeComposition()
    {
        _gradingViewService.ShowEditGradeCompositionDialog(() =>
        {
            NotifyPropertyChanged(nameof(GradingCompositionDisplayNames));
        });
        NotifyPropertyChanged(nameof(GradingCompositionDisplayNames));
    }

    public Teacher TeacherData { get; }

    public Course CourseData { get; }

    public string WindowTitle
        => $"Grading {CourseData.Id.Trim()}:{CourseData.Name.Trim()} of {TeacherData.Name.Trim()}";

    public IEnumerable<StudentWithGrade> Students =>
        from s in _gradingService.StudentsWithGrades
        select new StudentWithGrade(s.Student, s.Grade, (CourseGradingMethod)CourseData.GradingMethod switch
        {
            CourseGradingMethod.Score100 => s.Grade switch
            {
                null => "N/A",
                _ => s.Grade.ToString()
            },
            CourseGradingMethod.Score5 => s.Grade switch
            {
                null => "N/A",
                >= 4.5m => "A", // 5
                >= 3.5m => "B", // 4
                >= 2.5m => "C", // 3
                >= 1.5m => "D", // 2
                _ => "F" // 1
            },
            CourseGradingMethod.PF => s.Grade switch
            {
                null => "N/A",
                >= 0.5m => "Pass", // 1
                _ => "Fail" // 0
            },
        });


    public int StudentCount =>
        Students.Count();

    public int GradedStudentCount =>
        _gradingService.GradedStudents.Count();

    public int UngradedStudentCount =>
        _gradingService.UngradedStudents.Count();

    public IEnumerable<string> GradingCompositionDisplayNames
        => from gc in _gradingService.GradeCompositions.ToList() // ToList() to avoid EF Core's fucking restrictions
            select $"{gc.Weight:F1}% - {gc.GradeComposition.Name}";


    public ICommand EditGradeCompositionCommand => new DelegateCommand(_ => EditGradeComposition(), _ => true);

    public ICommand Grade100MarkSystemCommand => new DelegateCommand((sender) =>
    {
        var item = (sender as StudentWithGrade)!;
        var grade = _gradingViewService.ShowComplexGradingDialog();
        if (grade != null)
        {
            GradeStudent(grade!.Value, item);
        }
    }, _ => true);
}