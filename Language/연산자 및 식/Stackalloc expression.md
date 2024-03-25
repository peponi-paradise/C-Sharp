## Introduction

<br>

- `stackalloc` 식은 [스택](https://en.wikipedia.org/wiki/Data_segment)에 메모리 블록을 할당한다.
    - 할당된 메모리 블록은 메서드가 반환될 때 자동으로 해제되며 명시적으로 해제가 불가능하다.
    - 할당된 메모리 블록은 `GC`의 대상이 아니기 때문에 [fixed 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/fixed)을 사용하여 피닝하지 않아도 된다.

<br>

## Example

<br>

- `stackalloc` 식은 다음 형식 중 하나에 할당할 수 있다.
    - [System.Span\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.span-1?view=net-8.0) : `T`는 [비관리형 형식](https://peponi-paradise.tistory.com/entry/C-Language-Unmanaged-types)
        ```cs
        static void Main(string[] args)
        {
            Span<int> foo = stackalloc int[3] { 1, 2, 3 };
            Span<int> bar = stackalloc int[] { 1, 2, 3 };

            Console.WriteLine(foo[1]);
            Console.WriteLine(bar[1]);
        }

        /* output:
        2
        2
        */
        ```
    - [System.ReadOnlySpan\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.readonlyspan-1?view=net-8.0) : `T`는 [비관리형 형식](https://peponi-paradise.tistory.com/entry/C-Language-Unmanaged-types)
        ```cs
        static void Main(string[] args)
        {
            ReadOnlySpan<int> foo = stackalloc int[3] { 1, 2, 3 };
            ReadOnlySpan<int> bar = stackalloc int[] { 1, 2, 3 };

            Console.WriteLine(foo[1]);
            Console.WriteLine(bar[1]);
        }

        /* output:
        2
        2
        */
        ```
    - [Pointer](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/unsafe-code#pointer-types)
        ```cs
        unsafe static void Main(string[] args)
        {
            int* foo = stackalloc int[3];

            for (int i = 0; i < 3; i++)
            {
                foo[i] = i + 1;
            }

            Console.WriteLine(foo[1]);
        }

        /* output:
        2
        */
        ```

<br>

## 주의사항

<br>

- `stackalloc` 식을 루프 내에서 할당하면 반복적으로 메모리 할당을 수행한다.
    [StackOverflowException](https://learn.microsoft.com/ko-kr/dotnet/api/system.stackoverflowexception)이 일어날 수 있기 때문에 주의해야한다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int loopIndex = 10;
        while (--loopIndex > 0)
        {
            Span<int> ints = stackalloc int[2] { 1, 2 };    // CA2014
            fixed (int* foo = &ints[1])
            {
                Console.WriteLine($"{(long)foo:X2}");
            }
        }
    }

    /* output:
    B32B17E7A4
    B32B17E794
    B32B17E784
    B32B17E774
    B32B17E764
    B32B17E754
    B32B17E744
    B32B17E734
    B32B17E724
    */
    ```

<br>

## 참조 자료

<br>

- [stackalloc 식(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/stackalloc)
- [Data segment](https://en.wikipedia.org/wiki/Data_segment)
- [C# - Language - Unmanaged types](https://peponi-paradise.tistory.com/entry/C-Language-Unmanaged-types)