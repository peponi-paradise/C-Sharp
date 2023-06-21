using NModbus4.Wrapper.Util.Converter;
using System;
using System.Collections.Generic;

namespace NModbus4.Wrapper.Define
{
    /// <include file='ClassSummary.xml' path='Docs/Define_CommunicationData/Doc[@name="CommunicationData"]'/>
    public class CommunicationData
    {
        public DataStorage DataStorage;
        public DataType DataType;
        public Endian ToEndian;
        private ushort _StartAddress;

        /// <include file='ClassSummary.xml' path='Docs/Define_CommunicationData/Doc[@name="StartAddress"]'/>
        public ushort StartAddress
        {
            get => _StartAddress;
            set
            {
                if (value < 1) value = 1;
                _StartAddress = value;
            }
        }

        public int DataLength;

        public object Value { get; set; }

        /// <include file='ClassSummary.xml' path='Docs/Define_CommunicationData/Doc[@name="CommunicationData"]'/>
        public CommunicationData(DataStorage dataStorage, ushort startAddress, object value, Endian toEndian = Endian.Big)
        {
            DataStorage = dataStorage;
            StartAddress = startAddress;
            Value = value;
            ToEndian = toEndian;
            switch (value)
            {
                case bool _:
                    DataLength = 1;
                    DataType = DataType.Bool;
                    break;

                case int _:
                    DataLength = 2;
                    DataType = DataType.Int;
                    break;

                case float _:
                case double _:      // Convert to floating point
                    DataLength = 4;
                    DataType = DataType.Float;
                    break;
            }
        }

        public List<ushort> GetSendData()
        {
            List<ushort> datas = new List<ushort>();
            if (DataType == DataType.Float) datas = Converter.ToUshortHexData((float)Convert.ChangeType(Value, typeof(float)), ToEndian);
            else if (DataType == DataType.Int) datas = Converter.ToUshortHexData((int)Value, ToEndian);
            else datas = Converter.ToUshortHexData((bool)Value, ToEndian);
            return datas;
        }
    }
}