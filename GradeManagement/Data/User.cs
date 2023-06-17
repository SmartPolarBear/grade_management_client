using GradeManagement.Data.Base;
using GradeManagement.Data.Model;

namespace GradeManagement.Data;

public sealed record AdminUser(object Data) : User(UserType.Admin, Data);

public sealed record StudentUser(object Data) : User(UserType.Student, Data);

public sealed record TeacherUser(object Data) : User(UserType.Teacher, Data);