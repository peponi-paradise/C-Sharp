## 1. Introduction

<br>

- `switch`문은 제공된 식과 패턴을 이용해 실행할 문을 선택한다.
    - `switch`식은 [Switch expression](https://peponi-paradise.tistory.com/entry/C-Language-Switch-expression)을 참조한다.
- 제공된 식과 패턴이 일치할 때만 실행한다.
- `switch`문에서 지원되는 패턴은 다음 목록을 참조한다.
    1. [Type, declaration patterns](https://peponi-paradise.tistory.com/entry/C-Language-Type-declaration-patterns)
    2. [Constant pattern](https://peponi-paradise.tistory.com/entry/C-Language-Constant-pattern)
    3. [Relational patterns](https://peponi-paradise.tistory.com/entry/C-Language-Relational-patterns)
    4. [Logical patterns](https://peponi-paradise.tistory.com/entry/C-Language-Logical-patterns)
    5. [Property, positional patterns](https://peponi-paradise.tistory.com/entry/C-Language-Property-positional-patterns)
    6. [Discard, var pattern](https://peponi-paradise.tistory.com/entry/C-Language-Discard-var-pattern)
    7. [List patterns](https://peponi-paradise.tistory.com/entry/C-Language-List-patterns)

<br>

## 2. Example

<br>

- 기본적으로 `switch`문은 다음과 같이 사용한다.
    ```cs
    switch(expression)
    {
        case pattern:
        break;
    }
    ```
- 아래는 위 표현을 이용한 산술 연산 예시다.
    ```cs
    Console.WriteLine(Calculate(10));

    private int Calculate(int value)
    {
        switch(value)
        {
            case <= 10:
                value += 5;
                break;
        }

        return value;
    }

    /* output:
    15
    */
    ```
- 패턴은 한 번에 여러 개를 지정할 수 있다.
    ```cs
    Console.WriteLine(Calculate(11));

    private int Calculate(int value)
    {
        switch(value)
        {
            case <= 10:
            case > 10:
                value += 5;
                break;
        }

        return value;
    }

    /* output:
    16
    */
    ```
- 패턴을 추가하여 여러 패턴에 대응하는 `switch`문을 만들 수 있다.
    ```cs
    Console.WriteLine(Calculate(12));

    private int Calculate(int value)
    {
        switch (value)
        {
            case <= 10:
                value += 5;
                break;
            case <= 11:
                value += 4;
                break;
            case <= 12:
                value += 3;
                break;
        }

        return value;
    }

    /* output:
    15
    */
    ```
- `default` 패턴을 이용하여 다른 모든 조건을 만족하지 않을 경우 실행할 문을 지정할 수 있다.
    ```cs
    Console.WriteLine(Calculate(13));

    private int Calculate(int value)
    {
        switch (value)
        {
            case <= 10:
                value += 5;
                break;
            case <= 11:
                value += 4;
                break;
            case <= 12:
                value += 3;
                break;
            
            default:
                value += 2;
                break;
        }

        return value;
    }

    /* output:
    15
    */
    ```

<br>

## 3. Case guards

<br>

- 지정한 패턴으로 충분하지 않은 경우 케이스 가드를 이용하여 추가 조건을 지정할 수 있다.
    ```cs
    Console.WriteLine(Calculate(5));
    Console.WriteLine(Calculate(-1));

    private int Calculate(int value)
    {
        switch(value)
        {
            case > 0 and <= 10:
                value += 5;
                break;
        }

        return value;
    }

    /* output:
    10
    -1
    */
    ```

<br>

## 4. 참조 자료

<br>

- [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)