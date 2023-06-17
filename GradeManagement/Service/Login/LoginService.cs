using System;
using System.Linq;
using GradeManagement.Data;
using GradeManagement.Data.Base;
using GradeManagement.Data.Model;
using Microsoft.Identity.Client;

namespace GradeManagement.Service.Login;

public class LoginService
{
    private readonly string _userName;
    private readonly UserType _userType;
    private readonly GradeManagementContext _dbc;

    public LoginService(string userName, UserType userType)
    {
        _userName = userName;
        _userType = userType;
        _dbc = new GradeManagementContext(UserType.Default); // use default because before login, no user are there
    }

    private User? LoginAdmin(string pwd)
        => (from d in _dbc.Admins
            where d.Id == _userName || d.Name == _userName || d.Email == _userName
            where d.Password == pwd
            select new AdminUser(d)).FirstOrDefault();

    private User? LoginStudent(string pwd)
        => (from d in _dbc.Students
            where d.Id == _userName || d.Name == _userName || d.Email == _userName
            where d.Password == pwd
            select new StudentUser(d)).FirstOrDefault();

    private User? LoginTeacher(string pwd)
        => (from d in _dbc.Teachers
            where d.Id == _userName || d.Name == _userName || d.Email == _userName
            where d.Password == pwd
            select new TeacherUser(d)).FirstOrDefault();

    public User? Login(string pwd)
        => _userType switch
        {
            UserType.Admin => LoginAdmin(pwd),
            UserType.Student => LoginStudent(pwd),
            UserType.Teacher => LoginTeacher(pwd),
            _ => throw new ArgumentOutOfRangeException(nameof(_userType), _userType,
                "No login method for this user type")
        };
}