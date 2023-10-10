## Introduction

<br>

- Bitwise 및 shift 연산자는 피연산자에 대해 비트 연산을 수행하게 한다.
- [정수 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D) 또는 [문자형 (char)](https://peponi-paradise.tistory.com/entry/C-Language-%EB%AC%B8%EC%9E%90%ED%98%95-char)에 대해 비트 연산 수행이 가능하다.
    - 정수 형식은 `int`, `uint`, `long`, `ulong` 형식으로 정의된다.
        - 피연산자가 다른 정수 형식인 (`int`보다 작은 데이터형) 경우 연산 결과는 `int` 형식으로 변환된다.
    - 피연산자의 형식이 다른 경우 값을 포함할 수 있는 가장 가까운 형식으로 변환된다.
- 복합 할당 식의 결과 형식은 왼쪽 피연산자의 형식이다.

<br>

## 비트 연산자

<br>

### Complement (~)

<br>

- `~` 연산자는 피연산자의 각 비트를 뒤집어 비트 보수를 반환한다.

```cs
int A = 0b_1010_1011;
int B = ~A;

Console.WriteLine(Convert.ToString(A, 2).PadLeft(sizeof(int) * 8, '0'));
Console.WriteLine(Convert.ToString(B, 2));

/* output:
00000000000000000000000010101011
11111111111111111111111101010100
*/
```

<br>

### AND (&)

<br>

- `&` 연산자는 피연산자의 각 비트에 대해 AND 연산을 수행한다.

```cs
int A = 0b_1010_1011;
int B = 0b_1101_1101;
int C = A & B;

Console.WriteLine(Convert.ToString(C, 2));

/* output:
10001001
*/
```

<br>

### OR (|)

<br>

- `|` 연산자는 피연산자의 각 비트에 대해 OR 연산을 수행한다.

```cs
int A = 0b_1010_1011;
int B = 0b_1101_1101;
int C = A | B;

Console.WriteLine(Convert.ToString(C, 2));

/* output:
11111111
*/
```

<br>

### XOR (^)

<br>

- `^` 연산자는 피연산자의 각 비트에 대해 XOR 연산을 수행한다.

```cs
int A = 0b_1010_1011;
int B = 0b_1101_1101;
int C = A ^ B;          // 0b_0111_0110

Console.WriteLine(Convert.ToString(C, 2));

/* output:
1110110
*/
```

<br>

## 시프트 연산자

<br>

### Left shift (<<)

<br>

- `<<` 연산자는 왼쪽 피연산자를 오른쪽 피연산자로 지정된 시프트 수만큼 왼쪽으로 이동시킨다.
- 결과 형식의 범위를 벗어나는 상위 비트는 삭제되고, 하위 비트는 자동으로 0으로 설정된다.

```cs
byte A = 0b_0001;
int B = A << 8;

Console.WriteLine(Convert.ToString(A, 2));
Console.WriteLine(Convert.ToString(B, 2));

uint C = 0b_1001_0110_1100_0000_1110_1111_0010_0101;
uint D = C << 8;            // 0b_1100_0000_1110_1111_0010_0101_0000_0000

Console.WriteLine(Convert.ToString(C, 2));
Console.WriteLine(Convert.ToString(D, 2));

/* output:
1
100000000
10010110110000001110111100100101
11000000111011110010010100000000
*/
```

<br>

### Right shift (>>)

<br>

- `>>` 연산자는 왼쪽 피연산자를 오른쪽 피연산자로 지정된 시프트 수만큼 오른쪽으로 이동시킨다.
    ```cs
    byte A = 0b_1010_1101;
    int B = A >> 3;             // 0b_0001_0101

    Console.WriteLine(Convert.ToString(B, 2).PadLeft(8, '0'));

    /* output:
    00010101
    */
    ```
- 결과 형식의 범위를 벗어나는 하위 비트는 삭제되고, 상위 비트는 다음과 같이 결정된다.
    1. Signed type : 왼쪽 피연산자의 최상위 비트 값이 비어있는 상위 비트를 채운다.
        (왼쪽 피연산자가 음수가 아닌 경우 0, 음수인 경우 1로 설정된다.)
        ```cs
        int A = 16;
        int B = A >> 3;

        Console.WriteLine(Convert.ToString(A, 2).PadLeft(sizeof(int) * 8, '0'));
        Console.WriteLine(Convert.ToString(B, 2).PadLeft(sizeof(int) * 8, '0'));

        int C = -16;
        int D = C >> 3;

        Console.WriteLine(Convert.ToString(C, 2));
        Console.WriteLine(Convert.ToString(D, 2));

        /* output:
        00000000000000000000000000010000
        00000000000000000000000000000010
        11111111111111111111111111110000
        11111111111111111111111111111110
        */
        ```
    2. Unsigned type : 오른쪽 시프트는 논리적 시프트를 수행하며 비어있는 상위 비트는 0으로 채워진다.
        ```cs
        uint A = 0b_1000_0000_0000_0000_0000_0000_0000_0000;
        uint B = A >> 3;

        Console.WriteLine(Convert.ToString(A, 2));
        Console.WriteLine(Convert.ToString(B, 2).PadLeft(sizeof(int) * 8, '0'));

        /* output:
        10000000000000000000000000000000
        00010000000000000000000000000000
        */
        ```

<br>

### Unsigned right shift (>>>, C# 11)

<br>

- `>>>` 연산자는 왼쪽 피연산자를 오른쪽 피연산자로 지정된 시프트 수만큼 오른쪽으로 이동시킨다.
- `>>` 연산자와의 차이점은 `>>>` 연산자는 항상 논리적 시프트를 수행한다.
    ```cs
    int A = 16;
    int B = A >>> 3;

    Console.WriteLine(Convert.ToString(A, 2).PadLeft(sizeof(int) * 8, '0'));
    Console.WriteLine(Convert.ToString(B, 2).PadLeft(sizeof(int) * 8, '0'));

    int C = -16;
    int D = C >>> 3;

    Console.WriteLine(Convert.ToString(C, 2).PadLeft(sizeof(int) * 8, '0'));
    Console.WriteLine(Convert.ToString(D, 2).PadLeft(sizeof(int) * 8, '0'));
    
    /* output:
    00000000000000000000000000010000
    00000000000000000000000000000010
    11111111111111111111111111110000
    00011111111111111111111111111110
    */
    ```

<br>

### 시프트 수

<br>

- 시프트 연산자의 오른쪽에 지정하는 시프트 수는 왼쪽 피연산자의 형식에 따라 달라진다.
    1. 왼쪽 피연산자의 형식이 `int` 또는 `uint` : 오른쪽 피연산자의 `하위 5bit`으로 정의된다.
        `오른쪽 피연산자 & 0b_0001_1111`
        ```cs
        int A = 0b_0001;
        int shift = 0b_1010_0011;
        int shifted = A << shift;
        
        Console.WriteLine(Convert.ToString(shifted, 2));

        /* output:
        1000
        */
        ```

    2. 왼쪽 피연산자의 형식이 `long` 또는 `ulong` : 오른쪽 피연산자의 `하위 6bit`으로 정의된다.
        `오른쪽 피연산자 & 0b_0011_1111`
        ```cs
        long A = 0b_0001;
        int shift = 0b_1010_0000;
        int shift2 = 0b_1010_0011;
        long shifted = A << shift;
        long shifted2 = A << shift2;
        
        Console.WriteLine(Convert.ToString(shifted, 2));
        Console.WriteLine(Convert.ToString(shifted2, 2));

        /* output:
        100000000000000000000000000000000
        100000000000000000000000000000000000
        */
        ```

<br>

## 열거형 논리 연산자

<br>

- 열거형 형식에 대해 `~`, `&`, `|`, `^` 연산자가 지원되며, 지정 값에 대해 비트 논리 연산이 수행된다.
- 일반적으로 `[Flags]` 특성으로 정의된 열거형 형식에 사용한다.    
    ```cs
    [Flags]
    enum StatusCode
    {
        None = 0b_0,       // 0
        Idle = 0b_1,       // 1
        Run = 0b_10,       // 2
        Warning = 0b_100,  // 4
        Error = 0b_1000    // 8
    }

    StatusCode code = StatusCode.Idle | StatusCode.Warning | StatusCode.Error;  // 13

    Console.WriteLine(code);    // Idle, Warning, Error
    ```
- 자세한 내용은 [C# - Language - 열거형 (enum)](https://peponi-paradise.tistory.com/entry/C-Language-%EC%97%B4%EA%B1%B0%ED%98%95-enum#enum%20%EB%B9%84%ED%8A%B8%20%ED%94%8C%EB%9E%98%EA%B7%B8-1)을 참조한다.

<br>

## 참조 자료

<br>

- [비트 및 시프트 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators)
- [C# - Language - 숫자 형식 변환](https://peponi-paradise.tistory.com/entry/C-Language-%EC%88%AB%EC%9E%90-%ED%98%95%EC%8B%9D-%EB%B3%80%ED%99%98)
- [C# - Language - 열거형 (enum)](https://peponi-paradise.tistory.com/entry/C-Language-%EC%97%B4%EA%B1%B0%ED%98%95-enum#enum%20%EB%B9%84%ED%8A%B8%20%ED%94%8C%EB%9E%98%EA%B7%B8-1)
- [C# - Language - Logical operators (!, &, |, ^, &&, ||)](https://peponi-paradise.tistory.com/entry/C-Language-Logical-operators)