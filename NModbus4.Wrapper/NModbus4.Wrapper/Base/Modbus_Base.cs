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
    /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_Base/Doc[@name="Modbus"]'/>
    public partial class ModbusService
    {
        /// <summary>
        /// Connection status changed notice
        /// </summary>
        private readonly Action<ModbusInterface, bool> ConnectCallback;

        public delegate void ModbusGeneralExceptionHandler(ModbusInterface modbusInterface);

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_Base/Doc[@name="ModbusGeneralException"]'/>
        public event ModbusGeneralExceptionHandler ModbusGeneralException;

        public delegate void ModbusCommunicationExceptionHandler(ModbusInterface modbusInterface, CommunicationException exception);

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_Base/Doc[@name="ModbusCommunicationException"]'/>
        public event ModbusCommunicationExceptionHandler ModbusCommunicationException;

        public delegate void ModbusLogHandler(ModbusInterface modbusInterface, LogLevel logLevel, string message);

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_Base/Doc[@name="ModbusLog"]'/>
        public event ModbusLogHandler ModbusLog;

        public delegate void ModbusDataReceivedHandler(ModbusInterface modbusInterface, DataStorage dataStorage, List<int> addresses, List<ushort> values);

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_Base/Doc[@name="ModbusDataReceived"]'/>
        public event ModbusDataReceivedHandler ModbusDataReceived;

        public ModbusInterface Interface;
        private dynamic ModbusInstance;
        private Thread SlaveThread;

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_Base/Doc[@name="Modbus"]'/>
        public ModbusService(ModbusInterface modbusInterface, Action<ModbusInterface, bool> connectCallback = null)
        {
            Interface = modbusInterface;
            this.ConnectCallback = connectCallback;
        }

        ~ModbusService()
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
            if ((int)Interface.ModbusType < 10) CreateMaster();
            else
            {
                SlaveThread = new Thread(() => CreateSlave());
                SlaveThread.IsBackground = true;
                SlaveThread.Start();
            }
        }

        /// <summary>
        /// Disconnect function이 없어 Disconnect 대신 direct dispose
        /// </summary>
        /// <returns></returns>
        public bool Dispose()
        {
            bool rtn = true;
            try
            {
                ModbusInstance?.Dispose();
                Thread.Sleep(100);
                if (SlaveThread != null && SlaveThread.IsAlive) SlaveThread.Abort();
            }
            catch (Exception e)
            {
                rtn = false;
                ModbusLog?.Invoke(Interface, LogLevel.Exception, "Modbus dispose failed - " + e.ToString());
            }
            return rtn;
        }

        private void CreateMaster()
        {
            try
            {
                switch (Interface.ModbusType)
                {
                    case ModbusType.RTU_Master:
                        SerialPort modbusMasterPort = new SerialPort
                        {
                            PortName = Interface.SerialPortInformation.Value.Port,
                            BaudRate = Interface.SerialPortInformation.Value.Baudrate,
                            DataBits = Interface.SerialPortInformation.Value.Databits,
                            Parity = Interface.SerialPortInformation.Value.Parity,
                            StopBits = Interface.SerialPortInformation.Value.Stopbits,
                            Handshake = Interface.SerialPortInformation.Value.Handshake
                        };
                        modbusMasterPort.Open();
                        ModbusInstance = ModbusSerialMaster.CreateRtu(modbusMasterPort);
                        break;

                    case ModbusType.TCP_Master:
                        IPEndPoint tcpPoint = new IPEndPoint(IPAddress.Parse(Interface.EthernetInformation.Value.Address), Interface.EthernetInformation.Value.Port);
                        TcpClient masterTcpClient = new TcpClient();
                        masterTcpClient.Connect(tcpPoint);
                        ModbusInstance = ModbusIpMaster.CreateIp(masterTcpClient);
                        break;

                    case ModbusType.UDP_Master:
                        IPEndPoint udpPoint = new IPEndPoint(IPAddress.Parse(Interface.EthernetInformation.Value.Address), Interface.EthernetInformation.Value.Port);
                        UdpClient udpClient = new UdpClient();
                        udpClient.Connect(udpPoint);
                        ModbusInstance = ModbusIpMaster.CreateIp(udpClient);
                        break;
                }

                ModbusInstance.Transport.ReadTimeout = 400;
                ModbusInstance.Transport.WriteTimeout = 400;
                ModbusInstance.Transport.Retries = 1;
                ModbusInstance.Transport.WaitToRetryMilliseconds = 200;
                ModbusInstance.Transport.SlaveBusyUsesRetryCount = false;

                ConnectCallback?.Invoke(Interface, true);
            }
            catch (Exception e)
            {
                // Create argument, no port, socket connect fail
                if (e is ArgumentException || e is IOException || e is SocketException)
                {
                    ConnectCallback?.Invoke(Interface, false);
                }
                ModbusLog?.Invoke(Interface, LogLevel.Exception, "Exception occured in Modbus Device - " + e.ToString());
                ModbusGeneralException?.Invoke(Interface);
            }
        }

        /// <summary>
        /// Thread에서 돌려야 함(Listen이 무한 블로킹 : Listen 호출 이후 들어오는 요청 자동 응답). Exception 나면 Listen 풀리고 쓰레드 종료됨. Thread 생성하고 다시 Listen 걸어주거나 다시 시작해야함.
        /// </summary>
        private void CreateSlave()
        {
            try
            {
                switch (Interface.ModbusType)
                {
                    case ModbusType.RTU_Slave:
                        SerialPort modbusSlavePort = new SerialPort
                        {
                            PortName = Interface.SerialPortInformation.Value.Port,
                            BaudRate = Interface.SerialPortInformation.Value.Baudrate,
                            DataBits = Interface.SerialPortInformation.Value.Databits,
                            Parity = Interface.SerialPortInformation.Value.Parity,
                            StopBits = Interface.SerialPortInformation.Value.Stopbits,
                            Handshake = Interface.SerialPortInformation.Value.Handshake
                        };
                        modbusSlavePort.Open();
                        var rtuSlave = ModbusSerialSlave.CreateRtu((byte)Interface.SlaveNumber, modbusSlavePort);
                        rtuSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        rtuSlave.ModbusSlaveRequestReceived += Slave_RequestReceived;
                        rtuSlave.DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                        rtuSlave.DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                        ModbusInstance = rtuSlave;
                        break;

                    case ModbusType.TCP_Slave:
                        TcpListener tcpListener = new TcpListener(IPAddress.Parse(Interface.EthernetInformation.Value.Address), Interface.EthernetInformation.Value.Port);
                        var tcpSlave = ModbusTcpSlave.CreateTcp((byte)Interface.SlaveNumber, tcpListener);
                        tcpSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        tcpSlave.ModbusSlaveRequestReceived += Slave_RequestReceived;
                        tcpSlave.DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                        tcpSlave.DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                        ModbusInstance = tcpSlave;
                        break;

                    case ModbusType.UDP_Slave:
                        UdpClient udpClient = new UdpClient(Interface.EthernetInformation.Value.Port);
                        var udpSlave = ModbusUdpSlave.CreateUdp((byte)Interface.SlaveNumber, udpClient);
                        udpSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        udpSlave.ModbusSlaveRequestReceived += Slave_RequestReceived;
                        udpSlave.DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                        udpSlave.DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                        ModbusInstance = udpSlave;
                        break;
                }
                ConnectCallback?.Invoke(Interface, true);
                ModbusInstance.Listen();
            }
            catch (Exception e)
            {
                // Create argument, no port fail
                if (e is ArgumentException || e is IOException)
                {
                    ConnectCallback?.Invoke(Interface, false);
                }
                else if (e is NotImplementedException)
                {
                    if (e.ToString().Contains("Function code") || e.ToString().Contains("not supported")) ModbusCommunicationException?.Invoke(Interface, CommunicationException.SlaveFunctionCodeException);
                    else ModbusCommunicationException?.Invoke(Interface, CommunicationException.SlaveUnimplementedException);
                    ModbusLog?.Invoke(Interface, LogLevel.Exception, "Exception occured on Modbus Device - " + e.ToString());
                    return;
                }
                ModbusLog?.Invoke(Interface, LogLevel.Exception, "Exception occured on Modbus Device - " + e.ToString());
                ModbusGeneralException?.Invoke(Interface);
            }
        }
    }
}