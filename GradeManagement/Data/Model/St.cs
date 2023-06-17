using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[PrimaryKey("TeacherId", "StudentId")]
[Table("ST")]
public partial class St
{
    [Key]
    [Column("StudentID")]
    [StringLength(14)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [Key]
    [Column("TeacherID")]
    [StringLength(14)]
    [Unicode(false)]
    public string TeacherId { get; set; } = null!;

    public byte Rating { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Sts")]
    public virtual Student Student { get; set; } = null!;

    [ForeignKey("TeacherId")]
    [InverseProperty("Sts")]
    public virtual Teacher Teacher { get; set; } = null!;
}
