using System.IO.Ports;

namespace Library.Core;

public struct SerialPortInformation
{
    public string COMPort { get; set; }
    public int BaudRate { get; set; }
    public Parity Parity { get; set; }
    public int DataBits { get; set; }
    public StopBits StopBits { get; set; }
    public Handshake Handshake { get; set; }
}