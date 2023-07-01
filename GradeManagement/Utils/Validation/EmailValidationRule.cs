using System.Globalization;
using System.Windows.Controls;

namespace GradeManagement.Utils.Validation;

public class EmailValidationRule
    : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        return value != null && value.ToString().ValidateEmail()
            ? ValidationResult.ValidResult
            : new ValidationResult(false, "Invalid Email Address");
    }
}