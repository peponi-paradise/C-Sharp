using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Library.Core.Messenger;

public class RequestSerialPortOpen
{ }

public class RequestSerialPortClose
{ }

public class RequestSerialPortStatus : RequestMessage<bool>
{ }

public class RequestGetSerialPortInformation : RequestMessage<SerialPortInformation>
{ }

public class RequestSetSerialPortInformation
{
    public SerialPortInformation SerialPortInformation { get; set; }
}

public class RequestSerialPortSend : RequestMessage<bool>
{
    public string Message { get; init; } = string.Empty;
}

public class SerialPortReceived : ValueChangedMessage<string>
{
    public SerialPortReceived(string value) : base(value)
    { }
}