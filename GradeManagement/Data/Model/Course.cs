using System;
using System.Collections.Generic;

namespace GradeManagement.Data.Model;

public partial class Course
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Teacher { get; set; } = null!;

    public byte Credit { get; set; }

    public byte GradingMethod { get; set; }

    public virtual ICollection<Sc> Scs { get; set; } = new List<Sc>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
