## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 다음 패턴을 설명한다.
    - 무시 (`_`) 패턴 : `null`을 포함한 모든 식을 매칭한다.
    - `var` 패턴 : `null`을 포함한 모든 식을 매칭하고 지역 변수에 할당한다.

<br>

## `_` 패턴

<br>

- `_` 패턴은 `switch 식`에만 사용이 가능하고, `switch 문`의 `default`와 같은 기능을 한다.
- `switch 식`에서 `_`을 사용하지 않고 모든 경우를 커버하지 못하면 컴파일러는 경고를 생성한다. (CS8509)

```cs
int? foo = null;

Console.WriteLine(foo switch
{
    1 => 1,
    2 => 2,
    _ => "Not recognized"
});

/* output:
Not recognized
*/
```

<br>

## `var` 패턴

<br>

- `var` 패턴의 형식은 패턴에 대응하는 컴파일 타임 형식으로, 중간 결과 저장이 용이하다.

```cs
public record CartesianCoordinate2D(int X, int Y);
```
```cs
var foo = new CartesianCoordinate2D(-10, 2);
Console.WriteLine(GetValidCutOff(foo));

CartesianCoordinate2D GetValidCutOff(CartesianCoordinate2D? coord)
{
    if (coord is not null and var (x, y))
    {
        if (x < 0) x = 0;
        if (y < 0) y = 0;
        return new(x, y);
    }
    throw new ArgumentNullException($"{nameof(coord)}", $"{nameof(coord)} could not be null");
}

/* output:
CartesianCoordinate2D { X = 0, Y = 2 }
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 무시 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#discard-pattern)
- [패턴 - var 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#var-pattern)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)