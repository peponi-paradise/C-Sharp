
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
    - `int` 리터럴
        ```cs
        char A = (char)80;  // P
        ```
- `UTF-16` 테이블은 [ASCII 테이블](https://ko.wikipedia.org/wiki/ASCII) 위에 확장되어있다. 자주 사용하는 영문 및 숫자, 특수문자의 경우 ASCII 테이블을 참조하면 된다.
- 한글 코드의 경우 `AC00` (가) ~ `D7FF` (값 없음) 까지 할당되어있다. 
- 자세한 내용은 [TITUS](http://titus.uni-frankfurt.de/unicode/unitestx.htm) 페이지를 참조한다.

<br>

## char 연산

<br>

- 사용하는 일이 드물지만, `char`는 연산을 지원한다.
    - 증가, 감소, 비교, 상등 연산자를 지원한다.
    -