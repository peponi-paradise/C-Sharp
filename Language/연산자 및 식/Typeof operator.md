## Introduction

<br>

- `typeof` 연산자는 지정된 형식의 [System.Type](https://learn.microsoft.com/ko-kr/dotnet/api/system.type?view=net-8.0) 인스턴스를 반환한다.
- `typeof`의 인수는 형식 또는 형식 매개 변수의 이름으로, 제네릭 형식 또한 사용 가능하다.
- `typeof`에는 다음 형식을 사용할 수 없다.
    - [dynamic](https://peponi-paradise.tistory.com/entry/C-Language-%EB%8F%99%EC%A0%81-%ED%98%95%EC%8B%9D-dynamic)
    - [Nullable 참조 형식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/nullable-reference-types)

<br>

## Example

<br>

- 다음은 `typeof`를 통해 `System.Type` 인스턴스를 얻는 예시다.
    ```cs
    Console.WriteLine(typeof(int));
    Console.WriteLine(typeof(string));
    Console.WriteLine(typeof(Dictionary<int, string>));
    Console.WriteLine(typeof(Dictionary<, >));

    /* output:
    System.Int32
    System.String
    System.Collections.Generic.Dictionary`2[System.Int32,System.String]
    System.Collections.Generic.Dictionary`2[TKey,TValue]
    */
    ```

<br>

### `Object.GetType`을 이용한 형식 인스턴스 얻기

<br>

- `typeof`의 인수로는 식을 사용할 수 없다.
- 식의 런타임 형식을 얻으려는 경우, [Object.GetType](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.gettype?view=net-8.0)을 이용하면 가능하다.
    ```cs
    //Console.WriteLine(typeof(1 + 1));   // 불가

    Console.WriteLine((1 + 1.234).GetType());

    var foo = new List<int>();
    Console.WriteLine(foo.GetType());

    /* output:
    System.Double
    System.Collections.Generic.List`1[System.Int32]
    */
    ```

<br>

### `typeof`를 이용한 형식 확인

<br>

- `typeof` 연산자를 이용하여 형식 확인을 하는 경우 런타임 형식의 정확한 일치 여부를 파악할 수 있다.
    - [is 연산자](https://peponi-paradise.tistory.com/entry/C-Language-is-operator)와는 달리 정확히 동일한 형식일 때만 `true`를 반환한다.
    ```cs
    public class Base { }
    public class Derived : Base { }
    ```
    ```cs
    var foo = new Derived();

    Console.WriteLine(foo is Base);
    Console.WriteLine(foo is Derived);

    Console.WriteLine(foo.GetType() == typeof(Base));
    Console.WriteLine(foo.GetType() == typeof(Derived));

    /* output:
    True
    True
    False
    True
    */
    ```

<br>

## 참조 자료

<br>

- [형식 테스트 연산자 및 캐스트 식 - typeof 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/type-testing-and-cast)
- [Type 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.type?view=net-8.0)
- [Object.GetType Method](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.gettype?view=net-8.0)