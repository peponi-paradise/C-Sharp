## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 다음 패턴을 설명한다.
    - 형식 패턴 : 형식 확인
    - 선언 패턴 : 형식 확인 및 성공 시 결과를 변수에 할당
- 특정 객체가 `T` 형식임을 확인하는 경우, 아래 형식 조건 중 하나를 만족하면 `true`가 반환된다. 
    - `T` or `T?`
    - `T`의 파생 형식
    - `T`의 인터페이스 구현
    - `T` 형식으로 [boxing 또는 unboxing](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/types/boxing-and-unboxing#boxing) 가능

<br>

## 형식 패턴

<br>

- 식의 형식을 확인하려는 경우 다음과 같이 형식 패턴을 사용할 수 있다.

```cs
namespace TypePattern
{
    public interface Common { }

    public class Foo : Common { }
    public class Bar : Common { }
}
```
```cs
// is 식

List<Common> collection = [new Foo(), new Bar()];

foreach (var item in collection)
{
    if (item is Foo || item is Bar)
    {
        Console.WriteLine(item.ToString());
    }
}

/* output:
TypePattern.Foo
TypePattern.Bar
*/
```
```cs
// switch 식

List<Common> collection = [new Foo(), new Bar()];

foreach (var item in collection)
{
    Console.WriteLine(GetName(item));
}

string GetName(Common item)
{
    return item switch
    {
        Foo or Bar => item.ToString()!,
        _ => string.Empty
    };
}

/* output:
TypePattern.Foo
TypePattern.Bar
*/
```
```cs
// switch 문

List<Common> collection = [new Foo(), new Bar()];

foreach (var item in collection)
{
    Console.WriteLine(GetName(item));
}

string GetName(Common item)
{
    switch (item)
    {
        case Foo:
        case Bar:
            return item.ToString()!;

        default:
            return string.Empty;
    }
}

/* output:
TypePattern.Foo
TypePattern.Bar
*/
```

<br>

## 선언 패턴

<br>

- 식의 형식을 확인하고 변수를 할당하려는 경우 다음과 같이 선언 패턴을 사용할 수 있다.

```cs
namespace DeclarationPattern
{
    public interface Common { }

    public class Foo : Common { }
    public class Bar : Common { }
}
```
```cs
// is 식

List<Common> collection = [new Foo(), new Bar()];

foreach (var item in collection)
{
    if (item is Foo foo) Console.WriteLine(foo);
    else if (item is Bar bar) Console.WriteLine(bar);
}

/* output:
DeclarationPattern.Foo
DeclarationPattern.Bar
*/
```
```cs
// switch 식

List<Common> collection = [new Foo(), new Bar()];

foreach (var item in collection)
{
    Console.WriteLine(GetName(item));
}

string GetName(Common item)
{
    return item switch
    {
        Foo foo => foo.ToString()!,
        Bar bar => bar.ToString()!,
        _ => string.Empty
    };
}

/* output:
DeclarationPattern.Foo
DeclarationPattern.Bar
*/
```
```cs
// switch 문

List<Common> collection = [new Foo(), new Bar()];

foreach (var item in collection)
{
    Console.WriteLine(GetName(item));
}

string GetName(Common item)
{
    switch (item)
    {
        case Foo foo:
            return foo.ToString()!;
        case Bar bar:
            return bar.ToString()!;

        default:
            return string.Empty;
    }
}

/* output:
DeclarationPattern.Foo
DeclarationPattern.Bar
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 선언 및 형식 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#declaration-and-type-patterns)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- [Boxing 및 Unboxing(C# 프로그래밍 가이드)](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/types/boxing-and-unboxing#boxing)