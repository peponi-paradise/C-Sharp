using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NModbus4.Wrapper
{
    public partial class ModbusService
    {
        /// <summary>
        /// " address >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1
        /// </summary>
        /// <typeparam name="T">bool, int, float.</typeparam>
        /// <param name="dataStorage">Where to write</param>
        /// <param name="address">address >= 0<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="data">Data to send</param>
        public bool WriteData<T>(DataStorage dataStorage, int address, T data)
        {
            bool rtn = true;
            try
            {
                var commData = new CommunicationData(dataStorage, (ushort)address, data, Interface.EndianOption);
                // Master
                if ((int)Interface.ModbusType < 10) rtn = Master_WriteData(dataStorage, (ushort)address, commData.GetSendData());
                // Slave
                else Slave_WriteData(commData);
            }
            catch (Exception e)
            {
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        /// <summary>
        /// " startAddress >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1<br/>
        /// Write continuous data
        /// </summary>
        /// <typeparam name="T">bool, int, float.</typeparam>
        /// <param name="dataStorage">Where to write</param>
        /// <param name="startAddress">Start from 1.<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="datas">Datas to send</param>
        public bool WriteData<T>(DataStorage dataStorage, int startAddress, List<T> datas)
        {
            bool rtn = true;
            try
            {
                List<CommunicationData> commDatas = new List<CommunicationData>();
                for (int index = 0; index < datas.Count; index++)
                {
                    CommunicationData commData;
                    if (typeof(T) == typeof(float)) commData = new CommunicationData(dataStorage, (ushort)(startAddress + commDatas.Count * 2), datas[index], Interface.EndianOption);
                    else commData = new CommunicationData(dataStorage, (ushort)(startAddress + commDatas.Count), datas[index], Interface.EndianOption);
                    commDatas.Add(commData);
                }
                // Master
                if ((int)Interface.ModbusType < 10) rtn = Master_WriteProcess(commDatas);
                // Slave
                else foreach (var data in commDatas) Slave_WriteData(data);
            }
            catch (Exception e)
            {
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        public bool WriteData(CommunicationData communicationData)
        {
            bool rtn = true;
            try
            {
                // Master
                if ((int)Interface.ModbusType < 10) rtn = Master_WriteData(communicationData.DataStorage, (ushort)(communicationData.StartAddress - 1), communicationData.GetSendData());     // Subtract 1 manually when built in input type
                // Slave
                else Slave_WriteData(communicationData);
            }
            catch (Exception e)
            {
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
            }
            return rtn;
        }

        public bool WriteData(List<CommunicationData> communicationDatas)
        {
            bool rtn = true;

            try
            {
                // Master
                if ((int)Interface.ModbusType < 10) rtn = Master_WriteProcess(communicationDatas);
                // Slave
                else foreach (var data in communicationDatas) Slave_WriteData(data);
            }
            catch (Exception e)
            {
                ModbusLog?.Invoke(Interface, LogLevel.Exception, string.Format("Modbus WriteData Failed - {0}", e.ToString()));
            }

            return rtn;
        }

        /// <summary>
        /// " address >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1
        /// </summary>
        /// <typeparam name="T">bool, int, float.</typeparam>
        /// <param name="dataStorage">Where to write</param>
        /// <param name="address">address >= 0<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="data">Data to send</param>
        public Task<bool> WriteDataAsync<T>(DataStorage dataStorage, int address, T data) => Task.Run(() => WriteData(dataStorage, address, data));

        /// <summary>
        /// " startAddress >= 0 "<br/>
        /// C# is start from 0 but NModbus4 lib is start from 1<br/>
        /// Write continuous data
        /// </summary>
        /// <typeparam name="T">bool, int, float.</typeparam>
        /// <param name="dataStorage">Where to write</param>
        /// <param name="startAddress">Start from 1.<br/><br/>- Register address of dataStorage index 1 (address 0)<br/>Coil : 1<br/>Discrete Input : 10001<br/>Input Register : 30001<br/>Holding Register : 40001</param>
        /// <param name="datas">Datas to send</param>
        public Task<bool> WriteDataAsync<T>(DataStorage dataStorage, int startAddress, List<T> datas) => Task.Run(() => WriteData(dataStorage, startAddress, datas));

        public Task<bool> WriteDataAsync(CommunicationData communicationData) => Task.Run(() => WriteData(communicationData));

        public Task<bool> WriteDataAsync(List<CommunicationData> communicationDatas) => Task.Run(() => WriteData(communicationDatas));
    }
}