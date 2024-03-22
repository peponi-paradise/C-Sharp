## Introduction

<br>

- 다음 형식은 비관리형 형식이다.
    - [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D)
    - [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type)
    - [논리](https://peponi-paradise.tistory.com/entry/C-Language-%EB%85%BC%EB%A6%AC%ED%98%95-Boolean)
    - [열거형](https://peponi-paradise.tistory.com/entry/C-Language-%EC%97%B4%EA%B1%B0%ED%98%95-enum)
    - [구조체](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B5%AC%EC%A1%B0%EC%B2%B4-struct) : 비관리 형식의 필드만 보유하는 경우
    - [포인터](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/unsafe-code#pointer-types)

<br>

## 구조체

<br>

- 구조체의 경우 형태에 따라 관리형, 비관리형이 될 수 있다.
- 다음 구조체는 관리형이다.
    ```cs
    public struct Foo
    {
        public int Bar;
        public string Baz;
    }
    ```
- 관리형 형식인지 파악하기 위해 `CompilerServices`의 [RuntimeHelpers](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.runtimehelpers.isreferenceorcontainsreferences?view=net-8.0)를 이용할 수 있다.
    ```cs
    using System.Runtime.CompilerServices;

    static void Main(string[] args)
    {
        Console.WriteLine(RuntimeHelpers.IsReferenceOrContainsReferences<Foo>());
    }

    /* output:
    True
    */
    ```
- 다음 구조체는 비관리형이다.
    ```cs
    public struct Foo
    {
        public int Bar;
        public int Baz;
    }
    ```
    ```cs
    using System.Runtime.CompilerServices;

    static void Main(string[] args)
    {
        Console.WriteLine(RuntimeHelpers.IsReferenceOrContainsReferences<Foo>());
    }

    /* output:
    False
    */
    ```

<br>

## 제네릭 구조체

<br>

- 제네릭 구조체의 경우 형식 매개 변수에 따라 관리형, 비관리형 구조체가 될 수 있다.
    ```cs
    public struct Foo<T>
    {
        public T Bar;
        public T Baz;
    }
    ```
    ```cs
    using System.Runtime.CompilerServices;

    static void Main(string[] args)
    {
        Console.WriteLine(RuntimeHelpers.IsReferenceOrContainsReferences<Foo<int>>());
        Console.WriteLine(RuntimeHelpers.IsReferenceOrContainsReferences<Foo<string>>());
    }

    /* output:
    False
    True
    */
    ```
- 형식 매개 변수를 비관리형으로 강제하고 싶은 경우 [unmanaged](https://peponi-paradise.tistory.com/entry/C-Language-Generic-type-constraints#unmanaged%20constraint-1) 키워드를 이용하여 제약 조건을 걸 수 있다.
    ```cs
    public struct Foo<T> where T: unmanaged
    {
        public T Bar;
        public T Baz;
    }
    ```
    ```cs
    using System.Runtime.CompilerServices;

    static void Main(string[] args)
    {
        Console.WriteLine(RuntimeHelpers.IsReferenceOrContainsReferences<Foo<int>>());      // OK
        Console.WriteLine(RuntimeHelpers.IsReferenceOrContainsReferences<Foo<string>>());   // CS8377
    }
    ```

<br>

## 참조 자료

<br>

- [비관리형 형식(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/unmanaged-types)
- [RuntimeHelpers.IsReferenceOrContainsReferences\<T> Method](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.runtimehelpers.isreferenceorcontainsreferences?view=net-8.0)
- [C# - Language - 구조체 (struct)](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B5%AC%EC%A1%B0%EC%B2%B4-struct)
- [C# - Language - Generic type constraints (where, default, unmanaged, new)](https://peponi-paradise.tistory.com/entry/C-Language-Generic-type-constraints#unmanaged%20constraint-1)