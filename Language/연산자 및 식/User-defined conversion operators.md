## Introduction

<br>

- 사용자 정의 형식은 다른 형식으로 암시적 또는 명시적 변환을 정의할 수 있다.
    - 암시적 변환은 예외를 발생시키지 않고 항상 성공해야한다.
    - 명시적 변환은 예외를 발생하거나 정보 손실의 가능성이 있는 경우 정의한다.
        `checked` 키워드를 이용해 [System.OverflowException](https://learn.microsoft.com/ko-kr/dotnet/api/system.overflowexception?view=net-8.0)을 발생시킬 수 있는 변환을 정의할 수 있다. (C# 11)
        `checked` 변환을 정의하는 경우 `checked`를 사용하지 않는 변환 또한 정의해야 하며, 변환은 각각의 컨텍스트를 통해 수행된다.
- 사용자 정의 변환은 [is](https://peponi-paradise.tistory.com/entry/C-Language-is-operator) 및 [as](https://peponi-paradise.tistory.com/entry/C-Language-as-operator) 연산자를 사용하는 경우 고려되지 않는다. ([캐스트 식](https://peponi-paradise.tistory.com/entry/C-Language-Cast-expression) 이용)
- 사용자 정의 변환은 `operator` 키워드와 함께 다음 키워드를 사용한다.
    - 암시적 변환 : `implicit`
    - 명시적 변환 : `explicit`

<br>

## Example

<br>

- 다음 구조체를 통해 사용자 정의 변환 정의를 보여준다.
    Overflow 테스트를 위해, 명시적 변환은 10배를 반환하도록 한다.
    ```cs
    public readonly struct Byte
    {
        private readonly byte _value;

        public Byte(byte value) => _value = value;

        public static implicit operator byte(Byte value) => value._value;

        public static explicit operator Byte(byte value) => new((byte)(value * 10));
        public static explicit operator checked Byte(byte value)
        {
            checked
            {
                return new((byte)(value * 10));
            }
        }
    }
    ```
- 위에서 정의한 `Byte` 구조체는 아래와 같이 사용할 수 있다.
    ```cs
    byte foo = 2;
    Byte bar = (Byte)foo;
    byte baz = bar;

    Console.WriteLine(bar);
    Console.WriteLine(baz);

    /* output:
    20
    20
    */
    ```
- 위 예제에서 overflow를 일으키면 아래와 같이 수행된다.
    ```cs
    byte foo = 27;
    Byte bar = (Byte)foo;
    byte baz = bar;

    Console.WriteLine(bar);
    Console.WriteLine(baz);

    /* output:
    14
    14
    */
    ```
- 컨텍스트를 `checked`로 전환하면 아래와 같이 `OverflowException`이 발생한다.
    ```cs
    byte foo = 27;

    try
    {
        checked
        {
            Byte bar = (Byte)foo;
            byte baz = bar;

            Console.WriteLine(bar);
            Console.WriteLine(baz);
        }
    }
    catch (OverflowException e)
    {
        Console.WriteLine(e);
    }

    /* output:
    System.OverflowException: Arithmetic operation resulted in an overflow.
    */
    ```

<br>

## 참조 자료

<br>

- [사용자 정의 명시적 및 암시적 변환 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)
- [OverflowException Class](https://learn.microsoft.com/ko-kr/dotnet/api/system.overflowexception?view=net-8.0)