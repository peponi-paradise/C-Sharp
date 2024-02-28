## Introduction

<br>

- 람다 기호 (`=>`) 는 두 가지 기능을 한다.
    - 람다 연산자 : [람다 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-expressions)에서 `=>`는 입력 매개변수와 본문을 구분한다.
    - 식 본문 정의 : [식 본문 정의](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition)에서 `=>`는 멤버 이름과 구현을 구분한다.

<br>

## 람다 연산자

<br>

- 다음은 람다 식을 정의하는 방법을 보여준다.
    ```cs
    Action foo = () => Console.WriteLine("Hello, World!");
    Func<string> bar = () => "Hello, World!";
    ```
- 형식 추론이 가능한 경우, 람다 식의 매개 변수에 대한 형식 선언을 생략할 수 있다.
    ```cs
    Func<string, string> foo = message => $"Hello {message}";   // 매개 변수가 하나인 경우 괄호 () 생략 가능
    Func<string, string, string> bar = (messageA, messageB) => $"{messageA}{messageB}";
    ```
- 형식 추론이 불가능한 경우 형식을 지정할 수 있다.
    ```cs
    Func<string, string, string> bar = (string messageA, string messageB) => $"{messageA}{messageB}";
    ```
- [무시 항목](https://learn.microsoft.com/ko-kr/dotnet/csharp/fundamentals/functional/discards)을 이용하여 사용하지 않을 매개 변수를 지정할 수 있다.
    ```cs
    Func<string, string, string> bar = (_, messageB) => $"{messageB}";
    ```
- [LINQ](https://learn.microsoft.com/ko-kr/dotnet/csharp/linq/)를 사용할 때도 람다 식을 이용할 수 있다.
    ```cs
    List<int> foo = new() { 1, 2, 3, 4, 5 };
    var bar = foo.Where(x => x > 3);

    Console.WriteLine(string.Join(", ", bar));

    /* output:
    4, 5
    */
    ```

<br>

## 식 본문 정의

<br>

- 식 본문 정의의 구문은 다음과 같다.
    ```cs
    member => expression;
    ```
    - `expression`의 반환 형식은 멤버의 반환 형식이거나 암시적으로 변환할 수 있어야 한다.
    - 멤버의 형식이 다음과 같은 경우, `expression`은 [문 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/language-specification/statements#137-expression-statements)으로 정의한다.
        1. void
        2. 생성자
        3. 소멸자
        4. 프로퍼티 또는 인덱서의 setter
- 다음은 식 본문 정의의 구현을 보여준다.
    ```cs
    internal class Foo
    {
        private string _name;

        public string Property
        {
            get => _name;
            set => _name = value;
        }

        public string ReadonlyProperty => _name;

        public Foo(string name) => _name = name;

        public string GetName() => ReadonlyProperty;
    }
    ```

<br>

## 참조 자료

<br>

- [람다 식(=>) 연산자는 람다 식을 정의합니다.](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-operator)
- [람다 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-expressions)
- [식 본문 정의](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition)