using Define.EventAggregator;
using Model.Components;
using Prism.Events;
using Prism.Mvvm;

namespace ViewModel.Components;

public class TextReaderViewModel : BindableBase
{
    private TextReaderModel Model;
    private IEventAggregator Aggregator;

    public string? LoadedText
    {
        get => Model.LoadedText;
        set => Model.LoadedText = value;
    }

    public TextReaderViewModel(TextReaderModel model, IEventAggregator ea)
    {
        Model = model;
        Model.PropertyChanged += (s, e) => RaisePropertyChanged(e.PropertyName);    // Redirection of property
        Aggregator = ea;
        Aggregator.GetEvent<TextLoadCallEvent>().Subscribe(TextLoadCall);
    }

    private async void TextLoadCall(string filePath) => await Model.Load(filePath);
}