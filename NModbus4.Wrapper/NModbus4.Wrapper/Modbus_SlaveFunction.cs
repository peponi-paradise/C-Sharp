using Modbus.Data;
using Modbus.Device;
using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NModbus4.Wrapper
{
    public partial class Modbus
    {
        /// <summary>
        /// Read/Write data entry point<br/>
        /// Read/Write all datas in CommunicationData
        /// </summary>
        /// <returns>Success or fail<br/>If there is no data to send, return true</returns>
        private bool Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption ReadWriteOption)
        {
            bool rtn = true;
            ThreadBusy = true;

            List<Define_Modbus.CommunicationData> TempList = new List<Define_Modbus.CommunicationData>();
            while (true)
            {
                if (Slave_GetReadWriteCommunicationData(CommunicationData, ReadWriteOption, out var CommunicationDataList) == true)
                {
                    if (ReadWriteOption == Define_Modbus.ReadWriteOption.Write) foreach (var CommData in CommunicationDataList) Slave_WriteData(CommData);
                    else
                    {
                        foreach (var CommData in CommunicationDataList)
                        {
                            if (CommData.DataType == Define_Modbus.DataType.Float) if (Slave_ReadData(CommData.DataStorage, CommData.StartAddress, out float Value) == false) { rtn = false; break; } else { CommData.Value = Value; TempList.Add(CommData); }
                            else if (CommData.DataType == Define_Modbus.DataType.Int) if (Slave_ReadData(CommData.DataStorage, CommData.StartAddress, out int Value) == false) { rtn = false; break; } else { CommData.Value = Value; TempList.Add(CommData); }
                            else if (Slave_ReadData(CommData.DataStorage, CommData.StartAddress, out bool Value) == false) { rtn = false; break; } else { CommData.Value = Value; TempList.Add(CommData); }
                        }
                    }
                }
                else break;
            }

            if (TempList.Count > 0)
            {
                foreach (var CommData in TempList) CommunicationData.Add(CommData);
                ModbusReadData?.Invoke(Interface, TempList);
            }

            ThreadBusy = false;
            return rtn;
        }

        // ReadWriteOption만 보고 쭉쭉 뽑아냄. 정해진 데이터 기준으로 DataStore write/read만 함.
        private bool Slave_GetReadWriteCommunicationData(List<Define_Modbus.CommunicationData> CommunicationData, Define_Modbus.ReadWriteOption ReadWriteOption, out List<Define_Modbus.CommunicationData> CommunicationDataList)
        {
            CommunicationDataList = new List<Define_Modbus.CommunicationData>();
            if (CommunicationData.Count == 0) return false;

            var CopyMasterCommunicationData = CommunicationData.OrderBy(x => x.ReadWriteOption).ThenBy(x => x.DataStorage).ThenBy(x => x.StartAddress).ToList();

            for (int DataIndex = 0; DataIndex < CopyMasterCommunicationData.Count; DataIndex++)
            {
                if (CopyMasterCommunicationData[DataIndex].ReadWriteOption == ReadWriteOption)
                {
                    CommunicationDataList.Add(CopyMasterCommunicationData[DataIndex]);

                    CommunicationData.Remove(CopyMasterCommunicationData[DataIndex]);
                }
            }

            if (CommunicationDataList.Count > 0) return true; else return false;
        }

        /// <summary>
        /// Clear all data
        /// </summary>
        public void ClearDataStore()
        {
            ThreadBusy = true;
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
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus ClearRegister Failed - {0}", e.ToString()));
            }
            ThreadBusy = false;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Slave_WriteData"]'/>
        private void Slave_WriteData(Define_Modbus.CommunicationData CommData)
        {
            dynamic InputValue = null;

            switch (CommData.DataStorage)
            {
                case Define_Modbus.DataStorage.Coil:
                    InputValue = CommData.Data.First() == 1 ? true : false;
                    ModbusInstance.DataStore.CoilDiscretes[CommData.StartAddress] = InputValue;
                    break;

                case Define_Modbus.DataStorage.DiscreteInput:
                    InputValue = CommData.Data.First() == 1 ? true : false;
                    ModbusInstance.DataStore.InputDiscretes[CommData.StartAddress] = InputValue;
                    break;

                case Define_Modbus.DataStorage.InputRegister:
                    switch (CommData.DataType)
                    {
                        case Define_Modbus.DataType.Bool:
                        case Define_Modbus.DataType.Int:
                            InputValue = CommData.Data.First();
                            ModbusInstance.DataStore.InputRegisters[CommData.StartAddress] = InputValue;
                            break;

                        case Define_Modbus.DataType.Float:
                            for (int index = 0; index < CommData.Data.Count; index++) ModbusInstance.DataStore.InputRegisters[CommData.StartAddress + index] = CommData.Data[index];
                            break;
                    }
                    break;

                case Define_Modbus.DataStorage.HoldingRegister:
                    switch (CommData.DataType)
                    {
                        case Define_Modbus.DataType.Bool:
                        case Define_Modbus.DataType.Int:
                            InputValue = CommData.Data.First();
                            ModbusInstance.DataStore.HoldingRegisters[CommData.StartAddress] = InputValue;
                            break;

                        case Define_Modbus.DataType.Float:
                            for (int index = 0; index < CommData.Data.Count; index++) ModbusInstance.DataStore.HoldingRegisters[CommData.StartAddress + index] = CommData.Data[index];
                            break;
                    }
                    break;
            }
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Slave_ReadDataSingle"]'/>
        private bool Slave_ReadData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, out T Data)
        {
            bool rtn = false;
            Data = default(T);
            try
            {
                dynamic ReadData = null;

                switch (DataStorage)
                {
                    case Define_Modbus.DataStorage.Coil:
                        ReadData = new ushort[1] { ModbusInstance.DataStore.CoilDiscretes[StartAddress] == true ? (ushort)1 : (ushort)0 };
                        break;

                    case Define_Modbus.DataStorage.DiscreteInput:
                        ReadData = new ushort[1] { ModbusInstance.DataStore.InputDiscretes[StartAddress] == true ? (ushort)1 : (ushort)0 };
                        break;

                    case Define_Modbus.DataStorage.InputRegister:
                        if (typeof(T) != typeof(float)) ReadData = new ushort[1] { ModbusInstance.DataStore.InputRegisters[StartAddress] };
                        else ReadData = new ushort[2] { ModbusInstance.DataStore.InputRegisters[StartAddress], ModbusInstance.DataStore.InputRegisters[StartAddress + 1] };
                        break;

                    case Define_Modbus.DataStorage.HoldingRegister:
                        if (typeof(T) != typeof(float)) ReadData = new ushort[1] { ModbusInstance.DataStore.HoldingRegisters[StartAddress] };
                        else ReadData = new ushort[2] { ModbusInstance.DataStore.HoldingRegisters[StartAddress], ModbusInstance.DataStore.HoldingRegisters[StartAddress + 1] };
                        break;
                }
                Data = Define_Modbus.FromUShortHexData<T>(ReadData, Interface.EndianOption);
                return true;
            }
            catch (Exception e)
            {
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Slave_ReadData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Slave_ReadDataMulti"]'/>
        private bool Slave_ReadData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, int ReadCount, out List<T> Data)
        {
            bool rtn = false;
            Data = new List<T>();
            try
            {
                List<ushort> ReadData = null;
                DataStore DataStore = ModbusInstance.DataStore;     //DataStore 받아서 할당 해놓지 않으면 Linq function 사용 못함..

                switch (DataStorage)
                {
                    case Define_Modbus.DataStorage.Coil:
                        bool[] CoilArr = DataStore.CoilDiscretes.Skip(StartAddress).Take(ReadCount).ToArray();
                        ReadData = Array.ConvertAll(CoilArr, x => (x == true) ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case Define_Modbus.DataStorage.DiscreteInput:
                        bool[] DiscreteArr = DataStore.InputDiscretes.Skip(StartAddress).Take(ReadCount).ToArray();
                        ReadData = Array.ConvertAll(DiscreteArr, x => (x == true) ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case Define_Modbus.DataStorage.InputRegister:
                        if (typeof(T) != typeof(float)) ReadData = DataStore.InputRegisters.Skip(StartAddress).Take(ReadCount).ToList();
                        else ReadData = DataStore.InputRegisters.Skip(StartAddress).Take(ReadCount * 2).ToList();
                        break;

                    case Define_Modbus.DataStorage.HoldingRegister:
                        if (typeof(T) != typeof(float)) ReadData = DataStore.HoldingRegisters.Skip(StartAddress).Take(ReadCount).ToList();
                        else ReadData = DataStore.HoldingRegisters.Skip(StartAddress).Take(ReadCount * 2).ToList();
                        break;
                }

                for (int index = 0; index < ReadCount; index++)
                {
                    if (typeof(T) != typeof(float))
                    {
                        ushort[] InputData = new ushort[1] { ReadData[0] };
                        var convert = Define_Modbus.FromUShortHexData<T>(InputData, Interface.EndianOption);
                        Data.Add(convert);
                        ReadData.RemoveAt(0);
                    }
                    else
                    {
                        ushort[] InputData = new ushort[2] { ReadData[0], ReadData[1] };
                        var convert = Define_Modbus.FromUShortHexData<T>(InputData, Interface.EndianOption);
                        Data.Add(convert);
                        ReadData.RemoveRange(0, 2);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Slave_ReadData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Slave_RequestReceived"]'/>
        // Slave data request entry point
        private void Slave_RequestReceived(object sender, ModbusSlaveRequestEventArgs e)
        {
            ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Communication, string.Format("Slave {0} received request - Func code : {1}, Contents : {2}", e.Message.SlaveAddress.ToString(), e.Message.FunctionCode.ToString("X2"), ByteToHex(e.Message.ProtocolDataUnit)));
        }

        /// <summary>
        /// Master가 읽어감<br/>통신 address index 0 == DataStore index 1
        /// </summary>
        private void Slave_DataStoreReadFrom(object sender, DataStoreEventArgs e)
        {
            string DataLogString = string.Empty;
            var HexString = string.Empty;
            if (e.ModbusDataType == ModbusDataType.HoldingRegister || e.ModbusDataType == ModbusDataType.InputRegister)
            {
                var Contains = e.Data.B.ToList();   //B가 실질 데이터

                foreach (var data in Contains) HexString += data.ToString("X2") + ",";
                DataLogString = String.Join(", ", Contains);
            }
            else if (e.ModbusDataType == ModbusDataType.Coil || e.ModbusDataType == ModbusDataType.Input)
            {
                var Contains = e.Data.A.ToList();   //A가 실질 데이터

                foreach (var data in Contains) HexString += (data ? 1 : 0).ToString("X2") + ",";
                DataLogString = String.Join(", ", Contains);
            }
            ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Communication, string.Format("Data readed - Type : {0}, Start Address : {1}, Ushort : {2}, Hex : {3}", e.ModbusDataType.ToString(), (e.StartAddress + 1).ToString(), DataLogString, HexString));
        }

        /// <summary>
        /// Master가 씀<br/>통신 address index 0 == DataStore index 1
        /// </summary>
        private void Slave_DataStoreWrittenTo(object sender, DataStoreEventArgs e)
        {
            string DataLogString = string.Empty;
            var HexString = string.Empty;
            if (e.ModbusDataType == ModbusDataType.HoldingRegister)
            {
                var AddressList = new List<int>();
                var Contains = e.Data.B.ToList();   //B가 실질 데이터
                for (int i = (int)e.StartAddress; i < (int)e.StartAddress + Contains.Count; i++) AddressList.Add(i + 1);

                foreach (var data in Contains) HexString += data.ToString("X2") + ",";
                DataLogString = String.Join(", ", Contains);

                ModbusDataReceived?.Invoke(Interface, Define_Modbus.DataStorage.HoldingRegister, AddressList, Contains);
            }
            else if (e.ModbusDataType == ModbusDataType.Coil)
            {
                var AddressList = new List<int>();
                var Contains = e.Data.A.ToList();   //A가 실질 데이터
                for (int i = (int)e.StartAddress; i < (int)e.StartAddress + Contains.Count; i++) AddressList.Add(i + 1);

                foreach (var data in Contains) HexString += (data ? 1 : 0).ToString("X2") + ",";
                DataLogString = String.Join(", ", Contains);

                ModbusDataReceived?.Invoke(Interface, Define_Modbus.DataStorage.Coil, AddressList, Array.ConvertAll(Contains.ToArray(), x => (x == true) ? (ushort)1 : (ushort)0).ToList());
            }
            ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Communication, string.Format("Data written - Type : {0}, Start Address : {1}, Ushort : {2}, Hex : {3}", e.ModbusDataType.ToString(), (e.StartAddress + 1).ToString(), DataLogString, HexString));
        }
    }
}