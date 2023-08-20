## Introduction

<br>

- `when` 키워드는 필터 조건을 지정하는 데 사용한다.
    - [catch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/exception-handling-statements#a-when-exception-filter) 절의 예외 필터
    - [switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#case-guards) 문의 케이스 가드
    - [switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/switch-expression#case-guards) 식의 케이스 가드
- `when` 키워드는 아래와 같이 사용한다.

<br>

```cs
// catch 

public class Foo
{
    public static void RaiseException()
    {
        throw new NotImplementedException("My exception");
    }
}

private static void Main()
{
    try
    {
        Foo.RaiseException();
    }
    catch (Exception e) when (e is NotImplementedException)
    {
        Console.WriteLine($"Exception has been occurred - {e}");
    }
}

/* output:
Exception has been occurred - System.NotImplementedException: My exception
   at ConsoleApp1.Program.Foo.RaiseException() in ...
*/
```

```cs
// switch 문

public static class IntHelper
{
    public static void PositiveCompare(int a, int b)
    {
        switch ((a, b))
        {
            case ( > 0, > 0) when a > b:
                Console.WriteLine($"{a} is bigger than {b}");
                break;

            case ( > 0, > 0) when a < b:
                Console.WriteLine($"{b} is bigger than {a}");
                break;

            case ( > 0, > 0) when a == b:
                Console.WriteLine($"{a} is same as {b}");
                break;

            default:
                Console.WriteLine("Wrong compare case");
                break;
        }
    }
}

private static void Main()
{
    IntHelper.PositiveCompare(1, 2);
}

/* output:
2 is bigger than 1
*/
```

```cs
// switch 식

public static class IntHelper
{
    public static int PositiveCompare(int a, int b)
    {
        return (a, b) switch
        {
            ( > 0, > 0) when a > b => a,
            ( > 0, > 0) when a < b => b,
            ( > 0, > 0) when a == b => a,
            _ => throw new ArgumentException("Input value must be greater than 0")
        };
    }
}

private static void Main()
{
    Console.WriteLine(IntHelper.PositiveCompare(3, 7));
}

/* output:
7
*/
```

<br>

## 참조 자료

<br>

- [when(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/when)