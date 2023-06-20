using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using Course = GradeManagement.Data.Model.Course;
using CourseRecord = GradeManagement.Data.Course;

namespace GradeManagement.Service.Login;

public sealed class TeacherService
{
    private readonly Teacher _teacher;
    private readonly GradeManagementContext _dbc;

    public TeacherService(Teacher teacher)
    {
        _teacher = teacher;
        _dbc = new GradeManagementContext(UserType.Teacher);
    }

    public IEnumerable<CourseRecord> TaughtCourses =>
        from c in _dbc.Courses
        where c.Teachers.Contains(_teacher)
        select new CourseRecord(c.Id, c);

    public IEnumerable AverageScores =>
        from stcsc in (
            from stc in _dbc.Stcs
            where stc.Teacher.Id == _teacher.Id
            join sc in _dbc.Scs on
                new { Sno = stc.Student.Id, Cno = stc.Course.Id }
                equals
                new { Sno = sc.Student.Id, Cno = sc.Course.Id }
            select new { stc, sc.Score }
        )
        group stcsc by stcsc.stc.Course.Id
        into g
        select new { CourseId = g.Key, Avg = g.Average(stcsc => stcsc.Score) };
}