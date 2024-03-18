## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 다음 패턴을 설명한다.
    - 속성 패턴 : 각 패턴에 대해 속성, 필드를 매칭
    - 위치 패턴 : 식을 분해하고 패턴 매칭

<br>

## 속성 패턴

<br>

- `속성 패턴`은 중괄호 `{ }` 를 이용하여 매칭할 속성을 지정한다.

```cs
public record Foo(int A, int B);
```
```cs
var foo = new Foo(1, 2);

Console.WriteLine(foo switch
{
    { A: < 0 } => "A < 0",
    { B: < 0 } => "B < 0",
    { A: > 0, B: > 0 } => "A > 0, B > 0",
    _ => "Not recognized"
});

/* output:
A > 0, B > 0
*/
```

<br>

### 속성 패턴 - 중첩 패턴

<br>

- `속성 패턴`은 중첩된 형식에 접근 가능하다.

```cs
public record Foo(Bar A, Bar B);
public record Bar(int C);
```
```cs
var foo = new Foo(new(1), new(2));

Console.WriteLine(foo switch
{
    { A: { C: < 0 }, B: { C: < 0 } } => "A.C < 0, B.C < 0",
    { A: { C: > 0 }, B: { C: < 0 } } => "A.C > 0, B.C < 0",
    { A: { C: > 0 }, B: { C: > 0 } } => "A.C > 0, B.C > 0",
    _ => "Not recognized"
});

/* output:
A.C > 0, B.C > 0
*/
```

- 위 예제는 아래와 같이 표현할 수 있다. (C# 10)

```cs
var foo = new Foo(new(1), new(2));

Console.WriteLine(foo switch
{
    { A.C: < 0, B.C: < 0 } => "A.C < 0, B.C < 0",
    { A.C: > 0, B.C: < 0 } => "A.C > 0, B.C < 0",
    { A.C: > 0, B.C: > 0 } => "A.C > 0, B.C > 0",
    _ => "Not recognized"
});

/* output:
A.C > 0, B.C > 0
*/
```

<br>

## 위치 패턴

<br>

- `위치 패턴`은 _deconstructable_ 형식 (튜플, record, ...) 에 대해 식을 분해하고 패턴 매칭하는 기능을 제공한다.
- `위치 패턴` 구현 시 변수 이름을 선택적으로 사용할 수 있다.

```cs
public record Foo(int A, int B);
```
```cs
var foo = new Foo(1, 2);

Console.WriteLine(foo switch
{
    (A: < 0, _) => "A < 0",
    (_, B: < 0) => "B < 0",
    ( > 0, > 0) => "A > 0, B > 0",
    _ => "Not recognized"
});

/* output:
A > 0, B > 0
*/
```

<br>

### 위치 패턴 - 중첩 패턴

<br>

- `위치 패턴`은 중첩된 형식에 접근 가능하다.

```cs
public record Foo(Bar A, Bar B);
public record Bar(int C, int D);
```
```cs
var foo = new Foo(new(-1, 2), new(3, 4));

Console.WriteLine(foo switch
{
    (( > 0, _), ( > 0, _)) => "A.C > 0, B.C > 0",
    ((_, > 0), (_, > 0)) => "A.D > 0, B.D > 0",
    _ => "Not recognized",
});

/* output:
A.D > 0, B.D > 0
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 속성 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#property-pattern)
- [패턴 - 위치 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#positional-pattern)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)