## Introduction

<br>

- C#에서는 포인터를 제한된 범위에서 지원한다.
    - 포인터는 메모리 주소를 _보유_ 하기만 한다.
    - [비관리형 형식]()만 참조 가능하다.
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




























https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/pointer-related-operators