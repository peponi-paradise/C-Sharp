## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 C# 11부터 지원되는 `목록 패턴`에 대해 설명한다.

<br>

## Example

<br>

- `목록 패턴`은 [배열](https://peponi-paradise.tistory.com/entry/C-Language-Array), [컬렉션](https://peponi-paradise.tistory.com/entry/C-Language-Collection)에 대해 특정 시퀀스와 매칭하는 기능을 제공한다.

```cs
List<int> ints = [-1, 2, 3];
```
```cs
// 기본 사용

Console.WriteLine(ints switch
{
    [-1, 2, 3] => true,
    _ => false
});

/* output:
True
*/
```
```cs
// 패턴 중첩

Console.WriteLine(ints switch
{
    [< 0, var mid, 5 or _] => mid,
    _ => false
});

/* output:
2
*/
```

- `목록 패턴`은 [범위 연산자 (`..`)](https://peponi-paradise.tistory.com/entry/C-Language-Index-Range-operators#..%20%EC%97%B0%EC%82%B0%EC%9E%90-1)를 통한 부분 매칭을 지원한다.
    - `..` 연산자로 매칭하는 요소의 수는 0개 이상이며, 각 패턴에서 하나만 사용할 수 있다.
    - `..` 연산자와 함께 다른 패턴 및 하위 패턴을 중첩할 수 있다.

```cs
List<int> ints = [-1, 2, 3, 4, -5];
```
```cs
// 기본 사용

Console.WriteLine(ints switch
{
    [.., > 0, > 0] => "Case 1",
    [> 0, .., > 0] => "Case 2",
    [< 0, > 0, ..] => "Case 3",
    _ => false
});

/* output:
Case 3
*/
```
```cs
// 패턴 중첩

Console.WriteLine(ints switch
{
    [.., > 0, > 0] => "Case 1",
    [> 0, .. { Count: > 2 }, > 0] => "Case 2",
    [< 0, > 0, .. var datas] => string.Join(", ", datas),
    _ => false
});

/* output:
3, 4, -5
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 목록 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#list-patterns)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)