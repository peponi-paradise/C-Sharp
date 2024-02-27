## Introduction

<br>

- 네임스페이스 별칭 한정자 (`::`) 는 별칭이 있는 네임스페이스의 구성원에 접근할 수 있게 한다.
- 다음과 같은 경우에 `::` 한정자를 사용할 수 있다.
    - [Using alias](https://peponi-paradise.tistory.com/entry/C-Language-Using-directive#Using%20alias-1)
    - [Extern alias](https://peponi-paradise.tistory.com/entry/C-Language-Extern-alias)
    - Global alias : 전역 네임스페이스 별칭

<br>

## Example

<br>

```cs
// Using alias

namespace MyConsole
{
    public static class MyWriter
    {
        public static void Write(string message) => Console.WriteLine(message);
    }
}

namespace ConsoleApp
{
    using Writer = MyConsole;

    internal class Program
    {
        static void Main(string[] args)
        {
            Writer::MyWriter.Write("Hello, World!");
        }
    }
}

/* output:
Hello, World!
*/
```
```cs
// Extern alias

// Assembly

namespace ClassLibrary1
{
    public static class Class
    {
        public static void Write(string message) => Console.WriteLine(message);
    }
}

// Application csproj

<Reference Include="ClassLibrary1" Aliases="Import">
  <HintPath>..\ClassLibrary1.dll</HintPath>
</Reference>

// Application code

extern alias Import;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Import::ClassLibrary1.Class.Write("Hello, World!");
        }
    }
}

/* output:
Hello, World!
*/
```
```cs
// Global alias

global::System.Console.WriteLine("Hello, World!");

/* output:
Hello, World!
*/
```

<br>

## 참조 자료

<br>

- [:: 연산자 - 네임스페이스 별칭 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/namespace-alias-qualifier)
- [C# - Language - Using 지시문 (Using directive)](https://peponi-paradise.tistory.com/entry/C-Language-Using-directive)
- [C# - Language - Extern 별칭 (Extern alias)](https://peponi-paradise.tistory.com/entry/C-Language-Extern-alias)