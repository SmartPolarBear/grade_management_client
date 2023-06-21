using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[PrimaryKey("TeacherId", "StudentId", "CourseId")]
[Table("STC")]
public partial class Stc
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

    [Key]
    [Column("CourseID")]
    [StringLength(14)]
    [Unicode(false)]
    public string CourseId { get; set; } = null!;

    public byte? Rating { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Stcs")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("Stcs")]
    public virtual Student Student { get; set; } = null!;

    [ForeignKey("TeacherId")]
    [InverseProperty("Stcs")]
    public virtual Teacher Teacher { get; set; } = null!;
}
