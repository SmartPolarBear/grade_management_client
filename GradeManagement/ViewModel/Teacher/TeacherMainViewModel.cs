using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Service.Login;
using GradeManagement.Service.Teacher;
using Course = GradeManagement.Data.Model.Course;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using CourseRecord = Data.Course;

public class TeacherMainViewModel
    : ViewModelBase
{
    private TeacherService _service;

    public TeacherMainViewModel(TeacherUser teacher)
    {
        TeacherData = (teacher.Data as Teacher)!;
        _service = new TeacherService(TeacherData);
    }

    private void RefreshData()
    {
        _service = new TeacherService(TeacherData);
        NotifyAllPropertiesChanged<TeacherMainViewModel>();
    }

    private void ChangePassword()
    {
        var service = new TeacherViewService(TeacherData);
        service.ShowChangePasswordDialog();
    }

    public Teacher TeacherData { get; }

    public IEnumerable<Course> Courses =>
        from c in _service.TaughtCourses
        select c.Data as Course;

    public int CourseCount => Courses.Count();

    public ICommand RefreshCommand => new DelegateCommand(_ => RefreshData(),
        _ => true);
    
    public ICommand ChangePasswordCommand => new DelegateCommand(_ => ChangePassword(),
        _ => true);
}