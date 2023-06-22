using Modbus.Data;
using Modbus.Device;
using NModbus4.Wrapper.Define;
using NModbus4.Wrapper.Util.Converter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NModbus4.Wrapper
{
    public partial class ModbusService
    {
        /// <summary>
        /// Clear all data
        /// </summary>
        public void ClearDataStore()
        {
            try
            {
                if ((int)Interface.ModbusType > 10)
                {
                    var DataStore = DataStoreFactory.CreateDefaultDataStore();
                    DataStore.DataStoreReadFrom += Slave_DataStoreReadFrom;
                    DataStore.DataStoreWrittenTo += Slave_DataStoreWrittenTo;
                    ModbusInstance.DataStore = DataStore;
                }
            }
            catch (Exception e)
            {
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus ClearRegister Failed - {e}");
            }
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_SlaveFunction/Doc[@name="Slave_WriteData"]'/>
        private void Slave_WriteData(CommunicationData commData)
        {
            switch (commData.DataStorage)
            {
                case DataStorage.Coil:
                    ModbusInstance.DataStore.CoilDiscretes[commData.StartAddress] = commData.GetSendData().First() == 1;
                    break;

                case DataStorage.DiscreteInput:
                    ModbusInstance.DataStore.InputDiscretes[commData.StartAddress] = commData.GetSendData().First() == 1;
                    break;

                case DataStorage.InputRegister:
                    switch (commData.DataType)
                    {
                        case DataType.Bool:
                        case DataType.Int:
                            ModbusInstance.DataStore.InputRegisters[commData.StartAddress] = commData.GetSendData().First();
                            break;

                        case DataType.Float:
                            for (int index = 0; index < commData.GetSendData().Count; index++) ModbusInstance.DataStore.InputRegisters[commData.StartAddress + index] = commData.GetSendData()[index];
                            break;
                    }
                    break;

                case DataStorage.HoldingRegister:
                    switch (commData.DataType)
                    {
                        case DataType.Bool:
                        case DataType.Int:
                            ModbusInstance.DataStore.HoldingRegisters[commData.StartAddress] = commData.GetSendData().First();
                            break;

                        case DataType.Float:
                            for (int index = 0; index < commData.GetSendData().Count; index++) ModbusInstance.DataStore.HoldingRegisters[commData.StartAddress + index] = commData.GetSendData()[index];
                            break;
                    }
                    break;
            }
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_SlaveFunction/Doc[@name="Slave_ReadDataSingle"]'/>
        private bool Slave_ReadData<T>(DataStorage dataStorage, int address, out T data)
        {
            bool rtn = false;
            data = default;
            try
            {
                ushort[] readData = null;

                switch (dataStorage)
                {
                    case DataStorage.Coil:
                        readData = new ushort[1] { ModbusInstance.DataStore.CoilDiscretes[address] ? (ushort)1 : (ushort)0 };
                        break;

                    case DataStorage.DiscreteInput:
                        readData = new ushort[1] { ModbusInstance.DataStore.InputDiscretes[address] ? (ushort)1 : (ushort)0 };
                        break;

                    case DataStorage.InputRegister:
                        if (typeof(T) != typeof(float)) readData = new ushort[1] { ModbusInstance.DataStore.InputRegisters[address] };
                        else readData = new ushort[2] { ModbusInstance.DataStore.InputRegisters[address], ModbusInstance.DataStore.InputRegisters[address + 1] };
                        break;

                    case DataStorage.HoldingRegister:
                        if (typeof(T) != typeof(float)) readData = new ushort[1] { ModbusInstance.DataStore.HoldingRegisters[address] };
                        else readData = new ushort[2] { ModbusInstance.DataStore.HoldingRegisters[address], ModbusInstance.DataStore.HoldingRegisters[address + 1] };
                        break;
                }
                data = Converter.FromUShortHexData<T>(readData, Interface.EndianOption);
                return true;
            }
            catch (Exception e)
            {
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Slave_ReadData Failed - {e}");
            }
            return rtn;
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_SlaveFunction/Doc[@name="Slave_ReadDataMulti"]'/>
        private bool Slave_ReadData<T>(DataStorage dataStorage, int startAddress, int readCount, out List<T> datas)
        {
            bool rtn = false;
            datas = new List<T>();
            try
            {
                List<ushort> readData = null;
                DataStore dataStore = ModbusInstance.DataStore;     //DataStore 받아서 할당 해놓지 않으면 Linq function 사용 못함..

                switch (dataStorage)
                {
                    case DataStorage.Coil:
                        bool[] coilArr = dataStore.CoilDiscretes.Skip(startAddress).Take(readCount).ToArray();
                        readData = Array.ConvertAll(coilArr, x => x ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case DataStorage.DiscreteInput:
                        bool[] discreteArr = dataStore.InputDiscretes.Skip(startAddress).Take(readCount).ToArray();
                        readData = Array.ConvertAll(discreteArr, x => x ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case DataStorage.InputRegister:
                        if (typeof(T) != typeof(float)) readData = dataStore.InputRegisters.Skip(startAddress).Take(readCount).ToList();
                        else readData = dataStore.InputRegisters.Skip(startAddress).Take(readCount * 2).ToList();
                        break;

                    case DataStorage.HoldingRegister:
                        if (typeof(T) != typeof(float)) readData = dataStore.HoldingRegisters.Skip(startAddress).Take(readCount).ToList();
                        else readData = dataStore.HoldingRegisters.Skip(startAddress).Take(readCount * 2).ToList();
                        break;
                }

                for (int index = 0; index < readCount; index++)
                {
                    if (typeof(T) != typeof(float))
                    {
                        ushort[] inputData = new ushort[1] { readData[0] };
                        var convert = Converter.FromUShortHexData<T>(inputData, Interface.EndianOption);
                        datas.Add(convert);
                        readData.RemoveAt(0);
                    }
                    else
                    {
                        ushort[] inputData = new ushort[2] { readData[0], readData[1] };
                        var convert = Converter.FromUShortHexData<T>(inputData, Interface.EndianOption);
                        datas.Add(convert);
                        readData.RemoveRange(0, 2);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Slave_ReadData Failed - {e}");
            }
            return rtn;
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_SlaveFunction/Doc[@name="Slave_RequestReceived"]'/>
        // Slave data request entry point
        private void Slave_RequestReceived(object sender, ModbusSlaveRequestEventArgs e)
        {
            ModbusLog?.Invoke(Interface, LogLevel.Communication, $"Slave {e.Message.SlaveAddress} received request - Func code : {e.Message.FunctionCode.ToString("X2")}, Contents : {Converter.ByteToHex(e.Message.ProtocolDataUnit)}");
        }

        /// <summary>
        /// Master가 읽어감<br/>통신 address index 0 == DataStore index 1
        /// </summary>
        private void Slave_DataStoreReadFrom(object sender, DataStoreEventArgs e)
        {
            string dataLogString = string.Empty;
            var hexString = string.Empty;
            if (e.ModbusDataType == ModbusDataType.HoldingRegister || e.ModbusDataType == ModbusDataType.InputRegister)
            {
                var contains = e.Data.B.ToList();   //B가 실질 데이터

                foreach (var data in contains) hexString += data.ToString("X2") + ",";
                dataLogString = String.Join(", ", contains);
            }
            else if (e.ModbusDataType == ModbusDataType.Coil || e.ModbusDataType == ModbusDataType.Input)
            {
                var contains = e.Data.A.ToList();   //A가 실질 데이터

                foreach (var data in contains) hexString += (data ? 1 : 0).ToString("X2") + ",";
                dataLogString = String.Join(", ", contains);
            }
            ModbusLog?.Invoke(Interface, LogLevel.Communication, $"Data readed - Type : {e.ModbusDataType}, Start Address : {e.StartAddress}, Ushort : {dataLogString}, Hex : {hexString}");
        }

        /// <summary>
        /// Master가 씀<br/>통신 address index 0 == DataStore index 1
        /// </summary>
        private void Slave_DataStoreWrittenTo(object sender, DataStoreEventArgs e)
        {
            string dataLogString = string.Empty;
            var hexString = string.Empty;
            if (e.ModbusDataType == ModbusDataType.HoldingRegister)
            {
                var addresses = new List<int>();
                var contains = e.Data.B.ToList();   //B가 실질 데이터
                for (int i = (int)e.StartAddress; i < (int)e.StartAddress + contains.Count; i++) addresses.Add(i + 1);

                foreach (var data in contains) hexString += data.ToString("X2") + ",";
                dataLogString = String.Join(", ", contains);

                ModbusDataReceived?.Invoke(Interface, DataStorage.HoldingRegister, addresses, contains);
            }
            else if (e.ModbusDataType == ModbusDataType.Coil)
            {
                var addresses = new List<int>();
                var contains = e.Data.A.ToList();   //A가 실질 데이터
                for (int i = (int)e.StartAddress; i < (int)e.StartAddress + contains.Count; i++) addresses.Add(i + 1);

                foreach (var data in contains) hexString += (data ? 1 : 0).ToString("X2") + ",";
                dataLogString = String.Join(", ", contains);

                ModbusDataReceived?.Invoke(Interface, DataStorage.Coil, addresses, Array.ConvertAll(contains.ToArray(), x => x ? (ushort)1 : (ushort)0).ToList());
            }
            ModbusLog?.Invoke(Interface, LogLevel.Communication, $"Data written - Type : {e.ModbusDataType}, Start Address : {e.StartAddress}, Ushort : {dataLogString}, Hex : {hexString}");
        }
    }
}