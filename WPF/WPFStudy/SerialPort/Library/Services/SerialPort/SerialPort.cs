using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Library.Core;
using Library.Core.Messenger;

namespace Library.Services;

public class SerialPortService
{
    public SerialPortInformation SerialPortInformation { get; set; }
    public bool IsOpen { get; set; }

    System.IO.Ports.SerialPort Port;

    public SerialPortService()
    {
        WeakReferenceMessenger.Default.Register<RequestSerialPortOpen>(this, (o, e) => Open());
        WeakReferenceMessenger.Default.Register<RequestSerialPortClose>(this, (o, e) => Close());
        WeakReferenceMessenger.Default.Register<RequestSerialPortInformation>(this, (o, e) => e.Reply(SerialPortInformation));
        WeakReferenceMessenger.Default.Register<RequestSerialPortStatus>(this, (o, e) => e.Reply(IsOpen));
        Port = new System.IO.Ports.SerialPort();
        SerialPortInformation = new SerialPortInformation() { COMPort = "COM1", BaudRate = 115200, Parity = System.IO.Ports.Parity.None, DataBits = 7, StopBits = System.IO.Ports.StopBits.One, Handshake = System.IO.Ports.Handshake.None };
        InitPort();
    }

    private void InitPort()
    {
        Port = new System.IO.Ports.SerialPort
        {
            PortName = SerialPortInformation.COMPort,
            BaudRate = SerialPortInformation.BaudRate,
            Parity = SerialPortInformation.Parity,
            DataBits = SerialPortInformation.DataBits,
            StopBits = SerialPortInformation.StopBits,
            Handshake = SerialPortInformation.Handshake
        };
    }

    public void Open()
    {
        InitPort();
        Port.Open();
        IsOpen = Port.IsOpen;
        Port.DataReceived += Port_DataReceived;
        WeakReferenceMessenger.Default.Register<RequestSerialPortSend>(this, ReceivedRequestSerialPortSend);
    }

    public void Close()
    {
        Port.Close();
        IsOpen = Port.IsOpen;
        WeakReferenceMessenger.Default.Unregister<RequestSerialPortSend>(this);
    }

    private void ReceivedRequestSerialPortSend(object recipient, RequestSerialPortSend request) => request.Reply(Send(request.Message));

    public bool Send(string data)
    {
        try
        {
            Port.Write(data);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    {
        var read = Port.ReadExisting();
        WeakReferenceMessenger.Default.Send(new SerialPortReceived(read));
    }

    public void UpdateSerialPortInformation(SerialPortInformation serialPortInformation) => SerialPortInformation = serialPortInformation;
}