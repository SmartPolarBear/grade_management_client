using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GradeManagement.Base.ViewModel;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;
        field = newValue;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    protected void NotifyAllPropertiesChanged<T>() where T : ViewModelBase
    {
        foreach (var prop in typeof(T).GetProperties())
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}