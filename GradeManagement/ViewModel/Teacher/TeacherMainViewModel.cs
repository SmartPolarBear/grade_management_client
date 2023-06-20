using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Service.Login;
using Course = GradeManagement.Data.Model.Course;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using CourseRecord = Data.Course;

public record CourseDisplayItem(string Id, Course Data, GradingStatus Status, double Average);

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

    public Teacher TeacherData { get; }

    public IEnumerable<CourseDisplayItem> Courses =>
        from c in _service.TaughtCourses
        join cg in _service.CourseGrading on c.Id equals cg.CourseId
        select new CourseDisplayItem(c.Id, c.Data as Course, cg.Status, cg.Average);

    public int CourseCount => Courses.Count();

    public ICommand RefreshCommand => new DelegateCommand(_ => RefreshData(),
        _ => true);
}