using System;
using System.Windows;
using GradeManagement.Service.Teacher;
using GradeManagement.Utils;
using Microsoft.IdentityModel.Tokens;

namespace GradeManagement.View.Teacher;

using Teacher = Data.Model.Teacher;

public partial class ChangeAccountInfoDialog : Window
{
    private readonly Teacher _teacher;
    private TeacherService _service;
    private TeacherViewService _viewService;

    public string TeacherName
    {
        get => _teacher.Name;
        set => _teacher.Name = value;
    }

    public string Email
    {
        get => _teacher.Email;
        set => _teacher.Email = value;
    }

    public string Id => _teacher.Id;

    public ChangeAccountInfoDialog(Teacher teacher)
    {
        _teacher = teacher;
        _service = new TeacherService(_teacher);
        _viewService = new TeacherViewService(_teacher);
        InitializeComponent();
    }

    private void UpdatePasswordButton_OnClick(object sender, RoutedEventArgs e)
    {
        _viewService.ShowChangePasswordDialog();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private async void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (TeacherName.IsNullOrEmpty())
        {
            MessageBox.Show("Teacher Name cannot be empty!", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        if (Email.IsNullOrEmpty())
        {
            MessageBox.Show("Email cannot be empty!", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        if (!Email.ValidateEmail())
        {
            MessageBox.Show("Email is not valid!", "Validation Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        try
        {
            if (!await _service.UpdateTeacherInfoAsync(_teacher))
            {
                MessageBox.Show("Failed to update teacher info!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        }
        catch (SecurityTokenException ex)
        {
            MessageBox.Show($"Failed to update teacher info!\n{ex.Message}", "Error", MessageBoxButton.OK,
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