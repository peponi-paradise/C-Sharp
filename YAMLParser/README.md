<h1 id="title">C# - YAML을 이용한 Data serialization, deserialization</h1>

<h2 id="intro">Introduction</h2>

1. `YAML`은 `JSON`보다 간결하게 표시하기 위해 나온 상위 호환 포맷으로, `XML` 또는 `JSON`에 비해 사람이 읽기 쉽다.
2. 호환성이 지원되므로, 상호 변환이 가능하다. `JSON`문서의 경우 바로 `YAML`로 사용 가능하다.
3. 아래 예제는 YamlDotNet을 이용한 Data serialization, deserialization 예시이다.[footnote]`YAML`을 세팅파일로 활용[/footnote]
4. 꼭 YAML 포맷을 잘 알아야 하는 것은 아니다. 최소한만 알아도, 각 언어에서 활용할 수 있다.
   - 사용에 필요한 예시는 아래 [Use example - Code](#example2)를 참조
   - 본문에 나와있지 않은 struct 등도 비슷하게 사용한다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace FileParser.YAML
{
    public interface IYAML
    {
        bool LoadData(string dataPath);

        void SaveData(string dataPath);
    }

    public static class YAMLParser
    {
        public static bool LoadData<T>(string dataPath, out T outData, List<Type> dataTags = null)
        {
            bool isSuccess = true;
            outData = default;

            string YAMLContents = File.ReadAllText(dataPath);

            var deserializer = dataTags == null ? new DeserializerBuilder().Build() : SerializerDesigner(new DeserializerBuilder(), dataTags).Build();

            outData = deserializer.Deserialize<T>(YAMLContents);

            return isSuccess;
        }

        public static void SaveData<T>(string dataPath, T saveData, List<Type> dataTags = null)
        {
            var serializer = dataTags == null ? new SerializerBuilder().Build() : SerializerDesigner(new SerializerBuilder(), dataTags).Build();
            var YAMLContents = serializer.Serialize(saveData);
            File.WriteAllText(dataPath, YAMLContents);
        }

        // Type명을 태그 이름으로 사용하게 지정
        private static dynamic SerializerDesigner(dynamic currentBuilder, List<Type> dataTags)
        {
            foreach (var dataTag in dataTags) currentBuilder.WithTagMapping($"!{dataTag.Name}", dataTag);
            return currentBuilder;
        }
    }
}
```

<br><br>

<h2 id="example">Use example - YAML file</h2>

```yaml
StringParam: Hello world
IntParam: 1293
DoubleParam: 1213.585
BoolParam: true
NestedClassParam:
  Name: Nested
  Value: 3487.123
Ints:
  - 1
  - 2
  - 3
KeyValuePairs:
  Key1: true
  Key2: false
  Key3: true
NestedDatas:
  - !NestedClass
    Name: Nested
    Value: 31.23
  - !NestedClass2
    Name: Nested2
    Value: true
```

<br><br>

<h2 id="example2">Use example - Code</h2>

```csharp
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
```

<br><br>

<h2 id="reference">참조 문헌</h2>

1. [C# - Reflection을 이용한 Parameter handling](https://peponi-paradise.tistory.com/entry/C-Reflection%EC%9D%84-%EC%9D%B4%EC%9A%A9%ED%95%9C-Parameter-handling)

<br><br>