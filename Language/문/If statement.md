## 1. Introduction

<br>

- `if`문은 제공된 식의 결과에 따라 실행하는 기능을 제공한다.
- 제공된 식의 결과가 `true`일 때만 실행한다.
- 코드의 분기는 `if`, `else` 키워드를 통해 진행한다.

<br>

## 2. Example

<br>

- 기본적으로 `if`문은 다음과 같이 사용한다.
    ```cs
    // expression이 true를 나타낼 때 실행

    if (expression)
    {
    }
    ```
- 아래는 위 표현을 이용한 산술 연산 예시다.
    ```cs
    Console.WriteLine(Calculate(10));

    private int Calculate(int value)
    {
        if (value <= 10)
        {
            value += 5;
        }

        return value;
    }

    /* output:
    15
    */
    ```

<br>

## 3. else

<br>

- `else` 키워드를 통해 `if`문이 `false`인 경우에 확인할 추가 조건을 지정할 수 있다.
    ```cs
    Console.WriteLine(Calculate(11));

    private int Calculate(int value)
    {
        if (value <= 10)
        {
            value += 5;
        }
        else
        {
            value += 4;
        }

        return value;
    }

    /* output:
    15
    */
    ```
- `else`에 `if` 키워드를 추가하여 다음과 같이 여러 조건을 만들 수 있다.
    ```cs
    Console.WriteLine(Calculate(12));

    private int Calculate(int value)
    {
        if (value <= 10)
        {
            value += 5;
        }
        else if (value <= 11)
        {
            value += 4;
        }
        else if (value <= 12)
        {
            value += 3;
        }    
        else
        {
            value += 2;
        }

        return value;
    }

    /* output:
    15
    */
    ```

<br>

## 4. 참조 자료

<br>

- [선택 문 - if, if-else 및 switch](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-if-statement)