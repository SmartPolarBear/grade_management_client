using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;

namespace GradeManagement.ViewModel;

public class MainViewModel
    : ViewModelBase
{
    MainViewModel()
    {
        LoginCommand = new DelegateCommand(Login, _ => !string.IsNullOrEmpty(UserName));
    }

    private string _username = "";

    public string UserName
    {
        get => _username;
        set
        {
            SetProperty(ref _username, value);
            LoginCommand!.RaiseCanExecuteChanged();
        }
    }


    private int _roleIndex = 0;

    public int RoleIndex
    {
        get => _roleIndex;
        set => SetProperty(ref _roleIndex, value);
    }


    public DelegateCommand LoginCommand { get; }


    private void Login(object? param)
    {
        var pb = param! as PasswordBox;
        var password = pb!.Password;
    }
}