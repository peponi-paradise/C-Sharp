using System.IO.Ports;

namespace Library.Core;

public struct SerialPortInformation
{
    public string COMPort;
    public int BaudRate;
    public Parity Parity;
    public int DataBits;
    public StopBits StopBits;
    public Handshake Handshake;
}