using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GradeManagement.Data;
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
        var dbc = new GradeManagementContext(UserType.Admin);
        if (e.Id != null && dbc.Students.Any(i => i.Id == e.Id))
        {
            e.IsValid = false;
        }
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Student.Id) || string.IsNullOrEmpty(Student.Name) ||
            string.IsNullOrEmpty(Student.Email))
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

    private void GenderComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Student.Gender = GenderComboBox.SelectedIndex switch
        {
            0 => (int)Gender.Female,
            1 => (int)Gender.Male,
            _ => 0
        };
    }
}