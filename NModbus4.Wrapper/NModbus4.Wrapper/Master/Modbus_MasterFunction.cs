using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NModbus4.Wrapper
{
    public partial class Modbus
    {
        /// <summary>
        /// Write data entry point<br/>
        /// Write all datas in CommunicationData
        /// </summary>
        /// <returns>Success or fail<br/>If there is no data to send, return true</returns>
        private bool Master_WriteProcess()
        {
            bool rtn = true;
            ThreadBusy = true;
            while (true)
            {
                if (Master_GetWriteCommunicationData(CommunicationData, out var DataStorage, out var StartAddress, out var SendDataArray) == true)
                {
                    if (Master_WriteData(DataStorage, StartAddress, SendDataArray) == false) rtn = false;
                }
                else break;
            }
            ThreadBusy = false;
            return rtn;
        }

        /// <summary>
        /// Read data entry point<br/>
        /// Read all datas in CommunicationData
        /// </summary>
        /// <returns>Success or fail<br/>If there is no data to read, return true</returns>
        private bool Master_ReadProcess()
        {
            bool rtn = true;
            ThreadBusy = true;

            List<Define_Modbus.CommunicationData> TempList = new List<Define_Modbus.CommunicationData>();
            while (true)
            {
                if (Master_GetReadCommunicationData(CommunicationData, out var DataStorage, out var DataType, out var StartAddress, out var ReadCount) == true)
                {
                    if (DataType == Define_Modbus.DataType.Float)
                    {
                        if (Master_ReadData<float>(DataStorage, StartAddress, ReadCount, out var DataList) == true)
                        {
                            for (int Index = 0; Index < DataList.Count; Index++) TempList.Add(new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + Index * 2), DataList[Index], Interface.EndianOption, DataType, Define_Modbus.ReadWriteOption.Read));
                        }
                        else { rtn = false; break; }
                    }
                    else if (DataType == Define_Modbus.DataType.Int)
                    {
                        if (Master_ReadData<int>(DataStorage, StartAddress, ReadCount, out var DataList) == true)
                        {
                            for (int Index = 0; Index < DataList.Count; Index++) TempList.Add(new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + Index), DataList[Index], Interface.EndianOption, DataType, Define_Modbus.ReadWriteOption.Read));
                        }
                        else { rtn = false; break; }
                    }
                    else
                    {
                        if (Master_ReadData<bool>(DataStorage, StartAddress, ReadCount, out var DataList) == true)
                        {
                            for (int Index = 0; Index < DataList.Count; Index++) TempList.Add(new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + Index), DataList[Index], Interface.EndianOption, DataType, Define_Modbus.ReadWriteOption.Read));
                        }
                        else { rtn = false; break; }
                    }
                }
                else break;
            }

            if (TempList.Count > 0)
            {
                foreach (var TempData in TempList) CommunicationData.Add(TempData);
                ModbusReadData?.Invoke(Interface, TempList);
            }

            ThreadBusy = false;
            return rtn;
        }

        // Write는 정해진 데이터 보냄. 보냄과 동시에 삭제. 데이터형 신경 안쓰고 연속된 데이터 쭉쭉 뽑아내는게 통신 양 줄이는데 유리
        private bool Master_GetWriteCommunicationData(List<Define_Modbus.CommunicationData> CommunicationDataList, out Define_Modbus.DataStorage DataStorage, out ushort StartAddress, out ushort[] SendDataArray)
        {
            StartAddress = 59999;
            SendDataArray = null;
            DataStorage = Define_Modbus.DataStorage.HoldingRegister;
            if (CommunicationDataList.Count == 0) return false;
            List<ushort> Datas = new List<ushort>();

            var CopyMasterCommunicationData = CommunicationDataList.OrderBy(x => x.ReadWriteOption).ThenBy(x => x.DataStorage).ThenBy(x => x.StartAddress).ToList();

            for (int DataIndex = 0; DataIndex < CopyMasterCommunicationData.Count; DataIndex++)
            {
                if (CopyMasterCommunicationData[DataIndex].ReadWriteOption == Define_Modbus.ReadWriteOption.Write)
                {
                    if (StartAddress == 59999)
                    {
                        DataStorage = CopyMasterCommunicationData[DataIndex].DataStorage;
                        StartAddress = CopyMasterCommunicationData[DataIndex].StartAddress;
                        foreach (var data in CopyMasterCommunicationData[DataIndex].Data) Datas.Add(data);

                        CommunicationDataList.Remove(CopyMasterCommunicationData[DataIndex]);
                    }
                    else
                    {
                        if (CopyMasterCommunicationData[DataIndex].DataStorage != DataStorage ||
                            CopyMasterCommunicationData[DataIndex].StartAddress != StartAddress + Datas.Count) break;
                        if (CopyMasterCommunicationData[DataIndex].DataLength >= 2) { if (Datas.Count >= Define_Modbus.TransactionLimit - 1) break; }     //float data 대비, -1 해줌
                        else if (Datas.Count >= Define_Modbus.TransactionLimit) break;

                        foreach (var data in CopyMasterCommunicationData[DataIndex].Data) Datas.Add(data);

                        CommunicationDataList.Remove(CopyMasterCommunicationData[DataIndex]);
                    }
                }
            }

            SendDataArray = Datas.ToArray();
            if (SendDataArray.Length != 0) return true; else return false;
        }

        // Read는 데이터형도 구분해서 봐야함. 삭제 후 복원. 통신 데이터 길이가 다름
        private bool Master_GetReadCommunicationData(List<Define_Modbus.CommunicationData> CommunicationDataList, out Define_Modbus.DataStorage DataStorage, out Define_Modbus.DataType DataType, out int StartAddress, out int ReadCount)
        {
            StartAddress = 59999;
            DataStorage = Define_Modbus.DataStorage.HoldingRegister;
            DataType = Define_Modbus.DataType.Float;
            ReadCount = 0;
            if (CommunicationDataList.Count == 0) return false;
            List<ushort> Datas = new List<ushort>();

            var CopyMasterCommunicationData = CommunicationDataList.OrderBy(x => x.ReadWriteOption).ThenBy(x => x.DataStorage).ThenBy(x => x.DataType).ThenBy(x => x.StartAddress).ToList();

            for (int DataIndex = 0; DataIndex < CopyMasterCommunicationData.Count; DataIndex++)
            {
                if (CopyMasterCommunicationData[DataIndex].ReadWriteOption == Define_Modbus.ReadWriteOption.Read)
                {
                    if (StartAddress == 59999)
                    {
                        DataStorage = CopyMasterCommunicationData[DataIndex].DataStorage;
                        DataType = CopyMasterCommunicationData[DataIndex].DataType;
                        StartAddress = CopyMasterCommunicationData[DataIndex].StartAddress;
                        foreach (var data in CopyMasterCommunicationData[DataIndex].Data) Datas.Add(data);

                        CommunicationDataList.Remove(CopyMasterCommunicationData[DataIndex]);
                    }
                    else
                    {
                        if (CopyMasterCommunicationData[DataIndex].DataStorage != DataStorage ||
                            CopyMasterCommunicationData[DataIndex].DataType != DataType ||
                            CopyMasterCommunicationData[DataIndex].StartAddress != StartAddress + Datas.Count) break;
                        if (CopyMasterCommunicationData[DataIndex].DataLength >= 2) { if (Datas.Count >= Define_Modbus.TransactionLimit - 1) break; }     //float data 대비, -1 해줌
                        else if (Datas.Count >= Define_Modbus.TransactionLimit) break;

                        foreach (var data in CopyMasterCommunicationData[DataIndex].Data) Datas.Add(data);

                        CommunicationDataList.Remove(CopyMasterCommunicationData[DataIndex]);
                    }
                }
            }

            if (DataType == Define_Modbus.DataType.Float) ReadCount = Datas.Count / 2; else ReadCount = Datas.Count;
            if (ReadCount != 0) return true; else return false;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Master_WriteData"]'/>
        private bool Master_WriteData(Define_Modbus.DataStorage DataStorage, ushort StartAddress, ushort[] SendDataArray)
        {
            if (ModbusInstance.Transport == null) return false;
            var HexString = string.Empty;
            try
            {
                switch (DataStorage)
                {
                    case Define_Modbus.DataStorage.Coil:
                        List<bool> ConvertList = new List<bool>();
                        foreach (var data in SendDataArray) ConvertList.Add(Convert.ToBoolean(data));
                        ModbusInstance.WriteMultipleCoils((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), ConvertList.ToArray());

                        foreach (var data in ConvertList) HexString += (data ? 1 : 0).ToString("X2") + ",";
                        ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Communication, string.Format("Modbus write data - Data storage : {0}, Start address : {1}, Ushort : {2}, Hex : {3}", DataStorage.ToString(), StartAddress, String.Join(", ", SendDataArray), HexString));
                        break;

                    case Define_Modbus.DataStorage.HoldingRegister:
                        ModbusInstance.WriteMultipleRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), SendDataArray);

                        foreach (var data in SendDataArray) HexString += data.ToString("X2") + ",";
                        ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Communication, string.Format("Modbus write data - Data storage : {0}, Start address : {1}, Ushort : {2}, Hex : {3}", DataStorage.ToString(), StartAddress, String.Join(", ", SendDataArray), HexString));
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                //전송 후 자동으로 응답 대기 하는데, 그 사이에 연결 끊으면 Null exception으로 빠짐. 이미 끊겨있는 연결에 대해 통신 시도하면 Invalid exception으로 빠짐
                if (e is NullReferenceException || e is InvalidOperationException)
                {
                    ConnectCallback?.Invoke((Interface, false));
                    ModbusCommunicationException?.Invoke(Interface, Define_Modbus.CommunicationException.MasterTransportNullException);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Master_WriteData Failed - {0}", e.ToString()));
                    return false;
                }
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, "Exception occured in Modbus Device - " + e.ToString());
            }
            return false;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Master_ReadDataSingle"]'/>
        private bool Master_ReadData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, out T Data)
        {
            Data = default(T);
            try
            {
                dynamic ReadData = null;

                switch (DataStorage)
                {
                    case Define_Modbus.DataStorage.Coil:
                        ReadData = ModbusInstance.ReadCoils((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), 1)[0] == true ? 1 : 0;
                        ReadData = new ushort[1] { (ushort)ReadData };
                        break;

                    case Define_Modbus.DataStorage.DiscreteInput:
                        ReadData = ModbusInstance.ReadInputs((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), 1)[0] == true ? 1 : 0;
                        ReadData = new ushort[1] { (ushort)ReadData };
                        break;

                    case Define_Modbus.DataStorage.InputRegister:
                        if (typeof(T) != typeof(float)) ReadData = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), 1)[0];
                        else ReadData = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), 2);
                        break;

                    case Define_Modbus.DataStorage.HoldingRegister:
                        if (typeof(T) != typeof(float)) ReadData = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), 1)[0];
                        else ReadData = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), 2);
                        break;
                }
                Data = Define_Modbus.FromUShortHexData<T>(ReadData, Interface.EndianOption);
                return true;
            }
            catch (Exception e)
            {
                //전송 후 자동으로 응답 대기 하는데, 그 사이에 연결 끊으면 Null exception으로 빠짐. 이미 끊겨있는 연결에 대해 통신 시도하면 Invalid exception으로 빠짐
                if (e is NullReferenceException || e is InvalidOperationException)
                {
                    ConnectCallback?.Invoke((Interface, false));
                    ModbusCommunicationException?.Invoke(Interface, Define_Modbus.CommunicationException.MasterTransportNullException);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Master_ReadData Failed - {0}", e.ToString()));
                    return false;
                }
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Master_ReadData Failed - {0}", e.ToString()));
            }
            return false;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="Master_ReadDataMulti"]'/>
        private bool Master_ReadData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, int ReadCount, out List<T> Data)
        {
            Data = new List<T>();
            try
            {
                List<ushort> ReadData = null;

                switch (DataStorage)
                {
                    case Define_Modbus.DataStorage.Coil:
                        bool[] CoillArray = ModbusInstance.ReadCoils((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), (ushort)ReadCount);
                        ReadData = Array.ConvertAll(CoillArray, x => (x == true) ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case Define_Modbus.DataStorage.DiscreteInput:
                        bool[] DiscreteArray = ModbusInstance.ReadInputs((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), (ushort)ReadCount);
                        ReadData = Array.ConvertAll(DiscreteArray, x => (x == true) ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case Define_Modbus.DataStorage.InputRegister:
                        ushort[] InputUshortData = null;
                        if (typeof(T) != typeof(float)) InputUshortData = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), (ushort)ReadCount);
                        else InputUshortData = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), (ushort)(ReadCount * 2));
                        ReadData = InputUshortData.ToList();
                        break;

                    case Define_Modbus.DataStorage.HoldingRegister:
                        ushort[] HoldingUshortData = null;
                        if (typeof(T) != typeof(float)) HoldingUshortData = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), (ushort)ReadCount);
                        else HoldingUshortData = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)(StartAddress - 1), (ushort)(ReadCount * 2));
                        ReadData = HoldingUshortData.ToList();
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
                //전송 후 자동으로 응답 대기 하는데, 그 사이에 연결 끊으면 Null exception으로 빠짐. 이미 끊겨있는 연결에 대해 통신 시도하면 Invalid exception으로 빠짐
                if (e is NullReferenceException || e is InvalidOperationException)
                {
                    ConnectCallback?.Invoke((Interface, false));
                    ModbusCommunicationException?.Invoke(Interface, Define_Modbus.CommunicationException.MasterTransportNullException);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Master_ReadData Failed - {0}", e.ToString()));
                    return false;
                }
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus Master_ReadData Failed - {0}", e.ToString()));
            }
            return false;
        }
    }
}