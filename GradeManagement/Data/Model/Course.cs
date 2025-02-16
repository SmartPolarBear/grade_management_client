﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[Table("Course")]
public partial class Course
{
    [Key]
    [Column("ID")]
    [StringLength(14)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public byte Credit { get; set; }

    public byte GradingMethod { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string Term { get; set; } = null!;

    [InverseProperty("Course")]
    public virtual ICollection<Scaudit> Scaudits { get; set; } = new List<Scaudit>();

    [InverseProperty("Course")]
    public virtual ICollection<Sc> Scs { get; set; } = new List<Sc>();

    [InverseProperty("Course")]
    public virtual ICollection<Stc> Stcs { get; set; } = new List<Stc>();

    [InverseProperty("Course")]
    public virtual ICollection<Tcgc> Tcgcs { get; set; } = new List<Tcgc>();

    [ForeignKey("CourseId")]
    [InverseProperty("Courses")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
