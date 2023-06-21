using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[Table("SCAudit")]
public partial class Scaudit
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("StudentID")]
    [StringLength(14)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [Column("CourseID")]
    [StringLength(14)]
    [Unicode(false)]
    public string CourseId { get; set; } = null!;

    [Column(TypeName = "decimal(2, 0)")]
    public decimal OldScore { get; set; }

    [Column(TypeName = "decimal(2, 0)")]
    public decimal? NewScore { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Scaudits")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("Scaudits")]
    public virtual Student Student { get; set; } = null!;
}
