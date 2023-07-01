using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using Course = GradeManagement.Data.Model.Course;

namespace GradeManagement.Service.Student;

using Student = Data.Model.Student;
using Teacher = Data.Model.Teacher;

public sealed class StudentService(Student student)
{
    private readonly Student _student = student;
    private readonly GradeManagementContext _dbc = new(UserType.Student);

    public bool ValidatePassword(string password)
    {
        return _student.Password == password;
    }

    public async Task<bool> ChangePasswordAsync(string newPassword)
    {
        _student.Password = newPassword;
        _dbc.Entry(_student).State = EntityState.Modified;
        return await _dbc.SaveChangesAsync() > 0;
    }

    public IEnumerable<Sc> Grades
        => from sc in _student.Scs
            select sc;

    public IEnumerable<Course> Courses
        => from stc in _student.Stcs
            select stc.Course;

    public IEnumerable<Teacher> Teachers
        => from stc in _student.Stcs
            select stc.Teacher;

    public decimal Gpa
        => (from sc in _student.Scs
                join course in _dbc.Courses on sc.CourseId equals course.Id
                where (CourseGradingMethod)course.GradingMethod != CourseGradingMethod.PF
                select ((CourseGradingMethod)course.GradingMethod).ScoreToGpa(sc.Score!))
            .Average(i => i.Value);

    
}