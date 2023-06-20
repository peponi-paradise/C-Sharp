<h1 id="title">NModbus4.Wrapper summary</h1>

- This wrapper class use C# [NModbus4](https://github.com/NModbus4/NModbus4) dll
- Public properties, functions, events are on following table
<br><br><br>

<h2 id="Public">Public Properties, Functions, Events</h2>
<br>

1. Properties
    | Type                                                                                  | Name ( Property )                                                                                                            | Description                                    |
    |---------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------|
    | Define_Modbus.ModbusInterface                                                         | Interface                                                                                                                    | Communication information                      |
<br>

2. Functions
    | Type                                                                                  | Name ( Function )                                                                                                            | Description                                    |
    |---------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------|
    | void                                                                                  | Modbus(Define_Modbus.ModbusInterface, Action<(Define_Modbus.ModbusInterface, bool)>)                                         | Constructor                                    |
    | void                                                                                  | Connect()                                                                                                                    | Connect to remote master / slave               |
    | void                                                                                  | Dispose()                                                                                                                    | Disconnect connection / release thread         |
    | Action\<(Define_Modbus.ModbusInterface, bool)>                                        | ConnectCallback                                                                                                              | Connection status changed notice               |
    | void                                                                                  | ClearCommunicationData()                                                                                                     | CommunicationData list clear used for polling  |
    |                                                                                       |                                                                                                                              |                                                |
    | Read functions                                                                        |                                                                                                                              |                                                |
    | bool                                                                                  | ReadData\<T>(Define_Modbus.DataStorage, int, out T)                                                                          | Read function                                  |
    | bool                                                                                  | ReadData\<T>(Define_Modbus.DataStorage, int, int, out List\<T>)                                                              | Read function                                  |
    | bool                                                                                  | ReadData(ref Define_Modbus.CommunicationData)                                                                                | Read function                                  |
    | bool                                                                                  | ReadData(ref List\<Define_Modbus.CommunicationData>)                                                                         | Read function                                  |
    | Task<(bool, T)>                                                                       | ReadDataAsync\<T>(Define_Modbus.DataStorage, int)                                                                            | Async read function. Recommended for Master    |
    | Task<(bool, List\<T>)>                                                                | ReadDataAsync\<T>(Define_Modbus.DataStorage, int, int)                                                                       | Async read function. Recommended for Master    |
    | Task<(bool, Define_Modbus.CommunicationData)>                                         | ReadDataAsync(Define_Modbus.CommunicationData)                                                                               | Async read function. Recommended for Master    |
    | Task<(bool, List\<Define_Modbus.CommunicationData>)>                                  | ReadDataAsync(List\<Define_Modbus.CommunicationData>)                                                                        | Async read function. Recommended for Master    |
    |                                                                                       |                                                                                                                              |                                                |
    | Write functions                                                                       |                                                                                                                              |                                                |
    | bool                                                                                  | WriteData\<T>(Define_Modbus.DataStorage, int, T, Define_Modbus.DataType)                                                     | Write function                                 |
    | bool                                                                                  | WriteData\<T>(Define_Modbus.DataStorage, int, List\<T>, Define_Modbus.DataType)                                              | Write function                                 |
    | bool                                                                                  | WriteData(Define_Modbus.CommunicationData)                                                                                   | Write function                                 |
    | bool                                                                                  | WriteData(List\<Define_Modbus.CommunicationData>)                                                                            | Write function                                 |
    | Task\<bool>                                                                           | WriteDataAsync\<T>(Define_Modbus.DataStorage, int, T, Define_Modbus.DataType)                                                | Async write function. Recommended for Master   |
    | Task\<bool>                                                                           | WriteDataAsync\<T>(Define_Modbus.DataStorage, int, List\<T>, Define_Modbus.DataType)                                         | Async write function. Recommended for Master   |
    | Task\<bool>                                                                           | WriteDataAsync(Define_Modbus.CommunicationData)                                                                              | Async write function. Recommended for Master   |
    | Task\<bool>                                                                           | WriteDataAsync(List\<Define_Modbus.CommunicationData>)                                                                       | Async write function. Recommended for Master   |
    |                                                                                       |                                                                                                                              |                                                |
    | Master functions                                                                      |                                                                                                                              |                                                |
    | -                                                                                     |                                                                                                                              |                                                |
    |                                                                                       |                                                                                                                              |                                                |
    | Slave functions                                                                       |                                                                                                                              |                                                |
    | void                                                                                  | ClearDataStore()                                                                                                             | Clear and initialize all data                  |
<br>

3. Events
    |     Type      |     Name ( Event )                                                                                               |     Description                                                                                                                |
    |---------------|------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|
    | void          | ModbusGeneralExceptionHandler(Define_Modbus.ModbusInterface)                                                     | Deal general, unknown exception                                                                                                |
    | void          | ModbusCommunicationExceptionHandler(Define_Modbus.ModbusInterface, Define_Modbus.CommunicationException)         | Usually need to recreate class for communication after called this event                                                       |
    | void          | ModbusReadDataHandler(Define_Modbus.ModbusInterface, List\<Define_Modbus.CommunicationData>)                     | Data read event by set Define_Modbus.ModbusInterface.ReadUpdateOption and Define_Modbus.ModbusInterface.PollingInterval_ms     |
    | void          | ModbusLogHandler(Define_Modbus.ModbusInterface, Define_Modbus.LogLevel, string)                                  | Modbus log                                                                                                                     |
    |               |                                                                                                                  |                                                                                                                                |
    | Master events |                                                                                                                  |                                                                                                                                |
    | -             |                                                                                                                  |                                                                                                                                |
    |               |                                                                                                                  |                                                                                                                                |
    | Slave events  |                                                                                                                  |                                                                                                                                |
    | void          | ModbusDataReceivedHandler(Define_Modbus.ModbusInterface, Define_Modbus.DataStorage, List\<int>, List\<ushort>)   | Remote master wrote data to this slave                                                                                         |
<br><br><br>

<h2 id="ExampleCode">Example code</h2>
<br>

<h6 id="FromConstructorToConnect"></h6>

1. From constructor to connect
    ```cs
    string InterfaceName = "Test";
    ModbusType ModbusType = Define_Modbus.ModbusType.RTU_Master;
    int SlaveNo = 1;
    string Address = "COM1";
    int Port = 1;
    UpdateOption Write = UpdateOption.Immediate;
    UpdateOption Read = UpdateOption.Polling;
    int PollingInterval_ms = 1000;
    Endian Endian = Endian.Big;

    ModbusInterface InterfaceData = new ModbusInterface(InterfaceName, ModbusType, SlaveNo, Address, Port, Write, Read, PollingInterval_ms, Endian);
    Modbus Modbus = new Modbus(InterfaceData, CheckConnectSuccess);

    Modbus.ModbusLog += Modbus_LogWrite;
    Modbus.ModbusDataReceived += Modbus_ModbusDataReceived;
    Modbus.ModbusCommunicationException += Modbus_ModbusCommunicationException;
    Modbus.ModbusGeneralException += Modbus_ModbusGeneralException;
    Modbus.ModbusReadData += Modbus_ModbusReadData;

    Modbus.Connect();

    void CheckConnectionStatus(Define_Modbus.ModbusInterface Interface, bool ConnectOrDisconnect)
    {
        if(ConnectOrDisconnect == true)
        {
            // Do something...
        }
        else
        {
            // Reconnect, or other things to do..
        }
    }
    ```
<br>

2. Disconnect
    ```cs
    Modbus.Dispose();
    ```
<br>

3. Restart
    ```cs
    if (Modbus != null) Modbus.Dispose();

    Modbus = new Modbus(InterfaceData, CheckConnectSuccess);

    Modbus.ModbusLog += Modbus_LogWrite;
    Modbus.ModbusDataReceived += Modbus_ModbusDataReceived;
    Modbus.ModbusCommunicationException += Modbus_ModbusCommunicationException;
    Modbus.ModbusGeneralException += Modbus_ModbusGeneralException;
    Modbus.ModbusReadData += Modbus_ModbusReadData;

    Modbus.Connect();
    ```
<br>

4. Read data (Sync example)
    ```cs
    // 1. Single case

    var CommData = new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 1, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Read);

    if (Modbus.ReadData(ref CommData) == true)
    {
        // Data process...
    }
    else
    {
        // Do something...
    }


    // 2. List case

    List<Define_Modbus.CommunicationData> CommList = new List<CommunicationData>();

    CommList.Add(new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 1, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Read));
    CommList.Add(new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 2, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Read));

    if (Modbus.ReadData(ref CommList) == true)
    {
        // Data process...
    }
    else
    {
        // Do something...
    }
    ```
<br>

5. Write data (Async example)
    ```cs
    // 1. Single case

    var CommData = new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 1, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Write);

    var rtn = await Modbus.WriteDataAsync(CommData);

    if (rtn == false)
    {
        // Do something...
    }


    // 2. List case

    List<Define_Modbus.CommunicationData> CommList = new List<CommunicationData>();

    CommList.Add(new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 1, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Write));
    CommList.Add(new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 2, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Write));

    var rtn = await Modbus.WriteDataAsync(CommData);

    if (rtn == false)
    {
        // Do something...
    }
    ```
<br>

6. Read data by polling method
    - Start from ['1. From constructor to connect'](#FromConstructorToConnect)

    ```cs
    // Event will raise automatically after 1 time call read command

    var CommData = new CommunicationData(Define_Modbus.DataStorage.HoldingRegister, 1, 0, Modbus.Interface.EndianOption, DataType.Float, ReadWriteOption.Read);

    if (Modbus.ReadData(ref CommData) == true)
    {
        // Data process...
    }
    else
    {
        // Do something...
    }

    // ...

    // After 1 secs...

    void Modbus_ModbusReadData(List<Define_Modbus.CommunicationData> CommunicationData)
    {
        // CommData's Value (Define_Modbus.CommunicationData.Value) will updated and arrived here

        // Data processing...
    }
    ```