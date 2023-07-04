using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GradeManagement.Utils.Validation;

public sealed partial class NumericValidationRule
    : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        var validationResult = new ValidationResult(true, null);

        if (value != null)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var regex = NumericRegex(); //regex that matches disallowed text
                var parsingOk = !regex.IsMatch(value.ToString() ?? string.Empty);
                if (!parsingOk)
                {
                    validationResult = new ValidationResult(false, "Illegal Characters, Please Enter Numeric Value");
                }
            }
        }

        return validationResult;
    }

    [GeneratedRegex("[^0-9.-]+")]
    private static partial Regex NumericRegex();
}