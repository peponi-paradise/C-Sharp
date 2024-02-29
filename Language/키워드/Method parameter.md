## Introduction

<br>

- C#의 형식에는 [참조(class)](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D), [값(struct)](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D) 형식이 있고 이를 메서드 매개 변수로 전달할 수 있다.
    - 참조 형식은 변수에 대한 참조의 복사본을 전달한다.
    - 값 형식은 변수의 복사본을 전달한다.
- 매개 변수를 선언할 때 사용 가능한 키워드는 아래와 같다.
    - [params](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/params) : 인수의 수가 가변인 것을 지정한다.
    - [in](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/in-parameter-modifier) : 매개 변수를 참조로 전달, 호출된 메서드에서 읽기 가능
    - [ref](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref) : 매개 변수를 참조로 전달, 호출된 메서드에서 읽기 / 쓰기 가능
    - [ref readonly](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/proposals/csharp-12.0/ref-readonly-parameters) : 매개 변수를 참조로 전달, 호출된 메서드에서 읽기 가능 (C# 12)
    - [out](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/out-parameter-modifier) : 매개 변수를 참조로 전달, 호출된 메서드에서 쓰기 가능

<br>

## Example

<br>

```cs
class Foo
{
    public int Integer;
}

struct Bar
{
    public int Integer;
}

static void ChangeValue(Foo foo) => foo.Integer = 10;
static void ChangeValue(Bar bar) => bar.Integer = 10;

static void Main()
{
    Foo foo = new Foo() { Integer = 0 };
    ChangeValue(foo);   // 참조 전달

    Bar bar = new Bar() { Integer = 1 };
    ChangeValue(bar);   // 복사본 전달

    Console.WriteLine(foo.Integer);
    Console.WriteLine(bar.Integer);
}

/* output:
10
1
*/
```

<br>

## 참조 형식 매개 변수

<br>

- [참조 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D)을 메서드에 전달하는 경우 참조 (인스턴스의 주소) 에 대한 복사본을 전달한다.
    - 인스턴스의 주소를 통해 참조 멤버 액세스가 가능하다.
    - 호출된 메서드는 변수로 전달된 인스턴스의 주소를 변경할 수 없다.
- [ref](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref) 키워드를 통해 참조를 직접 전달할 수 있다.

<br>

### Example

<br>

```cs
class Test
{
    public int X;
}

static void Foo(Test test)
{
    Console.WriteLine($"Foo start : {test.X}");

    // Change Member

    test.X = 100;

    Console.WriteLine($"Foo - X changed : {test.X}");
}

static void Bar(Test test)
{
    Console.WriteLine($"Bar start : {test.X}");

    // Change reference

    test = new() { X = 50 };

    Console.WriteLine($"Bar - test changed : {test.X}");
}

static void Main()
{
    Test t = new() { X = 1 };

    Foo(t);

    Console.WriteLine($"After Foo : {t.X}");

    Bar(t);

    Console.WriteLine($"After Bar : {t.X}");
}

/* output:
Foo start : 1
Foo - X changed : 100
After Foo : 100
Bar start : 100
Bar - test changed : 50
After Bar : 100
*/
```

```cs
// ref 키워드를 사용한 예시

class Test
{
    public int X;
}

static void Foo(ref Test test)
{
    Console.WriteLine($"Foo start : {test.X}");

    // Change Member

    test.X = 100;

    Console.WriteLine($"Foo - X changed : {test.X}");
}

static void Bar(ref Test test)
{
    Console.WriteLine($"Bar start : {test.X}");

    // Change reference

    test = new() { X = 50 };

    Console.WriteLine($"Bar - test changed : {test.X}");
}

static void Main()
{
    Test t = new() { X = 1 };

    Foo(ref t);

    Console.WriteLine($"After Foo : {t.X}");

    Bar(ref t);

    Console.WriteLine($"After Bar : {t.X}");
}

/* output:
Foo start : 1
Foo - X changed : 100
After Foo : 100
Bar start : 100
Bar - test changed : 50
After Bar : 50
*/
```

<br>

## 값 형식 매개 변수

<br>

- [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)을 메서드에 전달하는 경우 값의 복사본을 전달한다.
    - 메서드는 구조체의 복사본을 전달받아 사용한다.
    - 인스턴스 및 모든 멤버가 복사되어 전달되기 때문에, 메서드에서는 원래 구조체에 접근이 불가능하다.
- `참조 형식`과 마찬가지로 `ref` 키워드를 통해 참조로 전달이 가능하다.

<br>

### Example

<br>

```cs
struct Test
{
    public int X;
}

static void Foo(Test test)
{
    Console.WriteLine($"Foo start : {test.X}");

    // Change Member

    test.X = 100;

    Console.WriteLine($"Foo - X changed : {test.X}");
}

static void Bar(Test test)
{
    Console.WriteLine($"Bar start : {test.X}");

    // Change reference

    test = new() { X = 50 };

    Console.WriteLine($"Bar - test changed : {test.X}");
}

static void Main()
{
    Test t = new() { X = 1 };

    Foo(t);

    Console.WriteLine($"After Foo : {t.X}");

    Bar(t);

    Console.WriteLine($"After Bar : {t.X}");
}

/* output:
Foo start : 1
Foo - X changed : 100
After Foo : 1
Bar start : 1
Bar - test changed : 50
After Bar : 1
*/
```

```cs
// ref 키워드를 사용한 예시

struct Test
{
    public int X;
}

static void Foo(ref Test test)
{
    Console.WriteLine($"Foo start : {test.X}");

    // Change Member

    test.X = 100;

    Console.WriteLine($"Foo - X changed : {test.X}");
}

static void Bar(ref Test test)
{
    Console.WriteLine($"Bar start : {test.X}");

    // Change reference

    test = new() { X = 50 };

    Console.WriteLine($"Bar - test changed : {test.X}");
}

static void Main()
{
    Test t = new() { X = 1 };

    Foo(ref t);

    Console.WriteLine($"After Foo : {t.X}");

    Bar(ref t);

    Console.WriteLine($"After Bar : {t.X}");
}

/* output:
Foo start : 1
Foo - X changed : 100
After Foo : 100
Bar start : 100
Bar - test changed : 50
After Bar : 50
*/
```

<br>

## 참조 자료

<br>

- [메서드 매개 변수(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/method-parameters)