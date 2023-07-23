using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser
{
    public class Data : CSVParser
    {
        public bool Data1 = false;
        public string Data2 = "";
        public double Data3 = 0;

        public bool InitDatas(string dataPath)
        {
            // Database load
            if (!LoadData(dataPath)) return false;

            // Assign params
            if (!GetData(nameof(Data1), ref Data1)) return false;
            if (!GetData(nameof(Data2), ref Data2)) return false;
            if (!GetData("NoParam", ref Data3)) return false;
            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            if (!data.InitDatas(@"C:\datafile.csv")) Console.WriteLine("Data load failed");
            Console.WriteLine(data.Data3);
        }
    }
}