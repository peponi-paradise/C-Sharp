using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NModbus4.Wrapper
{
    public partial class ModbusService
    {
        /// <summary>
        /// " address >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib dataStorage is start from 1
        /// </summary>
        /// <typeparam name="T">bool, int, float</typeparam>
        /// <param name="dataStorage">Where to read</param>
        /// <param name="address">address >= 0<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="data">Read data</param>
        /// <returns>Success or fail</returns>
        public bool ReadData<T>(DataStorage dataStorage, int address, out T data)
        {
            bool rtn = true;
            data = default;
            try
            {
                //Master
                if ((int)Interface.ModbusType < 10) rtn = Master_ReadData(dataStorage, address, out data);
                //Slave
                else rtn = Slave_ReadData(dataStorage, address + 1, out data);      // Add 1 manually when direct input type
            }
            catch (Exception e)
            {
                rtn = false;
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        /// <summary>
        /// " startAddress >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1<br/>
        /// Read continuous data for every time calling function
        /// </summary>
        /// <typeparam name="T">bool, int, float</typeparam>
        /// <param name="dataStorage">Where to read</param>
        /// <param name="startAddress">startAddress >= 0<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="dataCount">Number of datas</param>
        /// <param name="datas">Datas</param>
        /// <returns>Success or fail</returns>
        public bool ReadData<T>(DataStorage dataStorage, int startAddress, int dataCount, out List<T> datas)
        {
            bool rtn = true;
            datas = default;
            try
            {
                //Master
                if ((int)Interface.ModbusType < 10) rtn = Master_ReadData(dataStorage, startAddress, dataCount, out datas);
                //Slave
                else rtn = Slave_ReadData(dataStorage, startAddress + 1, dataCount, out datas);     // Add 1 manually when direct input type
            }
            catch (Exception e)
            {
                rtn = false;
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        /// <summary>
        /// Read data using built in type
        /// </summary>
        /// <param name="communicationData"></param>
        /// <returns>Success or fail</returns>
        public bool ReadData(ref CommunicationData communicationData)
        {
            bool rtn = true;
            try
            {
                //Master
                if ((int)Interface.ModbusType < 10)
                {
                    if (communicationData.DataType == DataType.Float) { rtn = Master_ReadData(communicationData.DataStorage, communicationData.StartAddress - 1, out float value); communicationData.Value = value; }       // Subtract 1 manually when built in input type
                    else if (communicationData.DataType == DataType.Int) { rtn = Master_ReadData(communicationData.DataStorage, communicationData.StartAddress - 1, out int value); communicationData.Value = value; }
                    else { rtn = Master_ReadData(communicationData.DataStorage, communicationData.StartAddress - 1, out bool value); communicationData.Value = value; }
                }
                //Slave
                else
                {
                    if (communicationData.DataType == DataType.Float) { rtn = Slave_ReadData(communicationData.DataStorage, communicationData.StartAddress, out float value); communicationData.Value = value; }
                    else if (communicationData.DataType == DataType.Int) { rtn = Slave_ReadData(communicationData.DataStorage, communicationData.StartAddress, out int value); communicationData.Value = value; }
                    else { rtn = Slave_ReadData(communicationData.DataStorage, communicationData.StartAddress, out bool value); communicationData.Value = value; }
                }
            }
            catch (Exception e)
            {
                rtn = false;
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
            }

            return rtn;
        }

        /// <summary>
        /// Read data using built in type<br/>Could get non continuous data
        /// </summary>
        /// <param name="communicationDatas"></param>
        /// <returns>Success or fail</returns>
        public bool ReadData(ref List<CommunicationData> communicationDatas)
        {
            bool rtn = true;
            try
            {
                //Master
                if ((int)Interface.ModbusType < 10)
                {
                    List<CommunicationData> tempDatas = communicationDatas.ToList();
                    List<CommunicationData> rtnDatas = new List<CommunicationData>();
                    while (true)
                    {
                        if (Master_GetReadInformation(tempDatas, out var dataStorage, out var dataType, out var startAddress, out var readCount))
                        {
                            var address = startAddress - 1;      // Subtract 1 manually when built in input type
                            switch (dataType)
                            {
                                case DataType.Float:
                                    if (Master_ReadData<float>(dataStorage, address, readCount, out var floatDatas))
                                    {
                                        for (int index = 0; index < floatDatas.Count; index++) rtnDatas.Add(new CommunicationData(dataStorage, (ushort)(address + index * 2), floatDatas[index], Interface.EndianOption));
                                        break;
                                    }
                                    else return false;
                                case DataType.Int:
                                    if (Master_ReadData<int>(dataStorage, address, readCount, out var intDatas))
                                    {
                                        for (int index = 0; index < intDatas.Count; index++) rtnDatas.Add(new CommunicationData(dataStorage, (ushort)(address + index), intDatas[index], Interface.EndianOption));
                                        break;
                                    }
                                    else return false;
                                case DataType.Bool:
                                    if (Master_ReadData<bool>(dataStorage, address, readCount, out var boolDatas))
                                    {
                                        for (int index = 0; index < boolDatas.Count; index++) rtnDatas.Add(new CommunicationData(dataStorage, (ushort)(address + index), boolDatas[index], Interface.EndianOption));
                                        break;
                                    }
                                    else return false;
                            }
                        }
                        else break;
                    }
                    communicationDatas = rtnDatas.ToList();
                }
                //Slave
                else
                {
                    for (int index = 0; index < communicationDatas.Count; index++) { var temp = communicationDatas[index]; ReadData(ref temp); communicationDatas[index] = temp; }
                }
            }
            catch (Exception e)
            {
                rtn = false;
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus ReadData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        /// <summary>
        /// " address >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib dataStorage is start from 1
        /// </summary>
        /// <typeparam name="T">bool, int, float</typeparam>
        /// <param name="dataStorage">Where to read</param>
        /// <param name="address">address >= 0<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <returns>Success of fail with Data</returns>
        public Task<(bool IsSuccess, T Data)> ReadDataAsync<T>(DataStorage dataStorage, int address) => Task.Run(() => { var rtn = ReadData(dataStorage, address, out T data); return (rtn, data); });

        /// <summary>
        /// " startAddress >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1<br/>
        /// Read continuous data for every time calling function
        /// </summary>
        /// <typeparam name="T">bool, int, float</typeparam>
        /// <param name="dataStorage">Where to read</param>
        /// <param name="startAddress">startAddress >= 0<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="dataCount">Number of datas</param>
        /// <returns>Success of fail with Datas</returns>
        public Task<(bool IsSuccess, List<T> Datas)> ReadDataAsync<T>(DataStorage dataStorage, int startAddress, int dataCount) => Task.Run(() => { var rtn = ReadData(dataStorage, startAddress, dataCount, out List<T> datas); return (rtn, datas); });

        /// <summary>
        /// Read data using built in type
        /// </summary>
        /// <param name="communicationData"></param>
        /// <returns>Success or fail with Data</returns>
        public Task<(bool IsSuccess, CommunicationData CommunicationData)> ReadDataAsync(CommunicationData communicationData) => Task.Run(() => { var rtn = ReadData(ref communicationData); return (rtn, communicationData); });

        /// <summary>
        /// Read data using built in type<br/>Could get non continuous data
        /// </summary>
        /// <param name="communicationDatas"></param>
        /// <returns>Success or fail with Datas</returns>
        public Task<(bool IsSuccess, List<CommunicationData> CommunicationDatas)> ReadDataAsync(List<CommunicationData> communicationDatas) => Task.Run(() => { var rtn = ReadData(ref communicationDatas); return (rtn, communicationDatas); });
    }
}