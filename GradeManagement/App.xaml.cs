// #define DEBUG_TEACHER
// #define DEBUG_STUDENT

#define DEBUG_ADMIN

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using GradeManagement.Service.Login;
using GradeManagement.View.Student;
using Microsoft.Extensions.Configuration;

namespace GradeManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        public App()
        {
        }

        private void DebugDirectlyLoadTeacher()
        {
            var loginService = new LoginService("teacher", Data.UserType.Teacher);
            var teacher = loginService.Login("12345678");
            var teacherWindow = new View.Teacher.TeacherMainWindow((teacher as TeacherUser)!);
            teacherWindow.Show();
        }

        private void DebugDirectlyLoadStudent()
        {
            var loginService = new LoginService("student", Data.UserType.Student);
            var teacher = loginService.Login("12345678");
            var teacherWindow = new StudentMainWindow((teacher as StudentUser)!);
            teacherWindow.Show();
        }

        private void DebugDirectlyLoadAdmin()
        {
            var loginService = new LoginService("admin", Data.UserType.Admin);
            var teacher = loginService.Login("12345678");
            var adminWindow = new View.Admin.AdminMainWindow((teacher as AdminUser)!);
            adminWindow.Show();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
#if DEBUG_TEACHER
            DebugDirectlyLoadTeacher();
#elif DEBUG_STUDENT
            DebugDirectlyLoadStudent();
#elif DEBUG_ADMIN
            DebugDirectlyLoadAdmin();
#else
            var loginWindow = new MainWindow();
            loginWindow.ShowDialog();
#endif
        }
    }
}