using System;
using System.Windows;
using GradeManagement.Data;
using Microsoft.Extensions.Configuration;

namespace GradeManagement.Utils;

public static class UserConnectionString
{
    private static string? FullConnectionString(this string key)
        => $"{App.Config.GetConnectionString("Source")}{App.Config.GetConnectionString(key)}";

    public static string? ConnectionString(this UserType t)
        => t switch
        {
            UserType.Default => "DefaultConnection".FullConnectionString(),
            UserType.Admin => "SAConnection".FullConnectionString(),
            UserType.Student => "StudentConnection".FullConnectionString(),
            UserType.Teacher => "TeacherConnection".FullConnectionString(),
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, "No connection string for this user type")
        };

    public static string? ConnectionString(this UserType? t)
        => t switch
        {
            null => "DefaultConnection".FullConnectionString(),
            _ => t.Value.ConnectionString()
        };
}