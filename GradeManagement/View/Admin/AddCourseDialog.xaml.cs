using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GradeManagement.Data;
using GradeManagement.Utils.Validation;

namespace GradeManagement.View.Admin;

public partial class AddCourseDialog : Window
{
    public Data.Model.Course Course { get; private set; } = new Data.Model.Course();


    public AddCourseDialog()
    {
        InitializeComponent();
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Course.Id) || string.IsNullOrEmpty(Course.Name) ||
            string.IsNullOrEmpty(Course.Term))
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

    private void NumericIdValidationRule_OnValidateId(object? sender, ValidateIdEventArgs e)
    {
        var dbc = new GradeManagementContext(UserType.Admin);
        if (e.Id != null && dbc.Courses.Any(i => i.Id == e.Id))
        {
            e.IsValid = false;
        }
    }

    private void GradingMethod_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Course.GradingMethod = GradingMethod.SelectedIndex switch
        {
            0 => (int)CourseGradingMethod.PF,
            1 => (int)CourseGradingMethod.Score5,
            2 => (int)CourseGradingMethod.Score100,
        };
    }
}