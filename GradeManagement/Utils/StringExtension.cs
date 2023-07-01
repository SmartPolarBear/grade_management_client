using System.Text.RegularExpressions;

namespace GradeManagement.Utils;

public static partial class StringExtension
{
    public static bool ValidateEmail(this string? email)
    {
        return EmailRegex().IsMatch(email);
    }

    [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    private static partial Regex EmailRegex();
}