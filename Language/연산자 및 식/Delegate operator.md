## Introduction

<br>

- `delegate` 연산자는 대리자 형식으로 변환할 수 있는 무명 메서드를 만든다.
  - [System.Action](https://learn.microsoft.com/ko-kr/dotnet/api/system.action?view=net-8.0), [System.Func\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.func-1?view=net-8.0) 형식과 같은 형식으로 변환 가능하다.

<br>

## Example

<br>

- 다음은 무명 메서드를 만드는 방법이다.
    ```cs
    Action foo = delegate { Console.WriteLine("foo"); };
    Action<int> bar = delegate (int a) { Console.WriteLine(a); };

    // 사용되지 않는 매개변수에 무시 ( '_' ) 지정 가능
    Action<int, int> baz = delegate (int _, int a) { Console.WriteLine(a); };
    ```
- 무명 메서드는 [람다 식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/lambda-expressions)과 달리 매개 변수를 생략할 수 있다.
    - 무명 메서드의 대부분이 람다 식으로 대체될 수 있지만, 이 부분은 무명 메서드만 가능하다.
    ```cs
    Action<int, int> baz = delegate { Console.WriteLine("baz"); };
    ```
- 무명 메서드 생성 시 [static](https://peponi-paradise.tistory.com/entry/C-Language-Static) 한정자를 적용할 수 있다.
    - 정적으로 선언된 무명 메서드는 외부 스코프의 non-static 멤버를 캡처할 수 없다.
    ```cs
    static void Foo() => Console.WriteLine("Foo");
    void Bar() => Console.WriteLine("Bar");

    Action action = static delegate { Foo(); };
    Action action2 = static delegate { Bar(); };    // CS8820
    ```
-  `static` 메서드의 delegate 처리를 간략하게 하는 것 또한 가능하다.
    ```cs
    static void Foo() => Console.WriteLine("Foo");
    Action bar = Foo;
    ```
    - C# 11부터는 컴파일러가 캐시 처리를 해줘 delegate 재사용에 대해 성능 저하 요인을 고려하지 않아도 된다.
        - C# 11 이전에는 재사용을 위해 람다 식을 사용해야 한다.
            ```cs
            static void Foo() => Console.WriteLine("Foo");
            Action bar = () => Foo();
            ```

<br>

## 참조 자료

<br>

- [delegate 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/delegate-operator)
- [C# 11 - 정적 메서드에 대한 delegate 처리 시 cache 적용](https://www.sysnet.pe.kr/2/0/13126?pageno=12)