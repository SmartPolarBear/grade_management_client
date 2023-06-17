using System;
using System.Collections.Generic;

namespace GradeManagement.Data.Model;

public partial class Sc
{
    public string StudentId { get; set; } = null!;

    public string CourseId { get; set; } = null!;

    public byte Score { get; set; }

    public byte Rating { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
