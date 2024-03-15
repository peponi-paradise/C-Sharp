## Introduction

<br>

- 다음 멤버는 괄호 `()`를 사용하여 호출할 수 있다.
    - [생성자](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/constructors)
    - [메서드](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/methods)
    - [대리자](https://peponi-paradise.tistory.com/entry/C-Language-%EB%8C%80%EB%A6%AC%EC%9E%90-Delegate)

<br>

## Example

<br>

```cs
// Constructor

var foo = new List<int>();
```
```cs
// Method

Console.WriteLine("Hello, World!");
```
```cs
// Delegate

delegate void ConsoleWriter(string message);

static void Main(string[] args)
{
    ConsoleWriter writer = new(Write);

    writer("Hello, World!");
}

static void Write(string message) => Console.WriteLine(message);
```

<br>

## 참조 자료

<br>

- [멤버 액세스 및 null 조건부 연산자 및 식 - 호출 식 ()](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#invocation-expression-)
- [생성자](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/constructors)
- [메서드](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/methods)
- [대리자](https://peponi-paradise.tistory.com/entry/C-Language-%EB%8C%80%EB%A6%AC%EC%9E%90-Delegate)