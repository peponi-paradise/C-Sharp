## Introduction

<br>

- [with](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/with-expression) 식은 형식의 일부를 수정한 복사를 가능하게 한다.
    ```cs
    public record Foo(bool X, int Y);

    public static void Main()
    {
        var foo = new Foo(true, 100);
        var bar = foo with { Y = 50 };

        Console.WriteLine(foo);
        Console.WriteLine(bar);
    }

    /* output:
    Foo { X = True, Y = 100 }
    Foo { X = True, Y = 50 }
    */
    ```
- C# 10부터는 구조체, 익명 형식 또한 `with` 식을 사용할 수 있다.
    ```cs
    public struct Foo
    {
        public bool X;
        public int Y;

        public Foo(bool x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"{X}, {Y}";
    }

    public static void Main()
    {
        var foo = new Foo(true, 100);
        var bar = foo with { Y = 50 };

        Console.WriteLine(foo);
        Console.WriteLine(bar);
    }

    /* output:
    True, 100
    True, 50
    */
    ```
    ```cs
    // Tuple

    public static void Main()
    {
        var foo = (true, 100);
        var bar = foo with { Item1 = false, Item2 = 20 };

        Console.WriteLine(foo);
        Console.WriteLine(bar);
    }

    /* output:
    (True, 100)
    (False, 20)
    */
    ```
    ```cs
    // Anonymous type

    public static void Main()
    {
        var foo = new { X = true, Y = 100 };
        var bar = foo with { X = false, Y = 10 };

        Console.WriteLine(foo);
        Console.WriteLine(bar);
    }

    /* output:
    { X = True, Y = 100 }
    { X = False, Y = 10 }
    */
    ```

<br>

## 참조 형식 멤버

<br>

- 참조 형식 멤버의 경우 `with`를 통한 복사 시 인스턴스의 참조만 복사되어 주의가 필요하다.
    - 원본과 복사본 둘 다 같은 참조를 가리킨다.
    ```cs
    public record Foo(List<int> A, int B);

    public static void Main()
    {
        var foo = new Foo(new() { 1, 2, 3, 4, 5 }, 6);
        var bar = foo with { B = 10 };
        bar.A.Add(777);

        Console.WriteLine(string.Join(", ", foo.A));
        Console.WriteLine(string.Join(", ", bar.A));
    }

    /* output:
    1, 2, 3, 4, 5, 777
    1, 2, 3, 4, 5, 777
    */
    ```
- [record](https://peponi-paradise.tistory.com/entry/C-Language-%EB%A0%88%EC%BD%94%EB%93%9C-%ED%98%95%EC%8B%9D-Record) 형식의 경우 `복사 생성자`가 컴파일러에 의해 생성된다. `with`를 이용한 복사 시 `복사 생성자` 호출 후 수정 사항이 반영되는데, 복사 생성자를 직접 작성하여 동작을 변경할 수 있다.
    ```cs
    public record Foo(List<int> A, int B)
    {
        protected Foo(Foo origin)
        {
            Console.WriteLine("Copy constructor called");

            A = new(origin.A);
            B = origin.B;
        }
    }

    public static void Main()
    {
        var foo = new Foo(new() { 1, 2, 3, 4, 5 }, 6);
        var bar = foo with { B = 10 };
        bar.A.Add(777);

        Console.WriteLine(string.Join(", ", foo.A));
        Console.WriteLine(string.Join(", ", bar.A));
    }

    /* output:
    Copy constructor called
    1, 2, 3, 4, 5
    1, 2, 3, 4, 5, 777
    */
    ```

<br>

## 참조 자료

<br>

- [with expression - Nondestructive mutation creates a new object with modified properties](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/with-expression)
- [C# - Language - 레코드 형식 (Record)](https://peponi-paradise.tistory.com/entry/C-Language-%EB%A0%88%EC%BD%94%EB%93%9C-%ED%98%95%EC%8B%9D-Record)