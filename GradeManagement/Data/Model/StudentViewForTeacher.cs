using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[Keyless]
public partial class StudentViewForTeacher
{
    [Column("ID")]
    [StringLength(14)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public byte Gender { get; set; }

    public byte Age { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Email { get; set; } = null!;
}
