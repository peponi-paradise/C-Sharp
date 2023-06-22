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
        /// Write data entry point<br/>
        /// Write all datas in CommunicationData
        /// </summary>
        /// <returns>Success or fail<br/>If there is no data to send, return true</returns>
        private bool Master_WriteProcess(List<CommunicationData> communicationDatas)
        {
            bool rtn = true;
            List<CommunicationData> sendDatas = communicationDatas.ToList();
            while (true)
            {
                if (Master_GetWriteDatas(sendDatas, out var dataStorage, out var startAddress, out var writeDatas))
                {
                    if (!Master_WriteData(dataStorage, (ushort)(startAddress - 1), writeDatas)) { rtn = false; break; }      // Subtract 1 manually when built in input type
                }
                else break;
            }
            return rtn;
        }

        // Write는 데이터형 신경 안쓰고 연속된 데이터 쭉쭉 뽑아내는게 통신 양 줄이는데 유리
        private bool Master_GetWriteDatas(List<CommunicationData> communicationDatas, out DataStorage dataStorage, out ushort startAddress, out List<ushort> writeDatas)
        {
            startAddress = ushort.MaxValue;
            writeDatas = null;
            dataStorage = DataStorage.HoldingRegister;
            if (communicationDatas.Count == 0) return false;
            List<ushort> datas = new List<ushort>();

            var dataCopy = communicationDatas.OrderBy(x => x.DataStorage).ThenBy(x => x.StartAddress).ToList();

            for (int index = 0; index < dataCopy.Count; index++)
            {
                if (startAddress == ushort.MaxValue)
                {
                    dataStorage = dataCopy[index].DataStorage;
                    startAddress = dataCopy[index].StartAddress;
                }
                else
                {
                    if (dataCopy[index].DataStorage != dataStorage ||
                        dataCopy[index].StartAddress != startAddress + datas.Count) break;  // 스토리지 바뀌거나 어드레스 끊기면 다음번 전송으로 넘김
                    if (dataCopy[index].DataType >= DataType.Int) { if (datas.Count >= ModbusInterface.TransactionLimit - 1) break; }     //float data 대비, -1 해줌
                    else if (datas.Count >= ModbusInterface.TransactionLimit) break;
                }

                foreach (var data in dataCopy[index].GetSendData()) datas.Add(data);
                communicationDatas.Remove(dataCopy[index]);
            }

            writeDatas = datas.ToList();
            return writeDatas.Count > 0;
        }

        // Read는 데이터형도 구분해서 봐야함. 통신 데이터 길이가 다름
        private bool Master_GetReadInformation(List<CommunicationData> communicationDatas, out DataStorage dataStorage, out DataType dataType, out int startAddress, out int readCount)
        {
            startAddress = ushort.MaxValue;
            dataStorage = DataStorage.HoldingRegister;
            dataType = DataType.Float;
            readCount = 0;
            if (communicationDatas.Count == 0) return false;
            List<ushort> datas = new List<ushort>();

            var dataCopy = communicationDatas.OrderBy(x => x.DataStorage).ThenBy(x => x.DataType).ThenBy(x => x.StartAddress).ToList();

            for (int index = 0; index < dataCopy.Count; index++)
            {
                if (startAddress == ushort.MaxValue)
                {
                    dataStorage = dataCopy[index].DataStorage;
                    dataType = dataCopy[index].DataType;
                    startAddress = dataCopy[index].StartAddress;
                }
                else
                {
                    if (dataCopy[index].DataStorage != dataStorage ||
                        dataCopy[index].DataType != dataType ||
                        dataCopy[index].StartAddress != startAddress + datas.Count) break;
                    if (dataCopy[index].DataType >= DataType.Int) { if (datas.Count >= ModbusInterface.TransactionLimit - 1) break; }     //float data 대비, -1 해줌
                    else if (datas.Count >= ModbusInterface.TransactionLimit) break;
                }

                foreach (var data in dataCopy[index].GetSendData()) datas.Add(data);
                communicationDatas.Remove(dataCopy[index]);
            }

            readCount = dataType == DataType.Float ? readCount / 2 : readCount;
            return readCount != 0;
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_MasterFunction/Doc[@name="Master_WriteData"]'/>
        private bool Master_WriteData(DataStorage dataStorage, ushort startAddress, List<ushort> sendDatas)
        {
            if (ModbusInstance.Transport == null) return false;
            var hexString = string.Empty;
            try
            {
                switch (dataStorage)
                {
                    case DataStorage.Coil:
                        List<bool> convertList = new List<bool>();
                        foreach (var data in sendDatas) convertList.Add(Convert.ToBoolean(data));
                        ModbusInstance.WriteMultipleCoils((byte)Interface.SlaveNumber, startAddress, convertList.ToArray());
                        foreach (var data in convertList) hexString += (data ? 1 : 0).ToString("X2") + ",";
                        break;

                    case DataStorage.HoldingRegister:
                        ModbusInstance.WriteMultipleRegisters((byte)Interface.SlaveNumber, startAddress, sendDatas.ToArray());
                        foreach (var data in sendDatas) hexString += data.ToString("X2") + ",";
                        break;
                }
                ModbusLog?.Invoke(Interface, LogLevel.Communication, $"Modbus write data - Data storage : {dataStorage}, Start address : {startAddress}, Ushort : {String.Join(", ", sendDatas)}, Hex : {hexString}");
                return true;
            }
            catch (Exception e)
            {
                //전송 후 자동으로 응답 대기 하는데, 그 사이에 연결 끊으면 Null exception으로 빠짐. 이미 끊겨있는 연결에 대해 통신 시도하면 Invalid exception으로 빠짐
                if (e is NullReferenceException || e is InvalidOperationException)
                {
                    ConnectCallback?.Invoke(Interface, false);
                    ModbusCommunicationException?.Invoke(Interface, CommunicationException.MasterTransportNullException);
                    ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Master_WriteData Failed - {e}");
                    return false;
                }
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Exception occured in Modbus Device - {e}");
            }
            return false;
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_MasterFunction/Doc[@name="Master_ReadDataSingle"]'/>
        private bool Master_ReadData<T>(DataStorage dataStorage, int address, out T data)
        {
            data = default;
            try
            {
                dynamic readData = null;

                switch (dataStorage)
                {
                    case DataStorage.Coil:
                        var coilBool = ModbusInstance.ReadCoils((byte)Interface.SlaveNumber, (ushort)address, 1)[0] ? 1 : 0;
                        readData = new ushort[1] { (ushort)coilBool };
                        break;

                    case DataStorage.DiscreteInput:
                        var inputBool = ModbusInstance.ReadInputs((byte)Interface.SlaveNumber, (ushort)address, 1)[0] ? 1 : 0;
                        readData = new ushort[1] { (ushort)inputBool };
                        break;

                    case DataStorage.InputRegister:
                        if (typeof(T) != typeof(float)) readData = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)address, 1)[0];
                        else readData = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)address, 2);
                        break;

                    case DataStorage.HoldingRegister:
                        if (typeof(T) != typeof(float)) readData = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)address, 1)[0];
                        else readData = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)address, 2);
                        break;
                }
                data = Converter.FromUShortHexData<T>(readData, Interface.EndianOption);
                return true;
            }
            catch (Exception e)
            {
                //전송 후 자동으로 응답 대기 하는데, 그 사이에 연결 끊으면 Null exception으로 빠짐. 이미 끊겨있는 연결에 대해 통신 시도하면 Invalid exception으로 빠짐
                if (e is NullReferenceException || e is InvalidOperationException)
                {
                    ConnectCallback?.Invoke(Interface, false);
                    ModbusCommunicationException?.Invoke(Interface, CommunicationException.MasterTransportNullException);
                    ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Master_ReadData Failed - {e}");
                    return false;
                }
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Master_ReadData Failed - {e}");
            }
            return false;
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Modbus_MasterFunction/Doc[@name="Master_ReadDataMulti"]'/>
        private bool Master_ReadData<T>(DataStorage dataStorage, int startAddress, int readCount, out List<T> datas)
        {
            datas = new List<T>();
            try
            {
                List<ushort> readData = null;

                switch (dataStorage)
                {
                    case DataStorage.Coil:
                        bool[] coillArray = ModbusInstance.ReadCoils((byte)Interface.SlaveNumber, (ushort)startAddress, (ushort)readCount);
                        readData = Array.ConvertAll(coillArray, x => x ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case DataStorage.DiscreteInput:
                        bool[] discreteArray = ModbusInstance.ReadInputs((byte)Interface.SlaveNumber, (ushort)startAddress, (ushort)readCount);
                        readData = Array.ConvertAll(discreteArray, x => x ? (ushort)1 : (ushort)0).ToList();
                        break;

                    case DataStorage.InputRegister:
                        ushort[] inputUshorts = null;
                        if (typeof(T) != typeof(float)) inputUshorts = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)startAddress, (ushort)readCount);
                        else inputUshorts = ModbusInstance.ReadInputRegisters((byte)Interface.SlaveNumber, (ushort)startAddress, (ushort)(readCount * 2));
                        readData = inputUshorts.ToList();
                        break;

                    case DataStorage.HoldingRegister:
                        ushort[] holdingUshorts = null;
                        if (typeof(T) != typeof(float)) holdingUshorts = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)startAddress, (ushort)readCount);
                        else holdingUshorts = ModbusInstance.ReadHoldingRegisters((byte)Interface.SlaveNumber, (ushort)startAddress, (ushort)(readCount * 2));
                        readData = holdingUshorts.ToList();
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
                //전송 후 자동으로 응답 대기 하는데, 그 사이에 연결 끊으면 Null exception으로 빠짐. 이미 끊겨있는 연결에 대해 통신 시도하면 Invalid exception으로 빠짐
                if (e is NullReferenceException || e is InvalidOperationException)
                {
                    ConnectCallback?.Invoke(Interface, false);
                    ModbusCommunicationException?.Invoke(Interface, CommunicationException.MasterTransportNullException);
                    ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Master_ReadData Failed - {e}");
                    return false;
                }
                ModbusGeneralException?.Invoke(Interface);
                ModbusLog?.Invoke(Interface, LogLevel.Exception, $"Modbus Master_ReadData Failed - {e}");
                return false;
            }
        }
    }
}