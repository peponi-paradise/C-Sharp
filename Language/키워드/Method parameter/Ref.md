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

    public ref Foo GetFoo(int index)
    {
        return ref _foos[index];
    }

    public override string ToString()
    {
        string merged = string.Empty;

        foreach (var foo in _foos)
        {
            merged += $"{foo.X}, ";
        }

        return merged;
    }
}

private static void Main()
{
    Bar bar = new();

    Console.WriteLine(bar.ToString());

    ref var foo = ref bar.GetFoo(1);
    foo.X = 100;

    Console.WriteLine(bar.ToString());
}

/* output:
1, 2, 3,
1, 100, 3,
*/
```

<br>

## 참조 자료

<br>

- [C# - Language - Method parameter](https://peponi-paradise.tistory.com/entry/C-Language-Method-parameter)
- [ref(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/ref)