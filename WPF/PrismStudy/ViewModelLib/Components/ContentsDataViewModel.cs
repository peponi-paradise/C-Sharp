using Define.EventAggregator;
using Define.Model;
using Define.ViewModel;
using ModelLib.Components;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace ViewModelLib.Components;

public class ContentsDataViewModel : BindableBase, IViewModel
{
    public ContentsDataModel? Model { get; set; }

    public string? TextData
    {
        get => Model != null ? Model.TextData : null;
        set
        {
            if (Model != null) Model.TextData = value;
        }
    }

    public string? LoadText
    {
        get => Model != null ? Model.LoadText : null;
        set
        {
            if (Model != null) Model.LoadText = value;
        }
    }

    public DelegateCommand LoadCommand { get; private set; }

    public ContentsDataViewModel(ContentsDataModel model)
    {
        Model = model;
        model.PropertyChanged += Model_PropertyChanged;
        LoadCommand = new DelegateCommand(Model.Load).ObservesCanExecute(() => Model.IsIdle);
    }

    private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) => base.RaisePropertyChanged(e.PropertyName);
}