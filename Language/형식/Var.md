## Introduction

<br>

- [var](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables) 키워드는 컴파일러가 변수의 형식을 유추하도록 한다.
- 초기화 식을 이용해 유추하며, 형식을 유추할 수 없는 경우 `CS0246` 에러가 발생한다.
- 초기화 식의 형식이 참조 형식인 경우 `var` 키워드는 `nullable` 형식으로 유추한다. (C# 9 이상)
- 초기화 식에 형식이 포함되는 경우 코드를 줄이기 위해 사용하기도 한다.

<br>

## Example

<br>

```cs
// ABC 클래스 선언 없음

var varTest = new ABC();    // CS0246: 'ABC' 형식 또는 네임스페이스 이름을 찾을 수 없습니다. using 지시문 또는 어셈블리 참조가 있는지 확인하세요.
```
```cs
// .NET framework 4.7.2

var varTest = "ABC";        // var = string

// .NET 5 이상

var varTest = "ABC";        // var = string?
```
```cs
var X = new List<int>();
```

<br>

## 익명 형식에서 사용

<br>

- [익명 형식](https://learn.microsoft.com/ko-kr/dotnet/csharp/fundamentals/types/anonymous-types)에서는 `var` 키워드를 사용해야 한다.
- 따로 형식을 정의할 필요 없이 사용 가능하여 편리하지만, 형식 이름을 코드에서 직접 사용할 수 없기 때문에 `var` 키워드가 필요하다.

<br>

```cs
// var = AnonymousType 'a?
// 'a 은(는) new { int A, double B }

var anonymousTest = new { A = 0, B = 1.1 };     // A = int, B = double
```

<br>

```cs
// LINQ 예시

public record CartesianCoordinate(string Series, List<double> X, List<double> Y);
```
```cs
// var = System.Collections.Generic.IEnumerable<out T>?

var coordinateData = from coordinate in coordinates
                     where coordinate.Series.Contains("2D")
                     select new { coordinate.X, coordinate.Y };
```

- 쿼리 식을 이용하여 익명 형식으로 유추하게 되는경우 `IEnumerable` 형식으로 지정되지만 `out` 타입이 `T`이다.
- 따라서 반복문 [foreach](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement)를 이어 호출하는 경우 역시 `var` 키워드가 필요하다.

```cs
foreach (var data in coordinateData)
{
    Console.WriteLine(string.Join(',', data.X));
    Console.WriteLine(string.Join(',', data.Y));
}
```

<br>

## 참조 자료

- [암시적으로 형식화된 지역 변수](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables)