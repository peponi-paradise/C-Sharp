## Introduction

<br>

- `as` 연산자는 식의 결과를 지정된 형식 (참조 또는 nullable 값 형식) 으로 변환한다.
    - 변환할 수 없는 경우 `null`을 반환하며, 예외를 출력하지 않는다.
- `as` 식은 아래와 같이 표현한다.
    ```cs
    expression as type
    ```
    - `expression`은 값을 반환하는 식, `type`은 형식 또는 형식 매개 변수의 이름이다.
    - 위 식은 아래 식의 축약형이다.
    
    ```cs
    expression is type ? (type)(expression) : (type)null
    ```
- `as` 식은 아래 항목 중 하나를 만족하면 변환된 객체를 반환한다.
    1. `T`의 파생 형식
    2. `T` 인터페이스 구현
    3. `T` 형식으로 [boxing 또는 unboxing](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/types/boxing-and-unboxing#boxing) 가능한 경우
- `as` 연산자는 [숫자 변환](https://peponi-paradise.tistory.com/entry/C-Language-%EC%88%AB%EC%9E%90-%ED%98%95%EC%8B%9D-%EB%B3%80%ED%99%98)에 대해서는 고려하지 않으며 [사용자 정의 변환](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/user-defined-conversion-operators)을 수행할 수 없다.

<br>

## Example

<br>

### `T`의 파생 형식

<br>

- 다음은 주어진 식을 `T`, `T`의 파생 형식으로 변환한다.
    ```cs
    namespace AsOperator
    {
        public class Base { }
        public class Derived : Base { }
    }
    ```
    ```cs
    var foo = new Derived();

    Console.WriteLine(foo as Base);
    Console.WriteLine(foo as Derived);

    /* output:
    AsOperator.Derived
    AsOperator.Derived
    */
    ```
- `Base` 객체의 경우, `Derived`에 대해 `null`을 반환하게 된다.
    ```cs
    var foo = new Base();

    Console.WriteLine(foo as Base);
    Console.WriteLine(foo as Derived);

    /* output:
    AsOperator.Base

    */
    ```

<br>

### `T` 인터페이스 구현

<br>

- 다음은 `T` 인터페이스 객체를 구현 형식으로 변환한다.
    ```cs
    namespace AsOperator
    {
        public interface Interface { }
        public class Class : Interface { }
    }
    ```
    ```cs
    Interface foo = new Class();

    Console.WriteLine(foo as Class);

    /* output:
    AsOperator.Class
    */
    ```

<br>

### `T` 형식으로 boxing 또는 unboxing 가능한 경우

<br>

- 다음은 주어진 식에 대해 `as` 연산자가 boxing, unboxing을 수행하는 것을 보여준다.
    ```cs
    int foo = 1;
    object fooBoxed = foo;

    Console.WriteLine(foo as object);
    Console.WriteLine(fooBoxed as int?);

    /* output:
    1
    1
    */
    ```

<br>

### 숫자 변환

<br>

- 다음은 주어진 식에 대해 `as` 연산자가 숫자 변환에 대해서는 고려하지 않는 것을 보여준다.
     ```cs
    byte foo = 1;
    int bar = foo;

    Console.WriteLine(foo as int?);

    /* output:
        
    */
    ```

<br>

## 참조 자료

<br>

- [형식 테스트 연산자 및 캐스트 식 - as 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/type-testing-and-cast#as-operator)