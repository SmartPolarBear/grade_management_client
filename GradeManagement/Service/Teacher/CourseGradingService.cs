using System.Collections.Generic;
using System.Linq;
using GradeManagement.Data;
using GradeManagement.Data.Model;

namespace GradeManagement.Service.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

public class CourseGradingService
{
    private readonly Teacher _teacher;
    private readonly Course _course;

    private readonly GradeManagementContext _dbc;

    public CourseGradingService(Teacher teacher, Course course)
    {
        _teacher = teacher;
        _course = course;
        _dbc = new GradeManagementContext(UserType.Teacher);
    }

    public int StudentCount => _teacher.Stcs.Count(i => i.CourseId == _course.Id);

    public IEnumerable<Stc> GradedStudents =>
        from stc in _teacher.Stcs
        where stc.CourseId == _course.Id
        join sc in _dbc.Scs on stc.StudentId equals sc.StudentId
        where sc.Score != null
        select stc;

    public IEnumerable<Stc> UngradedStudents =>
        from stc in _teacher.Stcs
        where stc.CourseId == _course.Id
        join sc in _dbc.Scs on stc.StudentId equals sc.StudentId
        where sc.Score == null
        select stc;
}