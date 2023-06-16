using Define.EventAggregator;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows.Media.Imaging;

namespace ViewModel.Views;

public class PictureViewViewModel : BindableBase
{
    private IEventAggregator Aggregator;

    public DelegateCommand PictureLoadCommand { get; private set; }

    private BitmapImage? _LoadedImage;

    public BitmapImage? LoadedImage
    {
        get => _LoadedImage;
        set => SetProperty(ref _LoadedImage, value);
    }

    public PictureViewViewModel(IEventAggregator ea)
    {
        Aggregator = ea;
        PictureLoadCommand = new DelegateCommand(CallLoad);
        Aggregator.GetEvent<PictureLoadDoneEvent>().Subscribe((image) => LoadedImage = image);
    }

    private void CallLoad() => Aggregator.GetEvent<PictureLoadCallEvent>().Publish(@$"{AppDomain.CurrentDomain.BaseDirectory}\TestPicture.jpg");
}