## Introduction

<br>

- `+`, `+=` 연산자는 [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D), [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type), [문자열](https://peponi-paradise.tistory.com/entry/C-Language-%EB%AC%B8%EC%9E%90%EC%97%B4-%ED%98%95%EC%8B%9D-SystemString), [대리자](https://peponi-paradise.tistory.com/entry/C-Language-%EB%8C%80%EB%A6%AC%EC%9E%90-Delegate) 형식을 지원한다.
- 여기서는 `+`, `+=` 연산자에 대해 간단히 알아본다.
    - 산술 연산자 `+`에 대한 자세한 내용은 [+, - operator (Arithmetic operator)](https://peponi-paradise.tistory.com/entry/C-Language-Arithmetic-operator)를 참조한다.

<br>

## 문자열 연결

<br>

- 피연산자 중 하나 또는 둘 다가 문자열 형식인 경우 `+` 연산자는 피연산자를 연결한다.
    ```cs
    Console.WriteLine("Hello " + "World!");
    Console.WriteLine("Score : " + 5);
    Console.WriteLine("Null's string representation is string.Empty - " + null);

    /* output:
    Hello World!
    Score : 5
    Null's string representation is string.Empty -
    */
    ```
- `+` 연산자는 UTF-8 리터럴 문자열에 대한 연결을 지원한다. (C# 11)
    ```cs
    System.ReadOnlySpan<byte> utf8 = "ABC"u8 + "DEF"u8;
    
    Console.WriteLine(string.Join(", ", utf8.ToArray()));

    /*output:
    65, 66, 67, 68, 69, 70
    */
    ```
- [문자열 보간 ($)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/tokens/interpolated)은 문자열을 지정하는 다른 방법을 제공한다.
    ```cs
    string foo = nameof(foo);
    string bar = nameof(bar);
    Console.WriteLine($"{foo}, {bar}");

    /* output:
    foo, bar
    */
    ```
    - 자리 표시자 (`{}`) 에 사용되는 모든 식이 상수 문자열인 경우 문자열 보간을 사용해 상수 문자열 초기화가 가능하다.
        ```cs
        private const string _foo = nameof(_foo);
        private const string _bar = nameof(_bar);
        private const string _baz = $"{_foo}, {_bar}";

        static void Main(string[] args)
        {
            Console.WriteLine(_baz);
        }

        /* output:
        _foo, _bar
        */
        ```

<br>

## 대리자 조합

<br>

- `+` 연산자는 동일한 대리자 형식의 연결을 지원한다.
    - 이 때, 반환되는 인스턴스를 호출하는 경우 왼쪽 피연산자를 호출한 다음, 오른쪽 피연산자를 호출한다.
        ```cs
        Action foo = delegate { Console.WriteLine("Foo"); };
        Action bar = delegate { Console.WriteLine("Bar"); };
        Action baz = foo + bar;

        baz();

        /* output:
        Foo
        Bar
        */
        ```
    - 피연산자 중 `null`이 있는 경우 해당 피연산자는 무시된다.
        ```cs
        Action? foo = null;
        Action bar = delegate { Console.WriteLine("Bar"); };
        Action baz = foo + bar;
    
        baz();
    
        /* output:
        Bar
        */
        ```

<br>

## 더하기 할당 연산자 (`+=`)

<br>

- `+=` 연산자를 통해, 이진 연산자 `+`의 복합 할당을 수행할 수 있다.
    ```cs
    int foo = 1;
    foo += 4;
    Console.WriteLine(foo);

    /* output:
    5
    */
    ```
    ```cs
    string foo = nameof(foo);
    foo += "bar";
    Console.WriteLine(foo);

    /* output:
    foobar
    */
    ```
    ```cs
    Action foo = delegate { Console.WriteLine(nameof(foo)); };
    foo += delegate { Console.WriteLine("bar"); };

    foo();

    /* output:
    foo
    bar
    */
    ```
- `+=` 연산자는 이벤트를 구독할 때도 사용된다.
    ```cs
    private static event EventHandler? _foo;

    static void Main(string[] args)
    {
        _foo += delegate { Console.WriteLine(nameof(_foo)); };

        _foo.Invoke(null, new());
    }

    /* output:
    _foo
    */
    ```
- `+=` 연산자를 통해 복합 할당을 수행하는 경우, 형식 확인은 왼쪽 피연산자에 대해 최초 1회만 수행된다.
    ```cs
    byte foo = 255;
    byte bar = 1;

    var baz = foo + bar;    // 복합 할당을 수행하지 않는 경우
    foo += bar;             // 복합 할당을 수행하는 경우

    Console.WriteLine(baz.GetType());
    Console.WriteLine(baz);
    Console.WriteLine(foo.GetType());
    Console.WriteLine(foo);

    /* output:
    System.Int32
    256
    System.Byte
    0
    */
    ```
    - 위 예시와 같이 복합 할당을 하는 경우 최초 1회 확인한 피연산자의 형식 (`byte`) 으로 결과가 출력된다.
        `byte` 형식은 연산 시 `int` 형식으로 변환되는데, 이로 인해 동일한 연산을 수행하는 두 경우에 대해 다른 결과가 나타난다.
        따라서 복합 할당 시 형식에 주의할 필요가 있다.

<br>

## 참조 자료

<br>

- [더하기 연산자 - + 및 +=](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/addition-operator)
- [문자열 보간 ($)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/tokens/interpolated)