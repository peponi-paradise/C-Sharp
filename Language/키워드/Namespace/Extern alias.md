## Introduction

<br>

- [using 지시문](https://peponi-paradise.tistory.com/entry/C-Language-Using-directive)을 여러 어셈블리를 대상으로 사용하는 경우, 형식이 동일한 경우가 발생할 수 있다.
- 이 때, `extern` 키워드를 이용하여 각 네임스페이스의 별칭을 생성할 수 있다.

<br>

## Example - Assembly

<br>

- 아래와 같은 동일한 두 어셈블리가 존재한다고 가정한다.

```cs
// Assembly 1

namespace ClassLibrary
{
    public class Class
    {
        public Class() => Console.WriteLine("Assembly1");
    }
}
```

```cs
// Assembly 2

namespace ClassLibrary
{
    public class Class
    {
        public Class() => Console.WriteLine("Assembly2");
    }
}
```

<br>

## Example - Assembly import & Project file setting

<br>

- 프로젝트에 두 어셈블리를 참조한 후, 아래와 같이 프로젝트 파일에 alias를 추가한다.

```xml
<ItemGroup>
  <Reference Include="ClassLibrary1" Aliases="ClassLibrary1">
    <HintPath>..\ClassLibrary1.dll</HintPath>
  </Reference>
  <Reference Include="ClassLibrary2" Aliases="ClassLibrary2">
    <HintPath>..\ClassLibrary2.dll</HintPath>
  </Reference>
</ItemGroup>
```

<br>

## Example - Source file

<br>

- `extern alias` 및 `using` 선언 후 사용한다.

```cs
extern alias ClassLibrary1;
extern alias ClassLibrary2;

using Class1 = ClassLibrary1.ClassLibrary.Class;
using Class2 = ClassLibrary2.ClassLibrary.Class;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Class1 c1 = new Class1();
            Class2 c2 = new Class2();
        }
    }
}
```

<br>

## 참조 자료

<br>

- [extern alias(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/extern-alias)