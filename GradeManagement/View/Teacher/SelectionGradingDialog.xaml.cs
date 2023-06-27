using System;
using System.Collections.Generic;
using System.Windows;
using GradeManagement.Data;

namespace GradeManagement.View.Teacher;

public partial class SelectionGradingDialog : Window, IGradingDialog
{
    public IEnumerable<string> Grades { get; }

    public int InitialSelectedIndex { get; private set; } = 0;

    public decimal GradeResult { get; private set; }

    private CourseGradingMethod _method;

    public SelectionGradingDialog(CourseGradingMethod method, decimal? initialGrade)
    {
        _method = method;
        this.Grades = method switch
        {
            CourseGradingMethod.Score5 => new List<string> { "A", "B", "C", "D", "F" },
            CourseGradingMethod.PF => new List<string> { "Pass", "Fail" },
            _ => throw new ArgumentException("Cannot use Score100 grading method for selection grading!")
        };

        InitialSelectedIndex = method switch
        {
            CourseGradingMethod.Score5 => 4 - (int)(initialGrade ?? 4),
            CourseGradingMethod.PF => 1 - (int)(initialGrade ?? 0),
            _ => throw new ArgumentException("Cannot use Score100 grading method for selection grading!")
        };

        InitializeComponent();
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        GradeResult = _method switch
        {
            CourseGradingMethod.PF => 1 - GradeComboBox.SelectedIndex,
            CourseGradingMethod.Score5 => 5 - GradeComboBox.SelectedIndex,
            _ => throw new ArgumentException("Cannot use Score100 grading method for selection grading!")
        };
        this.DialogResult = true;
        this.Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }
}