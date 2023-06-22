using NModbus4.Wrapper.Define;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NModbus4.Wrapper.Util.Converter
{
    public static class Converter
    {
        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Converter/Doc[@name="ToUshortHexData"]'/>
        public static List<ushort> ToUshortHexData<T>(T inputValue, Endian toEndian = Endian.Big)
        {
            bool isLittleEndian = BitConverter.IsLittleEndian;
            List<ushort> data = new List<ushort>();
            List<byte> b;

            if (typeof(T) == typeof(bool)) b = BitConverter.GetBytes((bool)Convert.ChangeType(inputValue, typeof(T))).ToList();
            else if (typeof(T) == typeof(int)) b = BitConverter.GetBytes((ushort)(int)Convert.ChangeType(inputValue, typeof(T))).ToList();
            else b = BitConverter.GetBytes((float)Convert.ChangeType(inputValue, typeof(float))).ToList();

            var arr = b.ToArray();

            if (isLittleEndian && toEndian == Endian.Big) Array.Reverse(arr);
            else if (!isLittleEndian && toEndian == Endian.Little) Array.Reverse(arr);

            if (typeof(T) == typeof(bool)) data.Add(arr[0]);
            else for (int index = 0; index < arr.Length / 2; index++) data.Add(Convert.ToUInt16(arr[index * 2].ToString("X2") + arr[index * 2 + 1].ToString("X2"), 16));

            return data;
        }

        /// <include file='NModbus4.Wrapper.Summary.xml' path='Docs/Converter/Doc[@name="FromUShortHexData"]'/>
        public static T FromUShortHexData<T>(ushort[] inputValues, Endian FromEndian = Endian.Big)
        {
            if (inputValues.Length == 1 && (inputValues[0] == 0 || inputValues[0] == 1)) return (T)Convert.ChangeType(inputValues[0], typeof(T)); //for bool

            bool isLittleEndian = BitConverter.IsLittleEndian;
            List<byte> totalBytes = new List<byte>();

            foreach (var data in inputValues)
            {
                var byteArray = BitConverter.GetBytes(data);
                if (isLittleEndian) Array.Reverse(byteArray);
                foreach (var Byte in byteArray) totalBytes.Add(Byte);
            }

            var arr = totalBytes.ToArray();
            if (isLittleEndian && FromEndian == Endian.Big) Array.Reverse(arr);
            else if (!isLittleEndian && FromEndian == Endian.Little) Array.Reverse(arr);

            dynamic rtnData = null;

            if (inputValues.Length == 1) rtnData = BitConverter.ToUInt16(arr, 0);  //for int
            else rtnData = BitConverter.ToSingle(arr, 0);   //for float
            return (T)Convert.ChangeType(rtnData, typeof(T));
        }

        public static string ByteToHex(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            return hex.Replace("-", " ");
        }
    }
}