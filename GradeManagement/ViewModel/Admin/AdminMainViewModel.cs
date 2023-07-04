using System.Collections.Generic;
using System.Collections.ObjectModel;
using GradeManagement.Base.Command;
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
    private readonly AdminViewService _viewService;

    public AdminMainViewModel(AdminUser admin)
    {
        AdminData = (admin.Data as Admin)!;

        _service = new AdminService(AdminData);
        _viewService = new AdminViewService(AdminData);

        Audit = new ObservableCollection<Scaudit>(_service.Audits);
        Courses = new ObservableCollection<Course>(_service.Courses);
        Students = new ObservableCollection<Student>(_service.Students);
        Teachers = new ObservableCollection<Teacher>(_service.Teachers);
    }

    public void AddStudent()
    {
        var student = _viewService.ShowAddStudentDialog();
        if (student is null) return;
        _service.AddStudent(student);
        Students.Add(student);
    }

    public void AddTeacher()
    {
        var teacher = _viewService.ShowAddTeacherDialog();
        if (teacher is null) return;
        _service.AddTeacher(teacher);
        Teachers.Add(teacher);
    }

    public Admin AdminData { get; }
    
    public string WindowTitle => $"Administrator - {AdminData.Name}";

    public ObservableCollection<Scaudit> Audit { get; }

    public ObservableCollection<Course> Courses { get; }

    public ObservableCollection<Student> Students { get; }

    public ObservableCollection<Teacher> Teachers { get; }

    public DelegateCommand AddStudentCommand => new(_ => AddStudent(), _ => true);

    public DelegateCommand AddTeacherCommand => new(_ => AddTeacher(), _ => true);
}