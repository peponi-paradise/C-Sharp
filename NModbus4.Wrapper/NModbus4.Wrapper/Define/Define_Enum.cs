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
        /// Remote connection lost
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
    /// Where to read / write
    /// </summary>
    public enum DataStorage
    {
        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_Enum/Doc[@name="DataStorage.Coil"]'/>
        Coil = 0,

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_Enum/Doc[@name="DataStorage.DiscreteInput"]'/>
        DiscreteInput = 1,

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_Enum/Doc[@name="DataStorage.InputRegister"]'/>
        InputRegister = 2,

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_Enum/Doc[@name="DataStorage.HoldingRegister"]'/>
        HoldingRegister = 3
    }

    /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_Enum/Doc[@name="Endian"]'/>
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
}