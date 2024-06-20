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

- `foreach` 문은 컬렉션의 요소를 반복하여 실행한다.
- [IEnumerable](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerable), [IEnumerable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ienumerable-1), [Span\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.span-1) 등을 지원한다.
- 구체적으로 `foreach` 문이 지원하는 형식은 아래와 같다.
    - `GetEnumerator` 메서드 구현
    - `GetEnumerator` 메서드의 반환 형식이 [IEnumerator](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerator?view=net-8.0)를 구현
    - `GetEnumerator` 메서드의 반환 형식이 `public Current` 속성과 `bool`을 반환하는 `MoveNext` 메서드를 구현
- `foreach` 문의 구성은 아래와 같다.
    ```cs
    foreach(T item in collection)
    { 
    }
    ```
    - `item`
        - `collection`의 각 요소
    - `collection`
        - 요소를 반복하여 실행할 컬렉션

<br>

### 3.1. Example

<br>

- 간단한 `foreach` 문의 형태는 다음과 같다.
    ```cs
    List<char> chars = ['H', 'E', 'L', 'L', 'O'];

    foreach(char item in chars)
    {
        Console.Write(item);
    }

    /* output:
    HELLO
    */
    ```
- 다음과 같이 `foreach` 문을 실행할 컬렉션이 비어있는 경우, 실행하지 않고 건너뛴다.
    ```cs
    List<char> chars = [];

    foreach (char item in chars)
    {
        Console.Write("A");
    }

    /* output:

    */
    ```
- `GetEnumerator` 메서드 반환 형식의 `Current` 속성이 [ref](https://peponi-paradise.tistory.com/entry/C-Language-Ref-keyword-Parameter-modifier) 형식인 경우 반복 변수에 `ref` 한정자를 사용할 수 있다.
    ```cs
    Span<int> ints = [1, 2, 3];

    foreach (ref int item in ints)
    {
        item++;
    }

    Console.WriteLine(string.Join(", ", ints.ToArray()));

    /* output:
    2, 3, 4
    */
    ```
- [var](https://peponi-paradise.tistory.com/entry/C-Language-implicitly-typed-local-variables-var) 키워드를 이용하여 `foreach` 문에서 반복 변수의 형식을 유추할 수도 있다.
    ```cs
    List<char> chars = ['H', 'E', 'L', 'L', 'O'];

    foreach (var item in chars)
    {
        Console.Write(item);
    }

    /* output:
    HELLO
    */
    ```

<br>

### 3.2. `await foreach`

<br>

- `await` 연산자를 `foreach` 문에 적용하여 컬렉션을 비동기적으로 접근할 수 있다.
- 기본적으로 각 요소는 진입 컨텍스트에서 처리된다.
    작업 컨텍스트를 유지하고 싶다면, [ConfigureAwait](https://learn.microsoft.com/ko-kr/dotnet/api/system.threading.tasks.taskasyncenumerableextensions.configureawait) 메서드의 `continueOnCapturedContext`를 `false`로 설정하여 진입 컨텍스트로 돌아오지 않을 수 있다.
- `await foreach` 문이 지원하는 형식은 다음과 같다.
    - [IAsyncEnumerable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.iasyncenumerable-1)를 구현하는 형식
    - `GetAsyncEnumerator` 메서드를 구현한 형식
        - 반환 형식이 `public Current` 속성 구현
        - 반환 형식의 `MoveNextAsync` 메서드가 [Task\<bool>](), [ValueTask\<bool>]()을 반환하거나 awaiter의 `GetResult` 메서드가 `bool`을 반환
- `await foreach` 문은 아래와 같이 사용한다.
    ```cs
    public async Task AwaitForeach()
    {
        await foreach(var response in GetResponseAsync())
        {
            Console.WriteLine(response);
        }
    }

    public async IAsyncEnumerable<HttpResponseMessage> GetResponseAsync(List<HttpRequestMessage> messages)
    {
        foreach (var message in messages)
        {
            var rtn = await client.SendAsync(message);
            yield return rtn;
        }
    }
    ```

<br>

## 4. 참조 자료

<br>

- [반복 문 - for, foreach](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/iteration-statements)
- [IEnumerable](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerable)
- [IEnumerable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ienumerable-1)
- [IEnumerator](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.ienumerator?view=net-8.0)
- [ConfigureAwait](https://learn.microsoft.com/ko-kr/dotnet/api/system.threading.tasks.taskasyncenumerableextensions.configureawait)
- [IAsyncEnumerable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.iasyncenumerable-1)
- [Iterating with Async Enumerables in C# 8](https://learn.microsoft.com/en-us/archive/msdn-magazine/2019/november/csharp-iterating-with-async-enumerables-in-csharp-8)