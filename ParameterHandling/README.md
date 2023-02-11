<h1 id="title">C# - Reflection을 이용한 Parameter handling</h1>

<h2 id="intro">Introduction</h2>

1. `System.Reflection`은 `FieldInfo`, `PropertyInfo` 등 런타임 조회 기능을 제공한다.
2. 현재 `Type`의 모든 퍼블릭 속성을 조회하여 값 할당, 복사 등을 할 수 있다.
3. 전체 복사의 경우 반복문이 두 번 들어가 시간복잡도는 `O(n^2)`이다.
4. Get 또는 Set의 경우 순차로 값을 조회하기 때문에 시간복잡도는 `O(n)`이다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using System;

namespace ParameterHandling
{
    public static class ParameterHandling
    {
        public static void CopyAllFieldsAndProperties<T>(in T willCopied, in T dataBase)
        {
            foreach (var fieldInfo in willCopied.GetType().GetFields())
            {
                foreach (var dataInfo in dataBase.GetType().GetFields())
                {
                    if (fieldInfo.Name == dataInfo.Name && fieldInfo.FieldType == dataInfo.FieldType) fieldInfo.SetValue(willCopied, dataInfo.GetValue(dataBase));
                }
            }
            foreach (var propertyInfo in willCopied.GetType().GetProperties())
            {
                foreach (var dataInfo in dataBase.GetType().GetProperties())
                {
                    if (propertyInfo.Name == dataInfo.Name && propertyInfo.PropertyType == dataInfo.PropertyType) propertyInfo.SetValue(willCopied, dataInfo.GetValue(dataBase));
                }
            }
        }

        public static bool GetParameter<T>(string paramName, ref T paramValue, object dataBase)
        {
            foreach (var fieldInfo in dataBase.GetType().GetFields())
            {
                if (fieldInfo.Name == paramName && fieldInfo.FieldType == typeof(T))
                {
                    paramValue = (T)Convert.ChangeType(fieldInfo.GetValue(dataBase), typeof(T));
                    return true;
                }
            }
            foreach (var propertyInfo in dataBase.GetType().GetProperties())
            {
                if (propertyInfo.Name == paramName && propertyInfo.PropertyType == typeof(T))
                {
                    paramValue = (T)Convert.ChangeType(propertyInfo.GetValue(dataBase), typeof(T));
                    return true;
                }
            }
            return false;
        }

        public static bool SetParameter<T>(string paramName, T paramValue, object dataBase)
        {
            foreach (var fieldInfo in dataBase.GetType().GetFields())
            {
                if (fieldInfo.Name == paramName && fieldInfo.FieldType == typeof(T))
                {
                    fieldInfo.SetValue(dataBase, paramValue);
                    return true;
                }
            }
            foreach (var propertyInfo in dataBase.GetType().GetProperties())
            {
                if (propertyInfo.Name == paramName && propertyInfo.PropertyType == typeof(T))
                {
                    propertyInfo.SetValue(dataBase, paramValue);
                    return true;
                }
            }
            return false;
        }
    }
}
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
namespace ParameterHandling
{
    internal class Program
    {
        public class Parameter
        {
            public string Name = default;
            public bool Value1 = false;
            public double Value2 = 0;
            public int Value3 = 0;
        }

        static void Main(string[] args)
        {
            Parameter parameter = new Parameter();
            Parameter parameter2 = new Parameter();
            parameter2.Name = "parameter2";
            ParameterHandling.CopyAllFieldsAndProperties(parameter, parameter2);
            string a = "";
            ParameterHandling.GetParameter(nameof(parameter.Name), ref a, parameter);
            ParameterHandling.SetParameter(nameof(parameter.Value2), 0.1213, parameter);
        }
    }
}
```