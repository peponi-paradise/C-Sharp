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



```