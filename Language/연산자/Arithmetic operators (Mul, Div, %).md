## Introduction

<br>

- 산술 연산자 중 `*`, `/`, `%` 연산자는 [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D), [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type) 형식을 지원한다.
    - 정수 형식은 `int`, `uint`, `long`, `ulong` 형식으로 정의된다.
        - 피연산자가 다른 정수 형식인 (`int`보다 작은 데이터형) 경우 연산 결과는 `int` 형식으로 변환된다.
    - 피연산자가 정수 또는 부동 소수점 형식인 경우 값을 포함할 수 있는 가장 가까운 형식으로 변환된다.
- 복합 할당 식의 결과 형식은 왼쪽 피연산자의 형식이다.

<br>

## * operator

<br>

- `*` 연산자는 피연산자의 곱을 계산한다.
    ```cs
    var a = 3 * 4;
    var b = 2 * 2.2;
    var c = 2.1f * 3.4d;

    Console.WriteLine(a.GetType());
    Console.WriteLine(a);
    Console.WriteLine(b.GetType());
    Console.WriteLine(b);
    Console.WriteLine(c.GetType());
    Console.WriteLine(c);

    /* output:
    System.Int32
    12
    System.Double
    4.4
    System.Double
    7.139999675750732
    */
    ```

<br>

## / operator

<br>

- `/` 연산자는 왼쪽 피연산자를 오른쪽 피연산자로 나눈다.

<br>

### 정수

<br>

- 정수 형식의 나눗셈을 하는 경우 결과는 정수 형식으로 표현되며, 소수점 단위의 나머지는 버림 처리된다.
    ```cs
    var a = 7 / 4;

    Console.WriteLine(a.GetType());
    Console.WriteLine(a);

    /* output:
    System.Int32
    1
    */
    ```
