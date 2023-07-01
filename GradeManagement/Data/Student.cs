using System;

namespace GradeManagement.Data;

public enum Gender
{
    Female,
    Male
}

public record Student(int Id, object Data)
{
}

public static class GenderExtension
{
    public static string DisplayName(this Gender g)
    {
        return g switch
        {
            Gender.Female => "Female",
            Gender.Male => "Male",
            _ => throw new ArgumentOutOfRangeException(nameof(g), g, null)
        };
    }
}