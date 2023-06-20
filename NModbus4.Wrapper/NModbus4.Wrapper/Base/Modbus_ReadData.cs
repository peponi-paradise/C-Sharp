using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NModbus4.Wrapper
{
    public partial class Modbus
    {
        /// <summary>
        /// " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1
        /// </summary>
        /// <typeparam name="T">bool, int, float</typeparam>
        /// <param name="DataStorage">Where to read</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="Data">Read data</param>
        /// <returns>Success or fail</returns>
        public bool ReadData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, out T Data)
        {
            bool rtn = true;
            Data = default(T);
            lock (ThreadLocker)
            {
                try
                {
                    //Master
                    if ((int)Interface.ModbusType < 10) rtn = Master_ReadData(DataStorage, StartAddress, out Data);
                    //Slave
                    else rtn = Slave_ReadData(DataStorage, StartAddress, out Data);

                    if (Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling)
                    {
                        dynamic CommData = null;
                        if (typeof(T) == typeof(float)) CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)StartAddress, Data, Interface.EndianOption, Define_Modbus.DataType.Float, Define_Modbus.ReadWriteOption.Read);
                        else if (typeof(T) == typeof(int)) CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)StartAddress, Data, Interface.EndianOption, Define_Modbus.DataType.Int, Define_Modbus.ReadWriteOption.Read);
                        else CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)StartAddress, Data, Interface.EndianOption, Define_Modbus.DataType.Bool, Define_Modbus.ReadWriteOption.Read);
                        AddorUpdateCommunicationData(CommData);
                    }
                }
                catch (Exception e)
                {
                    rtn = false;
                    ModbusGeneralException?.Invoke(Interface);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        /// <summary>
        /// " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1<br/>
        /// Input Continuous data for every time calling function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DataStorage"></param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="ReadCount">Means data count, not array length<br/>Array length will be auto converted</param>
        /// <param name="Data">Data list</param>
        /// <returns>Success or fail</returns>
        public bool ReadData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, int ReadCount, out List<T> Data)
        {
            bool rtn = true;
            Data = default(List<T>);
            lock (ThreadLocker)
            {
                try
                {
                    //Master
                    if ((int)Interface.ModbusType < 10) rtn = Master_ReadData(DataStorage, StartAddress, ReadCount, out Data);
                    //Slave
                    else rtn = Slave_ReadData(DataStorage, StartAddress, ReadCount, out Data);

                    if (Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling)
                    {
                        dynamic CommData = null;
                        for (int Index = 0; Index < ReadCount; Index++)
                        {
                            if (typeof(T) == typeof(float)) CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + (Index * 2)), Data[Index], Interface.EndianOption, Define_Modbus.DataType.Float, Define_Modbus.ReadWriteOption.Read);
                            else if (typeof(T) == typeof(int)) CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + Index), Data[Index], Interface.EndianOption, Define_Modbus.DataType.Int, Define_Modbus.ReadWriteOption.Read);
                            else CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + Index), Data[Index], Interface.EndianOption, Define_Modbus.DataType.Bool, Define_Modbus.ReadWriteOption.Read);
                            AddorUpdateCommunicationData(CommData);
                        }
                    }
                }
                catch (Exception e)
                {
                    rtn = false;
                    ModbusGeneralException?.Invoke(Interface);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        /// <summary>
        /// Slave의 경우 DataStore 데이터를 가져와 Value 업데이트 후 반환
        /// </summary>
        /// <param name="CommunicationData"></param>
        /// <returns>Success or fail</returns>
        public bool ReadData(ref Define_Modbus.CommunicationData CommunicationData)
        {
            bool rtn = true;
            lock (ThreadLocker)
            {
                try
                {
                    //Master
                    if ((int)Interface.ModbusType < 10)
                    {
                        if (CommunicationData.DataType == Define_Modbus.DataType.Float) { rtn = Master_ReadData(CommunicationData.DataStorage, CommunicationData.StartAddress, out float Value); CommunicationData.Value = Value; }
                        else if (CommunicationData.DataType == Define_Modbus.DataType.Int) { rtn = Master_ReadData(CommunicationData.DataStorage, CommunicationData.StartAddress, out int Value); CommunicationData.Value = Value; }
                        else { rtn = Master_ReadData(CommunicationData.DataStorage, CommunicationData.StartAddress, out bool Value); CommunicationData.Value = Value; }
                    }
                    //Slave
                    else
                    {
                        if (CommunicationData.DataType == Define_Modbus.DataType.Float) { rtn = Slave_ReadData(CommunicationData.DataStorage, CommunicationData.StartAddress, out float Value); CommunicationData.Value = Value; }
                        else if (CommunicationData.DataType == Define_Modbus.DataType.Int) { rtn = Slave_ReadData(CommunicationData.DataStorage, CommunicationData.StartAddress, out int Value); CommunicationData.Value = Value; }
                        else { rtn = Slave_ReadData(CommunicationData.DataStorage, CommunicationData.StartAddress, out bool Value); CommunicationData.Value = Value; }
                    }

                    if (Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling) AddorUpdateCommunicationData(CommunicationData);
                }
                catch (Exception e)
                {
                    rtn = false;
                    ModbusGeneralException?.Invoke(Interface);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        /// <summary>
        /// Slave의 경우 DataStore 데이터를 가져와 Value 업데이트 후 반환
        /// </summary>
        /// <param name="CommunicationDataList"></param>
        /// <returns>Success or fail</returns>
        public bool ReadData(ref List<Define_Modbus.CommunicationData> CommunicationDataList)
        {
            bool rtn = true;
            lock (ThreadLocker)
            {
                try
                {
                    //Master
                    if ((int)Interface.ModbusType < 10)
                    {
                        List<Define_Modbus.CommunicationData> TempList = new List<Define_Modbus.CommunicationData>();
                        while (true)
                        {
                            if (Master_GetReadCommunicationData(CommunicationDataList, out var DataStorage, out var DataType, out var StartAddress, out var ReadCount) == true)
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
                        CommunicationDataList = TempList.ToList();
                    }
                    //Slave
                    else
                    {
                        for (int Index = 0; Index < CommunicationDataList.Count; Index++) { var Temp = CommunicationDataList[Index]; ReadData(ref Temp); CommunicationDataList[Index] = Temp; }
                    }

                    if (Interface.ReadUpdateOption == Define_Modbus.UpdateOption.Polling)
                    {
                        foreach (var CommData in CommunicationDataList) AddorUpdateCommunicationData(CommData);
                    }
                }
                catch (Exception e)
                {
                    rtn = false;
                    ModbusGeneralException?.Invoke(Interface);
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        /// <summary>
        ///  " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1, Async function for master mode
        /// </summary>
        /// <param name="DataStorage">Where to write</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <returns>Success of fail with Data</returns>
        public Task<(bool IsSuccess, T Data)> ReadDataAsync<T>(Define_Modbus.DataStorage DataStorage, int StartAddress) => Task.Run(() => { var rtn = ReadData(DataStorage, StartAddress, out T Data); return (rtn, Data); });

        /// <summary>
        ///  " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1, Async function for master mode
        /// </summary>
        /// <param name="DataStorage">Where to write</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="ReadCount">Means data count, not array length<br/>Array length will be auto converted</param>
        /// <returns>Success of fail with DataList</returns>
        public Task<(bool IsSuccess, List<T> Data)> ReadDataAsync<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, int ReadCount) => Task.Run(() => { var rtn = ReadData(DataStorage, StartAddress, ReadCount, out List<T> Data); return (rtn, Data); });

        public Task<(bool IsSuccess, Define_Modbus.CommunicationData CommunicationData)> ReadDataAsync(Define_Modbus.CommunicationData CommunicationData) => Task.Run(() => { var rtn = ReadData(ref CommunicationData); return (rtn, CommunicationData); });

        public Task<(bool IsSuccess, List<Define_Modbus.CommunicationData> CommunicationDataList)> ReadDataAsync(List<Define_Modbus.CommunicationData> CommunicationDataList) => Task.Run(() => { var rtn = ReadData(ref CommunicationDataList); return (rtn, CommunicationDataList); });
    }
}