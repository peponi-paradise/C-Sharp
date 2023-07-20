using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Library.Core;
using Library.Core.Messenger;
using System.Collections.ObjectModel;
using System.Timers;

namespace Library.Services.SerialPort;

public partial class SerialPortViewModel : ObservableObject
{
    [ObservableProperty]
    private SerialPortInformation serialPortInformation;

    public ObservableCollection<string> CommunicationData => Model.CommunicationData;
    public ObservableCollection<string> SendingData { get; init; }

    [ObservableProperty]
    private bool isOpen;

    private SerialPortModel Model { get; init; }
    private Timer StatusTimer { get; init; }

    public SerialPortViewModel(SerialPortModel model)
    {
        SendingData = new ObservableCollection<string>();
        Model = model;
        Model.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
        SerialPortInformation = WeakReferenceMessenger.Default.Send(new RequestGetSerialPortInformation()).Response;
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
    public void UpdateSerialInformation(SerialPortInformation serialPortInformation) => WeakReferenceMessenger.Default.Send(new RequestSetSerialPortInformation() { SerialPortInformation = serialPortInformation });

    [RelayCommand]
    public void Send(string message)
    {
        if (WeakReferenceMessenger.Default.Send(new RequestSerialPortSend() { Message = message }).Response == false)
        {
            // error 처리
        }
        else
        {
            SendingData.Add(message);
        }
    }
}