namespace NModbus4.Wrapper.Define
{
    public struct SerialPortInformation
    {
        public string Port;
        public int Baudrate;
        public System.IO.Ports.Parity Parity;
        public int Databits;
        public System.IO.Ports.StopBits Stopbits;
        public System.IO.Ports.Handshake Handshake;
    }

    public struct EthernetInformation
    {
        public string Address;
        public int Port;
    }

    public class ModbusInterface
    {
        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_ModbusInterface/Doc[@name="TransactionLimit"]'/>
        public const int TransactionLimit = 123;

        public string Name;
        public ModbusType ModbusType;
        public int SlaveNumber;

        public SerialPortInformation? SerialPortInformation;
        public EthernetInformation? EthernetInformation;

        /// <summary>
        /// By the Modbus specification, default EndianOption is Endian.Big
        /// </summary>
        public Endian EndianOption;

        public ModbusInterface(string name, ModbusType modbusType, int slaveNumber, SerialPortInformation information, Endian endianOption = Endian.Big)
        {
            Name = name;
            ModbusType = modbusType;
            SlaveNumber = slaveNumber;
            SerialPortInformation = information;
            EndianOption = endianOption;
        }

        public ModbusInterface(string name, ModbusType modbusType, int slaveNumber, EthernetInformation information, Endian endianOption = Endian.Big)
        {
            Name = name;
            ModbusType = modbusType;
            SlaveNumber = slaveNumber;
            EthernetInformation = information;
            EndianOption = endianOption;
        }
    }
}