using System;
using System.Collections.Generic;
using NModbus4.Wrapper.Util.Converter;

namespace NModbus4.Wrapper.Define
{
    public enum LogLevel
    {
        Communication = 1,
        Exception = 2,
    }

    public enum CommunicationException
    {
        SlaveFunctionCodeException = 1,
        SlaveUnimplementedException = 2,

        /// <summary>
        /// 수신자 연결 종료
        /// </summary>
        MasterTransportNullException = 3,
    }

    /// <summary>
    /// enum value : bit(Bool) or byte(others)
    /// </summary>
    public enum DataType
    {
        Bool = 1,
        Int = 2,
        Float = 4,
    }

    public enum ReadWriteOption
    {
        Read = 1,
        Write = 2,
    }

    /// <summary>
    /// RTU : Serial
    /// </summary>
    public enum ModbusType
    {
        RTU_Master = 1,
        TCP_Master = 2,
        UDP_Master = 3,
        RTU_Slave = 11,
        TCP_Slave = 12,
        UDP_Slave = 13,
    }

    /// <summary>
    /// Where to write
    /// </summary>
    public enum DataStorage
    {
        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Coil"]'/>
        Coil = 0,

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="DiscreteInput"]'/>
        DiscreteInput = 1,

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="InputRegister"]'/>
        InputRegister = 2,

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="HoldingRegister"]'/>
        HoldingRegister = 3
    }

    /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Endian"]'/>
    public enum Endian
    {
        /// <summary>
        /// Endian : Big Endian  <br/>
        /// Bool : A  <br/>
        /// Int16 : AB  <br/>
        /// Float : ABCD
        /// </summary>
        Big = 1,

        /// <summary>
        /// Endian : Little Endian  <br/>
        /// Bool : A  <br/>
        /// Int16 : BA  <br/>
        /// Float : DCBA
        /// </summary>
        Little = 2,
    }

    public enum UpdateOption
    {
        Immediate = 1,

        /// <summary>
        /// Time interval updates
        /// </summary>
        Polling = 2
    }

    /// <include file='ClassSummary.xml' path='Docs/Doc[@name="TransactionLimit"]'/>
    public static int TransactionLimit
    {
        get => _TransactionLimit;
        set
        {
            if (value >= 123) value = 123;
            _TransactionLimit = value;
        }
    }

    private static int _TransactionLimit = 50;

    public class ModbusInterface
    {
        public string Name;
        public ModbusType ModbusType;
        public int SlaveNumber;

        /// <summary>
        /// RTU : COM port<br/>
        /// TCP : IP address<br/>
        /// UDP : Not use
        /// </summary>
        public string Address;

        public int Port;
        public UpdateOption WriteUpdateOption;
        public UpdateOption ReadUpdateOption;
        public int PollingInterval_ms;

        /// <summary>
        /// By the Modbus specification, default EndianOption is Endian.Big
        /// </summary>
        public Endian EndianOption;

        public ModbusInterface(string Name, ModbusType ModbusType, int SlaveNumber, string Address, int Port = 0, UpdateOption WriteUpdateOption = UpdateOption.Polling, UpdateOption ReadUpdateOption = UpdateOption.Immediate, int PollingInterval_ms = 1000, Endian EndianOption = Endian.Big)
        {
            this.Name = Name;
            this.ModbusType = ModbusType;
            this.SlaveNumber = SlaveNumber;
            this.Address = Address;
            this.Port = Port;
            this.WriteUpdateOption = WriteUpdateOption;
            this.ReadUpdateOption = ReadUpdateOption;
            this.PollingInterval_ms = PollingInterval_ms;
            this.EndianOption = EndianOption;
        }
    }

    /// <include file='ClassSummary.xml' path='Docs/Doc[@name="CommunicationData"]'/>
    public class CommunicationData
    {
        public DataStorage DataStorage;
        public Endian Endian;
        public DataType DataType;
        public ReadWriteOption ReadWriteOption;
        private ushort _StartAddress;

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="StartAddress"]'/>
        public ushort StartAddress
        {
            get => _StartAddress;
            set
            {
                if (value < 1) value = 1;
                _StartAddress = value;
            }
        }

        public int DataLength;
        public List<ushort> Data;
        private object _Value;

        public object Value
        {
            get => _Value;
            set
            {
                _Value = value;
                if (value != null)
                {
                    if (DataType == DataType.Float) Data = Converter.ToUshortHexData((float)Convert.ChangeType(_Value, typeof(float)), Endian);
                    else if (DataType == DataType.Int) Data = Converter.ToUshortHexData((int)_Value, Endian);
                    else Data = Converter.ToUshortHexData((bool)_Value, Endian);
                }
            }
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="CommunicationData"]'/>
        public CommunicationData(DataStorage DataStorage, ushort StartAddress, object Value, Endian ToEndian, DataType DataType = DataType.Float, ReadWriteOption ReadWriteOption = ReadWriteOption.Write)
        {
            this.DataStorage = DataStorage;
            this.Endian = ToEndian;
            this.DataType = DataType;
            this.StartAddress = StartAddress;
            this.Data = new List<ushort>();
            this.Value = Value;
            this.ReadWriteOption = ReadWriteOption;

            DataLength = this.Data.Count;
        }
    }
}