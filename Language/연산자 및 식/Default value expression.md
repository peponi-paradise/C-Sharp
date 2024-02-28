## Introduction

<br>

- 기본값 식은 주어진 형식의 기본값을 생성한다.
- [default 키워드](https://peponi-paradise.tistory.com/entry/C-Language-Literal-keywords#default-1)를 이용하여 `default 연산자`, `default 리터럴`을 호출할 수 있다.

<br>

## default 연산자

<br>

- default 연산자의 인수는 형식 또는 형식 매개 변수가 되어야 한다.
    ```cs
    Console.WriteLine(default(bool));
    Console.WriteLine(default(int));

    WriteDefaultValue<bool>();
    WriteDefaultValue<int>();

    void WriteDefaultValue<T>() => Console.WriteLine(default(T));

    /* output:
    False
    0
    False
    0
    */
    ```

<br>

## default 리터럴

<br>

- 컴파일러가 형식을 유추할 수 있는 경우 `default` 키워드를 사용하여 기본값을 생성할 수 있다.
- 이 때 생성되는 기본값은 `default` 연산자와 동일하다.
    ```cs
    int X = default;

    Console.WriteLine(X);
    WriteValue(default);
    WriteValue(5);
    Console.WriteLine(GetDefaultValue<bool>());

    void WriteValue(int value = default) => Console.WriteLine(value);

    T? GetDefaultValue<T>() => default;

    /* output:
    0
    0
    5
    False
    */
    ```

<br>

## 참조 자료

<br>

- [기본값 식 - 기본값 생성](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/default#default-operator)
- [C# - Language - Literal keywords (null, default, bool)](https://peponi-paradise.tistory.com/entry/C-Language-Literal-keywords)