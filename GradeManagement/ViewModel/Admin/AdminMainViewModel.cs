using System.Collections.Generic;
using System.Collections.ObjectModel;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using GradeManagement.Service.Admin;
using Course = GradeManagement.Data.Model.Course;

namespace GradeManagement.ViewModel.Admin;

using Admin = Data.Model.Admin;
using Student = Data.Model.Student;
using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

public class AdminMainViewModel
    : ViewModelBase
{
    private readonly AdminService _service;

    public AdminMainViewModel(AdminUser admin)
    {
        AdminData = (admin.Data as Admin)!;
        _service = new AdminService(AdminData);

        Audit = new ObservableCollection<Scaudit>(_service.Audits);
        Courses = new ObservableCollection<Course>(_service.Courses);
        Students = new ObservableCollection<Student>(_service.Students);
        Teachers = new ObservableCollection<Teacher>(_service.Teachers);
    }

    public Admin AdminData { get; }

    public ObservableCollection<Scaudit> Audit { get; }

    public ObservableCollection<Course> Courses { get; }

    public ObservableCollection<Student> Students { get; }

    public ObservableCollection<Teacher> Teachers { get; }
}