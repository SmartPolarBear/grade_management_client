using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using GradeManagement.Data.Model;

namespace GradeManagement.View.Teacher;

public partial class ComplexGradingDialog : Window
{
    public decimal? GradeResult { get; private set; } = null;

    public ComplexGradingDialog(List<Tcgc> composition) // limited by the db, we cannot have an intial value
    {
        InitializeComponent();
    }
}