﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[PrimaryKey("StudentId", "CourseId")]
[Table("SC")]
public partial class Sc
{
    [Key]
    [Column("StudentID")]
    [StringLength(14)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [Key]
    [Column("CourseID")]
    [StringLength(14)]
    [Unicode(false)]
    public string CourseId { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Score { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Scs")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("Scs")]
    public virtual Student Student { get; set; } = null!;
}
