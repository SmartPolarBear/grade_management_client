using System;
using System.Windows;
using GradeManagement.Service.Student;
using GradeManagement.Service.Teacher;
using Microsoft.EntityFrameworkCore;

namespace GradeManagement.View.Student;

using Student = Data.Model.Student;

public partial class ChangePasswordDialog : Window
{
    private readonly Student _student;

    public ChangePasswordDialog(Student student)
    {
        _student = student;
        InitializeComponent();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private async void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        StudentService service = new StudentService(_student);

        if (!service.ValidatePassword(CurrentPasswordBox.Password))
        {
            MessageBox.Show("Wrong Current Password!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (NewPasswordTextBox.Text != RetypePasswordBox.Password)
        {
            MessageBox.Show("New Password and Confirm Password are not the same!", "Validation Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            if (!await service.ChangePasswordAsync(NewPasswordTextBox.Text))
            {
                MessageBox.Show("Failed to change password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        catch (DbUpdateException dbe)
        {
            MessageBox.Show($"Failed to change password!\n{dbe.Message}", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            throw;
        }


        this.DialogResult = true;
        this.Close();
    }
}