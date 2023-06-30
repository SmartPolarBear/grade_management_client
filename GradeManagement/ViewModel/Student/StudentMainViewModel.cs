using System;
using System.Linq;
using System.Windows.Data;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;

namespace GradeManagement.ViewModel.Student;

using Student = Data.Model.Student;
using Course = Data.Model.Course;

public sealed record CourseWithGrade(Course Course, decimal? Grade)
{
    public string DisplayGrade
        => ((CourseGradingMethod)Course.GradingMethod).DisplayGrade(Grade) ?? "N/A";
}

public class StudentMainViewModel
    : ViewModelBase
{
    private readonly Student _user;

    public StudentMainViewModel(StudentUser user)
    {
        _user = (user.Data as Student)!;

        Courses = new CollectionViewSource
        {
            Source = from stc in _user.Stcs
                join sc in _user.Scs on new { stc.StudentId, stc.CourseId } equals new { sc.StudentId, sc.CourseId }
                    into scs
                from matched in scs.DefaultIfEmpty()
                select new CourseWithGrade(stc.Course, matched?.Score)
        };
    }

    public string WindowTitle
        => $"Grade Management - {_user.Name} ({_user.Id})";

    public Student StudentData
        => _user;

    public decimal AverageScore
        => (from sc in _user.Scs select sc.Score).Average();

    public decimal TotalCredit
        => (from sc in _user.Scs select sc.Course.Credit).Sum(Convert.ToDecimal);

    public int GradedCourseCount
        => (from sc in _user.Scs select sc).Count();

    public int TotalCourseCount
        => (from stc in _user.Stcs select stc).Count();

    public int UngradedCourseCount
        => TotalCourseCount - GradedCourseCount;

    private CollectionViewSource _courses;

    public CollectionViewSource Courses
    {
        get => _courses;
        set => SetProperty(ref _courses, value);
    }
}