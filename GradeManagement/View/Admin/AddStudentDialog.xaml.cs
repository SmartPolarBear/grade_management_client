using System.Windows;
using GradeManagement.Utils.Validation;

namespace GradeManagement.View.Admin;

public partial class AddStudentDialog : Window
{
    public Data.Model.Student Student { get; private set; } = new Data.Model.Student();

    public AddStudentDialog()
    {
        InitializeComponent();
    }

    private void NumericIdValidationRule_OnValidateId(object? sender, ValidateIdEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}