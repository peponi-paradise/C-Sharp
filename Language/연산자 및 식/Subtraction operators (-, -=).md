## Introduction

<br>

- `-`, `-=` 연산자는 [정수](https://peponi-paradise.tistory.com/entry/C-Language-%EC%A0%95%EC%88%98-%ED%98%95%EC%8B%9D), [부동 소수점](https://peponi-paradise.tistory.com/entry/C-Language-floating-point-type), [대리자](https://peponi-paradise.tistory.com/entry/C-Language-%EB%8C%80%EB%A6%AC%EC%9E%90-Delegate) 형식을 지원한다.
- 여기서는 `-`, `-=` 연산자에 대해 간단히 알아본다.
    - 산술 연산자 `-`에 대한 자세한 내용은 [+, - operator (Arithmetic operator)](https://peponi-paradise.tistory.com/entry/C-Language-Arithmetic-operator)를 참조한다.

<br>

## 대리자 제거

<br>

- 동일한 대리자 형식에 대해 `-` 연산자는 제거를 지원하며, 다음 규칙에 따라 인스턴스를 반환한다.
    - 오른쪽 피연산자가 왼쪽 피연산자의 호출 목록에 포함되어 있는 경우, 왼쪽 피연산자의 오른쪽에서부터 제거 후 반환
        ```cs
        Action a = delegate { Console.Write(nameof(a)); };
        Action b = delegate { Console.Write(nameof(b)); };

        var foo = a + b + a + b;
        var bar = foo - a;

        bar();

        /* output:
        abb
        */
        ```
    - 오른쪽 피연산자가 왼쪽 피연산자의 호출 목록에 포함되어 있지 않은 경우, 왼쪽 피연산자를 반환
        ```cs
        Action a = delegate { Console.Write(nameof(a)); };
        Action b = delegate { Console.Write(nameof(b)); };

        var foo = a + a;
        var bar = foo - b;

        bar();

        /* output:
        aa
        */
        ```
    - 제거로 인해 모든 대리자가 제거되는 경우 `null` 반환
        ```cs
        Action a = delegate { Console.Write(nameof(a)); };

        var foo = a + a;
        var bar = foo - a - a;

        Console.WriteLine(bar is null);

        /* output:
        True
        */
        ```
    - 왼쪽 피연산자가 `null`인 경우 연산 결과는 `null`이다.
    - 오른쪽 피연산자가 `null`인 경우 연산 결과는 왼쪽 피연산자이다.
        ```cs
        Action a = delegate { Console.Write(nameof(a)); };

        var foo = null - a;
        var bar = a - null;

        Console.WriteLine(foo is null);
        bar();

        /* output:
        True
        a
        */
        ```

<br>

## 빼기 할당 연산자 (`-=`)

<br>

- `-=` 연산자를 통해, 이진 연산자 `-`의 복합 할당을 수행할 수 있다.
    ```cs
    int foo = 5;
    foo -= 4;
    Console.WriteLine(foo);

    /* output:
    1
    */
    ```
    ```cs
    Action a = delegate { Console.Write(nameof(a)); };

    var foo = a + a + a;
    foo -= a;

    foo();

    /* output:
    aa
    */
    ```
- `-=` 연산자는 이벤트를 제거할 때도 사용된다.
    ```cs
    private static event EventHandler? _foo;

    private static void Main(string[] args)
    {
        _foo += Write;

        _foo.Invoke(null, new());

        _foo -= Write;

        _foo?.Invoke(null, new());

        void Write(object? sender, EventArgs e) => Console.WriteLine("Event");
    }

    /* output:
    Event
    */
    ```
- `-=` 연산자를 통해 복합 할당을 수행하는 경우, 형식 확인은 왼쪽 피연산자에 대해 최초 1회만 수행된다.
    ```cs
    byte foo = 1;
    byte bar = 5;

    var baz = foo - bar;    // 복합 할당을 수행하지 않는 경우
    foo -= bar;             // 복합 할당을 수행하는 경우

    Console.WriteLine(baz.GetType());
    Console.WriteLine(baz);
    Console.WriteLine(foo.GetType());
    Console.WriteLine(foo);

    /* output:
    System.Int32
    -4
    System.Byte
    252
    */
    ```
    - 위 예시와 같이 복합 할당을 하는 경우 최초 1회 확인한 피연산자의 형식 (`byte`) 으로 결과가 출력된다.
        `byte` 형식은 연산 시 `int` 형식으로 변환되는데, 이로 인해 동일한 연산을 수행하는 두 경우에 대해 다른 결과가 나타난다.
        따라서 복합 할당 시 형식에 주의할 필요가 있다.

<br>

## 참조 자료

<br>

- [- 및 -= 연산자 - 빼기(빼기)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/subtraction-operator)