using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.Data.Model;

[Table("GradeComposition")]
public partial class GradeComposition
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("GradeComposition")]
    public virtual ICollection<Tcgc> Tcgcs { get; set; } = new List<Tcgc>();
}
