using NModbus4.Wrapper.Define;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NModbus4.Wrapper.Util.Converter
{
    public static class Converter
    {
        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="ToUshortHexData"]'/>
        public static List<ushort> ToUshortHexData<T>(T inputValue, Endian toEndian = Endian.Big)
        {
            bool isLittleEndian = BitConverter.IsLittleEndian;
            List<ushort> data = new List<ushort>();
            List<byte> b = new List<byte>();

            if (typeof(T) == typeof(bool)) b = BitConverter.GetBytes((bool)Convert.ChangeType(inputValue, typeof(T))).ToList();
            else if (typeof(T) == typeof(int)) b = BitConverter.GetBytes((ushort)(int)Convert.ChangeType(inputValue, typeof(T))).ToList();
            else b = BitConverter.GetBytes((float)Convert.ChangeType(inputValue, typeof(float))).ToList();

            var arr = b.ToArray();

            if (isLittleEndian && toEndian == Endian.Big) Array.Reverse(arr);
            else if (isLittleEndian && toEndian == Endian.Little) Array.Reverse(arr);

            if (typeof(T) == typeof(bool)) data.Add(arr[0]);
            else for (int index = 0; index < arr.Length / 2; index++) data.Add(Convert.ToUInt16(arr[index * 2].ToString("X2") + arr[index * 2 + 1].ToString("X2"), 16));

            return data;
        }

        /// <include file='ClassSummary.xml' path='Docs/Doc[@name="FromUShortHexData"]'/>
        public static T FromUShortHexData<T>(ushort[] Data, Endian FromEndian = Endian.Big)
        {
            if (Data.Length == 1) { if (Data[0] == 0 || Data[0] == 1) return (T)Convert.ChangeType(Data[0], typeof(T)); }//for bool

            bool IsLittleEndian = BitConverter.IsLittleEndian;
            List<byte> TotalBytes = new List<byte>();

            foreach (var data in Data)
            {
                var ByteArray = BitConverter.GetBytes(data);
                if (IsLittleEndian == true) Array.Reverse(ByteArray);
                foreach (var Byte in ByteArray) TotalBytes.Add(Byte);
            }

            var Arr = TotalBytes.ToArray();
            if (IsLittleEndian == true && FromEndian == Endian.Big) Array.Reverse(Arr);
            else if (IsLittleEndian == false && FromEndian == Endian.Little) Array.Reverse(Arr);

            dynamic rtnData = null;

            if (Data.Length == 1) rtnData = BitConverter.ToUInt16(Arr, 0);  //for int
            else rtnData = BitConverter.ToSingle(Arr, 0);   //for float
            return (T)Convert.ChangeType(rtnData, typeof(T));
        }
    }
}