using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using Course = GradeManagement.Data.Model.Course;
using CourseRecord = GradeManagement.Data.Course;

namespace GradeManagement.Service.Login;

public enum GradingStatus
{
    None,
    Partial,
    All
}

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
        from c in _teacher.Courses
        select new CourseRecord(c.Id, c);

    public IEnumerable<(string CourseId, GradingStatus Status, double Average)> CourseGrading =>
        from stc in _teacher.Stcs
        join sc in _dbc.Scs on stc.CourseId equals sc.CourseId into stcsc
        from stg in stcsc.DefaultIfEmpty()
        group stg by stg.CourseId
        into g
        select (g.Key,
            g.Count(i => i.Score != null) == _teacher.Stcs.Count
                ? GradingStatus.All
                : g.Count(i => i.Score != null) == 0
                    ? GradingStatus.None
                    : GradingStatus.Partial,
            g.Average(i => i.Score ?? 0));
}