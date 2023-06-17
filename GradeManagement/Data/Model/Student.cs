using System;
using System.Collections.Generic;

namespace GradeManagement.Data.Model;

public partial class Student
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte Gender { get; set; }

    public byte Age { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Sc> Scs { get; set; } = new List<Sc>();

    public virtual ICollection<St> Sts { get; set; } = new List<St>();
}
