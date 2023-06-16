using Define.EventAggregator;
using Prism.Commands;
using Prism.Events;
using System;

namespace ViewModel.Views;

public class TextViewViewModel
{
    private IEventAggregator Aggregator;

    public DelegateCommand TextLoadCommand { get; private set; }

    public TextViewViewModel(IEventAggregator ea)
    {
        Aggregator = ea;
        TextLoadCommand = new DelegateCommand(CallLoad);
    }

    private void CallLoad() => Aggregator.GetEvent<TextLoadCallEvent>().Publish(@$"{AppDomain.CurrentDomain.BaseDirectory}\TextTest.txt");
}