- 0으로 나누기를 시도하는 경우 [DivideByZeroException](https://learn.microsoft.com/ko-kr/dotnet/api/system.dividebyzeroexception?view=net-7.0)이 발생한다.
    ```cs
    var a = 7;
    var b = 0;
    var c = a / b;

    /* output:
    Unhandled exception. System.DivideByZeroException: Attempted to divide by zero.
    */
    ```

<br>

### 부동 소수점

<br>

- 부동 소수점 형식의 나눗셈을 하는 경우 결과는 부동 소수점 형식으로 표현된다.
    ```cs
    var a = 6.5f / 3.1d;

    Console.WriteLine(a.GetType());
    Console.WriteLine(a);

    /* output:
    System.Double
    2.096774193548387
    */
    ```
- 피연산자 중 `decimal` 형식이 있으면 암시적 변환이 불가하기 때문에 캐스팅이 필요하다.
    ```cs
    var a = 6.5f / (decimal)3.1d;

    Console.WriteLine(a.GetType());
    Console.WriteLine(a);

    /* output:
    System.Decimal
    2.0967741935483870967741935484
    */
    ```
- 0으로 나누기를 시도하는 경우 `double.NegativeInfinity`, `double.PositiveInfinity`, `double.NaN` 중 하나를 경우에 따라 반환한다.
    ```cs
    double doubleNeg = -9E307;
    double doublePos = 9E307;
    double doubleZero = 0;
    double div = 0;

    Console.WriteLine(doubleNeg / div);
    Console.WriteLine(doublePos / div);
    Console.WriteLine(doubleZero / div);

    /* output:
    -∞
    ∞
    NaN
    */
    ```

<br>

### 나눗셈 오차

<br>

- 연산 결과는 정확하지 않을 수 있는데, 이는 주어진 값의 2진수 표현이 `무한소수`인 경우가 있기 때문이다.
- 컴퓨터에서 소수부에 대한 2진수 표현은 $1/2^k$ 형태의 수에 대해 정확하게 가능하며, 0.1, 0.2, 0.3 등의 소수에 대해서는 정확하게 처리하지 못한다.
    ```cs
    var a = 0.1 + 0.2;
    var b = 1.5 + 0.5;

    Console.WriteLine(a == 0.3);
    Console.WriteLine(a);
    Console.WriteLine(b == 2);
    Console.WriteLine(b);

    /* output:
    False
    0.30000000000000004
    True
    2
    */
    ```
- .NET의 `double` 및 `float` 형식은 [IEC 60559:1989(IEEE 754, Wikipedia)](https://en.wikipedia.org/wiki/IEEE_754-1985#IEC_60559)를 준수한다.

<br>

## % operator

<br>

- `%` 연산자는 왼쪽 피연산자를 오른쪽 피연산자로 나눈 후 나머지를 반환한다.
- 피연산자 중 `decimal` 형식이 있으면 암시적 변환이 불가하기 때문에 캐스팅이 필요하다.
    ```cs
    var a = 10 % 7;
    var b = 10.1 % 7.2;
    var c = 10.3m % (decimal)7.4;

    Console.WriteLine(a.GetType());
    Console.WriteLine(a);
    Console.WriteLine(b.GetType());
    Console.WriteLine(b);
    Console.WriteLine(c.GetType());
    Console.WriteLine(c);

    /* output:
    System.Int32
    3
    System.Double
    2.8999999999999995
    System.Decimal
    2.9
    */
    ```
- .NET의 `%` 연산 수행은 IEEE 754 사양과는 다르다고 한다. IEEE 754를 준수하는 연산이 필요한 경우 [Math.IEEERemainder](https://learn.microsoft.com/ko-kr/dotnet/api/system.math.ieeeremainder?view=net-7.0)를 사용한다. [(부동 소수점 나머지 - MSDN)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/arithmetic-operators#floating-point-remainder)

<br>

## 산술 오버플로

<br>

- 숫자 형식은 표현 가능한 범위가 있는데, 이 범위를 넘어서면 발생한다.
- 기본적으로 산술 연산은 `unchecked` 컨텍스트에서 수행하며, 정수와 부동 소수점의 오버플로는 다르게 동작한다.
    - 정수의 경우 `checked` 컨텍스트로 지정하면 [OverflowException](https://learn.microsoft.com/ko-kr/dotnet/api/system.overflowexception?view=net-7.0)이 발생하게 된다.
    - `unchecked` 컨텍스트에서 연산 수행 시 결과값에서 상위 비트를 버린다.
        ```cs
        var int1 = int.MaxValue;
        var int2 = 5;
        var a = int1 * int2;
        
        Console.WriteLine(a);
        
        checked
        {
            a = int1 * int2;
            Console.WriteLine(a);
        }
        
        /* output:
        2147483643
        Unhandled exception. System.OverflowException: Arithmetic operation resulted in an overflow.
        */
        ```
    - 부동 소수점의 경우 예외를 발생시키지 않는 대신 `double.NegativeInfinity`, `double.PositiveInfinity` 중 하나를 경우에 따라 반환한다.
        ```cs
        double doubleNeg = -9E307;
        double doublePos = 9E307;
        double mul = 9E100;

        Console.WriteLine(doubleNeg * mul);
        Console.WriteLine(doublePos * mul);

        /* output:
        -∞
        ∞
        */
        ```

<br>

## 할당 연산자

<br>

- 할당 연산자 `*=`, `/=`, `%=` 는 연산과 동시에 할당이 가능해지는 복합 할당 식을 가능하게 한다.
- 다음 두 식은 동일한 연산을 수행한다.
    (byte 연산 시 `int`로 변환되어 `X = X * Y;`는 실제로는 수행할 수 없는 코드이다.)
    ```cs
    byte X = 254;
    byte Y = 3;

    X = X * Y;
    X *= Y;
    ```

- 연산은 동일하지만 캐스팅이 다르게 되는데, 위의 예제를 조금 더 자세히 풀어보면 아래와 같다.
    ```cs
    byte X = 254;
    byte Y = 3;

    X = (int)X * (int)Y;
    X = (byte)((int)X * (int)Y);
    ```

- 이런 캐스팅의 특성으로 인해 의도치 않은 계산의 결과가 일어날 수 있다.
    ```cs
    byte X = 254;
    byte Y = 3;

    var Z = X * Y;

    Console.WriteLine(Z.GetType());
    Console.WriteLine(Z);

    X *= Y;

    Console.WriteLine(X.GetType());
    Console.WriteLine(X);

    /* output:
    System.Int32
    762
    System.Byte
    250
    */
    ```
    - 새로운 값 `Z`는 `int` 형식이므로 762를 출력한다.
    - 반면, `byte` 형식의 최대값은 255 (FF) 이기 때문에 `X`의 값은 250이 된다. (`762(0010_1111_1010)의 상위 비트 버림 : 250(1111_1010)`)
    - 코드 양을 줄이기 위해 축약형 표현을 사용하는 경우가 많은데, 연산을 하는 경우에는 주의가 필요하다.

<br>

## 참조 자료

<br>

- [산술 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/arithmetic-operators)
- [IEEE 754-1985(Wikipedia)](https://en.wikipedia.org/wiki/IEEE_754-1985#IEC_60559)
- [C# - Language - 정수 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D)
- [C# - Language - 부동 소수점 형식](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type)
- [C# - Language - 숫자 형식 변환](https://peponi-paradise.tistory.com/entry/C-Language-%EC%88%AB%EC%9E%90-%ED%98%95%EC%8B%9D-%EB%B3%80%ED%99%98)