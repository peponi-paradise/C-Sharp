## Introduction

<br>

- `partial` 키워드는 정의를 나눌 수 있게 해주며 다음 형식 또는 멤버에 사용 가능하다.
    - 클래스
    - 구조체
    - 인터페이스
    - 레코드
    - 메서드
- `partial`을 이용하여 형식을 정의하는 경우는 다음과 같다.
    - [소스 생성기](https://learn.microsoft.com/ko-kr/dotnet/csharp/roslyn-sdk/source-generators-overview) (Community toolkit, Visual studio designer, ...) 를 사용하는 경우
    - 형식을 개별 파일에 분산하여 정의하는 경우
- `partial` 키워드는 용도에 따라 코드 파일을 분리할 수 있는 이점을 제공한다.
    - 그러나 키워드의 편의성으로 인해 과도하게 형식이 커질 수 있다. 해당 부분은 코드 작성 시 주의해야 한다.
- `partial`로 선언된 형식은 하나의 어셈블리 내에서 정의되어야 하며, 모든 선언이 동일하게 `partial` 키워드를 갖고 있어야 한다.

<br>

## partial 형식

<br>

- `partial` 형식은 아래와 같이 정의한다.

```cs
public partial class CartesianCoordinate
{
    public double X = 0;
    public double Y = 0;
}

public partial class CartesianCoordinate
{
    public CartesianCoordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }
}

private static void Main()
{
    CartesianCoordinate coordinate = new(10, 5);

    var (X, Y) = coordinate;
    Console.WriteLine($"{X}, {Y}");
}

/* output:
10, 5
*/
```

- 동일한 방법으로 아래와 같이 인터페이스 등을 정의할 수 있다.

```cs
public partial interface ICoordinate
{
    double X { get; set; }
}

public partial interface ICoordinate
{
    double Y { get; set; }
}

public partial struct Coordinate : ICoordinate
{
    public double X { get; set; }
    public double Y { get; set; }
}

public partial struct Coordinate
{
    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }
}

public partial record CoordinateR : ICoordinate
{
    public double X { get; set; }
    public double Y { get; set; }
}

public partial record CoordinateR
{
    public CoordinateR(double x, double y)
    {
        X = x;
        Y = y;
    }
}

private static void Main()
{
    Coordinate coordinate = new(10, 5);
    var (X, Y) = coordinate;

    CoordinateR coordinateR = new(10, 5);

    Console.WriteLine($"{X}, {Y}");
    Console.WriteLine(coordinateR);
}

/* output:
10, 5
CoordinateR { X = 10, Y = 5 }
*/
```

<br>

## partial 메서드

<br>

- `partial` 메서드는 반드시 `partial` 형식 내에 있어야 하며 선언이 동일해야 한다.
- `partial` 메서드는 다음과 같은 경우에는 허용되지 않는다.
    - 생성자
    - 소멸자
    - 연산자 오버로드
    - 속성
    - 이벤트
- `partial` 메서드는 아래와 같이 정의한다.

```cs
public partial class CartesianCoordinate
{
    public double X = 0;
    public double Y = 0;

    public static int Created = 0;

    // partial method 선언

    public partial void Write();

    public static partial void WriteCreatedCount();
}

public partial class CartesianCoordinate
{
    public CartesianCoordinate(double x, double y)
    {
        X = x;
        Y = y;
        Created++;
    }

    // partial method 구현

    public partial void Write()
    {
        Console.WriteLine($"{X}, {Y}");
    }

    public static partial void WriteCreatedCount()
    {
        Console.WriteLine(Created);
    }
}

private static void Main()
{
    CartesianCoordinate coordinate = new(10, 5);

    coordinate.Write();
    CartesianCoordinate.WriteCreatedCount();
}

/* output:
10, 5
1
*/
```

- 상기 예제의 `static partial` 메서드는 일부러 추가한 부분이다.
    [MSDN 한글 문서](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods#partial-methods)에 다음과 같이 번역 오류가 있어 주의하도록 한다.
    > 부분 메서드(Partial Method)는 static 및 unsafe 한정자를 사용할 수 없습니다.
- [영어 원문](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods#partial-methods)의 해당 부분에는 다음과 같이 작성되어 있다.
    > Partial methods can have static and unsafe modifiers.

<br>

## 참조 자료

<br>

- [partial 형식(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/partial-type)
- [partial 메서드(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/partial-method)
- [Partial 클래스 및 메서드(C# 프로그래밍 가이드)](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods)