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

    public static string? DisplayGrade(this CourseGradingMethod method, decimal? score)
    {
        return (CourseGradingMethod)method switch
        {
            CourseGradingMethod.Score100 => score switch
            {
                null => "N/A",
                _ => score.ToString()
            },
            CourseGradingMethod.Score5 => score switch
            {
                null => "N/A",
                >= 4.5m => "A", // 5
                >= 3.5m => "B", // 4
                >= 2.5m => "C", // 3
                >= 1.5m => "D", // 2
                _ => "F" // 1
            },
            CourseGradingMethod.PF => score switch
            {
                null => "N/A",
                >= 0.5m => "Pass", // 1
                _ => "Fail" // 0
            },
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }

    public static decimal? ScoreToGpa(this CourseGradingMethod gm, decimal Score)
    {
        return gm switch
        {
            CourseGradingMethod.Score5 => Score switch
            {
                >= 3.5m => 4.0m,
                >= 2.5m => 3.0m,
                >= 1.5m => 2.0m,
                >= 0.5m => 1.0m,
                _ => 0.0m
            },
            CourseGradingMethod.Score100 => Score switch
            {
                >= 90m => 4.0m,
                >= 85m => 3.7m,
                >= 81m => 3.3m,
                >= 78m => 3.0m,
                >= 75m => 2.7m,
                >= 72m => 2.3m,
                >= 68m => 2.0m,
                >= 64m => 1.7m,
                >= 60m => 1.0m,
                _ => 0.0m
            },
            _ => throw new ArgumentOutOfRangeException(nameof(gm), gm, "Invalid Grading Method for GPA calculation")
        };
    }
}