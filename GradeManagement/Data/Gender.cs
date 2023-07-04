using System;

namespace GradeManagement.Data;

public enum Gender
{
    Female,
    Male
}

public static class GenderExtenstion
{
    public static string ToDisplayName(this Gender gender)
        => gender switch
        {
            Gender.Female => "Female",
            Gender.Male => "Male",
            _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
        };
}