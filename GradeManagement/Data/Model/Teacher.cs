using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[Table("Teacher")]
public partial class Teacher
{
    [Key]
    [Column("ID")]
    [StringLength(14)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [InverseProperty("Teacher")]
    public virtual ICollection<St> Sts { get; set; } = new List<St>();

    [ForeignKey("TeacherId")]
    [InverseProperty("Teachers")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
