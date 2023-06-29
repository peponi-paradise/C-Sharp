
## Introduction

<br>

- `char` (문자) 키워드는 [UTF-16](https://ko.wikipedia.org/wiki/UTF-16) 문자 1개를 나타낸다. .NET 형식은 `System.Char`이다.
- 기본값은 `\u0000`, 값의 범위는 `\u0000` ~ `\uFFFF` 이다. (16 bit)
- 흔히 사용하는 `string`의 경우 `char` 값의 시퀀스이다.

<br>

## char 초기화

<br>

- `char`는 다음 네 가지 방법을 이용하여 초기화할 수 있다.
    - 문자 리터럴
        ```cs
        char A = 'P';       // ' (따옴표)
        ```
    - 유니코드 16진수
        ```cs
        char A = '\u0050';  // P
        ```
    - 16진수
        ```cs
        char A = '\x0050';  // P

        // 16진수 초기화의 경우 앞자리 0 무시 가능

        char A = '\x050';   // P
        char A = '\x50';    // P
        ```
    - `숫자 형식` 리터럴
        ```cs
        char A = (char)80;  // P
        char A = (char)80.6 // P
        ```
- `UTF-16` 테이블은 [ASCII 테이블](https://ko.wikipedia.org/wiki/ASCII) 위에 확장되어있다. 자주 사용하는 영문 및 숫자, 특수문자의 경우 ASCII 테이블을 참조하면 된다.
- 한글 코드의 경우 `AC00` (가) ~ `D7FF` (값 없음) 까지 할당되어있다. 
- 자세한 내용은 [TITUS](http://titus.uni-frankfurt.de/unicode/unitestx.htm) 페이지를 참조한다.

<br>

## char 연산

<br>

- 사용하는 일이 드물지만, `char`는 연산을 지원한다.
    - 증가, 감소, 비교, 상등 연산자를 지원한다.
    - `char`가 피연산자인 경우, 산술 또는 비트 연산자는 `int` 형식으로 연산이 수행된다.
        - 결과를 `char`로 확인하고 싶은 경우, `char`로 캐스팅을 수행한다.
    ```cs
    char A = 'A';               // 41
    char B = '\uAC01';          // AC01 = 나

    B = (char)(B - (char)1);    // AC00 = 가
    B = (char)(A + B);          // AC41 = 걁
    ```

<br>

## char 변환

<br>

- 모든 숫자 형식은 `char` 형식으로 명시적 변환이 가능하다.
- `char` 형식은 다음 숫자 형식으로 변환할 수 있다.
    |변환 방법|대상 형식|
    |---|---|
    |암시적 변환|정수 형식 : `ushort`, `int`, `uint`, `long`, `ulong`<br>실수 형식 : `float`, `double`, `decimal`|
    |명시적 변환|정수 형식 : `sbyte`, `byte`, `short`|

    ```cs
    char C = 'P';           // 80

    int A = C;              // 80
    double B = C;           // 80
    short S = (short)C;     // 80

    C = '\uAC00';           // 가, 44032

    A = C;                  // 44032
    B = C;                  // 44032
    S = (short)C;           // -21504
    ```

<br>

## 참조 자료

<br>

- [char(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/char)
- [C# - Language - 숫자 형식 변환](https://peponi-paradise.tistory.com/entry/C-Language-%EC%88%AB%EC%9E%90-%ED%98%95%EC%8B%9D-%EB%B3%80%ED%99%98)