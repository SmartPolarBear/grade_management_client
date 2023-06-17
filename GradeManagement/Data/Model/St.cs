using System;
using System.Collections.Generic;

namespace GradeManagement.Data.Model;

public partial class St
{
    public string StudentId { get; set; } = null!;

    public string TeacherId { get; set; } = null!;

    public byte Rating { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
