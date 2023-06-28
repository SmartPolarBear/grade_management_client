using System;
using System.Linq;
using System.Windows;
using GradeManagement.Data;
using GradeManagement.Data.Model;

namespace GradeManagement.View.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

public partial class CourseStatisticWindow : Window
{
    public CourseStatisticWindow(Teacher teacher, Course course)
    {
        InitializeComponent();

        var allGrades =
            (from stc in teacher.Stcs
                join sc in course.Scs
                    on new { stc.StudentId, stc.CourseId } equals
                    new { sc.StudentId, sc.CourseId }
                where stc.CourseId == course.Id
                select sc.Score).ToList();

        var average = allGrades.Average();
        AverageTextBlock.Text = average.ToString("F2");
        StdTextBlock.Text = Math.Sqrt(Convert.ToDouble((from g in allGrades
            select (g - average) * (g - average)).Average())).ToString("F2");


        var (passThreshold, excelThreshold) = (CourseGradingMethod)course.GradingMethod switch
        {
            CourseGradingMethod.PF => (1.0m, 1.0m),
            CourseGradingMethod.Score5 => (1.0m, 4.0m),
            CourseGradingMethod.Score100 => (60.0m, 90.0m),
        };

        var passCount = allGrades.Count(g => g >= passThreshold);
        var excelCount = allGrades.Count(g => g >= excelThreshold);
        var allCount = (from stc in teacher.Stcs where stc.CourseId == course.Id select stc).Count();

        PassingTextBlock.Text = $"{passCount} of {allCount} ({(passCount / (double)allCount):P})";
        ExcellenceTextBlock.Text = $"{excelCount} of {allCount} ({(excelCount / (double)allCount):P})";
    }
}