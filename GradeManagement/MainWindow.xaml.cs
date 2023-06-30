using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GradeManagement.Data;
using GradeManagement.Utils;
using GradeManagement.View.Admin;
using GradeManagement.View.Student;
using GradeManagement.View.Teacher;
using GradeManagement.ViewModel;
using Microsoft.Extensions.Configuration;

namespace GradeManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // MessageBox.Show(UserType.Admin.ConnectionString());
            ((this.DataContext as MainViewModel)!).LoginFailed += (sender, args) =>
            {
                MessageBox.Show("Wrong password or user name.", "Login failed", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            };

            ((this.DataContext as MainViewModel)!).LoginSucceeded += (sender, args) =>
            {
                var user = (args as LoginSucceededEventArgs)!.User;
                Window? window = user!.Type switch
                {
                    UserType.Admin => new AdminMainWindow(),
                    UserType.Student => new StudentMainWindow((user as StudentUser)!),
                    UserType.Teacher => new TeacherMainWindow((user as TeacherUser)!),
                    _ => throw new ArgumentOutOfRangeException(nameof(args),
                        (args as LoginSucceededEventArgs)!.User!.Type, "No window for this user type")
                };
                window?.Show();
                this.Close();
            };
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}