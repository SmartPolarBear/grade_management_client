using System;
using System.Windows.Documents;

namespace GradeManagement.Data;

public sealed record Course(string Id, object Data);

public enum CourseGradingMethod
{
    PF = 0,
    Score100 = 1,
    Score5 = 2,
}

public static class CourseGradingMethodExtensions
{
    public static string ToDisplayName(this CourseGradingMethod gm) =>
        gm switch
        {
            CourseGradingMethod.PF => "Pass/Fail",
            CourseGradingMethod.Score100 => "100-mark System",
            CourseGradingMethod.Score5 => "5-grading System",
            _ => throw new ArgumentOutOfRangeException(nameof(gm), gm, null)
        };

    public static string ToShortDisplayName(this CourseGradingMethod gm) =>
        gm switch
        {
            CourseGradingMethod.PF => "PF",
            CourseGradingMethod.Score100 => "100",
            CourseGradingMethod.Score5 => "5",
            _ => throw new ArgumentOutOfRangeException(nameof(gm), gm, null)
        };

    public static CourseGradingMethod GradingMethodFromDisplayName(this string displayName) =>
        displayName switch
        {
            "Pass/Fail" => CourseGradingMethod.PF,
            "100-mark System" => CourseGradingMethod.Score100,
            "5-grading System" => CourseGradingMethod.Score5,
            _ => throw new ArgumentOutOfRangeException(nameof(displayName), displayName, null)
        };

    public static CourseGradingMethod GradingMethodFromShortDisplayName(this string shortDisplayName) =>
        shortDisplayName switch
        {
            "PF" => CourseGradingMethod.PF,
            "100" => CourseGradingMethod.Score100,
            "5" => CourseGradingMethod.Score5,
            _ => throw new ArgumentOutOfRangeException(nameof(shortDisplayName), shortDisplayName, null)
        };
}