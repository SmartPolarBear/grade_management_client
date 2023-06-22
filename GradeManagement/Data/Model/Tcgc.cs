using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[PrimaryKey("TeacherId", "CourseId", "GradeCompositionId")]
[Table("TCGC")]
public partial class Tcgc
{
    [Key]
    [Column("GradeCompositionID")]
    public int GradeCompositionId { get; set; }

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

    [Column(TypeName = "decimal(6, 3)")]
    public decimal Weight { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Tcgcs")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("GradeCompositionId")]
    [InverseProperty("Tcgcs")]
    public virtual GradeComposition GradeComposition { get; set; } = null!;

    [ForeignKey("TeacherId")]
    [InverseProperty("Tcgcs")]
    public virtual Teacher Teacher { get; set; } = null!;
}
