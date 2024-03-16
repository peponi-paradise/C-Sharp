## Introduction

<br>

- Null 조건부 연산자 (`?.`, `?[]`) 는 [멤버 액세스 `.`](https://peponi-paradise.tistory.com/entry/C-Language-Member-access-expression), [인덱서 액세스 `[]`](https://peponi-paradise.tistory.com/entry/C-Language-Indexer-operator) 시 피연산자가 `null`이 아닐 때만 연산을 수행하며 `null`인 경우에는 `null`을 반환한다.

<br>

## Example

<br>

- Null 조건부 연산자는 단락 연산자로 피연산자가 `null`인 경우 `.` 및 `[]` 연산이 실행되지 않고 `null`을 반환한다.
    ```cs
    string? foo = null;

    if (foo?.Length is null) Console.WriteLine("Null-conditional member access returns null");
    if (foo?[1] is null) Console.WriteLine("Null-conditional indexer access returns null");

    /* output:
    Null-conditional member access returns null
    Null-conditional indexer access returns null
    */
    ```
- 피연산자가 `null`이 아닌 경우, `?.` 및 `?[]`는 각각 `.` 및 `[]` 연산의 결과를 반환한다.
    ```cs
    string? foo = "ABCDE";

    if (foo?.Length is int length)
        Console.WriteLine(length);
    else
        Console.WriteLine("Null-conditional member access returns null");

    if (foo?[1] is char value)
        Console.WriteLine(value);
    else
        Console.WriteLine("Null-conditional indexer access returns null");

    /* output:
    5
    B
    */
    ```
- 피연산자의 멤버 또는 인덱서가 `T` 형식인 경우, `?.X`, `?[X]`는 `T?` 형식이다.
    - null 조건부 연산을 사용해 `T` 형식을 얻으려는 경우 [null 병합 연산자](https://peponi-paradise.tistory.com/entry/C-Language-Null-coalescing-operators)를 식에 넣어준다.

    ```cs
    public class Foo
    {
        public int Bar = 1;
    }
    ```
    ```cs
    Foo? foo = new();
    var bar = foo?.Bar;

    PrintType(bar);
    PrintType(bar ?? 0);

    void PrintType<T>(T value) => Console.WriteLine($"{typeof(T)}");

    /* output:
    System.Nullable`1[System.Int32]
    System.Int32
    */
    ```
- null 조건부 연산자는 괄호 등으로 인해 단락 연산자의 성질을 잃게 되는 경우 예기치 않은 오류를 일으킬 수 있다.
    ```cs
    public class Foo
    {
        public Bar FooMember = new();

        public class Bar
        {
            public int BarMember = 1;
        }
    }
    ```
    ```cs
    Foo? foo = null;

    var bar = foo?.FooMember.BarMember;     // null

    var baz = (foo?.FooMember).BarMember;   // NullReferenceException
    ```

<br>

## 안전한 대리자 호출

<br>

- `?.` 연산자는 대리자에 대한 `null` 체크 및 thread-safe 호출을 지원한다.
    ```cs
    public static EventHandler? Foo;
    ```
    ```cs
    Foo?.Invoke(null, new());
    ```
- 위 예제의 `Foo?.Invoke()`는 런타임에 다음과 같이 동작하여 thread-safe 하게 된다.
    ```cs
    var delegateExpression = Foo;
    if (delegateExpression is not null) delegateExpression?.Invoke(null, new());
    ```
    - delegate를 미리 복사하여 `null` 체크와 delegate invoke 사이에 `Foo`가 변경되더라도 안전한 동작이 보장된다.

<br>

## 참조 자료

<br>

- [멤버 액세스 및 null 조건부 연산자 및 식 - Null 조건부 연산자 ?. 및 ?[]](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)
- [C# - Language - Member access expression (.)](https://peponi-paradise.tistory.com/entry/C-Language-Member-access-expression)
- [C# - Language - Indexer operator ([])](https://peponi-paradise.tistory.com/entry/C-Language-Indexer-operator)