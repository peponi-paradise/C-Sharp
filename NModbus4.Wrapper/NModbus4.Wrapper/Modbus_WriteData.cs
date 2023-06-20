using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NModbus4.Wrapper
{
    public partial class Modbus
    {
        /// <summary>
        /// " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1
        /// </summary>
        /// <param name="DataStorage">Where to write</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="Data">Any type</param>
        /// <param name="DataType">bool, int, float.</param>
        public bool WriteData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, T Data, Define_Modbus.DataType DataType = Define_Modbus.DataType.Float)
        {
            bool rtn = true;
            lock (ThreadLocker)
            {
                try
                {
                    var CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)StartAddress, Data, Interface.EndianOption, DataType, Define_Modbus.ReadWriteOption.Write);
                    AddorUpdateCommunicationData(CommData);
                    if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Immediate)
                    {
                        //Master
                        if ((int)Interface.ModbusType < 10) rtn = Master_WriteProcess();
                        else rtn = Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption.Write);
                    }
                }
                catch (Exception e)
                {
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        /// <summary>
        /// " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1<br/>
        /// Input Continuous data for every time calling function
        /// </summary>
        /// <param name="DataStorage">Where to write</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="Data">Any type of list</param>
        /// <param name="DataType">bool, int, float.</param>
        public bool WriteData<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, List<T> Data, Define_Modbus.DataType DataType = Define_Modbus.DataType.Float)
        {
            bool rtn = true;
            lock (ThreadLocker)
            {
                try
                {
                    List<Define_Modbus.CommunicationData> CommDataList = new List<Define_Modbus.CommunicationData>();
                    for (int index = 0; index < Data.Count; index++)
                    {
                        if (DataType == Define_Modbus.DataType.Float)
                        {
                            var CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + CommDataList.Count * (int)DataType / 2), Data[index], Interface.EndianOption, DataType);
                            CommDataList.Add(CommData);
                        }
                        else
                        {
                            var CommData = new Define_Modbus.CommunicationData(DataStorage, (ushort)(StartAddress + CommDataList.Count), Data[index], Interface.EndianOption, DataType);
                            CommDataList.Add(CommData);
                        }
                    }
                    foreach (var CommData in CommDataList) AddorUpdateCommunicationData(CommData);

                    if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Immediate)
                    {
                        //Master
                        if ((int)Interface.ModbusType < 10) rtn = Master_WriteProcess();
                        else rtn = Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption.Write);
                    }
                }
                catch (Exception e)
                {
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        public bool WriteData(Define_Modbus.CommunicationData CommunicationData)
        {
            bool rtn = true;
            lock (ThreadLocker)
            {
                try
                {
                    AddorUpdateCommunicationData(CommunicationData);
                    if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Immediate)
                    {
                        //Master
                        if ((int)Interface.ModbusType < 10) rtn = Master_WriteProcess();
                        else rtn = Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption.Write);
                    }
                }
                catch (Exception e)
                {
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        public bool WriteData(List<Define_Modbus.CommunicationData> CommunicationDataList)
        {
            bool rtn = true;
            lock (ThreadLocker)
            {
                try
                {
                    foreach (var CommData in CommunicationDataList) AddorUpdateCommunicationData(CommData);
                    if (Interface.WriteUpdateOption == Define_Modbus.UpdateOption.Immediate)
                    {
                        //Master
                        if ((int)Interface.ModbusType < 10) rtn = Master_WriteProcess();
                        else rtn = Slave_ReadWriteProcess(Define_Modbus.ReadWriteOption.Write);
                    }
                }
                catch (Exception e)
                {
                    ModbusLog?.Invoke(Interface, Define_Modbus.LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
                }
            }
            return rtn;
        }

        /// <summary>
        ///  " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1, Async function for Immediate master mode
        /// </summary>
        /// <param name="DataStorage">Where to write</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="Data">Any type</param>
        /// <param name="DataType">bool, int, float.</param>
        public Task<bool> WriteDataAsync<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, T Data, Define_Modbus.DataType DataType = Define_Modbus.DataType.Float)
        {
            return Task.Run(() => WriteData(DataStorage, StartAddress, Data, DataType));
        }

        /// <summary>
        ///  " StartAddress > 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1, Async function for Immediate master mode<br/>
        /// Input Continuous data for every time calling function
        /// </summary>
        /// <param name="DataStorage">Where to write</param>
        /// <param name="StartAddress">Start from 1.<br/><br/>- Register address of index 1<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="Data">Any type of list</param>
        /// <param name="DataType">bool, int, float.</param>
        public Task<bool> WriteDataAsync<T>(Define_Modbus.DataStorage DataStorage, int StartAddress, List<T> Data, Define_Modbus.DataType DataType = Define_Modbus.DataType.Float)
        {
            return Task.Run(() => WriteData(DataStorage, StartAddress, Data, DataType));
        }

        public Task<bool> WriteDataAsync(Define_Modbus.CommunicationData CommunicationData)
        {
            return Task.Run(() => WriteData(CommunicationData));
        }

        public Task<bool> WriteDataAsync(List<Define_Modbus.CommunicationData> CommunicationDataList)
        {
            return Task.Run(() => WriteData(CommunicationDataList));
        }
    }
}