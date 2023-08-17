## Introduction

<br>

- `get`, `set`, `init`는 속성 ([property](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/properties)), 인덱서([indexer](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/indexers/))의 액세스 메서드를 정의하는 키워드이다.
- `value`는 속성 및 인덱서의 설정자 (setter) 에 사용하는 키워드이다.

<br>

## get

<br>

- `get`은 접근자 (getter) 의 액세스 메서드를 정의하는 키워드이다.
- 일반적으로 `get`은 특정 값을 반환하는 데 주로 사용한다.

```cs
public class Foo
{
    public List<int> X = new() { 1, 2, 3 };

    public int CenterX
    {
        get => X[1];
    }

    public int this[int index]
    {
        get => X[index];
    }
}

internal class Program
{
    private static void Main()
    {
        Foo foo = new();

        Console.WriteLine(foo.CenterX);
        Console.WriteLine(foo[2]);
    }
}

/* output:
2
3
*/
```

<br>

## set

<br>

- `set`은 설정자 (setter) 의 액세스 메서드를 정의하는 키워드이다.
- 속성 또는 인덱서 요소에 `value` 키워드를 이용하여 값을 할당한다.

```cs
public class Foo
{
    public List<int> X = new() { 1, 2, 3 };

    public int CenterX
    {
        get => X[1];
        set => X[1] = value;
    }

    public int this[int index]
    {
        get => X[index];
        set => X[index] = value;
    }
}

internal class Program
{
    private static void Main()
    {
        Foo foo = new();

        foo.CenterX = 5;
        foo[2] = 10;

        Console.WriteLine(foo.CenterX);
        Console.WriteLine(foo[2]);
    }
}

/* output:
5
10
*/
```

<br>

## init (C# 9)

<br>

- `init`은 설정자 (setter) 의 액세스 메서드를 정의하는 키워드로 `C# 9` 에 도입되었다.
- 속성 또는 인덱서 요소에 `value` 키워드를 이용하여 값을 할당한다.
- `init` 키워드를 사용하면 `생성자`에서만 값 할당이 가능해 불변성이 적용된다.

```cs
public class Foo
{
    public List<int> X = new() { 1, 2, 3 };

    public int CenterX
    {
        get => X[1];
        init => X[1] = value;
    }

    public int this[int index]
    {
        get => X[index];
        init => X[index] = value;
    }
}

internal class Program
{
    private static void Main()
    {
        Foo foo = new() { CenterX = 5, [2] = 10 };

        // foo.CenterX = 5;    // CS8852

        Console.WriteLine(foo.CenterX);
        Console.WriteLine(foo[2]);
    }
}

/* output:
5
10
*/
```

<br>

## value

<br>