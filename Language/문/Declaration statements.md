## 1. Introduction

<br>

- `선언문`은 지역 변수, 상수 또는 참조 변수를 선언한다.
    ```cs
    // local variable
    string foo = string.Empty;

    // local constant
    const int bar = 0;

    // local ref variable
    ref string baz = ref foo;
    ```
- 선언문은 한번에 여러 변수를 선언할 수도 있다.
    ```cs
    int foo, bar, baz;
    ```
- 선언문은 `block`, `switch block`에서는 허용되지만 `embedded statement`에는 허용되지 않는다.
    ```cs
    {
        int foo;    // allowed
    }

    if(true)
        bool bar;   // CS1023
    ```

<br>

## 2. Local variable

<br>

- 지역 변수 선언은 다음과 같이 한다.
    ```cs
    string foo;
    ```
- 초기 값을 사용하여 변수를 초기화할 수 있다.
    ```cs
    string foo = "Hello, world!";
    ```
- [`var` 키워드](https://peponi-paradise.tistory.com/entry/C-Language-implicitly-typed-local-variables-var)를 이용하여 컴파일러가 형식을 유추하게 할 수 있다.
    - Nullable 컨텍스트에서는 대상 형식이 참조 형식인 경우 nullable 형식으로 유추한다.
    ```cs
    var foo = 1;         // int
    var bar = "bar";     // string?
    ```
- `var` 키워드를 사용하여 지역 변수 선언 시 다음과 같이 형식을 유추할 수 없으면 오류가 발생한다.
    ```cs
    var foo;                // CS0818
    var bar = null;         // CS0815
    var baz = x => x + 1;   // CS8917
    ```
- [무명 형식](https://peponi-paradise.tistory.com/entry/C-Language-implicitly-typed-local-variables-var#%EC%9D%B5%EB%AA%85%20%ED%98%95%EC%8B%9D%EC%97%90%EC%84%9C%20%EC%82%AC%EC%9A%A9-1)을 선언하는 경우 `var` 키워드를 사용해야 한다.
    ```cs
    // var = AnonymousType 'a?
    // 'a 은(는) new { int A, double B }

    var foo = new { A = 0, B = 1.1 };
    ```

<br>

## 3. Local constant

<br>

- 지역 상수는 선언 시 초기화를 해야 한다.
    ```cs
    const int foo = 1;
    const int bar;      // CS0145
    ```

<br>

## 4. Local ref variable

<br>

- 지역 변수에 `ref` 키워드를 사용하여 `참조 변수`를 선언할 수 있다.
    ```cs
    int foo = 1;
    ref int bar = ref foo;
    ```
- `readonly` 키워드를 추가하여 읽기 전용으로 만들 수 있다.
    ```cs
    int foo = 1;
    ref readonly int bar = ref foo;
    bar = 2;    // CS0131
    ```
    - 이 때, `ref` 키워드를 사용하여 [참조 대상을 변경](https://peponi-paradise.tistory.com/entry/C-Language-Assignment-operator#%3D%20ref%20%ED%95%A0%EB%8B%B9-1)하는 것은 가능하다.
        ```cs
        int foo = 1;
        int bar = 2;
        ref readonly int baz = ref foo;
        baz = ref bar;
        ```
- 다음과 같이 [참조 반환](https://peponi-paradise.tistory.com/entry/C-Language-Ref-keyword-Parameter-modifier#%EC%B0%B8%EC%A1%B0%20%EB%B0%98%ED%99%98-1)을 참조 변수에 할당할 수 있다.
    ```cs
    private static int _foo = 5;

    static void Main(string[] args)
    {
        ref int foo = ref GetFoo();
        foo = 10;
        Console.WriteLine(_foo);
    }

    static ref int GetFoo() => ref _foo;

    /* output:
    10
    */
    ```

<br>

## 5. scoped ref

<br>

- `scoped`는 C# 11에 추가된 키워드로 다음과 같은 경우에 사용한다.
    1. 변수의 수명을 선언된 범위(메서드) 내로 제한
    2. 변수의 수명이 모호한 경우(외부 노출 등에 의해) 컴파일러에 명확한 수명 범위 제공
- `scoped` 키워드는 `ref` 또는 `ref struct`에만 적용할 수 있다.
    ```cs
    // Local variable

    static Span<int> NonScoped()
    {
        Span<int> span = default;
        return span;   // OK
    }
     
    static Span<int> Scoped()
    {
        scoped Span<int> span = default;
        return span;   // CS8352
    }
    ```
    ```cs
    // Method parameter

    static Span<int> NonScoped(Span<int> span)
    {
        return span;    // OK
    }

    static Span<int> Scoped(scoped Span<int> span)
    {
        return span;    // CS8352
    }
    ```
    ```cs
    // Ref struct

    public ref struct Foo
    {
        public void NonScoped(ReadOnlySpan<char> span)
        {
        }

        public void Scoped(scoped ReadOnlySpan<char> span)
        {
        }
    }
     
    static void Main(string[] args)
    {
        Span<char> chars = stackalloc char[] { 'a', 'b', 'c' };
        var foo = new Foo();
        foo.NonScoped(chars);  // CS8350
        foo.Scoped(chars);     // OK
    }
    ```
- 형식이 `ref struct`인 경우 메서드의 `this`, `out` 및 `ref` 변수에 `scoped` 한정자가 암시적으로 적용된다.

<br>

## 6. 참조 자료

<br>

- [선언문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/declarations)
- [Declation statements](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/language-specification/statements#136-declaration-statements)
- [`ref` 구조체 형식(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/ref-struct)
- [`scoped` modifier](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/proposals/csharp-11.0/low-level-struct-improvements#scoped-modifier)
- [C# 11: ref struct안의 ref 필드](http://m.csharpstudy.com/latest/View/?aspx=CS11-ref-field.aspx)
- [C# 11 - ref struct/ref field를 위해 새롭게 도입된 scoped 예약어](https://www.sysnet.pe.kr/2/0/13276)