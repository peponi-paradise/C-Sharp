## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 식 결과가 지정된 상수와 비교할 수 있는 `관계형 패턴`을 설명한다.
- `관계형 패턴`에는 다음 형식을 사용할 수 있다.
    - [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D), [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type)
    - [char](https://peponi-paradise.tistory.com/entry/C-Language-%EB%AC%B8%EC%9E%90%ED%98%95-char)
    - [enum](https://peponi-paradise.tistory.com/entry/C-Language-%EC%97%B4%EA%B1%B0%ED%98%95-enum)

<br>

## Example

<br>

```cs
// is 식

int foo = 7;

if (foo is < 0) Console.WriteLine("foo < 0");
else if (foo is < 5) Console.WriteLine("foo < 5");
else if (foo is < 10) Console.WriteLine("foo < 10");

/* output:
foo < 10
*/
```
```cs
// switch 식

int foo = 7;

Console.WriteLine(foo switch
{
    < 0 => "foo < 0",
    < 5 => "foo < 5",
    < 10 => "foo < 10",
    _ => string.Empty
});

/* output:
foo < 10
*/
```
```cs
// switch 문

int foo = 7;
string writeString = string.Empty;

switch (foo)
{
    case < 0:
        writeString = "foo < 0";
        break;

    case < 5:
        writeString = "foo < 5";
        break;

    case < 10:
        writeString = "foo < 10";
        break;
}

Console.WriteLine(writeString);

/* output:
foo < 10
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 관계형 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#relational-patterns)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)