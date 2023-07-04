using System.Linq;
using System.Windows;
using GradeManagement.Data;
using GradeManagement.Service.Admin;
using GradeManagement.Utils.Validation;
using Microsoft.IdentityModel.Tokens;

namespace GradeManagement.View.Admin;

using Teacher = Data.Model.Teacher;

public partial class AddTeacherDialog : Window
{
    public Teacher Teacher { get; private set; } = new Teacher();

    public AddTeacherDialog()
    {
        InitializeComponent();
    }

    private void NumericIdValidationRule_OnValidateId(object? sender, ValidateIdEventArgs e)
    {
        var dbc = new GradeManagementContext(UserType.Admin);
        if (e.Id != null && dbc.Teachers.Any(i => i.Id == e.Id))
        {
            e.IsValid = false;
        }
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Teacher.Id) || string.IsNullOrEmpty(Teacher.Name) ||
            string.IsNullOrEmpty(Teacher.Email))
        {
            MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        this.DialogResult = true;
        this.Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }
}