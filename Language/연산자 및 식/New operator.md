## Introduction

<br>

- `new` 연산자는 새 인스턴스를 생성한다.
- 생성자 호출, 배열, 익명 형식의 인스턴스 생성 등에 사용한다.

<br>

## 생성자 호출

<br>

- `new` 연산자를 사용하여 지정된 형식의 생성자 중 하나를 호출하며 새 인스턴스를 반환한다.
    ```cs
    public class Foo
    {
        private string _name;

        public Foo() => _name = string.Empty;
        public Foo(string name) => _name = name;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Foo foo = new Foo();
            Foo foo1 = new Foo("foo");
        }
    }
    ```
- 다음 식은 모두 같은 결과를 가져온다.
    ```cs
    Foo foo = new Foo();
    Foo foo = new();
    var foo = new Foo();
    ```
- Collection, array 또한 `new` 연산자를 이용하여 인스턴스를 생성하고 초기화할 수 있다.
    ```cs
    List<int> ints = new() { 1, 2, 3 };
    Dictionary<string, int> dic = new()
    {
        { "a", 1 },
        { "b", 2 },
        { "c", 3 }
    };
    ```
    ```cs
    var arr = new[] { 1, 2, 3 };
    ```

<br>

## 익명 형식

<br>

- `new` 연산자와 함께 초기화 구문을 사용하여 익명 형식을 생성할 수 있다.
    ```cs
    static void Main(string[] args)
    {
        var foo = new { ID = 1, Name = "Peponi" };
        Console.WriteLine(foo.GetType());
        Console.WriteLine(foo);
    }

    /* output:
    <>f__AnonymousType0`2[System.Int32,System.String]
    { ID = 1, Name = Peponi }
    */
    ```

<br>

## 참조 자료

<br>

- [new 연산자 - new 연산자는 형식의 새 인스턴스를 만듭니다.](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/new-operator)