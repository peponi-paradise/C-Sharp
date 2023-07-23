<h1 id="title">C# - 레지스트리 읽기/쓰기</h1>

<h2 id="intro">Introduction</h2>

1. C#을 이용하여 레지스트리를 읽고 쓰려는 경우 Win32 API를 통해야한다.
2. 레지스트리 기본 키와 서브 키를 사용하여 레지스트리 값을 읽고 쓸 수 있다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using Microsoft.Win32;
using System;

namespace Registry
{
    public static class Registry
    {
        static RegistryKey BaseKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Peponi");

        public static bool AppendRegistry(string name, string value)
        {
            try
            {
                BaseKey.SetValue(name, value);
            }
            catch (Exception e)
            {
                // Write exception log
                return false;
            }
            return true;
        }

        public static bool GetRegistry(string name, out string value)
        {
            value = string.Empty;
            try
            {
                value = (string)BaseKey.GetValue(name);
            }
            catch (Exception e)
            {
                // Write exception log
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
namespace ReadWriteRegistry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Write registry
            Registry.Registry.AppendRegistry("KeyNameToCreate", "ValueToWrite");

            // Read registry
            bool isSuccess = Registry.Registry.GetRegistry("KeyNameToCreate", out string readValue);
        }
    }
}
```