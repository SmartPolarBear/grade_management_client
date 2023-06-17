using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data.Base;
using GradeManagement.Service.Login;

namespace GradeManagement.ViewModel;

public class MainViewModel
    : ViewModelBase
{
    public MainViewModel()
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

        var userType = _roleIndex switch
        {
            0 => Data.UserType.Student,
            1 => Data.UserType.Teacher,
            2 => Data.UserType.Admin,
            _ => throw new ArgumentOutOfRangeException(nameof(_roleIndex), _roleIndex,
                "No connection string for this user type")
        };

        var loginService = new LoginService(UserName, userType);

        var user = loginService.Login(password);

        if (user == null)
        {
            LoginFailed?.Invoke(this, new LoginFailedEventArgs());
        }
        else
        {
            LoginSucceeded?.Invoke(this, new LoginSucceededEventArgs() { User = user });
        }
    }


    public event EventHandler<LoginFailedEventArgs>? LoginFailed;
    public event EventHandler<LoginSucceededEventArgs>? LoginSucceeded;
}

public class LoginSucceededEventArgs
    : EventArgs
{
    public User? User { get; internal set; }
}

public class LoginFailedEventArgs
    : EventArgs
{
}