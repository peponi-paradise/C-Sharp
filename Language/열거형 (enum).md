## Introduction

<br>

- `enum` (열거형) 은 기본적으로 `int` 형식으로 만들어진 `정수 숫자` 형식의 값 형식이다.
- `enum`은 `int` 뿐만 아니라 다른 정수 숫자 형식 (`short`, `long` 등) 을 이용하여 정의할 수 있다.
- `정수 숫자` 형식의 값을 가지는 만큼, `enum`에 정의되어 있지 않더라도 기본값은 0이다.

<br>

## enum 정의

<br>

- 기본적인 `enum`의 정의 방법은 아래와 같다.
    ```cs
    // 값을 명시하지 않는 경우 : 0부터 순차 증가

    enum StatusCode
    {
        Idle,             // 0
        Run,              // 1
        Error             // 2
    }


    // 값을 명시하는 경우 : 해당 값으로 적용

    enum StatusCode
    {
        Idle = 10,        
        Run,              // 11
        Error = 1000      
    }


    // 숫자 형식을 명시하는 경우 : 해당 형식으로 적용, 숫자 형식의 최대값을 넘을 수 없다.

    enum StatusCode : byte  // byte : 0 ~ 255
    {
        Idle = 10,        
        Run = 100,        
        Error = 1000      // CS0031 : '1000' 상수 값을 'byte'(으)로 변환할 수 없습니다.
    }
    ```

<br>

## enum 초기화

<br>

- 아래와 같은 방법으로 `enum`을 초기화 할 수 있다.
    ```cs
    enum StatusCode
    {
        Idle = 10,        
        Run = 100,        
        Error = 1000      
    }

    StatusCode code = (StatusCode)10;
    StatusCode code2 = StatusCode.Run;

    
    // enum에는 0이 정의되지 않았지만, 0이 기본값이다.

    StatusCode code3 = default;         // 0


    // enum 정의 외 정수를 할당할 수도 있다. 이 경우, 매칭되는 키가 없어 값이 키가 된다.
    // 비트 플래그 사용 시 이런 식으로 쓰기도 한다.

    StatusCode code4 = (StatusCode)1;
    ```

<br>

## enum 비트 플래그

<br>

- `enum`의 경우 [비트 연산자 (`|`, `&`)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#enumeration-logical-operators)를 통해 선택 및 조합이 가능하다.
- 해당 기능을 사용하려면 `enum` 선언 시 `[Flags]` 특성과 함께 `2의 제곱` 형태로 값을 정의한다.
- `enum`의 기본값이 `0`이기 때문에, 0 값은 `None`, `Empty` 등으로 지정해주는 것이 좋다.

<br>

```cs
// 10진수 표현

[Flags]
enum StatusCode
{
    None = 0,         // 0
    Idle = 1,         // 1
    Run = 2,          // 2
    Warning = 4,      // 4
    Error = 8         // 8
}

// 2진수 표현

[Flags]
enum StatusCode
{
    None = 0b_0,       // 0
    Idle = 0b_1,       // 1
    Run = 0b_10,       // 2
    Warning = 0b_100,  // 4
    Error = 0b_1000    // 8
}

StatusCode code = StatusCode.Idle | StatusCode.Warning | StatusCode.Error;  // 13

Console.WriteLine(code);    // Idle, Warning, Error

code = (StatusCode)13;

Console.WriteLine(code);    // Idle, Warning, Error
```

<br>

## enum 변환

<br>

- `enum`은 `정수 형식`과 상호 명시적 변환이 가능하다.
- `enum`을 `정수 형식`으로 변환하는 경우, 키에 매칭되는 값으로 반환된다.
- `enum`에는 `ToString()` 메서드가 있다. 호출 시 매칭되는 키가 반환된다.

<br>

```cs
[Flags]
enum StatusCode
{
    None = 0,         // 0
    Idle = 1,         // 1
    Run = 2,          // 2
    Warning = 4,      // 4
    Error = 8         // 8
}

StatusCode code = (StatusCode)2;

string keyValue = code.ToString();  // 키 반환 : Run
int codeValue = (int)code;          // 값 반환 : 2
```

<br>

- 아래는 `string`을 `enum`으로 변환 및 정의 여부 조회 방법 이다.

<br>

```cs
[Flags]
enum StatusCode
{
    None = 0,       // 0
    Idle = 1,       // 1
    Run = 2,        // 2
    Warning = 4,    // 4
    Error = 8       // 8
}

// string 변환 방법

string key = "Warning";
StatusCode code = code = Enum.Parse<StatusCode>("Warning");      // 키가 정의되어 있지 않은 경우 예외가 발생한다.
bool isSuccess = Enum.TryParse<StatusCode>("Warning", out StatusCode code2);


// 정의 여부 조회

bool isDefined = Enum.IsDefined(typeof(StatusCode), "Hello");   // false
```

<br>

## 참조 자료

<br>

- [열거형(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/enum)