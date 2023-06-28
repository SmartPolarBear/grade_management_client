using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data.Model;

namespace GradeManagement.ViewModel.Teacher;

public sealed record GradingItem(Tcgc Item, Action<decimal>? EditedCallback) : IEditableObject
{
    public decimal Value { get; set; } = 100;

    public void BeginEdit()
    {
    }

    public void CancelEdit()
    {
    }

    public void EndEdit()
    {
        EditedCallback?.Invoke(Value * Item.Weight);
    }
}

public sealed class ComplexGradingViewModel
    : ViewModelBase
{
    public ComplexGradingViewModel(List<Tcgc> composition)
    {
        Items = composition;
        DisplayItems =
            new ObservableCollection<GradingItem>(composition.Select(i => new GradingItem(i, UpdateTotalValue)));

        TotalValue = DisplayItems.Sum(i => i.Value * i.Item.Weight) / DisplayItems.Sum(i => i.Item.Weight);
    }

    private void UpdateTotalValue(decimal _)
    {
        TotalValue = DisplayItems.Sum(i => i.Value * i.Item.Weight) / DisplayItems.Sum(i => i.Item.Weight);
        NotifyPropertyChanged(nameof(TotalValue));
        NotifyPropertyChanged(nameof(DisplayValue));
    }


    private decimal _totalValue;

    public decimal TotalValue
    {
        get => _totalValue;
        set => SetProperty(ref _totalValue, value);
    }

    public string DisplayValue => $"{TotalValue:F2}";

    public List<Tcgc> Items { get; init; }

    public ObservableCollection<GradingItem> DisplayItems { get; set; }
}