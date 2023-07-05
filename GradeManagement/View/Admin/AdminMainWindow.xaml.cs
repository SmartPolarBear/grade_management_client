using System.Windows;
using GradeManagement.Data;
using GradeManagement.Data.Model;
using GradeManagement.Service.Admin;
using GradeManagement.Utils;
using GradeManagement.ViewModel.Admin;

namespace GradeManagement.View.Admin;

using Admin = Data.Model.Admin;

public partial class AdminMainWindow : Window
{
    public AdminMainWindow(AdminUser user)
    {
        InitializeComponent();
        this.DataContext = new AdminMainViewModel(user);
    }

    private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void ChangePasswordMenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        var service = new AdminViewService(this.DataContextOf<AdminMainViewModel>().AdminData);
        service.ShowChangePasswordDialog();
    }

    private void DeleteStudentButton_OnClick(object sender, RoutedEventArgs e)
    {
        var service = new AdminService(this.DataContextOf<AdminMainViewModel>().AdminData);
        var res = MessageBox.Show("Are you sure to delete this student?", "Warning", MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (res == MessageBoxResult.Yes)
        {
            var student = (StudentDataGrid.SelectedItem as StudentViewItem)!.Student;
            service.DeleteStudent(student);
        }

        this.DataContextOf<AdminMainViewModel>()?.RefreshAll();
    }

    private void DeleteTeacherButton_OnClick(object sender, RoutedEventArgs e)
    {
        var service = new AdminService(this.DataContextOf<AdminMainViewModel>().AdminData);
        var res = MessageBox.Show("Are you sure to delete this teacher?", "Warning", MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (res == MessageBoxResult.Yes)
        {
            var teacher = (TeacherDataGrid.SelectedItem as TeacherViewItem)!.Teacher;
            service.DeleteTeacher(teacher);
        }

        this.DataContextOf<AdminMainViewModel>()?.RefreshAll();
    }

    private void DeleteCourseButton_OnClick(object sender, RoutedEventArgs e)
    {
        var service = new AdminService(this.DataContextOf<AdminMainViewModel>().AdminData);
        var res = MessageBox.Show("Are you sure to delete this course?", "Warning", MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (res == MessageBoxResult.Yes)
        {
            var course = CourseDataGrid.SelectedItem as CourseViewItem;
            service.DeleteCourse(course!.Course);
        }

        this.DataContextOf<AdminMainViewModel>()?.RefreshAll();
    }

    private void RollbackAuditButton_OnClick(object sender, RoutedEventArgs e)
    {
var service = new AdminService(this.DataContextOf<AdminMainViewModel>().AdminData);
        var res = MessageBox.Show("Are you sure to rollback this audit?", "Warning", MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (res == MessageBoxResult.Yes)
        {
            var audit = AuditDataGrid.SelectedItem as Scaudit;
            service.RollbackAudit(audit!);
        }

        this.DataContextOf<AdminMainViewModel>()?.RefreshAll();
    }
}