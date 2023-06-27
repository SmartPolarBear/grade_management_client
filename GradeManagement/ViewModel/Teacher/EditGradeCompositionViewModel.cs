using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GradeManagement.Base.Command;
using GradeManagement.Base.ViewModel;
using GradeManagement.Data.Model;
using GradeManagement.Service.Teacher;

namespace GradeManagement.ViewModel.Teacher;

using Teacher = Data.Model.Teacher;
using Course = Data.Model.Course;
using Student = Data.Model.Student;
using Tcgc = Data.Model.Tcgc;

public record class GradeItemForCourse(GradeComposition GradeItem,
    decimal Weight,
    Tcgc Relation,
    Action<GradeItemForCourse> Callback) : IEditableObject
{
    public void BeginEdit()
    {
    }

    public void CancelEdit()
    {
    }

    public void EndEdit()
    {
        try
        {
            Callback(this);
        }
        catch (Exception e)
        {
            MessageBox.Show("Please enter a valid weight.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}

public class EditGradeCompositionViewModel
    : ViewModelBase
{
    private CourseGradingService _gradingService;
    private TeacherService _teacherService;

    public EditGradeCompositionViewModel(Teacher teacher, Course course)
    {
        TeacherData = teacher;
        CourseData = course;

        _teacherService = new TeacherService(TeacherData);
        _gradingService = new CourseGradingService(TeacherData, CourseData);
    }

    private bool _canRemove = false;

    public bool CanRemove
    {
        get => _canRemove;
        private set
        {
            _canRemove = value;
            NotifyPropertyChanged(nameof(CanRemove));
        }
    }

    private bool _canAdd = false;

    public bool CanAdd
    {
        get => _canAdd;
        private set
        {
            _canAdd = value;
            NotifyPropertyChanged(nameof(CanAdd));
        }
    }

    private decimal _newItemWeight = 0;

    public decimal NewItemWeight
    {
        get => _newItemWeight;
        set
        {
            _newItemWeight = value;
            NotifyPropertyChanged(nameof(NewItemWeight));
        }
    }


    public Teacher TeacherData { get; }

    public Course CourseData { get; }

    public ObservableCollection<GradeItemForCourse> GradeItemsForCourse =>
        new ObservableCollection<GradeItemForCourse>(
            from gc in _gradingService.GradeCompositions.ToList()
            select new GradeItemForCourse(gc.GradeComposition, gc.Weight, gc,
                (newvalue) => { _gradingService.ChangeGradeCompositionWeight(newvalue.Relation, newvalue.Weight); }));


    public IEnumerable<GradeComposition> UnusedGradeCompositions
        => _gradingService.UnusedGradeCompositions;

    private Tcgc? _itemToRemove = null;

    public ICommand OnDataGridItemSelection =>
        new DelegateCommand((sender) =>
        {
            _itemToRemove = (sender as GradeItemForCourse)?.Relation;
            CanRemove = sender != null;
        });

    private GradeComposition? _newItem = null;

    public ICommand OnComboBoxItemSelection =>
        new DelegateCommand((sender) =>
        {
            _newItem = sender as GradeComposition;
            CanAdd = _newItem != null;
        });

    public ICommand OnAddNewItem =>
        new DelegateCommand(
            _ =>
            {
                _gradingService.AddGradeComposition(_newItem!, NewItemWeight);
                this.NotifyAllPropertiesChanged<EditGradeCompositionViewModel>();
            });

    public ICommand OnRemoveItem =>
        new DelegateCommand(
            _ =>
            {
                _gradingService.RemoveGradeComposition(_itemToRemove!);
                this.NotifyAllPropertiesChanged<EditGradeCompositionViewModel>();
            });
}