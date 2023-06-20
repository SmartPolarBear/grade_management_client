using GradeManagement.Data.Model;
using GradeManagement.Utils;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data;

public enum UserType
{
    Default = 1,
    Student,
    Teacher,
    Admin
}

public sealed class GradeManagementContext
    : GmsContext
{
    private readonly UserType? _userType;

    public GradeManagementContext()
    {
        _userType = null;
    }

    public GradeManagementContext(UserType t)
    {
        _userType = t;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_userType != null)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(_userType.ConnectionString());
        }
        else
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}