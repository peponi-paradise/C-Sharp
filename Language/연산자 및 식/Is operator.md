# Introduction

<br>

- `is` 연산자는 주어진 식의 결과를 이용하여 다음 작업을 수행한다.
    1. 형식 일치 여부 확인
    2. 패턴 일치 여부 확인
- `is` 식은 아래와 같이 표현한다.
    ```cs
    expression is type      // 1
    expression is pattern   // 2
    ```
    1. `expression`은 값을 반환하는 식, `type`은 형식 또는 형식 매개 변수의 이름이다.
        여기서 `type`은 무명 메서드 또는 람다 식으로 표현할 수 없다.
    2. `expression`은 값을 반환하는 식, `pattern`은 일치 여부를 확인할 [패턴](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns)이다.

<br>

## 형식 일치 여부 확인

<br>

- `is` 연산자를 이용하여 형식 일치 여부를 확인할 수 있다.
- 주어진 식이 `null`이 아닌 경우, `is` 연산자는 다음 조건 중 하나를 만족하면 `true`를 반환한다.
    1. `T` or `T?` (HasValue == true 인 경우)
    2. `T`의 파생 형식
    3. `T` 인터페이스 구현
    4. `T` 형식으로 [boxing 또는 unboxing](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/types/boxing-and-unboxing#boxing) 가능한 경우
- 이 때, `is` 연산자는 [숫자 변환](https://peponi-paradise.tistory.com/entry/C-Language-%EC%88%AB%EC%9E%90-%ED%98%95%EC%8B%9D-%EB%B3%80%ED%99%98), [사용자 정의 변환](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)에 대해서는 고려하지 않는다.

<br>

### `T` or `T?`

<br>

- 다음은 주어진 식이 `T` 또는 `T?`임을 확인한다.
    ```cs
    int? foo = 1;

    Console.WriteLine(foo is int);
    Console.WriteLine(foo is int?);

    foo = null;

    Console.WriteLine(foo is int);
    Console.WriteLine(foo is int?);

    /* output:
    True
    True
    False
    False
    */
    ```

<br>

### `T`의 파생 형식

<br>

- 다음은 주어진 식이 `T`의 파생 형식임을 확인한다.
    ```cs
    public class Base { }
    public class Derived : Base { }
    ```
    ```cs
    var derived = new Derived();

    Console.WriteLine(derived is Base);
    Console.WriteLine(derived is Derived);

    /* output:
    True
    True
    */
    ```
- `Base` 객체의 경우, `Derived`에 대해 `false`를 반환하게 된다.
    ```cs
    var @base = new Base();

    Console.WriteLine(@base is Base);
    Console.WriteLine(@base is Derived);

    /* output:
    True
    False
    */
    ```

<br>

### `T` 인터페이스 구현

<br>

- 다음은 주어진 식이 `T` 인터페이스를 구현한 형식임을 확인한다.
    ```cs
    public interface Interface { }
    public class Class : Interface { }
    ```
    ```cs
    var foo = new Class();

    Console.WriteLine(foo is Interface);

    /* output:
    True
    */
    ```

<br>

### `T` 형식으로 boxing 또는 unboxing 가능한 경우

<br>

- 다음은 주어진 식에 대해 `is` 연산자가 boxing, unboxing을 확인한다는 것을 보여준다.
    ```cs
    int value = 1;
    object boxed = value;

    Console.WriteLine(value is object);
    Console.WriteLine(boxed is int);

    /* output:
    True
    */
    ```

<br>

### 숫자 변환, 사용자 정의 변환

<br>

- 다음은 주어진 식에 대해 `is` 연산자가 숫자 변환, 사용자 정의 변환에 대해서는 고려하지 않는 것을 보여준다.
    1. 숫자 변환
        ```cs
        byte foo = 1;
        int bar = foo;      

        Console.WriteLine(foo is int);

        /* output:
        False
        */
        ```
    2. 사용자 정의 변환
        ```cs
        public readonly struct Integer
        {
            private readonly byte _value;

            public Integer(byte value) => _value = value;

            public static explicit operator byte(Integer value) => value._value;
            public static implicit operator Integer(byte value) => new(value);
        }
        ```
        ```cs
        byte foo = 1;
        Integer bar = foo;

        Console.WriteLine(foo is Integer);

        /* output:
        False
        */
        ```

<br>

## 패턴 일치 여부 확인

<br>

- `is` 연산자를 이용하여 패턴 일치 여부를 확인할 수 있다.
- 여기서는 간단한 사례를 보여준다.
    자세한 사항은 아래 항목을 참조한다.
    1. [MSDN - 패턴 일치 - 패턴의 is 및 switch 식, 연산자 and, or 및 not](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns)
    2. [블로그 - 패턴 발행글](https://peponi-paradise.tistory.com/tag/%ED%8C%A8%ED%84%B4)

<br>

```cs
// 상수 패턴

int foo = 7;

if (foo is 7) Console.WriteLine("foo is 7");
else Console.WriteLine("foo is not 7");

/* output:
foo is 7
*/
```
```cs
// 관계형 패턴

int foo = 7;

if (foo is < 0) Console.WriteLine("foo < 0");
else if (foo is < 5) Console.WriteLine("foo < 5");
else if (foo is < 10) Console.WriteLine("foo < 10");

/* output:
foo < 10
*/
```

<br>

## 참조 자료

<br>

- [형식 테스트 연산자 및 캐스트 식 - is 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/type-testing-and-cast#is-operator)
- [패턴 일치 - 패턴의 is 및 switch 식, 연산자 and, or 및 not](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/patterns)
- [블로그 - 패턴 발행글](https://peponi-paradise.tistory.com/tag/%ED%8C%A8%ED%84%B4)