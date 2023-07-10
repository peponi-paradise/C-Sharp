## Introduction

<br>

- [void](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/void)는 변수 형식으로 사용할 수 없는 형식이다.
- `메서드`, `로컬 메서드`, `delegate`의 `반환 형식`으로 지정하여 반환 값이 없는 것을 나타낸다.
- `unsafe 컨텍스트`에서 포인터 형식으로 사용 가능하다. (추천하는 방식은 아니다)

<br>

## Example

<br>

```cs
// Method

public void PrintConsole(string message) => Console.WriteLine($"{DateTime.Now} - {message}");
```
```cs
// Local Method

public void PrintConsole(string message)
{
    string printMessage = $"{DateTime.Now} - {message}";

    Print(printMessage);

    void Print(string input) => Console.WriteLine(input);
}
```
```cs
// delegate

public delegate void PrintDelegate(string message);
```

<br>

## 참조 자료

<br>

- [void(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/void)