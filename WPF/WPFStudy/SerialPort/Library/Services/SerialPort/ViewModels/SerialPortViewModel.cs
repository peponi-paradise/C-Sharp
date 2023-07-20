using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Library.Core;
using Library.Core.Messenger;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Timers;

namespace Library.Services.SerialPort;

public partial class SerialPortViewModel : ObservableObject
{
    [ObservableProperty]
    SerialPortInformation serialPortInformation;

    public ObservableCollection<string> CommunicationData => Model.CommunicationData;

    [ObservableProperty]
    bool isOpen;

    private SerialPortModel Model { get; init; }
    private Timer StatusTimer { get; init; }

    public SerialPortViewModel(SerialPortModel model)
    {
        Model = model;
        Model.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
        SerialPortInformation = WeakReferenceMessenger.Default.Send(new RequestSerialPortInformation()).Response;
        StatusTimer = new Timer();
        StatusTimer.Interval = 1000;
        StatusTimer.Elapsed += StatusTimer_Elapsed;
        StatusTimer.Start();
    }

    private void StatusTimer_Elapsed(object? sender, ElapsedEventArgs e) => IsOpen = WeakReferenceMessenger.Default.Send<RequestSerialPortStatus>().Response;

    [RelayCommand]
    public void Open() => WeakReferenceMessenger.Default.Send(new RequestSerialPortOpen());

    [RelayCommand]
    public void Close() => WeakReferenceMessenger.Default.Send(new RequestSerialPortClose());

    [RelayCommand]
    public void Send(string message)
    {
        if (WeakReferenceMessenger.Default.Send(new RequestSerialPortSend() { Message = message }).Response == false)
        {
            // error 처리
        }
    }
}