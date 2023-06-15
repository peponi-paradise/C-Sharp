using Define.ViewModel;
using ModelLib.Components;
using Prism.Commands;
using Prism.Mvvm;

namespace ViewModelLib.Components;

public class ContentsDataViewModel : BindableBase, IViewModel
{
    private ContentsDataModel? Model { get; set; }

    public string? TextData
    {
        get => Model != null ? Model.TextData : null;
        set
        {
            if (Model != null) Model.TextData = value;
        }
    }

    private string? _LoadText = "";

    public string? LoadText
    {
        get => _LoadText;
        set => SetProperty(ref _LoadText, value);
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