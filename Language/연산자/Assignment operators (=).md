## Introduction

<br>

- 대입 연산자 (`=`) 는 오른쪽 피연산자의 값을 왼쪽 피연산자에 할당한다.
  - 변수, 속성, 인덱서에 할당 가능하다.
- 오른쪽 피연산자의 형식은 왼쪽 피연산자의 형식과 동일하거나 암시적으로 변환할 수 있어야 한다.

<br>

## `=` 연산자

<br>

- 다음 두 식은 동일한 결과를 가져온다.
    ```cs
    a = b = c
    a = (b = c)
    ```
    - 즉, `=` 연산자는 오른쪽에서부터 왼쪽으로 연산을 수행한다.

<br>

### 값 형식

<br>

- 왼쪽 피연산자가 [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)인 경우 `=` 연산은 오른쪽 피연산자의 값을 복사한다.
    ```cs
    int X = 1;
    int Y = 2;

    X = Y;

    Console.WriteLine(X);
    Console.WriteLine(Y);

    Y = 10;

    Console.WriteLine(X);
    Console.WriteLine(Y);

    /* output:
    2
    2
    2
    10
    */
    ```

<br>

### 참조 형식

<br>

- 왼쪽 피연산자가 [참조 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D)인 경우 `=` 연산은 오른쪽 피연산자의 참조를 복사한다.
    ```cs
    public class Foo
    {
        public int Bar { get; set; }
    }
    ```
    ```cs
    var foo = new Foo();
    foo.Bar = 5;
    var newFoo = foo;
    newFoo.Bar = 10;

    Console.WriteLine(foo.Bar);
    Console.WriteLine(newFoo.Bar);

    /* output:
    10
    10
    */
    ```

<br>

## `= ref` 할당

<br>

- ref 할당 (`= ref`) 왼쪽 피연산자를 오른쪽 피연산자의 별칭으로 만든다.
    - 왼쪽 피연산자는 [ref](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref) 변수, [ref 필드](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/ref-struct#ref-fields), 메서드 파라미터 ([in](https://peponi-paradise.tistory.com/entry/C-Language-In-keyword-Parameter-modifier), [out](https://peponi-paradise.tistory.com/entry/C-Language-Out-keyword-Parameter-modifier), [ref](https://peponi-paradise.tistory.com/entry/C-Language-Ref-keyword-Parameter-modifier)) 가 될 수 있다.

    ```cs
    int X = 1;
    ref int Y = ref X;

    Y = 5;  // 별칭으로 만들었기 때문에 X 값이 업데이트된다

    Console.WriteLine(X);

    /* output:
    5
    */
    ```

<br>

## 복합 할당 식

<br>

- 복합 할당은 아래의 이진 연산자 (`op`) 를 통해 사용할 수 있다.
    - Arithmetic operators
        - [+, - operator](https://peponi-paradise.tistory.com/entry/C-Language-Arithmetic-operator)
        - [*, /, % operator](https://peponi-paradise.tistory.com/entry/C-Language-arithmetic-operator-1)
    - [Logical operators (!, &, |, ^, &&, ||)](https://peponi-paradise.tistory.com/entry/C-Language-Logical-operators)
    - [Bitwise and shift operators (~, &, |, ^, <<, >>, >>>)](https://peponi-paradise.tistory.com/entry/C-Language-Bitwise-and-shift-operators)
- 이진 연산자를 이용한 복합 할당 식은 아래와 같다.
    ```cs
    X op= Y

    // 위 식은 아래의 연산을 수행한다.

    X = X op Y
    ```
- 위 예제의 두 식의 연산은 동일하지만 캐스팅이 다르게 될 수 있다. 조금 더 자세히 풀어보면 아래와 같다.
    ```cs
    byte X = 255;
    byte Y = 1;

    // X op= Y. 아래 코드는 실제로는 수행 불가 (byte 연산 시 int로 변환됨)
    X = (int)X + (int)Y;

    // X = X op Y
    X = (byte)((int)X + (int)Y);
    ```
- 위 예제로 알 수 있듯 캐스팅이 다르게 진행되는데, 이로 인해 의도치 않은 결과를 가져올 수 있다.
    ```cs
    // X = X op Y
    var Z = X + Y;

    Console.WriteLine(Z);
    Console.WriteLine(Z.GetType());

    // X op= Y
    X += Y;

    Console.WriteLine(X);
    Console.WriteLine(X.GetType());

    /* output:
    256
    System.Int32
    0
    System.Byte
    */
    ```
- 따라서 연산 시 형식에 대한 주의가 필요하다.

<br>

## 참조 자료

<br>

- [대입 연산자(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/assignment-operator)
- [ref(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref)
- [ref 필드](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/ref-struct#ref-fields)
- [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)
- [참조 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D)
- [C# - Language - In keyword (Parameter modifier)](https://peponi-paradise.tistory.com/entry/C-Language-In-keyword-Parameter-modifier)
- [C# - Language - Out keyword (Parameter modifier)](https://peponi-paradise.tistory.com/entry/C-Language-Out-keyword-Parameter-modifier)
- [C# - Language - Ref keyword (Parameter modifier)](https://peponi-paradise.tistory.com/entry/C-Language-Ref-keyword-Parameter-modifier)
- [C# - Language - +, - operator (Arithmetic operator)](https://peponi-paradise.tistory.com/entry/C-Language-Arithmetic-operator)
- [C# - Language - *, /, % arithmetic operator](https://peponi-paradise.tistory.com/entry/C-Language-arithmetic-operator-1)
- [C# - Language - Logical operators (!, &, |, ^, &&, ||)](https://peponi-paradise.tistory.com/entry/C-Language-Logical-operators)
- [C# - Language - Bitwise and shift operators (~, &, |, ^, <<, >>, >>>)](https://peponi-paradise.tistory.com/entry/C-Language-Bitwise-and-shift-operators)