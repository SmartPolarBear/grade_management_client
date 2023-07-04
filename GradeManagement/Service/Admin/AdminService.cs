using System.Collections.Generic;
using GradeManagement.Data;
using GradeManagement.Data.Model;
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


    public IEnumerable<Scaudit> Audits => _dbc.Scaudits;

    public IEnumerable<Course> Courses => _dbc.Courses;

    public IEnumerable<Student> Students => _dbc.Students;

    public IEnumerable<Teacher> Teachers => _dbc.Teachers;
}