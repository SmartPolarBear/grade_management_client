using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using GradeManagement.Utils;
using GradeManagement.ViewModel.Teacher;

namespace GradeManagement.View.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;

public partial class EditGradeCompositionDialog : Window
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("user32.dll")]
    private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    private const uint MF_BYCOMMAND = 0x00000000;
    private const uint MF_GRAYED = 0x00000001;
    private const uint SC_CLOSE = 0xF060;
    private const int WM_SHOWWINDOW = 0x00000018;

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var hWnd = new WindowInteropHelper(this);
        var sysMenu = GetSystemMenu(hWnd.Handle, false);
        EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
    }

    private readonly Teacher _teacher;
    private readonly Course _course;

    public EditGradeCompositionDialog(Teacher teacher, Course course)
    {
        _teacher = teacher;
        _course = course;
        InitializeComponent();
        this.DataContext = new EditGradeCompositionViewModel(_teacher, _course);
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void NewItemComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = this.DataContextOf<EditGradeCompositionViewModel>();
        vm.OnComboBoxItemSelection.Execute(NewItemComboBox.SelectedItem);
    }

    private void MainDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var vm = this.DataContextOf<EditGradeCompositionViewModel>();
        vm.OnDataGridItemSelection.Execute(MainDataGrid.SelectedItem);
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        var vm = this.DataContextOf<EditGradeCompositionViewModel>();
        vm.OnAddNewItem.Execute(null);
    }

    private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
    {
        var vm = this.DataContextOf<EditGradeCompositionViewModel>();
        vm.OnRemoveItem.Execute(null);
    }
}