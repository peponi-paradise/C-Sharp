using Modbus.Data;
using Modbus.Device;
using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NModbus4.Wrapper
{
    /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Modbus"]'/>
    public partial class Modbus
    {
        /// <summary>
        /// Connection status changed notice
        /// </summary>
        private Action<(Define_Modbus.ModbusInterface ModbusInterface, bool ConnectStatus)> ConnectCallback;

        public delegate void ModbusGeneralExceptionHandler(Define_Modbus.ModbusInterface modbusInterface);

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="ModbusGeneralException"]'/>
        public event ModbusGeneralExceptionHandler ModbusGeneralException;

        public delegate void ModbusCommunicationExceptionHandler(Define_Modbus.ModbusInterface modbusInterface, Define_Modbus.CommunicationException exception);

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="ModbusCommunicationException"]'/>
        public event ModbusCommunicationExceptionHandler ModbusCommunicationException;

        public delegate void ModbusReadDataHandler(Define_Modbus.ModbusInterface modbusInterface, List<Define_Modbus.CommunicationData> communicationData);

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="ModbusReadData"]'/>
        public event ModbusReadDataHandler ModbusReadData;

        public delegate void ModbusLogHandler(Define_Modbus.ModbusInterface modbusInterface, Define_Modbus.LogLevel logLevel, string message);

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="ModbusLog"]'/>
        public event ModbusLogHandler ModbusLog;

        public delegate void ModbusDataReceivedHandler(Define_Modbus.ModbusInterface modbusInterface, Define_Modbus.DataStorage dataStorage, List<int> address, List<ushort> value);

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="ModbusDataReceived"]'/>
        public event ModbusDataReceivedHandler ModbusDataReceived;

        public Define_Modbus.ModbusInterface Interface;
        private Thread ModbusThread;
        private dynamic ModbusInstance;
        private List<Define_Modbus.CommunicationData> CommunicationData = new List<Define_Modbus.CommunicationData>();
        private object ThreadLocker = new object();
        private bool ThreadRun = false;
        private bool ThreadBusy = false;

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Modbus"]'/>
        public Modbus(Define_Modbus.ModbusInterface modbusInterface, Action<(Define_Modbus.ModbusInterface ModbusInterface, bool ConnectStatus)> connectCallback = null)
        {
            Interface = modbusInterface;
            this.ConnectCallback = connectCallback;
        }

        ~Modbus()
        {
            try
            {
                this.Dispose();
            }
            catch
            {
            }
        }

        public void Connect()
        {
            ThreadRun = true;
            if ((int)Interface.ModbusType < 10) Master_Worker();
            else
            {
                ModbusThread = new Thread(() => Slave_Worker());
                ModbusThread.IsBackground = true;
                ModbusThread.Start();
            }
        }

        /// <summary>
        /// Disconnect function이 없어 direct dispose
        /// </summary>
        /// <returns></returns>
        public bool Dispose()
        {
            bool rtn = true;
            try
            {
                ThreadRun = false;
                while (ThreadBusy) { Thread.Sleep(10); }
                ModbusInstance?.Dispose();
                Thread.Sleep(100);
                if (ModbusThread != null && ModbusThread.IsAlive) ModbusThread.Abort();
            }
            catch (Exception e)
            {
                rtn = false;
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, "Modbus dispose failed - " + e.ToString());
            }
            return rtn;
        }

        // Master worker는 Communication thread 만들고 종료
        private void Master_Worker()
        {
            try
            {
                switch (Interface.ModbusType)
                {
                    case Define_Modbus.ModbusType.RTU_Master:
                        SerialPort ModbusMasterPort = new SerialPort();
                        ModbusMasterPort.PortName = Interface.Address;
                        ModbusMasterPort.BaudRate = 115200;
                        ModbusMasterPort.DataBits = 8;
                        ModbusMasterPort.Parity = Parity.None;
                        ModbusMasterPort.StopBits = StopBits.One;
                        ModbusMasterPort.Open();
                        ModbusInstance = ModbusSerialMaster.CreateRtu(ModbusMasterPort);
                        break;

                    case Define_Modbus.ModbusType.TCP_Master:
                        IPEndPoint TCPPoint = new IPEndPoint(IPAddress.Parse(Interface.Address), Interface.Port);
                        TcpClient MasterTcpClient = new TcpClient();
                        MasterTcpClient.Connect(TCPPoint);
                        ModbusInstance = ModbusIpMaster.CreateIp(MasterTcpClient);
                        break;

                    case Define_Modbus.ModbusType.UDP_Master:
                        IPEndPoint UDPPoint = new IPEndPoint(IPAddress.Parse(Interface.Address), Interface.Port);
                        UdpClient udpClient = new UdpClient();
                        udpClient.Connect(UDPPoint);
                        ModbusInstance = ModbusIpMaster.CreateIp(udpClient);
                        break;
                }

                ModbusInstance.Transport.ReadTimeout = 400;
                ModbusInstance.Transport.WriteTimeout = 400;
                ModbusInstance.Transport.Retries = 1;
                ModbusInstance.Transport.WaitToRetryMilliseconds = 200;
                ModbusInstance.Transport.SlaveBusyUsesRetryCount = false;

                ConnectCallback?.Invoke((Interface, true));

                if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Polling || Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling)
                {
                    ModbusThread = new Thread(() => Communication_Thread());
                    ModbusThread.IsBackground = true;
                    ModbusThread.Start();
                }
            }
            catch (Exception e)
            {
                // Create argument, no port, socket connect fail
                if (e is ArgumentException || e is IOException || e is SocketException)
                {
                    ConnectCallback?.Invoke((Interface, false));
                }
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, "Exception occured in Modbus Device - " + e.ToString());
                ModbusGeneralException?.Invoke(Interface);
            }
        }

        //Thread에서 돌려야 함(Listen이 무한 블로킹 : Listen 호출 이후 들어오는 요청 자동 응답). Exception 나면 Listen 풀리고 쓰레드 종료됨. Thread 생성하고 다시 Listen 걸어주거나 다시 시작해야함. 일단은 다시 시작하는 정책으로
        private void Slave_Worker()
        {
            try
            {
                if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Polling || Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling)
                {
                    ModbusThread = new Thread(() => Communication_Thread());
                    ModbusThread.IsBackground = true;
                    ModbusThread.Start();
                }
                switch (Interface.ModbusType)
                {
                    case Define_Modbus.ModbusType.RTU_Slave:
                        SerialPort ModbusSlavePort = new SerialPort();
                        ModbusSlavePort.PortName = Interface.Address;
                        ModbusSlavePort.BaudRate = 115200;
                        ModbusSlavePort.DataBits = 8;
                        ModbusSlavePort.Parity = Parity.None;
                        ModbusSlavePort.StopBits = StopBits.One;
                        ModbusSlavePort.Open();
                        var RTUSlave = ModbusSerialSlave.CreateRtu((byte)Interface.SlaveNumber, ModbusSlavePort);
                        RTUSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        RTUSlave.ModbusSlaveRequestReceived += Slave_RequestReceived;
                        RTUSlave.DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                        RTUSlave.DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                        ModbusInstance = RTUSlave;
                        ConnectCallback?.Invoke((Interface, true));
                        ModbusInstance.Listen();
                        break;

                    case Define_Modbus.ModbusType.TCP_Slave:
                        TcpListener tcpListener = new TcpListener(IPAddress.Parse(Interface.Address), Interface.Port);
                        var TCPSlave = ModbusTcpSlave.CreateTcp((byte)Interface.SlaveNumber, tcpListener);
                        TCPSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        TCPSlave.ModbusSlaveRequestReceived += Slave_RequestReceived;
                        TCPSlave.DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                        TCPSlave.DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                        ModbusInstance = TCPSlave;
                        ConnectCallback?.Invoke((Interface, true));
                        ModbusInstance.Listen();
                        break;

                    case Define_Modbus.ModbusType.UDP_Slave:
                        UdpClient udpClient = new UdpClient(Interface.Port);
                        var UDPSlave = ModbusUdpSlave.CreateUdp((byte)Interface.SlaveNumber, udpClient);
                        UDPSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        UDPSlave.ModbusSlaveRequestReceived += Slave_RequestReceived;
                        UDPSlave.DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                        UDPSlave.DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                        ModbusInstance = UDPSlave;
                        ConnectCallback?.Invoke((Interface, true));
                        ModbusInstance.Listen();
                        break;
                }
            }
            catch (Exception e)
            {
                // Create argument, no port fail
                if (e is ArgumentException || e is IOException)
                {
                    ConnectCallback?.Invoke((Interface, false));
                }
                else if (e is NotImplementedException)
                {
                    if (e.ToString().Contains("Function code") || e.ToString().Contains("not supported")) ModbusCommunicationException?.Invoke(Interface, Define_Modbus.CommunicationException.SlaveFunctionCodeException);
                    else ModbusCommunicationException?.Invoke(Interface, Define_Modbus.CommunicationException.SlaveUnimplementedException);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, "Exception occured on Modbus Device - " + e.ToString());
                    return;
                }
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, "Exception occured on Modbus Device - " + e.ToString());
                ModbusGeneralException?.Invoke(Interface);
            }
        }

        private void Communication_Thread()
        {
            while (ThreadRun)
            {
                if (CommunicationData.Count != 0)
                {
                    lock (ThreadLocker)
                    {
                        switch (Interface.ModbusType)
                        {
                            case Define_Modbus.ModbusType.RTU_Master:
                            case Define_Modbus.ModbusType.TCP_Master:
                            case Define_Modbus.ModbusType.UDP_Master:
                                if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Polling) Master_WriteProcess();
                                if (Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling) Master_ReadProcess();
                                break;

                            case Define_Modbus.ModbusType.RTU_Slave:
                            case Define_Modbus.ModbusType.TCP_Slave:
                            case Define_Modbus.ModbusType.UDP_Slave:
                                if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Polling) Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption.Write);
                                if (Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling) Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption.Read);
                                break;
                        }
                    }
                    Thread.Sleep(Interface.PollingInterval_ms);
                }
                else Thread.Sleep(Interface.PollingInterval_ms);
            }
        }

        private void AddorUpdateCommunicationData(Define_Modbus.CommunicationData CommData)
        {
            lock (ThreadLocker)
            {
                int FindIndex = -1;
                for (int Index = 0; Index < CommunicationData.Count; Index++)
                {
                    if (CommunicationData[Index].DataStorage.Equals(CommData.DataStorage) && CommunicationData[Index].StartAddress.Equals(CommData.StartAddress) && CommunicationData[Index].ReadWriteOption.Equals(CommData.ReadWriteOption)) { FindIndex = Index; break; }
                }
                if (FindIndex != -1) CommunicationData[FindIndex] = CommData; else CommunicationData.Add(CommData);
            }
        }

        public void ClearCommunicationData()
        {
            CommunicationData.Clear();
        }

        private string ByteToHex(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            return hex.Replace("-", " ");
        }
    }
}