using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using GradeManagement.Service.Admin;
using GradeManagement.ViewModel.Teacher;
using Course = GradeManagement.Data.Model.Course;

namespace GradeManagement.ViewModel.Admin;

using Admin = Data.Model.Admin;
using Student = Data.Model.Student;
using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

public record StudentViewItem(Student Student, Action<StudentViewItem> Callback)
    : IEditableObject
{
    public void BeginEdit()
    {
    }

    public void CancelEdit()
    {
    }

    public void EndEdit()
    {
        try
        {
            Callback(this);
        }
        catch (Exception e)
        {
            MessageBox.Show("Please enter a valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}

public record TeacherViewItem(Teacher Teacher, Action<TeacherViewItem> Callback)
    : IEditableObject
{
    public void BeginEdit()
    {
    }

    public void CancelEdit()
    {
    }

    public void EndEdit()
    {
        try
        {
            Callback(this);
        }
        catch (Exception e)
        {
            MessageBox.Show("Please enter a valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}

public record CourseViewItem(Course Course, Action<CourseViewItem> Callback)
    : IEditableObject
{
    public void BeginEdit()
    {
    }

    public void CancelEdit()
    {
    }

    public void EndEdit()
    {
        try
        {
            Callback(this);
        }
        catch (Exception e)
        {
            MessageBox.Show("Please enter a valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}

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

        RefreshAll();
    }

    public void RefreshAll()
    {
        Audit = new ObservableCollection<Scaudit>(_service.Audits);

        Courses = new ObservableCollection<CourseViewItem>(from c in _service.Courses
            select new CourseViewItem(c, item => { _service.UpdateCourse(item.Course); }));
        Students = new ObservableCollection<StudentViewItem>(from s in _service.Students
            select new StudentViewItem(s, item => { _service.UpdateStudent(item.Student); }));
        Teachers = new ObservableCollection<TeacherViewItem>(from t in _service.Teachers
            select new TeacherViewItem(t, item => { _service.UpdateTeacher(item.Teacher); }));
    }

    public void AddStudent()
    {
        var student = _viewService.ShowAddStudentDialog();
        if (student is null) return;
        _service.AddStudent(student);
        Students.Add(new StudentViewItem(student, item => { _service.UpdateStudent(item.Student); }));
    }

    public void AddTeacher()
    {
        var teacher = _viewService.ShowAddTeacherDialog();
        if (teacher is null) return;
        _service.AddTeacher(teacher);
        Teachers.Add(new TeacherViewItem(teacher, item => { _service.UpdateTeacher(item.Teacher); }));
    }


    private void AddCourse()
    {
        var course = _viewService.ShowAddCourseDialog();
        if (course is null) return;
        _service.AddCourse(course);
        Courses.Add(new CourseViewItem(course, item => { _service.UpdateCourse(item.Course); }));
    }

    public Admin AdminData { get; }

    public string WindowTitle => $"Administrator - {AdminData.Name}";

    private ObservableCollection<Scaudit> _audit;

    public ObservableCollection<Scaudit> Audit
    {
        get => _audit;
        set => SetProperty(ref _audit, value);
    }


    private ObservableCollection<CourseViewItem> _courses;

    public ObservableCollection<CourseViewItem> Courses
    {
        get => _courses;
        set => SetProperty(ref _courses, value);
    }


    private ObservableCollection<StudentViewItem> _students;

    public ObservableCollection<StudentViewItem> Students
    {
        get => _students;
        set => SetProperty(ref _students, value);
    }

    private ObservableCollection<TeacherViewItem> _teachers;

    public ObservableCollection<TeacherViewItem> Teachers
    {
        get => _teachers;
        set => SetProperty(ref _teachers, value);
    }

    public IEnumerable<Sc> Grades => _service.Grades.ToList();

    public DelegateCommand AddStudentCommand => new(_ => AddStudent(), _ => true);

    public DelegateCommand AddTeacherCommand => new(_ => AddTeacher(), _ => true);

    public DelegateCommand AddCourseCommand => new(_ => AddCourse(), _ => true);
}