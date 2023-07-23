using System;
using System.Collections.Generic;
using FileParser.YAML;
using ParameterHandling;

namespace YAMLParser
{
    public class DataClass : IYAML
    {
        public class NestedData { }
        public class NestedClass : NestedData
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        public class NestedClass2 : NestedData
        {
            public string Name { get; set; }
            public bool Value { get; set; }
        }

        public string StringParam;
        public int IntParam;
        public double DoubleParam;
        public bool BoolParam;
        public NestedClass NestedClassParam;
        public List<int> Ints;
        public Dictionary<string, bool> KeyValuePairs;
        public List<NestedData> NestedDatas;

        List<Type> tags = new List<Type>() { typeof(NestedClass), typeof(NestedClass2) };

        public bool LoadData(string dataPath)
        {
            if (!FileParser.YAML.YAMLParser.LoadData(dataPath, out DataClass dataClass, tags)) return false;

            // C# - Reflection을 이용한 Parameter handling 참조 : https://peponi-paradise.tistory.com/entry/C-Reflection%EC%9D%84-%EC%9D%B4%EC%9A%A9%ED%95%9C-Parameter-handling
            ParameterHandling.ParameterHandling.CopyAllFieldsAndProperties(this, dataClass);

            return true;
        }

        public void SaveData(string dataPath) => FileParser.YAML.YAMLParser.SaveData(dataPath, this, tags);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DataClass dataClass = new DataClass();
            dataClass.LoadData(@"C:\datafile.yaml");
        }
    }
}