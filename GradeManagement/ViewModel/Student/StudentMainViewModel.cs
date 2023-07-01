using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data;
using GradeManagement.Service.Student;
using GradeManagement.Utils;

namespace GradeManagement.ViewModel.Student;

using Student = Data.Model.Student;
using Course = Data.Model.Course;

public sealed record CourseWithGrade(Course Course, decimal? Grade)
{
    public string DisplayGrade
        => ((CourseGradingMethod)Course.GradingMethod).DisplayGrade(Grade) ?? "N/A";

    public string DisplayGradingMethod
        => ((CourseGradingMethod)Course.GradingMethod).ToDisplayName();
}

public class StudentMainViewModel
    : ViewModelBase
{
    private readonly StudentService _service;
    private readonly StudentViewService _viewService;

    public StudentMainViewModel(StudentUser user)
    {
        StudentData = (user.Data as Student)!;
        _service = new StudentService(StudentData);
        _viewService = new StudentViewService(StudentData);

        _courses = new CollectionViewSource
        {
            Source = GetFilteredItems()
        };

        if (AllTerms.Any())
        {
            FilterTerm = AllTerms.First();
        }

        UpdateEmailCommand = new DelegateCommand((addr) => UpdateEmail(addr as string),
            s =>
                s != null && s.ToString().ValidateEmail());
    }

    private IEnumerable<CourseWithGrade> GetFilteredItems()
    {
        var courses = from stc in StudentData.Stcs
            join sc in StudentData.Scs on new { stc.StudentId, stc.CourseId } equals new
                    { sc.StudentId, sc.CourseId }
                into scs
            from matched in scs.DefaultIfEmpty()
            select new CourseWithGrade(stc.Course, matched?.Score);

        if (IsTermFiltering)
        {
            courses = courses.Where(c => c.Course.Term == FilterTerm);
        }

        if (IsKeywordFiltering)
        {
            courses = courses.Where(c =>
                c.Course.Name.ToLower().Trim().Contains(FilterKeyword.ToLower().Trim()));
        }

        return courses;
    }

    private void UpdateGrouping()
    {
        var view = _courses.View;
        view.GroupDescriptions.Clear();
        if (!IsGrouping)
        {
            return;
        }

        if (GroupingMode == 0)
        {
            view.GroupDescriptions.Add(new PropertyGroupDescription("Course.Term"));
        }
        else if (GroupingMode == 1)
        {
            view.GroupDescriptions.Add(new PropertyGroupDescription("Course.Credit"));
        }
        else if (GroupingMode == 2)
        {
            view.GroupDescriptions.Add(new PropertyGroupDescription("DisplayGradingMethod"));
        }

        view.Refresh();

        NotifyPropertyChanged(nameof(Courses));
    }

    private void UpdateFilter()
    {
        _courses = new CollectionViewSource
        {
            Source = GetFilteredItems()
        };
        NotifyPropertyChanged(nameof(Courses));

        UpdateGrouping();
    }

    private void UpdateEmail(string? newEmail)
    {
        StudentData.Email = newEmail!;
        _service.UpdateEmail(newEmail!);
        NotifyPropertyChanged(nameof(StudentData));
    }


    public string WindowTitle
        => $"Grade Management - {StudentData.Name} ({StudentData.Id})";

    public Student StudentData { get; }

    public string StudentGender
        => ((Gender)StudentData.Gender).DisplayName();


    public decimal AverageScore
        => (from sc in _service.Grades
            join c in _service.Courses on sc.CourseId equals c.Id
            where (CourseGradingMethod)c.GradingMethod != CourseGradingMethod.PF
            select ((CourseGradingMethod)c.GradingMethod)switch
            {
                CourseGradingMethod.Score5 => sc.Score * 20,
                CourseGradingMethod.Score100 => sc.Score,
                _ => throw new ArgumentOutOfRangeException()
            }).Average();

    public decimal TotalCredit
        => (from c in _service.Courses select c.Credit).Sum(Convert.ToDecimal);

    public int GradedCourseCount
        => _service.Grades.Count();

    public int TotalCourseCount
        => _service.Courses.Count();

    public int UngradedCourseCount
        => TotalCourseCount - GradedCourseCount;

    public IEnumerable<string> AllTerms
        => _service.Courses.Select(c => c.Term).Distinct();


    private bool _isGrouping = false;

    public bool IsGrouping
    {
        get => _isGrouping;
        set
        {
            SetProperty(ref _isGrouping, value);
            UpdateGrouping();
        }
    }

    private int _groupingMode = 0;

    public int GroupingMode
    {
        get => _groupingMode;
        set
        {
            SetProperty(ref _groupingMode, value);
            UpdateGrouping();
        }
    }


    private bool _isTermFiltering = false;

    public bool IsTermFiltering
    {
        get => _isTermFiltering;
        set
        {
            SetProperty(ref _isTermFiltering, value);
            UpdateFilter();
        }
    }

    private string _filterTerm = string.Empty;

    public string FilterTerm
    {
        get => _filterTerm;
        set
        {
            SetProperty(ref _filterTerm, value);
            UpdateFilter();
        }
    }

    private string _filterName = string.Empty;

    public string FilterName
    {
        get => _filterName;
        set
        {
            SetProperty(ref _filterName, value);
            UpdateFilter();
        }
    }

    private bool _isKeywordFiltering = false;

    public bool IsKeywordFiltering
    {
        get => _isKeywordFiltering;
        set
        {
            SetProperty(ref _isKeywordFiltering, value);
            UpdateFilter();
        }
    }

    private string _filterKeyword = string.Empty;

    public string FilterKeyword
    {
        get => _filterKeyword;
        set
        {
            SetProperty(ref _filterKeyword, value);
            UpdateFilter();
        }
    }


    private CollectionViewSource _courses;

    public CollectionViewSource Courses
    {
        get => _courses;
        set => SetProperty(ref _courses, value);
    }

    public string DisplayGpa
        => $"{_service.Gpa:F2}";

    public DelegateCommand UpdateEmailCommand { get; }

    public DelegateCommand ChangePasswordCommand
        => new DelegateCommand((_) => _viewService.ShowChangePasswordDialog(),
            (_) => true);
}