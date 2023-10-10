## Introduction

<br>

- 논리 연산자는 단항 및 이진 논리 연산을 포함한다.
    - NOT : `!`
    - AND : `&`
    - OR : `|`
    - XOR : `^`
    - Conditional AND : `&&`
        - 왼쪽 피연산자가 `true`일 때만 오른쪽 피연산자를 평가
    - Conditional OR : `||`
        - 왼쪽 피연산자가 `false`일 때만 오른쪽 피연산자를 평가

<br>

## NOT (!)

<br>

- 단항 논리 연산자 `!`는 피연산자의 부정을 반환한다.
- 접두사로 사용하며, 접미사로 사용하는 경우 [null 허용 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/null-forgiving)가 된다.

```cs
Console.WriteLine(!true);
Console.WriteLine(!false);

/* output:
False
True
*/
```

<br>

## AND (&)

<br>

- `&` 연산자는 AND 연산을 수행한다.
- 왼쪽과 오른쪽 피연산자 둘 다 `true`의 값을 가질 때만 `true`를 반환한다.
    ```cs
    Console.WriteLine(true & true);
    Console.WriteLine(true & false);
    Console.WriteLine(false & true);
    Console.WriteLine(false & false);

    /* output:
    True
    False
    False
    False
    */
    ```
- 왼쪽 피연산자가 `false`일 때도 오른쪽 연산자의 값을 확인한다.
    ```cs
    bool Right()
    {
        Console.WriteLine("Right");
        return true;
    }

    bool Left = true;

    Console.WriteLine(Left & Right());
    Console.WriteLine(!Left & Right());

    /* output:
    Right
    True
    Right
    False
    */
    ```
- 정수 형식 피연산자의 경우 [비트 연산](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-and-operator-)을 수행한다.
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

## OR (|)

<br>

- `|` 연산자는 OR 연산을 수행한다.
- 왼쪽 또는 오른쪽 피연산자가 `true`의 값을 가질 때 `true`를 반환한다.
    ```cs
    Console.WriteLine(true | true);
    Console.WriteLine(true | false);
    Console.WriteLine(false | true);
    Console.WriteLine(false | false);

    /* output:
    True
    True
    True
    False
    */
    ```
- 왼쪽 피연산자가 `true`일 때도 오른쪽 연산자의 값을 확인한다.
    ```cs
    bool Right()
    {
        Console.WriteLine("Right");
        return true;
    }

    bool Left = false;

    Console.WriteLine(Left | Right());
    Console.WriteLine(!Left | Right());

    /* output:
    Right
    True
    Right
    True
    */
    ```
- 정수 형식 피연산자의 경우 [비트 연산](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-or-operator-)을 수행한다.
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

## XOR (^)

<br>

- `^` 연산자는 XOR 연산을 수행한다.
- 왼쪽과 오른쪽 피연산자의 값이 다를 때 `true`를 반환한다.
    ```cs
    Console.WriteLine(true ^ true);
    Console.WriteLine(true ^ false);
    Console.WriteLine(false ^ true);
    Console.WriteLine(false ^ false);

    /* output:
    False
    True
    True
    False
    */
    ```
- 정수 형식 피연산자의 경우 [비트 연산](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-exclusive-or-operator-)을 수행한다.
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

## Conditional AND (&&)

<br>

- `&&` 연산자는 AND 연산을 수행한다.
- 왼쪽과 오른쪽 피연산자 둘 다 `true`의 값을 가질 때만 `true`를 반환한다.
    ```cs
    Console.WriteLine(true && true);
    Console.WriteLine(true && false);
    Console.WriteLine(false && true);
    Console.WriteLine(false && false);

    /* output:
    True
    False
    False
    False
    */
    ```
- 왼쪽 피연산자가 `false`일 때는 오른쪽 연산자의 값을 확인하지 않는다.
    ```cs
    bool Right()
    {
        Console.WriteLine("Right");
        return true;
    }

    bool Left = true;

    Console.WriteLine(Left && Right());
    Console.WriteLine(!Left && Right());

    /* output:
    Right
    True
    False
    */
    ```

<br>

## Conditional OR (||)

<br>

- `||` 연산자는 OR 연산을 수행한다.
- 왼쪽 또는 오른쪽 피연산자가 `true`의 값을 가질 때 `true`를 반환한다.
    ```cs
    Console.WriteLine(true || true);
    Console.WriteLine(true || false);
    Console.WriteLine(false || true);
    Console.WriteLine(false || false);

    /* output:
    True
    True
    True
    False
    */
    ```
- 왼쪽 피연산자가 `true`일 때는 오른쪽 연산자의 값을 확인하지 않는다.
    ```cs
    bool Right()
    {
        Console.WriteLine("Right");
        return true;
    }

    bool Left = false;

    Console.WriteLine(Left || Right());
    Console.WriteLine(!Left || Right());

    /* output:
    Right
    True
    True
    */
    ```

<br>

## Nullable bool (bool?)

<br>

- `bool?` 피연산자의 경우 `!`, `&`, `|`, `^`를 지원하지만 `&&`, `||` 연산자는 지원하지 않는다.
- `bool?` 피연산자의 연산 결과는 `bool`과 다르게 나타난다.
- 아래는 `bool?` 피연산자의 연산 결과이다.
    |X|Y|!X|X & Y|X \| Y|X ^ Y|
    |:-:|:-:|:--:|:-:|:-:|:--:|
    |true|true|false|true|true|false|
    |true|false||false|true|true|
    |true|null||null|true|null|
    |false|true|true|false|true|true|
    |false|false||false|false|false|
    |false|null||false|null|null|
    |null|true|null|null|true|null|
    |null|false||false|null|null|
    |null|null||null|null|null|

<br>

## 할당 연산자

<br>

- 할당 연산자 `&=`, `|=`, `^=` 는 연산과 동시에 할당이 가능해지는 복합 할당 식을 가능하게 한다.
- 다음 두 식은 동일한 연산을 수행한다.
    ```cs
    bool X = true;

    X = X & false;
    X &= false;
    ```
- 할당 연산자는 다음과 같이 사용한다.
    ```cs
    bool AND = true;
    bool OR = true;
    bool XOR = true;

    AND &= false;
    OR |= false;
    XOR ^= false;

    Console.WriteLine(AND);
    Console.WriteLine(OR);
    Console.WriteLine(XOR);

    /* output:
    False
    True
    True
    */
    ```

<br>

## 참조 자료

<br>

- [부울 논리 연산자 - AND, OR, NOT, XOR](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/boolean-logical-operators)
- [C# - Language - 논리형 (Boolean)](https://peponi-paradise.tistory.com/entry/C-Language-%EB%85%BC%EB%A6%AC%ED%98%95-Boolean)
- [C# - Language - Bitwise and shift operators](https://peponi-paradise.tistory.com/entry/C-Language-Bitwise-and-shift-operators)