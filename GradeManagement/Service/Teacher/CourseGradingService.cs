using System.Collections.Generic;
using System.Linq;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using Student = GradeManagement.Data.Model.Student;

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

    public IEnumerable<Stc> Stcs =>
        from stc in _teacher.Stcs
        where stc.CourseId == _course.Id
        select stc;

    public IEnumerable<Student> Students =>
        from stc in Stcs
        select stc.Student;

    public IEnumerable<(Student Student, decimal? Grade)> StudentsWithGrades =>
        from s in Stcs
        join sc in _dbc.Scs on
            new { s.StudentId, s.CourseId } equals
            new { sc.StudentId, sc.CourseId }
            into scs
        from sc in scs.DefaultIfEmpty()
        select (s.Student, sc?.Score);

    public IEnumerable<(Student Student, Sc? Sc)> GradedStudents =>
        from s in Stcs
        join sc in _dbc.Scs on
            new { s.StudentId, s.CourseId } equals
            new { sc.StudentId, sc.CourseId }
            into scs
        from sc in scs.DefaultIfEmpty()
        where sc != null
        select (s.Student, sc);

    public IEnumerable<(Student Student, Sc? Sc)> UngradedStudents =>
        from s in Stcs
        join sc in _dbc.Scs on
            new { s.StudentId, s.CourseId } equals
            new { sc.StudentId, sc.CourseId }
            into scs
        from sc in scs.DefaultIfEmpty()
        where sc == null
        select (s.Student, sc);
}