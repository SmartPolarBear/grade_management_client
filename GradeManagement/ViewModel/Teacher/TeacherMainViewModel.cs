using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Data.Model;
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
    private bool _filtered = false;

    public TeacherMainViewModel(TeacherUser teacher)
    {
        TeacherData = (teacher.Data as Teacher)!;
        _service = new TeacherService(TeacherData);

        ClearCourseFilterCommand = new DelegateCommand(ClearCourseFilter,
            _ =>
                FilterGradingMethod || FilterCredit || FilterKeyword || _filtered);

        FilterResultsCommand = new DelegateCommand(FilterResults,
            _ =>
                FilterGradingMethod || FilterCredit || FilterKeyword);

        _filtered = false;

        FilterResults(null);
    }

    private void FilterResults(object? filterToolBar)
    {
        UpdateCourses(filterToolBar);
        UpdateStudents(filterToolBar);
    }

    private void UpdateStudents(object? filterToolBar)
    {
        var students = from s in TeacherData.Stcs select s;

        if (filterToolBar == null || !FilterGradingMethod && !FilterCredit && !FilterKeyword)
        {
            StudentsWithCourses = students;
            _filtered = false;
            return;
        }

        var toolbar = (filterToolBar as ToolBar)!;

        if (FilterGradingMethod)
        {
            var gradingMethodComboBox = (ComboBox)toolbar.FindName("GradingMethodComboBox")!;

            students = from s in students
                where (CourseGradingMethod)s.Course.GradingMethod ==
                      (CourseGradingMethod)gradingMethodComboBox.SelectedValue
                select s;

            _filtered = true;
        }

        if (FilterCredit)
        {
            var creditComboBox = (ComboBox)toolbar.FindName("CreditComboBox")!;

            students = from s in students
                where Convert.ToInt32(s.Course.Credit) == Convert.ToInt32(creditComboBox.SelectedValue)
                select s;

            _filtered = true;
        }

        if (FilterKeyword)
        {
            var keywordTextBox = (TextBox)toolbar.FindName("KeywordTextBox")!;

            students = from s in students
                where s.Course.Name.Contains(keywordTextBox.Text) || s.Course.Id.Contains(keywordTextBox.Text)
                                                                  || s.Student.Name.Contains(keywordTextBox.Text) ||
                                                                  s.Student.Id.Contains(keywordTextBox.Text)
                select s;

            _filtered = true;
        }


        StudentsWithCourses = students;
    }

    private void UpdateCourses(object? filterToolBar)
    {
        var courses = from c in _service.TaughtCourses
            select c.Data as Course;


        if (filterToolBar == null || !FilterGradingMethod && !FilterCredit && !FilterKeyword)
        {
            Courses = courses;
            _filtered = false;
            return;
        }

        var toolbar = (filterToolBar as ToolBar)!;

        if (FilterGradingMethod)
        {
            var gradingMethodComboBox = (ComboBox)toolbar.FindName("GradingMethodComboBox")!;

            courses = from c in courses
                where (CourseGradingMethod)c.GradingMethod ==
                      (CourseGradingMethod)gradingMethodComboBox.SelectedValue
                select c;

            _filtered = true;
        }

        if (FilterCredit)
        {
            var creditComboBox = (ComboBox)toolbar.FindName("CreditComboBox")!;

            courses = from c in courses
                where Convert.ToInt32(c.Credit) == Convert.ToInt32(creditComboBox.SelectedValue)
                select c;

            _filtered = true;
        }

        if (FilterKeyword)
        {
            var keywordTextBox = (TextBox)toolbar.FindName("KeywordTextBox")!;

            courses = from c in courses
                where c.Name.Contains(keywordTextBox.Text) || c.Id.Contains(keywordTextBox.Text)
                select c;

            _filtered = true;
        }

        Courses = courses;
    }

    private void ClearCourseFilter(object? filterToolBar)
    {
        FilterGradingMethod = FilterCredit = FilterKeyword = false;
        FilterResults(filterToolBar);
        _filtered = false;
        ClearCourseFilterCommand.RaiseCanExecuteChanged();
        FilterResultsCommand.RaiseCanExecuteChanged();
    }

    private void RefreshData(object? filterToolBar)
    {
        _service = new TeacherService(TeacherData);
        NotifyAllPropertiesChanged<TeacherMainViewModel>();
        FilterResults(filterToolBar);
    }

    private void ChangePassword()
    {
        var service = new TeacherViewService(TeacherData);
        service.ShowChangePasswordDialog();
        FilterResults(null);
    }

    private void ModifyProfile()
    {
        var service = new TeacherViewService(TeacherData);
        service.ShowChangeAccountInfoDialog();
        NotifyPropertyChanged(nameof(TeacherData));
    }

    public string WindowTitle => $"Teacher - {TeacherData.Name.Trim()}";

    public Teacher TeacherData { get; }

    private IEnumerable<Stc>? _studentsWithCourses;

    public IEnumerable<Stc>? StudentsWithCourses
    {
        get => _studentsWithCourses;
        set
        {
            SetProperty(ref _studentsWithCourses, value);
            NotifyPropertyChanged(nameof(StudentCount));
        }
    }


    private IEnumerable<Course>? _courses;

    public IEnumerable<Course>? Courses
    {
        get => _courses;
        set
        {
            SetProperty(ref _courses, value);
            NotifyPropertyChanged(nameof(CourseCount));
        }
    }

    public int CourseCount => Courses?.Count() ?? 0;

    public int StudentCount => StudentsWithCourses?.Count() ?? 0;

    public int AllCourseCount => TeacherData.Courses.Count;

    public int AllStudentCount => TeacherData.Stcs.Count;

    public IEnumerable<int> AllCredits =>
        (from c in TeacherData.Courses select Convert.ToInt32(c.Credit))
        .Distinct()
        .OrderBy(Convert.ToInt32);

    public IEnumerable<CourseGradingMethod> AllGradingMethods =>
        (from c in TeacherData.Courses select (CourseGradingMethod)c.GradingMethod)
        .Distinct()
        .OrderBy(i => (int)i);

    private bool _filterGradingMethod;

    public bool FilterGradingMethod
    {
        get => _filterGradingMethod;
        set
        {
            SetProperty(ref _filterGradingMethod, value);
            ClearCourseFilterCommand.RaiseCanExecuteChanged();
            FilterResultsCommand.RaiseCanExecuteChanged();
        }
    }

    private bool _filterKeyword;

    public bool FilterKeyword
    {
        get => _filterKeyword;
        set
        {
            SetProperty(ref _filterKeyword, value);
            ClearCourseFilterCommand.RaiseCanExecuteChanged();
            FilterResultsCommand.RaiseCanExecuteChanged();
        }
    }

    private bool _filterCredit;

    public bool FilterCredit
    {
        get => _filterCredit;
        set
        {
            SetProperty(ref _filterCredit, value);
            ClearCourseFilterCommand.RaiseCanExecuteChanged();
            FilterResultsCommand.RaiseCanExecuteChanged();
        }
    }

    public ICommand RefreshCommand => new DelegateCommand(RefreshData,
        _ => true);

    public ICommand ChangePasswordCommand => new DelegateCommand(_ => ChangePassword(),
        _ => true);

    public ICommand ModifyProfileCommand => new DelegateCommand(_ => ModifyProfile(),
        _ => true);


    public DelegateCommand FilterResultsCommand { get; }

    public DelegateCommand ClearCourseFilterCommand { get; }
}