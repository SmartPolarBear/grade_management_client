using System;
using System.Collections.Generic;

namespace GradeManagement.Data.Model;

public partial class Admin
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}
