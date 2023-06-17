using System;
using System.Collections.Generic;

namespace GradeManagement.Data.Model;

public partial class Teacher
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<St> Sts { get; set; } = new List<St>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
