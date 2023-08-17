## Introduction

<br>

- `add`, `remove` 키워드는 [event](https://peponi-paradise.tistory.com/entry/C-Language-Event) 접근자를 정의하는 데 사용한다.
- `add`, `remove` 키워드는 항상 쌍으로 정의되어야 한다.
- 일반적으로는 컴파일러가 제공하는 접근자로 충분하다. 다음과 같은 경우에는 직접 접근자를 제공해야 할 수 있다.
    1. 동일한 이름을 가진 여러 인터페이스의 이벤트
    2. 하나의 이벤트를 통해 여러 이벤트를 발생시키는 경우
    ...

<br>

## Example

<br>

```cs
private class Foo
{
    private event EventHandler<string>? PreEvent;

    private event EventHandler<string>? PostEvent;

    private object _lock = new();

    public event EventHandler<string> OnEvent
    {
        add
        {
            lock (_lock)
            {
                PreEvent += value;
                PostEvent += value;
            }
        }
        remove
        {
            lock (_lock)
            {
                PreEvent -= value;
                PostEvent -= value;
            }
        }
    }

    public void RaiseEvent()
    {
        PreEvent?.Invoke(this, $"{nameof(PreEvent)}");

        Console.WriteLine($"{nameof(Foo)} event raised");

        PostEvent?.Invoke(this, $"{nameof(PostEvent)}");
    }
}

private class Bar
{
    public Bar(Foo foo)
    {
        foo.OnEvent += Foo_OnEvent;
    }

    private void Foo_OnEvent(object? sender, string e)
    {
        Console.WriteLine($"{nameof(Bar)} receives {nameof(Foo)} event : {e}");
    }
}

private static void Main()
{
    Foo foo = new();
    Bar bar = new(foo);

    foo.RaiseEvent();
}

/* output:
Bar receives Foo event : PreEvent
Foo event raised
Bar receives Foo event : PostEvent
*/
```

<br>

## 참조 자료

<br>

- [C# - Language - Event](https://peponi-paradise.tistory.com/entry/C-Language-Event)
- [add(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/add)
- [remove(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/remove)