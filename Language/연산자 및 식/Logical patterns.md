## Introduction

<br>

- 다음 C# 식과 문은 패턴을 지원한다.
    - [is 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
    - [switch 식](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
    - [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- 여기서는 각 패턴을 조합할 수 있는 `논리 패턴`을 설명한다.

<br>

## not (부정 패턴)

<br>

- `not` 패턴은 지정된 식과 일치하지 않는 경우 `true`를 반환한다.

```cs
int? foo = 7;

if (foo is not int) Console.WriteLine("foo is not int");
else if (foo is int) Console.WriteLine("foo is int");

/* output:
foo is int
*/
```

<br>

## and (결합 패턴)

<br>

- `and` 패턴은 지정된 모든 식이 일치하는 경우 `true`를 반환한다.

```cs
int foo = 7;

Console.WriteLine(foo switch
{
    < 0 => "foo < 0",
    > 0 and < 5 => "0 < foo < 5",
    > 5 and < 10 => "5 < foo < 10",
    _ => string.Empty
});

/* output:
5 < foo < 10
*/
```

<br>

## or (분리 패턴)

<br>

- `or` 패턴은 지정된 식 중 하나가 일치하는 경우 `true`를 반환한다.

```cs
int foo = 7;
string writeString = string.Empty;

switch (foo)
{
    case 1 or 2 or 3:
        writeString = "foo is one of 1, 2, 3";
        break;

    case < 0 or > 10:
        writeString = "foo < 0 or foo > 10";
        break;

    case > 5 or < 10:
        writeString = "5 < foo < 10";
        break;
}

Console.WriteLine(writeString);

/* output:
5 < foo < 10
*/
```

<br>

## 패턴 우선 순위

<br>

- 패턴 조합은 다음 우선 순위에 따라 정렬되며 괄호 (`( )`) 를 이용해 우선 순위를 지정할 수 있다.
    1. `not`
    2. `and`
    3. `or`

```cs
int? foo = 7;

if (foo is < 5 and not 7 or > 1)
    Console.WriteLine(foo);
else
    Console.WriteLine("Not matched");

/* output:
7
*/
```

- 위 예제에서 괄호를 사용하여 우선 순위를 바꾸면 다음 결과를 얻을 수 있다.

```cs
int? foo = 7;

if (foo is < 5 and (not 7 or > 1))
    Console.WriteLine(foo);
else
    Console.WriteLine("Not matched");

/* output:
Not matched
*/
```

<br>

## 참조 자료

<br>

- [패턴 - 논리 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#logical-patterns)
- [is 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/is)
- [C# - Language - Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)
- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)