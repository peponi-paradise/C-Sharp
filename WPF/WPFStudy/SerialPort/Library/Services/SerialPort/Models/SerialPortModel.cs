using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Library.Core.Messenger;
using System.Collections.ObjectModel;
using System.Threading;

namespace Library.Services.SerialPort;

public partial class SerialPortModel : ObservableObject
{
    public ObservableCollection<string> CommunicationData;

    readonly SynchronizationContext? SyncContext;

    public SerialPortModel()
    {
        SyncContext = SynchronizationContext.Current;
        CommunicationData = new ObservableCollection<string>();
        WeakReferenceMessenger.Default.Register<SerialPortReceived>(this, (o, e) => UpdateCommunicationData(e.Value));
    }

    private void UpdateCommunicationData(string communicationData)
    {
        SyncContext?.Post(delegate
        {
            CommunicationData.Add(communicationData);
            if (CommunicationData.Count > 100) CommunicationData.RemoveAt(0);
        }, null);
    }
}