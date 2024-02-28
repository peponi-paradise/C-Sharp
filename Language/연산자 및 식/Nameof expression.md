## Introduction

<br>

- `nameof` 식은 변수, 형식, 멤버의 이름을 문자열 상수로 생성한다.
- 컴파일 타임에 계산되어 상수로 변환된다.
- 형식 또는 네임스페이스가 피연산자에 포함되더라도 생성된 이름은 [정규화](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/language-specification/basic-concepts#783-fully-qualified-names)되지 않는다.
- 피연산자가 [축자 식별자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/tokens/verbatim)인 경우 `@` 문자는 이름에서 제외된다.

<br>

## Example

<br>

```cs
static void Main(string[] args)
{
    Console.WriteLine(nameof(Main));
    Console.WriteLine(nameof(System));
    Console.WriteLine(nameof(System.Console));             // 정규화되지 않고 Console만 출력

    Console.WriteLine(nameof(System.Drawing.Point.X));     // 정규화되지 않고 X만 출력
    Console.WriteLine(nameof(System.Drawing.Point.Y));     // 정규화되지 않고 Y만 출력

    var @int = 5;
    Console.WriteLine(nameof(@int));                           // int만 출력
}

/* output:
Main
System
Console
X
Y
int
*/
```

- 이름이 내용과 동일한 변수 또는 멤버, 변수 검사 등의 경우 `nameof`를 사용하면 관리를 용이하게 할 수 있다.
    ```cs
    string paramA = nameof(paramA);
    if (string.IsNullOrWhiteSpace(paramA))
    {
        throw new ArgumentException($"{nameof(paramA)} is catched by {nameof(string.IsNullOrWhiteSpace)}", nameof(paramA));
    }
    ```
- C# 11부터는 메서드 또는 메서드 파라미터 특성 내에서 `nameof`가 사용 가능하다.
    ```cs
    [Custom(nameof(value))]
    void Foo(string value)
    {
        [Custom(nameof(localValue))]
        void Local(string localValue) { }

        var lambda = (string str1, [Custom(nameof(str1))] string str2) => { };
    }

    internal class CustomAttribute : Attribute
    {
        public CustomAttribute(string name) { }
    }
    ```

<br>

## 참조 자료

<br>

- [nameof 식(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/nameof)