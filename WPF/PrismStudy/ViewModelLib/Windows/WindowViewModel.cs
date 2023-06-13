using Define.EventAggregator;
using Define.ViewModel;
using ModelLib.Windows;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Diagnostics;

namespace ViewModelLib.Windows;

public class WindowViewModel : BindableBase, IViewModel
{
    private MainWindowModel _Model;

    private string _TextData = "";

    public string TextData
    {
        get => _Model.TextData;
        set => _Model.TextData = value;
    }

    private string _LoadData = "";

    public string LoadData
    {
        get => _LoadData;
        set => SetProperty(ref _LoadData, value);
    }

    public DelegateCommand LoadCommand { get; private set; }

    private bool _IsIdle = true;

    public bool IsIdle
    {
        get => _IsIdle;
        set => SetProperty(ref _IsIdle, value);
    }

    private IEventAggregator _Aggregator;

    public WindowViewModel(MainWindowModel model, IEventAggregator ea)
    {
        _Model = model;
        _Model.PropertyChanged += _Model_PropertyChanged;
        _Aggregator = ea;
        _Aggregator.GetEvent<TextLoadDoneEvent>().Subscribe(LoadDone);
        LoadCommand = new DelegateCommand(Load).ObservesCanExecute(() => IsIdle);
    }

    private void _Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) => base.RaisePropertyChanged(e.PropertyName);

    private void Load()
    {
        IsIdle = false;
        _Aggregator.GetEvent<TextLoadCallEvent>().Publish(@$"{AppDomain.CurrentDomain.BaseDirectory}\TextTest.txt");
    }

    private void LoadDone(string contents)
    {
        LoadData = contents;
        IsIdle = true;
    }
}