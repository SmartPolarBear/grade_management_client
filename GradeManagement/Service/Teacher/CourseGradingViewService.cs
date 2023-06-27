using System;
using System.Linq;
using GradeManagement.View.Teacher;

namespace GradeManagement.Service.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using GradeComposition = Data.Model.GradeComposition;

public class CourseGradingViewService(Teacher teacher, Course course)
{
    private readonly Teacher _teacher = teacher;
    private readonly Course _course = course;

    public void ShowEditGradeCompositionDialog(Action? onDialogClosed = null)
    {
        var dialog = new EditGradeCompositionDialog(_teacher, _course);
        dialog.ShowDialog();
        dialog.Closed += (sender, args) => onDialogClosed?.Invoke();
    }

    public decimal? ShowComplexGradingDialog(Action? onDialogClosed = null)
    {
        var service = new CourseGradingService(_teacher, _course);
        var dialog = new ComplexGradingDialog(service.GradeCompositions.ToList());
        dialog.ShowDialog();
        dialog.Closed += (sender, args) => onDialogClosed?.Invoke();
        if (dialog.DialogResult == true)
        {
            return dialog.GradeResult;
        }
        else
        {
            return null;
        }
    }
}