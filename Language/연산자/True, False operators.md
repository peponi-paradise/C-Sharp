## Introduction

<br>

- `true` 및 `false` 연산자는 각각 피연산자의 상태를 boolean으로 나타낸다.
  - 객체가 `true`인지 확인할 때 `true` 연산자를 확인한다.
  - 객체가 `false`인지 확인할 때 `false` 연산자를 확인한다.
- `true`, `false` 연산자는 반드시 같이 오버로드 되어야 하며, 구현에 따라 모두 `true`, `false`를 반환할 수도 있다.
- `true` 연산자는 주로 [문 키워드](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/statement-keywords), [논리 연산자](https://peponi-paradise.tistory.com/entry/C-Language-Logical-operators)의 조건 확인에 사용된다.
- `false` 연산자는 주로 [논리 연산자](https://peponi-paradise.tistory.com/entry/C-Language-Logical-operators)의 조건 확인에 사용된다.

<br>

## Example

<br>

- 아래의 예제는 `true`, `false`, `&` 연산자를 오버로드하여 정의하는 방법을 보여준다.
- 예제에서는 `&&` 연산 수행 시 필드 Value가 `null`일 때만 `false`를 반환하도록 한다.

```cs
public class NullToFalse
{
    public bool? Value;

    public static bool operator true(NullToFalse a) => a.Value != null ? true : false;

    public static bool operator false(NullToFalse a) => a.Value == null;

    public static NullToFalse operator &(NullToFalse a, NullToFalse b)
    {
        if (a.Value == null || b.Value == null) return new() { Value = null };
        else return new() { Value = true };
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        NullToFalse A = new() { Value = true };
        NullToFalse B = new() { Value = null };

        var C = A && B;

        Console.WriteLine(C ? "True" : "False");
    }
}

/* output:
False
*/
```

- 위 에제에서는 다음과 같은 과정을 거쳐 `False`가 출력된다.
    1. A의 false 여부를 확인한다
        - Value가 true임에 따라 `false` operator에서 false를 반환한다.
    2. `&` operator로 진입하여, B의 Value 값이 null임을 확인 후 `null` Value를 가진 새 객체를 반환한다.
    3. C의 true 여부를 확인한다.
        - Value가 null임에 따라 `false`를 반환한다.

<br>

## 참조 자료

- [true 및 false 연산자 - 개체를 부울 값으로 처리](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/true-false-operators)
- [문 키워드](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/statement-keywords)
- [C# - Language - Logical operators (!, &, |, ^, &&, ||)](https://peponi-paradise.tistory.com/entry/C-Language-Logical-operators)