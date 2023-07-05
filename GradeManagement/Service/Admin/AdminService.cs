using System.Collections.Generic;
using System.Threading.Tasks;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using Course = GradeManagement.Data.Model.Course;

namespace GradeManagement.Service.Admin;

using Admin = Data.Model.Admin;
using Student = Data.Model.Student;
using Teacher = Data.Model.Teacher;

public sealed class AdminService
{
    private readonly Admin _admin;
    private readonly GradeManagementContext _dbc;

    public AdminService(Admin admin)
    {
        _admin = admin;
        _dbc = new GradeManagementContext(UserType.Admin);
    }

    public void AddCourse(Course course)
    {
        _dbc.Courses.Add(course);
        _dbc.SaveChanges();
    }

    public void AddStudent(Student student)
    {
        _dbc.Students.Add(student);
        _dbc.SaveChanges();
    }

    public void AddTeacher(Teacher teacher)
    {
        _dbc.Teachers.Add(teacher);
        _dbc.SaveChanges();
    }

    public bool ValidatePassword(string password)
    {
        return _admin.Password == password;
    }

    public async Task<bool> ChangePasswordAsync(string newPassword)
    {
        _admin.Password = newPassword;
        _dbc.Entry(_admin).State = EntityState.Modified;
        return await _dbc.SaveChangesAsync() > 0;
    }

    public void UpdateCourse(Course itemCourse)
    {
        _dbc.Entry(itemCourse).State = EntityState.Modified;
        _dbc.SaveChanges();
    }


    public void UpdateStudent(Student itemStudent)
    {
        _dbc.Entry(itemStudent).State = EntityState.Modified;
        _dbc.SaveChanges();
    }

    public void UpdateTeacher(Teacher itemTeacher)
    {
        _dbc.Entry(itemTeacher).State = EntityState.Modified;
        _dbc.SaveChanges();
    }


    public void DeleteStudent(Student student)
    {
        _dbc.Students.Remove(student);
        _dbc.SaveChanges();
    }

    public void DeleteTeacher(Teacher teacher)
    {
        _dbc.Teachers.Remove(teacher);
        _dbc.SaveChanges();
    }

    public void DeleteCourse(Course course)
    {
        _dbc.Courses.Remove(course);
        _dbc.SaveChanges();
    }

    public void RollbackAudit(Scaudit audit)
    {
        _dbc.Database.ExecuteSqlRaw("EXECUTE dbo.RollbackAudit @p0", audit.Id);
    }

    public IEnumerable<Scaudit> Audits => _dbc.Scaudits;

    public IEnumerable<Course> Courses => _dbc.Courses;

    public IEnumerable<Student> Students => _dbc.Students;

    public IEnumerable<Teacher> Teachers => _dbc.Teachers;

    public IEnumerable<Sc> Grades => _dbc.Scs;
}