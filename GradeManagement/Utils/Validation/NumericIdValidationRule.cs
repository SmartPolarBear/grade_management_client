using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;


namespace GradeManagement.Utils.Validation;

public sealed class ValidateIdEventArgs : EventArgs
{
    public string? Id { get; init; }
    public bool IsValid { get; set; }
}

public sealed partial class NumericIdValidationRule
    : ValidationRule
{
    public event EventHandler<ValidateIdEventArgs>? ValidateId;

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

                var arg = new ValidateIdEventArgs { IsValid = parsingOk, Id = value.ToString() };
                ValidateId?.Invoke(this, arg);

                if (!arg.IsValid)
                {
                    validationResult = new ValidationResult(false, "Illegal Custom Check!");
                }
            }
        }

        return validationResult;
    }

    [GeneratedRegex("[^0-9.-]+")]
    private static partial Regex NumericRegex();
}