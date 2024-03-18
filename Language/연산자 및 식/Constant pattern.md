## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 식 결과가 지정된 상수와 같은지 확인할 수 있는 `상수 패턴`을 설명한다.
- `상수 패턴`에는 상수 형식으로 변환할 수 있는 형식을 사용할 수 있다.
    - [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D), [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type)
    - [char](https://peponi-paradise.tistory.com/entry/C-Language-%EB%AC%B8%EC%9E%90%ED%98%95-char)
    - [string](https://peponi-paradise.tistory.com/entry/C-Language-%EB%AC%B8%EC%9E%90%EC%97%B4-%ED%98%95%EC%8B%9D-SystemString)
    - [bool](https://peponi-paradise.tistory.com/entry/C-Language-%EB%85%BC%EB%A6%AC%ED%98%95-Boolean)
    - [enum](https://peponi-paradise.tistory.com/entry/C-Language-%EC%97%B4%EA%B1%B0%ED%98%95-enum)
    - [const](https://peponi-paradise.tistory.com/entry/C-Language-Const-%EC%83%81%EC%88%98) 필드 또는 로컬 변수의 이름
    - [Span\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.span-1?view=net-8.0), [ReadOnlySpan\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.readonlyspan-1?view=net-8.0) (C# 11)
    - `null`

<br>

## Example

<br>

```cs
// is 식

int foo = 7;

if (foo is 7) Console.WriteLine("foo is 7");
else Console.WriteLine("foo is not 7");

/* output:
foo is 7
*/
```
```cs
// switch 식

int foo = 7;

Console.WriteLine(foo switch
{
    7 => "foo is 7",
    _ => "foo is not 7"
});

/* output:
foo is 7
*/
```
```cs
// switch 문

int foo = 7;
string writeString = string.Empty;

switch (foo)
{
    case 7:
        writeString = "foo is 7";
        break;

    default:
        writeString = "foo is not 7";
        break;
}

Console.WriteLine(writeString);

/* output:
foo is 7
*/
```

- `null` 체크를 수행하는 경우, [is 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)를 사용하면 [오버로드](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/operator-overloading)된 [같음 연산자 (`==`)](https://peponi-paradise.tistory.com/entry/C-Language-Equality-operators)가 호출되지 않도록 보장된다.

```cs
public class Foo
{
    public static bool operator ==(Foo a, Foo b) => false;
    public static bool operator !=(Foo a, Foo b) => false;
}
```
```cs
Foo? foo = null;

if (foo == null) Console.WriteLine("== null");
if (foo is null) Console.WriteLine("is null");

/* output:
is null
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 상수 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#constant-pattern)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- [연산자 오버로딩 - 미리 정의된 단항, 산술, 항등 및 비교 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/operator-overloading)
- [같음 연산자 - 두 개체가 같은지 여부를 테스트합니다.](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/equality-operators)