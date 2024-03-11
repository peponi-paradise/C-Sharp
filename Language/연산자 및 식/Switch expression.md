## Introduction

<br>

- `switch` 식은 패턴 매칭을 기반으로 식을 계산한다.
    - `switch` 문은 [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)을 참조한다.
- `switch` 식은 다음과 같이 구성한다.
    ```cs
    x switch
    {
        Condition1 => Value1,
        Condition2 => Value2
    };
    ```
    - x : 입력 값
    - 스위치 암 (arm) : `,`로 구분 된 각각의 식
        - Condition`X` : 조건 (패턴 또는 케이스 가드)
        - Value`X` : 반환 식
- `switch` 식의 결과는 조건이 `true`로 반환되는 첫번째 arm 반환 식의 값이다.
    - 상위 arm으로 인해 하위 arm의 값 출력이 불가한 경우 컴파일러에서 오류가 발생한다.
- `switch` 식에 사용 가능한 패턴은 [패턴 일치 - 패턴의 is 및 switch 식, 연산자 and, or 및 not](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns)을 참조한다.

<br>

## Example

<br>

```cs
int foo = 1;
string bar = foo switch
{
    0 => "Zero",
    1 => "One",
    2 => "Two"
};

Console.WriteLine(bar);

/* output:
One
*/
```

<br>

## 불완전 switch 식

<br>

- .NET 런타임은 `switch`식의 패턴과 입력 값이 일치하지 않는 경우 예외를 출력한다.
    - .NET Core 3.0 이상 : [System.Runtime.CompilerServices.SwitchExpressionException](https://learn.microsoft.com/ko-kr/dotnet/api/system.runtime.compilerservices.switchexpressionexception?view=net-8.0)
    - .NET Framework : [InvalidOperationException](https://learn.microsoft.com/ko-kr/dotnet/api/system.invalidoperationexception?view=net-8.0)
- 입력 가능한 모든 값을 처리하지 않는 `switch` 식이 선언되는 경우 컴파일러는 `CS8509`를 생성한다.
- 위의 예시 코드 또한 마찬가지의 경우로, 아래와 같이 [무시 패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns#discard-pattern) (`_`)을 이용하여 모든 입력 값을 처리하도록 할 수 있다.
    ```cs
    int foo = 10;
    string bar = foo switch
    {
        0 => "Zero",
        1 => "One",
        2 => "Two",
        _ => "Don't know"
    };

    Console.WriteLine(bar);

    /* output:
    Don't know
    */
    ```

<br>

## 케이스 가드

<br>

- 패턴이 조건 평가에 맞지 않은 경우 `케이스 가드`를 사용하여 세부 조건을 설정할 수 있다.
    ```cs
    (int X, int Y) XY = (3, 4);
    string foo = XY switch
    {
        { X: 0, Y: 0 } => "Both 0",
        { X: > 0, Y: > 0 } => "Both bigger than 0",
        { X: var x, Y: var y } when x < y => "Y is bigger than X"
    };

    Console.WriteLine(foo);

    /* output:
    Both bigger than 0
    */
    ```

<br>

## 참조 자료

<br>

- [switch 식 - switch 키워드를 사용하는 패턴 일치 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/switch-expression)
- [switch 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/selection-statements#the-switch-statement)
- [패턴 일치 - 패턴의 is 및 switch 식, 연산자 and, or 및 not](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns)
- [System.Runtime.CompilerServices.SwitchExpressionException](https://learn.microsoft.com/ko-kr/dotnet/api/system.runtime.compilerservices.switchexpressionexception?view=net-8.0)
- [InvalidOperationException](https://learn.microsoft.com/ko-kr/dotnet/api/system.invalidoperationexception?view=net-8.0)