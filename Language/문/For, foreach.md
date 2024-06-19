## 1. Introduction

<br>

- `for`, `foreach` 문은 이하 블록을 반복 실행한다.
- `for`, `foreach`의 실행 차이점은 아래와 같다.
    - `for` : 주어진 조건식이 `true`일 때 실행
    - `foreach` : 주어진 컬렉션 전체 실행
- 반복문에서는 [break 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/jump-statements#the-break-statement), [continue 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/jump-statements#the-continue-statement)을 통해 반복 중단 또는 다음 반복 실행을 할 수 있다.

<br>

## 2. `for` 문

<br>

- `for` 문은 주어진 조건식이 `true`일 때 블록을 실행한다.
- `for` 문의 구성은 아래와 같다.
    ```cs
    for ( initializer; condition; iterator)
    {
    }
    ```
    - `initializer`
        - `for` 문의 최초 진입 시 1회 실행된다.
        - 일반적으로 루프 초기화 요소를 설정한다.
        - `initializer`에 선언된 변수는 루프 외부에서 접근할 수 없다.
    - `condition`
        - 루프 실행을 위한 조건을 지정한다.
        - 조건식은 `boolean` 식으로 설정하며, `true`일 때 실행된다.
    - `iterator`
        - 루프 블록 실행 후 수행하는 작업을 정의한다.
        - `iterator` 섹션에는 다음 식이 포함될 수 있다.
            - [증감 연산](https://peponi-paradise.tistory.com/entry/C-Language-Arithmetic-operator#%EC%A6%9D%EA%B0%90%20%EC%97%B0%EC%82%B0%EC%9E%90-1)
            - [할당](https://peponi-paradise.tistory.com/entry/C-Language-Assignment-operator)
            - 메서드 호출
            - [await](https://peponi-paradise.tistory.com/entry/C-Language-Async-Await)
            - [new](https://peponi-paradise.tistory.com/entry/C-Language-New-operator)
- `for` 문의 `condition`을 제외한 각 섹션에는 여러 개의 식이 포함될 수 있다.
    - `initializer` : 루프 변수를 선언하지 않는 경우 여러 개의 식 사용 가능

<br>

### 2.1. Example

<br>

- 가장 간단한 `for` 문의 형태는 아래와 같다.
    ```cs
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(i);
    }

    /* output:
    0
    1
    2
    3
    4
    */
    ```
- 각 섹션에 여러 개의 식이 포함될 수 있다는 특성을 이용하여 아래와 같이 `for` 문을 구성할 수도 있다.
    ```cs
    int first = 1, last = 1, fibonacci = 0;
    for (Console.WriteLine("Start - "), Console.WriteLine($"first : {first}, last : {last}, fibonacci : {fibonacci}");
        fibonacci < 50;
        first = last, last = fibonacci, Console.WriteLine($"fibonacci : {fibonacci}"))
    {
        fibonacci = first + last;
    }

    /* output:
    Start -
    first : 1, last : 1, fibonacci : 0
    fibonacci : 2
    fibonacci : 3
    fibonacci : 5
    fibonacci : 8
    fibonacci : 13
    fibonacci : 21
    fibonacci : 34
    fibonacci : 55
    */
    ```

<br>

## 3. `foreach` 문

<br>

- 