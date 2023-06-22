
# NModbus4.Wrapper

<br>

- [NModbus4.Wrapper](#nmodbus4wrapper)
  - [1. Instruction](#1-instruction)
    - [1.1. About NModbus4](#11-about-nmodbus4)
      - [1.1.1. NModbus4 License](#111-nmodbus4-license)
    - [1.2. About NModbus4.Wrapper](#12-about-nmodbus4wrapper)
      - [1.2.1. NModbus4.Wrapper License](#121-nmodbus4wrapper-license)
    - [1.3. Code Example](#13-code-example)
      - [1.3.1. From constructor to connect](#131-from-constructor-to-connect)
      - [1.3.2. Disconnect](#132-disconnect)
      - [1.3.3. Restart](#133-restart)
      - [1.3.4. Read data synchronous](#134-read-data-synchronous)
      - [1.3.5. Write data asynchronous](#135-write-data-asynchronous)
  - [2. API Information](#2-api-information)
    - [2.1. namespace NModbus4.Wrapper.Define](#21-namespace-nmodbus4wrapperdefine)
      - [2.1.1. enum LogLevel](#211-enum-loglevel)
      - [2.1.2. enum CommunicationException](#212-enum-communicationexception)
      - [2.1.3. enum DataType](#213-enum-datatype)
      - [2.1.4. enum ModbusType](#214-enum-modbustype)
      - [2.1.5. enum DataStorage](#215-enum-datastorage)
      - [2.1.6. enum Endian](#216-enum-endian)
      - [2.1.7. struct SerialPortInformation](#217-struct-serialportinformation)
      - [2.1.8. struct EthernetInformation](#218-struct-ethernetinformation)
      - [2.1.9. class CommunicationData](#219-class-communicationdata)
        - [2.1.9.1. Members, Methods](#2191-members-methods)
      - [2.1.10. class ModbusInterface](#2110-class-modbusinterface)
        - [2.1.10.1 Members, Methods](#21101-members-methods)
    - [2.2. namespace NModbus4.Wrapper](#22-namespace-nmodbus4wrapper)
      - [2.2.1. class ModbusService](#221-class-modbusservice)
        - [2.2.1.1 Members, Methods, Events](#2211-members-methods-events)

<br>

- This wrapper class use C# [NModbus4](https://github.com/NModbus4/NModbus4) dll
- Instruction & API information is on following section
<br><br><br>

## 1. Instruction

<br><br>

### 1.1. About NModbus4

<br>

```text
NModbus is a C# implementation of the Modbus protocol.
Provides connectivity to Modbus slave compatible devices and applications.
Supports serial ASCII, serial RTU, TCP, and UDP protocols.
NModbus4 it's a fork of NModbus(https://code.google.com/p/nmodbus).
NModbus4 differs from original NModbus in following:

1. removed USB support(FtdAdapter.dll)
2. removed log4net dependency
3. removed Unme.Common.dll dependency
4. assembly renamed to NModbus4.dll
5. target framework changed to .NET 4
```

<br><br>

#### 1.1.1. NModbus4 License

<br>

```text
The MIT License (MIT)

Copyright (c) 2006 Scott Alexander, 2015 Dmitry Turin

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
```

<br><br>

### 1.2. About NModbus4.Wrapper

<br>

```text
NModbus4.Wrapper is driven from NModbus4 for integrated usage.
Difference of NModbus4.Wrapper from NModbus4 is: 

1. integrate class (RTU, TCP, UDP...) into ModbusService
2. support C# data types (bool, int, float)
3. support threading when using Client
```

<br><br>

#### 1.2.1. NModbus4.Wrapper License

<br>

```text
The MIT License (MIT)

Copyright (c) 2023 Peponi

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
```

<br><br>

### 1.3. Code Example

<br><br>

#### 1.3.1. From constructor to connect

<br>

```cs
string interfaceName = "Test";
ModbusType modbusType = ModbusType.RTU_Master;
int slaveNo = 1;
EthernetInformation ethernetInformation = new EthernetInformation
{
    Address = "127.0.0.1",
    Port = 50001
};
Endian endian = Endian.Big;

ModbusInterface interfaceData = new ModbusInterface(interfaceName, modbusType, slaveNo, ethernetInformation, endian);
ModbusService modbus = new ModbusService(interfaceData, CheckConnectionStatus);

modbus.ModbusLog += Modbus_LogWrite;
modbus.ModbusDataReceived += Modbus_ModbusDataReceived;
modbus.ModbusCommunicationException += Modbus_ModbusCommunicationException;
modbus.ModbusGeneralException += Modbus_ModbusGeneralException;

modbus.Connect();

void CheckConnectionStatus(ModbusInterface modbusInterface, bool connectStatus)
{
    if (connectStatus)
    {
        // Do something...
    }
    else
    {
        // Reconnect, or other things to do..
    }
}
```
<br><br>

#### 1.3.2. Disconnect

<br>

```cs
modbus.Dispose();
```
<br><br>

#### 1.3.3. Restart

<br>

```cs
if (modbus != null) modbus.Dispose();

modbus = new Modbus(interfaceData, CheckConnectionStatus);

modbus.ModbusLog += Modbus_LogWrite;
modbus.ModbusDataReceived += Modbus_ModbusDataReceived;
modbus.ModbusCommunicationException += Modbus_ModbusCommunicationException;
modbus.ModbusGeneralException += Modbus_ModbusGeneralException;

modbus.Connect();
```
<br><br>

#### 1.3.4. Read data synchronous

<br>

```cs
// 1. Single case

var commData = new CommunicationData(DataStorage.HoldingRegister, 0, default(double), modbus.Interface.EndianOption);

if (modbus.ReadData(ref commData))
{
    // Data process...
}
else
{
    // Do something for error handling...
}

// 2. List case

List<CommunicationData> commDatas = new List<CommunicationData>();

commDatas.Add(new CommunicationData(DataStorage.HoldingRegister, 0, default(float), modbus.Interface.EndianOption));
commDatas.Add(new CommunicationData(DataStorage.HoldingRegister, 2, default(bool), modbus.Interface.EndianOption));
commDatas.Add(new CommunicationData(DataStorage.HoldingRegister, 3, default(int), modbus.Interface.EndianOption));

if (modbus.ReadData(ref commDatas))
{
    // Data process...
}
else
{
    // Do something for error handling...
}
```
<br><br>

#### 1.3.5. Write data asynchronous

<br>

```cs
// 1. Single case

var commData = new CommunicationData(DataStorage.HoldingRegister, 0, default(double), modbus.Interface.EndianOption);

if (!await modbus.WriteDataAsync(commData))
{
    // Do something for error handling...
}

// 2. List case

List<CommunicationData> commDatas = new List<CommunicationData>();

commDatas.Add(new CommunicationData(DataStorage.HoldingRegister, 0, default(float), modbus.Interface.EndianOption));
commDatas.Add(new CommunicationData(DataStorage.Coil, 0, default(bool), modbus.Interface.EndianOption));
commDatas.Add(new CommunicationData(DataStorage.HoldingRegister, 2, default(int), modbus.Interface.EndianOption));

if (!await modbus.WriteDataAsync(commDatas))
{
    // Do something for error handling...
}
```

<br><br><br>

## 2. API Information

<br><br>

### 2.1. namespace NModbus4.Wrapper.Define

<br><br>

#### 2.1.1. enum LogLevel

<br>

| Name          | Value |
|---------------|-------|
| Communication | 1     |
| Exception     | 2     |

<br><br>

#### 2.1.2. enum CommunicationException

<br>

| Name                         | Value | Description            |
|------------------------------|-------|------------------------|
| SlaveFunctionCodeException   | 1     |                        |
| SlaveUnimplementedException  | 2     |                        |
| MasterTransportNullException | 3     | Remote connection lost |

<br><br>

#### 2.1.3. enum DataType

<br>

- Support data type

| Name  | Value |
|-------|-------|
| Bool  | 1     |
| Int   | 2     |
| Float | 4     |

<br><br>

#### 2.1.4. enum ModbusType

<br>

- Set Network type, Master / Slave mode

| Name       | Value |
|------------|-------|
| RTU_Master | 1     |
| TCP_Master | 2     |
| UDP_Master | 3     |
| RTU_Slave  | 11    |
| TCP_Slave  | 12    |
| UDP_Slave  | 13    |

<br><br>

#### 2.1.5. enum DataStorage

<br>

| Name            | Value |
|-----------------|-------|
| Coil            | 0     |
| DiscreteInput   | 1     |
| InputRegister   | 2     |
| HoldingRegister | 3     |

<br><br>

#### 2.1.6. enum Endian

<br>

- Remote's endian matching enum

| Name   | Value |
|--------|-------|
| Big    | 1     |
| Little | 2     |

<br><br>

#### 2.1.7. struct SerialPortInformation

<br>

| Type                      | Name      |
|---------------------------|-----------|
| string                    | Port      |
| int                       | Baudrate  |
| System.IO.Ports.Parity    | Parity    |
| int                       | Databits  |
| System.IO.Ports.StopBits  | Stopbits  |
| System.IO.Ports.Handshake | Handshake |

<br><br>

#### 2.1.8. struct EthernetInformation

<br>

| Type   | Name    |
|--------|---------|
| string | Address |
| int    | Port    |

<br><br>

#### 2.1.9. class CommunicationData

<br>

- Built in type for communication of NModbus4.Wrapper

<br>

##### 2.1.9.1. Members, Methods

<br>

1. Members

| Type                                 | Name         | Description                          |
|--------------------------------------|--------------|--------------------------------------|
| [DataStorage](#215-enum-datastorage) | DataStorage  |                                      |
| [DataType](#213-enum-datatype)       | DataType     |                                      |
| [Endian](#216-enum-endian)           | RemoteEndian |                                      |
| ushort                               | StartAddress | Address of data<br>StartAddress >= 0 |
| object                               | Value        | bool, int, float                     |

<br>

2. Methods

| Return type  | Method                                                 | Description                 |
|--------------|--------------------------------------------------------|-----------------------------|
| void         | CommunicationData(DataStorage, ushort, object, Endian) | ctor                        |
| List<ushort> | GetSendData()                                          | Parsing `Value` to hex data |

<br><br>

#### 2.1.10. class ModbusInterface

<br>

##### 2.1.10.1 Members, Methods

<br>

1. Members

| Type                                                        | Name                  | Description         |
|-------------------------------------------------------------|-----------------------|---------------------|
| const int                                                   | TransactionLimit      | For packet dividing |
| string                                                      | Name                  |                     |
| [ModbusType](#214-enum-modbustype)                          | ModbusType            |                     |
| int                                                         | SlaveNumber           | For master mode     |
| [SerialPortInformation](#217-struct-serialportinformation)? | SerialPortInformation |                     |
| [EthernetInformation](#218-struct-ethernetinformation)?     | EthernetInformation   |                     |
| [Endian](#216-enum-endian)                                  | EndianOption          | Remote's endian     |

<br>

2. Methods

| Return type | Method                                                                  | Description |
|-------------|-------------------------------------------------------------------------|-------------|
| void        | ModbusInterface(string, ModbusType, int, SerialPortInformation, Endian) | ctor        |
| void        | ModbusInterface(string, ModbusType, int, EthernetInformation, Endian)   | ctor        |


<br><br><br>

### 2.2. namespace NModbus4.Wrapper

<br>

#### 2.2.1. class ModbusService

<br>

##### 2.2.1.1 Members, Methods, Events

1. Members
    | Type                                           | Name      | Description               |
    |------------------------------------------------|-----------|---------------------------|
    | [ModbusInterface](#2110-class-modbusinterface) | Interface | Communication information |
<br>

2. Methods
    | Return type                            | Method                                                         | Description                              |
    |----------------------------------------|----------------------------------------------------------------|------------------------------------------|
    | void                                   | ModbusService(ModbusInterface, Action\<ModbusInterface, bool>) | ctor                                     |
    | void                                   | Connect()                                                      | Connect to remote master / slave         |
    | void                                   | Dispose()                                                      | Disconnect connection / release thread   |
    | Action\<ModbusInterface, bool>         | ConnectCallback                                                | Connection status changed notice         |
    |                                        |                                                                |                                          |
    | Read methods                           |                                                                |                                          |
    | bool                                   | ReadData\<T>(DataStorage, int, out T)                          | Read function                            |
    | bool                                   | ReadData\<T>(DataStorage, int, int, out List\<T>)              | Read function                            |
    | bool                                   | ReadData(ref CommunicationData)                                | Read function using built in type        |
    | bool                                   | ReadData(ref List\<CommunicationData>)                         | Read function using built in type        |
    | Task<(bool, T)>                        | ReadDataAsync\<T>(DataStorage, int)                            | Async read function                      |
    | Task<(bool, List\<T>)>                 | ReadDataAsync\<T>(DataStorage, int, int)                       | Async read function                      |
    | Task<(bool, CommunicationData)>        | ReadDataAsync(CommunicationData)                               | Async read function using built in type  |
    | Task<(bool, List\<CommunicationData>)> | ReadDataAsync(List\<CommunicationData>)                        | Async read function using built in type  |
    |                                        |                                                                |                                          |
    | Write methods                          |                                                                |                                          |
    | bool                                   | WriteData\<T>(DataStorage, int, T)                             | Write function                           |
    | bool                                   | WriteData\<T>(DataStorage, int, List\<T>)                      | Write function                           |
    | bool                                   | WriteData(CommunicationData)                                   | Write function using built in type       |
    | bool                                   | WriteData(List\<CommunicationData>)                            | Write function using built in type       |
    | Task\<bool>                            | WriteDataAsync\<T>(DataStorage, int, T)                        | Async write function                     |
    | Task\<bool>                            | WriteDataAsync\<T>(DataStorage, int, List\<T>)                 | Async write function                     |
    | Task\<bool>                            | WriteDataAsync(CommunicationData)                              | Async write function using built in type |
    | Task\<bool>                            | WriteDataAsync(List\<CommunicationData>)                       | Async write function using built in type |
    |                                        |                                                                |                                          |
    | Master methods                         |                                                                |                                          |
    | -                                      |                                                                |                                          |
    |                                        |                                                                |                                          |
    | Slave methods                          |                                                                |                                          |
    | void                                   | ClearDataStore()                                               | Clear and initialize all data            |
<br>

3. Events
    | Type          | Event                                                                              | Description                                                              |
    |---------------|------------------------------------------------------------------------------------|--------------------------------------------------------------------------|
    | void          | ModbusGeneralExceptionHandler(ModbusInterface)                                     | Deal general, unknown exception                                          |
    | void          | ModbusCommunicationExceptionHandler(ModbusInterface, CommunicationException)       | Usually need to recreate class for communication after called this event |
    | void          | ModbusLogHandler(ModbusInterface, LogLevel, string)                                | Modbus log                                                               |
    |               |                                                                                    |                                                                          |
    | Master events |                                                                                    |                                                                          |
    | -             |                                                                                    |                                                                          |
    |               |                                                                                    |                                                                          |
    | Slave events  |                                                                                    |                                                                          |
    | void          | ModbusDataReceivedHandler(ModbusInterface, DataStorage, List\<int>, List\<ushort>) | Remote master wrote data to this slave                                   |
<br><br><br>