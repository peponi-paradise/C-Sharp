## Introduction

<br>

- `string`은 0자 이상의 유니코드 ([char](https://peponi-paradise.tistory.com/entry/C-Language-%EB%AC%B8%EC%9E%90%ED%98%95-char), `UTF-16`) 문자 시퀀스를 나타낸다. .NET 형식은 [System.String](https://learn.microsoft.com/ko-kr/dotnet/api/system.string?view=net-7.0)이다.
- `string` 객체의 최대 크기는 2GB, 약 10억 자이다.
- `string` 객체는 내용을 변경할 수 없다.
- 여기서는 기본적인 내용만 알아본다. `string`을 다루는 자세한 방법은 아래 링크를 참조하여 다음에 정리할 예정이다.
    - [문자열 및 문자열 리터럴](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/strings/)
  - [문자열이 숫자 값을 나타내는지 확인하는 방법(C# 프로그래밍 가이드)](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/strings/how-to-determine-whether-a-string-represents-a-numeric-value)

<br>

## string 초기화

<br>

- 아래와 같은 방법으로 string을 초기화 할 수 있다.

```cs
// String literal

string string1 = "Sample string";
string string2 = @"Sample Path : C:\Temp\SampleText.txt";
string string3 = "Sample Path : C:\\Temp\\SampleText.txt";
```
```cs
// From char

char[] chars = { 'A', 'B', 'C' };
string string4 = new string(chars);
```
```cs
// Repeated string

string string5 = new string('A', 5);
```
```cs
// From bytes

byte[] bytes = { 0x41, 0x42, 0x43 };    // { A, B, C }
string string6 = Encoding.Default.GetString(bytes);
```
```cs
// Raw string literal (C# 11)

string string7 = """This is "Raw string literal".""";
string jsonString = """
    {
        "SampleValue": 1
    }
    """;
```

<br>

## string 연산자

<br>

- `string`은 비교 2가지 (`==`, `!=`) 및 `+` 연산자를 제공한다.
- `string`의 비교 연산자는 다른 참조 형식과는 다르게 값을 비교하도록 정의되어 있다.
- `+` 연산자는 두 문자열을 연결한다. 
    ```cs
    string test1 = "ABC";
    string test2 = "AB";

    Console.WriteLine(test1 == test2);                          // False
    Console.WriteLine(object.ReferenceEquals(test1, test2));    // False

    test2 += "C";   // 기존에 할당된 문자열은 버려지고 (AB), 새 문자열이 test2에 할당된다 (ABC)

    Console.WriteLine(test1 == test2);                          // True
    Console.WriteLine(object.ReferenceEquals(test1, test2));    // False
    ```
- `string`은 [인덱서 연산자\[\]](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#indexer-operator-) 또한 지원한다.
    ```cs
    string test1 = "ABC";

    Console.WriteLine(test1[0]);    // A
    Console.WriteLine(test1[1]);    // B
    Console.WriteLine(test1[2]);    // C
    ```

<br>

## string 유효성 검사

<br>

- `string`은 `null`, `공백` 등의 값을 가질 수 있고, 이는 유효하지 않은 값이 될 수 있다.<br>아래는 `string`이 유효한 값을 가지고 있는지 파악하는 방법이다.
    ```cs
    // 1. string.IsNullOrEmpty(string)
    // string 값이 null 또는 string.Empty인 경우 True 반환

    bool isValid = string.IsNullOrEmpty(string.Empty);     // True
    isValid = string.IsNullOrEmpty(null);                  // True
    isValid = string.IsNullOrEmpty(" ");                   // False
    isValid = string.IsNullOrEmpty("ABC");                 // False
    ```
    ```cs
    // 2. string.IsNullOrWhiteSpace(string)
    // string 값이 null 또는 string.Empty, whitespace (공백 문자) 인 경우 True 반환

    bool isValid = string.IsNullOrEmpty(string.Empty);     // True
    isValid = string.IsNullOrEmpty(null);                  // True
    isValid = string.IsNullOrEmpty(" ");                   // True
    isValid = string.IsNullOrEmpty("ABC");                 // False
    ```
- 공백 문자 (` `) 가 유효한 문자인 경우도 있다. 필요에 맞게 사용하도록 한다.

<br>

## 참조 자료

<br>

- [기본 제공 참조 형식(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/reference-types#the-string-type)
- [String 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.string?view=net-7.0)