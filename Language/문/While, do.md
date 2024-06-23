## 1. Introduction

<br>

- `while`, `do` 문은 이하 블록을 반복 실행한다.
- `while`, `do`의 실행 차이점은 아래와 같다.
    - `while` : 주어진 조건식이 `true`일 때 실행
    - `do` : 주어진 조건식이 `false`일지라도 반드시 한 번 이상 실행
- 반복문에서는 [break 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/jump-statements#the-break-statement), [continue 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/jump-statements#the-continue-statement)을 통해 반복 중단 또는 다음 반복 실행을 할 수 있다.

<br>

## 2. while

<br>

- `while` 문은 조건식이 `true`일 때 이하 블록을 반복 실행한다.
- `while` 문의 구성은 아래와 같다.
    ```cs
    while (condition)
    {
    }
    ```
    - `condition`
        - 루프 실행을 위한 조건을 지정한다.
        - 조건식은 `boolean` 식으로 설정하며, `true`일 때 실행된다.
- `while` 문은 다음과 같이 사용할 수 있다.
    ```cs
    var index = 0;
    while (index < 5)
    {
        Console.WriteLine(index++);
    }

    /* output:
    0
    1
    2
    3
    4
    */
    ```

<br>

## 3. do

<br>

- `do` 문은 조건식이 `true`일 때 지정 블록을 반복 실행한다.
- 블록 실행 후 조건을 판단하기 때문에 `1회 이상 실행`을 보장한다.
- `do` 문은 다음과 같이 사용할 수 있다.
    ```cs
    var index = 0;
    do
    {
        Console.WriteLine(index++);
    }
    while (index < 5);

    /* output:
    0
    1
    2
    3
    4
    */
    ```

<br>

## 4. 참조 자료

<br>

- [반복 문 - do, while](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements)