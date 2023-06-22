using NModbus4.Wrapper.Util.Converter;
using System;
using System.Collections.Generic;

namespace NModbus4.Wrapper.Define
{
    public class CommunicationData
    {
        public DataStorage DataStorage;
        public DataType DataType;
        public Endian RemoteEndian;
        private ushort _StartAddress;

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_CommunicationData/Doc[@name="StartAddress"]'/>
        public ushort StartAddress
        {
            get => _StartAddress;
            set
            {
                if (value < 0) value = 0;
                _StartAddress = (ushort)(value + 1);
            }
        }

        public object Value { get; set; }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Define_CommunicationData/Doc[@name="CommunicationData"]'/>
        public CommunicationData(DataStorage dataStorage, ushort startAddress, object value, Endian remoteEndian = Endian.Big)
        {
            DataStorage = dataStorage;
            StartAddress = startAddress;
            Value = value;
            RemoteEndian = remoteEndian;
            switch (value)
            {
                case bool _:
                    DataType = DataType.Bool;
                    break;

                case int _:
                    DataType = DataType.Int;
                    break;

                case float _:
                case double _:      // Convert to floating point
                    DataType = DataType.Float;
                    break;
            }
        }

        public List<ushort> GetSendData()
        {
            List<ushort> datas;
            if (DataType == DataType.Float) datas = Converter.ToUshortHexData((float)Convert.ChangeType(Value, typeof(float)), RemoteEndian);
            else if (DataType == DataType.Int) datas = Converter.ToUshortHexData((int)Value, RemoteEndian);
            else datas = Converter.ToUshortHexData((bool)Value, RemoteEndian);
            return datas;
        }
    }
}