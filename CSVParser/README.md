<h1 id="title">C# - CSV setting file parser 구현</h1>

<h2 id="intro">Introduction</h2>

1. `.csv`파일 (Comma separated file, `,`로 열 구성) 을 SW 파라미터 파일로 활용할 수 있다.
2. 아래 예시는 `.csv`파일 각 행의 `,`를 기준으로, 좌측에 `Name`과 우측에 `Value`로 구성한다.
3. `,`를 여러개 또는 `:`, `^` 기호 등을 통해 여러 개의 `Value` 구성 또한 가능하다.
    - 이 부분은 json, xml, yaml 등 다른 좋은 형식이 많다.
    - csv는 `single value type` 파라미터에 활용하기 좋다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSVParser
{
    public class CSVParameter
    {
        public string ParamName { get; set; } = string.Empty;
        public string ParamValue { get; set; } = string.Empty;
    }

    public abstract class CSVParser
    {
        private string Path;
        private List<CSVParameter> Params;

        public CSVParser()
        {
            Params = new List<CSVParameter>();
        }

        public virtual bool LoadData(string loadingPath)
        {
            Path = loadingPath;
            //파일 불러오기
            string text = string.Empty;
            try
            {
                text = System.IO.File.ReadAllText(loadingPath);
            }
            catch
            {
                return false;
            }

            //스트링 가공
            text = text.Replace("\r", "");
            string[] textArr = text.Split('\n');

            if (textArr.Length < 1) return false;

            try
            {
                if (!SettingFileParser(textArr)) return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public virtual bool GetData<T>(string dataName, ref T dataValue)
        {
            try
            {
                dataValue = (T)Convert.ChangeType(Params.Find(param => param.ParamName == dataName).ParamValue, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void SetData<T>(string dataName, T dataValue)
        {
            var index = Params.FindIndex(param => param.ParamName == dataName);
            if (index == -1)
            {
                Params.Add(new CSVParameter() { ParamName = dataName, ParamValue = dataValue.ToString() });
            }
            else Params[index].ParamValue = dataValue.ToString();
        }

        public virtual List<string> GetNames() => Params.Select(param => param.ParamName).ToList();

        public virtual List<CSVParameter> GetAllDatas() => Params;

        public virtual void ClearData() => Params.Clear();

        public virtual void RemoveData(string dataName)
        {
            var index = Params.FindIndex(param => param.ParamName == dataName);
            if (index != -1) Params.RemoveAt(index);
        }

        public virtual bool SaveData()
        {
            string saveData = string.Empty;
            for (int i = 0; i < Params.Count; i++)
            {
                saveData += i != Params.Count - 1 ? $"{Params[i].ParamName},{Params[i].ParamValue}\r\n" : $"{Params[i].ParamName},{Params[i].ParamValue}";
            }
            System.IO.File.WriteAllText(Path, saveData);
            return true;
        }

        private bool SettingFileParser(string[] textArr)
        {
            Params.Clear();
            try
            {
                for (int i = 0; i < textArr.Length; i++)
                {
                    if (i == textArr.Length - 1 && textArr[i] == "") break; //for excel csv
                    string[] splitter = textArr[i].Split(',');  // Index 0 : Param name, Index 1~ : Param value
                    if (!Params.Exists(param => param.ParamName == splitter[0]))
                    {
                        var parameter = new CSVParameter() { ParamName = splitter[0], ParamValue = splitter[1] };
                        Params.Add(parameter);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
using System;

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
            if (!GetData(nameof(Data3), ref Data3)) return false;
            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            if (!data.InitDatas(@"C:\datafile.csv")) Console.WriteLine("Data load failed");
            Console.WriteLine(data.Data1);
        }
    }
}
```