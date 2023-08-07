## Introduction

<br>

- `this` 키워드는 현재 인스턴스를 가리킨다. 이를 통해 변수와 인스턴스를 분명하게 할 수 있다.
- `static` 메서드에서는 사용할 수 없다.
- [확장 메서드](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)의 변수 한정자로도 사용된다. 

<br>

### Example

<br>

```cs
public class Foo
{
    List<string> bar;

    // 변수와 인스턴스 구분
    public Foo(List<string> bar) => this.bar = bar;

    // 인덱서 선언
    public string this[int index] => bar[index];

    // 인스턴스를 변수로 전달
    public void MethodA() => MethodB(this);
    void MethodB(Foo foo) => Console.WriteLine(foo);
}
```

<br>

## 확장 메서드 한정자

<br>

- `this` 키워드는 [확장 메서드](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)의 한정자로도 사용된다.
- 확장 메서드는 정적 메서드이지만 인스턴스 메서드와 같이 호출이 가능하다.
- 확장 메서드를 정의할 때 첫 매개 변수에 형식을 지정하게 되는데, 여기에서 `this` 키워드가 사용된다.

<br>

```cs
// 확장 메서드 선언

public static class DoubleExtension
{
    public static double GetSquare(this double value) => value * value;
}
```
```cs
double foo = 3.14;

// 확장 메서드 호출

foo = foo.GetSquare();

// or

foo = DoubleExtension.GetSquare(foo);
```

<br>

## 참조 자료

<br>

- [this(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/this)
- [확장명 메서드(C# 프로그래밍 가이드)](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)