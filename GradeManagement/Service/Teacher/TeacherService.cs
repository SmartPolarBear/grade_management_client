using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeManagement.Data;
using Microsoft.EntityFrameworkCore;
using CourseRecord = GradeManagement.Data.Course;

namespace GradeManagement.Service.Teacher;

public enum GradingStatus
{
    None,
    Partial,
    All
}

public sealed class TeacherService
{
    private readonly Data.Model.Teacher _teacher;
    private readonly GradeManagementContext _dbc;

    public TeacherService(Data.Model.Teacher teacher)
    {
        _teacher = teacher;
        _dbc = new GradeManagementContext(UserType.Teacher);
    }

    public bool ValidatePassword(string password)
    {
        return _teacher.Password == password;
    }

    public async Task<bool> ChangePasswordAsync(string newPassword)
    {
        _teacher.Password = newPassword;
        _dbc.Entry(_teacher).State = EntityState.Modified;
        return await _dbc.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateTeacherInfoAsync(Data.Model.Teacher teacher)
    {
        _dbc.Entry(teacher).State = EntityState.Modified;
        return await _dbc.SaveChangesAsync() > 0;
    }

    public IEnumerable<CourseRecord> TaughtCourses =>
        from c in _teacher.Courses
        select new CourseRecord(c.Id, c);
}