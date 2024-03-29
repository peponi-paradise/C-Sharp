## Introduction

<br>

- `ref` 키워드는 참조 매개 변수를 의미한다.
    - [in](https://peponi-paradise.tistory.com/entry/C-Language-In-keyword-Parameter-modifier) 키워드와 마찬가지로 메서드에 전달되기 전 초기화가 필요하다.
- `ref` 키워드를 통해 넘겨진 매개 변수는 호출된 메서드에서 읽기 / 쓰기가 가능하다.
- `ref` 매개 변수는 메서드 정의와 호출 모두 명시적으로 사용해야 한다.
- 다음 요소가 포함된 메서드에는 `in`, `ref`, `out` 키워드 사용이 불가능하다.
    - [async](https://peponi-paradise.tistory.com/entry/C-Language-Async-Await)
    - [yield return](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/statements/yield), `yield break`
- 확장 메서드에는 다음과 같은 제약이 있다.
    - 첫 번째 인수가 `ref struct`가 아니거나 구조체로 제한되지 않은 제네릭 형식인 경우 사용 불가
- `ref readonly` 키워드를 통해 읽기 전용으로 참조를 전달할 수 있다 (C# 12)

<br>

## Example

<br>

```cs
// 값 형식

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

```cs
// 참조 형식

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

## 참조 반환

<br>

- `ref return`은 메서드가 호출자에게 참조로 값을 반환한다.
- 호출자는 반환 값을 수정할 수 있으며, 해당 값은 즉시 반영된다.

```cs
private class Foo
{
    public int X;
}

private class Bar
{
    private Foo[] _foos = { new() { X = 1 },
                    new() { X = 2 },
                    new() { X = 3 } };

    // ref return
    public ref Foo this[int index]
    {
        get => ref _foos[index];
    }

    public override string ToString()
    {
        return string.Join(", ", _foos.Select(item => item.X));
    }
}

private static void Main()
{
    Bar bar = new();

    Console.WriteLine(bar.ToString());

    ref var foo = ref bar[1];
    foo = new Foo() { X = 100 };

    Console.WriteLine(bar.ToString());
}

/* output:
1, 2, 3
1, 100, 3
*/
```

<br>

## ref readonly

<br>

- C# 12부터 `ref readonly` 한정자를 이용해 매개 변수를 읽기 전용으로 만들 수 있다.
- 읽기 전용이므로 쓰기 작업이 불가능해진다.
- 메서드 호출 등을 통해 내부 값을 조작할 수 있는데, 이 때 내부적으로 복사가 일어나 의도치 않은 결과를 얻을 수 있다.
```cs
public struct Foo
{
    public int X;

    public int AddOne() => X++;
}
```
```cs
static void Main()
{
    Foo foo = new();

    RefReadonly(ref foo);

    Console.WriteLine(foo.AddOne());
}

static void RefReadonly(ref readonly Foo foo)
{
    //foo = new();    // CS8331
    //foo.X = 5;       // CS8332

    Console.WriteLine(foo.AddOne());
    Console.WriteLine(foo.AddOne());
}

/* output:
0
0
0
*/
```

<br>

## 참조 자료

<br>

- [C# - Language - Method parameter](https://peponi-paradise.tistory.com/entry/C-Language-Method-parameter)
- [ref(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref)