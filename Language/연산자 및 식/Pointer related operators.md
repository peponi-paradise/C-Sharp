## Introduction

<br>

- C#에서는 포인터를 제한된 범위에서 지원한다.
    - 포인터는 메모리 주소를 _보유_ 하기만 한다.
    - [비관리형 형식](https://peponi-paradise.tistory.com/entry/C-Language-Unmanaged-types)만 참조 가능하다.
    - 포인터로 지정 가능한 대상은 `GC`의 영향을 받지 않도록 고정되어야 한다.
        (스택에 존재하거나 [fixed 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/fixed)으로 고정되어야 함)
    - 포인터를 사용하기 위해서는 unsafe 컨텍스트가 필요하다.
- 포인터 연산자의 종류와 역할은 아래와 같다.
    - `&` : 메모리 주소 반환
    - `*` : 포인터 역참조
    - `->` : 멤버 액세스
    - `[]` : 요소 액세스

<br>

## `&` 연산자

<br>

- `&` 연산자는 피연산자의 주소를 반환한다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int foo = 6;
        int* bar = &foo;

        Console.WriteLine($"{(long)bar:X2}");   // 실행할 때마다 값이 달라진다.
    }

    /* output:
    65717DEBAC
    */
    ```
- 피연산자가 `GC`의 영향을 받는 경우 `fixed 문`을 이용하여 고정 후 주소를 가져올 수 있다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int[] foo = [1, 2, 3];

        //int* bar = &foo[1];     // CS0212

        fixed (int* bar = &foo[1])
        {
            Console.WriteLine($"{(long)bar:X2}");
        }
    }

    /* output:
    1B99C9019F4
    */
    ```

<br>

## `*` 연산자

<br>

- 역참조 연산자 `*`는 포인터가 가리키는 변수를 반환한다.
    이를 이용해 데이터 작업을 수행할 수 있다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int foo = 7;
        int* bar = &foo;

        Console.WriteLine(*bar);

        *bar = 10;

        Console.WriteLine(foo);
    }

    /* output:
    7
    10
    */
    ```

<br>

## `->` 연산자

<br>

- 멤버 액세스 연산자 `->`는 `*` 연산자와 [멤버 액세스 식 (`.`)](https://peponi-paradise.tistory.com/entry/C-Language-Member-access-expression)을 결합한다.
    다음 두 식은 같은 결과를 가져온다.
    ```cs
    pointer->member

    (*pointer).member
    ```
- `->` 연산자는 아래와 같이 사용한다.
    ```cs
    public struct Foo
    {
        public int Bar;
    }
    ```
    ```cs
    unsafe static void Main(string[] args)
    {
        Foo foo = new();
        Foo* bar = &foo;

        Console.WriteLine((*bar).Bar);

        bar->Bar = 10;

        Console.WriteLine(bar->Bar);
    }

    /* output:
    0
    10
    */
    ```

<br>

## `[]` 연산자

<br>

- `type* p`의 `p[n]` 액세스는 `*(p + n)`으로 계산된다.
    `n`은 암시적으로 `int`, `uint`, `long`, `ulong` 중 하나로 전환할 수 있는 형식이어야 한다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int* foo = stackalloc int[3];

        for (int i = 0; i < 3; i++)
        {
            foo[i] = i;
        }

        for (int i = 0; i < 3; i++)
        {
            Console.Write($"{foo[i]} ");
        }
    }

    /* output:
    0 1 2
    */
    ```

<br>

## 포인터 덧셈 (`+`)

<br>

- `pointer + n`, `n + pointer`는 `pointer + n * sizeof(T)` 포인터를 반환한다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int* ints = stackalloc int[] { 1, 3, 5, 7, 9, 11 };
        int* foo = &ints[0];
        int* bar = foo + 2;

        Console.WriteLine(*bar);
        Console.WriteLine(*++bar);
    }

    /* output:
    5
    7
    */
    ```

<br>

## 포인터 뺄셈 (`-`)

<br>

- `pointer - n`은 `pointer - n * sizeof(T)` 포인터를 반환한다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int* ints = stackalloc int[] { 1, 3, 5, 7, 9, 11 };
        int* foo = &ints[5];
        int* bar = foo - 2;

        Console.WriteLine(*bar);
        Console.WriteLine(*--bar);
    }

    /* output:
    7
    5
    */
    ```
- 포인터끼리 뺄셈을 수행하는 경우 주소값을 연산하여 반환한다.
    `(pointer1 - pointer2) / sizeof(T)` 값을 반환하며 형식은 `long`이다.
    ```cs
    unsafe static void Main(string[] args)
    {
        int* ints = stackalloc int[] { 1, 3, 5, 7, 9, 11 };
        int* foo = &ints[5];
        int* bar = foo - 3;

        var baz = foo - bar;

        Console.WriteLine(baz.GetType());
        Console.WriteLine(baz);
    }

    /* output:
    System.Int64
    3
    */
    ```






https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/pointer-related-operators