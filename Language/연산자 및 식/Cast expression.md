## Introduction

<br>

- 캐스트 식은 `(T)expression`으로 나타내며, 명시적으로 `expression`을 `T`로 형식 변환한다.
    - 명시적 변환이 불가한 경우 컴파일 타임 오류가 발생한다.
    - 런타임에 명시적 변환에 실패하는 경우 [System.InvalidCastException](https://learn.microsoft.com/ko-kr/dotnet/api/system.invalidcastexception?view=net-8.0), [System.OverflowException](https://learn.microsoft.com/en-us/dotnet/api/system.overflowexception?view=net-8.0) 등이 발생할 수 있다.

<br>

## Example

<br>

- 다음은 캐스트 식을 사용하는 사례를 보여준다.
    ```cs
    long foo = 123;

    Console.WriteLine((ushort)foo);

    List<int> bar = [1, 2, 3];
    IEnumerable<int> baz = (IEnumerable<int>)bar;

    Console.WriteLine(string.Join(", ", baz));

    /* output:
    123
    1, 2, 3
    */
    ```
- [사용자 정의 변환](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)을 수행하는 예시는 아래와 같다.
    ```cs
    public readonly struct Byte
    {
        private readonly byte _value;

        public Byte(byte value) => _value = value;

        public static explicit operator byte(Byte value) => value._value;
        public static implicit operator Byte(byte value) => new(value);
    }
    ```
    ```cs
    var foo = new Byte(5);
    byte bar = (byte)foo;

    Console.WriteLine(bar);

    /* output:
    5
    */
    ```
- 다음은 명시적 변환에 실패하는 경우를 보여준다.
    ```cs
    int? foo = null;
    int bar = (int)foo;     // System.InvalidOperationException
    ```
    ```cs
    int foo = -1;
    checked
    {
        byte bar = (byte)foo;   // System.OverflowException
    }
    ```

<br>

## 참조 자료

<br>

- [형식 테스트 연산자 및 캐스트 식 - 캐스트 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/type-testing-and-cast#cast-expression)
- [사용자 정의 명시적 및 암시적 변환 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)