using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GradeManagement.Data.Model;
using GradeManagement.Utils;
using GradeManagement.ViewModel.Teacher;

namespace GradeManagement.View.Teacher;

public sealed class GradeItemValidationRule
    : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var valid = decimal.TryParse(value.ToString(), out var result);

        if (!valid)
        {
            return new ValidationResult(false, "Invalid input format");
        }

        return result switch
        {
            >= 0.0m and <= 100.0m => ValidationResult.ValidResult,
            _ => new ValidationResult(false, "Invalid input range. Must between 0 and 100")
        };
    }
}

public partial class ComplexGradingDialog : Window
{
    public decimal? GradeResult { get; private set; } = null;

    public ComplexGradingDialog(List<Tcgc> composition) // limited by the db, we cannot have an intial value
    {
        InitializeComponent();
        this.DataContext = new ComplexGradingViewModel(composition);
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        var vm = this.DataContextOf<ComplexGradingViewModel>()!;
        this.GradeResult = vm.TotalValue;
        this.DialogResult = true;
        this.Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.GradeResult = null;
        this.DialogResult = false;
        this.Close();
    }
}