## Introduction

<br>

- Null-forgiving 연산자 (`!`) 는 단항 후위 연산자로, [nullable 컨텍스트](https://learn.microsoft.com/ko-kr/dotnet/csharp/nullable-references#nullable-contexts)에서 null 경고를 억제한다.
- `!` 연산자는 런타임에 영향을 주지 않고, 컴파일러의 분석에만 영향을 준다.
    - 런타임에 `x!` 식은 `x`로 해석된다.

<br>

## Example

<br>

- 다음 nullable 컨텍스트의 `Bar`는 `CS8618`을 생성한다.
    ```cs
    #nullable enable
    public class Foo
    {
        public string Bar;      // CS8618
        public string? Baz;     // 경고 없음
    }
    ```
- 다음 nullable 컨텍스트의 `Bar`는 `CS8618`을 생성하지 않고, 생성자에서 `null`을 전달할 시 `CS8625`를 생성한다.
    ```cs
    #nullable enable
    public class Foo(string value)
    {
        public string Bar = value;
    }
    ```
    ```cs
    static void Main(string[] args)
    {
        Foo foo = new(null);    // CS8625
        Foo bar = new(null!);   // 경고 없음
    }
    ```
- 코드 구성에 의해 `null`이 될 수 없지만, 컴파일러가 이를 인식할 수 없는 경우 `!` 연산자를 이용해 역참조할 수 있다.
    ```cs
    static void Main(string[] args)
    {
        var foo = GetFoo();
        Console.WriteLine(foo.Bar);     // CS8602
        Console.WriteLine(foo!.Bar);    // 경고 없음
    }

    static Foo? GetFoo() => new Foo("foo");
    ```
- 컴파일러가 인식할 수 없는 컨텍스트에서 [null 허용 정적 분석](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/attributes/nullable-analysis) 특성을 이용해 컴파일러에 `null`이 아님을 알릴 수 있다.
    ```cs
    static void Main(string[] args)
    {
        var foo = GetFoo();
        if (IsNotNull(foo))
        {
            Console.WriteLine(foo.Bar);     // NotNullWhen 특성을 제거하면 CS8602 발생
        }
    }

    static Foo? GetFoo() => new Foo("foo");

    // NotNullWhen 특성을 이용해 컴파일러에 null이 아님을 알림
    static bool IsNotNull([NotNullWhen(true)] Foo? foo) => true;
    ```

<br>

## 참조 자료

<br>

- [! (null-forgiving) 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/null-forgiving)
- [nullable 컨텍스트](https://learn.microsoft.com/ko-kr/dotnet/csharp/nullable-references#nullable-contexts)
- [null 허용 정적 분석](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/attributes/nullable-analysis)