using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[Table("Student")]
public partial class Student
{
    [Key]
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
    public string Password { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [InverseProperty("Student")]
    public virtual ICollection<Sc> Scs { get; set; } = new List<Sc>();

    [InverseProperty("Student")]
    public virtual ICollection<St> Sts { get; set; } = new List<St>();
}
