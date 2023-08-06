## Introduction

<br>

- `using`문은 [IDisposable](https://learn.microsoft.com/ko-kr/dotnet/api/system.idisposable?view=net-7.0), [IAsyncDisposable](https://learn.microsoft.com/ko-kr/dotnet/api/system.iasyncdisposable?view=net-7.0) 객체가 scope 끝에 바로 dispose될 수 있도록 한다.
    - `using` 내에서 예외가 발생하더라도 dispose가 된다.
- `using`문 또는 선언으로 생성된 인스턴스는 읽기 전용이다. 재할당하거나 [ref](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref), [out](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/out-parameter-modifier) 매개변수로 전달이 불가능하다.

<br>

## Example

<br>

- `using`문은 다음과 같이 사용한다.
    ```cs
    using (Graphics g = Graphics.FromImage(image))
    {
    }

    // or

    using Graphics g = Graphics.FromImage(image);
    ```
    - 위의 예제와 같이 `using { }`을 선언하는 경우 해당 scope의 끝에 dispose된다.
    - 아래의 예제와 같이 지역 변수가 using 선언되면 해당 변수가 선언된 scope의 끝에 dispose된다.
- `using`문은 다음과 같이 사용할 수도 있다.
    ```cs
    Graphics g = Graphics.FromImage(image);

    using (g)
    { 
    }
    ```
- 아래와 같이, 하나의 `using`문에서 여러 인스턴스를 생성할 수도 있다.
    ```cs
    using (Graphics g1 = Graphics.FromImage(image1), g2 = Graphics.FromImage(image2))
    {
    }
    ```
    - 위와 같이 여러 인스턴스를 생성하는 경우, dispose는 생성의 역순으로 진행된다.

<br>

## 참조 자료

<br>

- [using 문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/using)