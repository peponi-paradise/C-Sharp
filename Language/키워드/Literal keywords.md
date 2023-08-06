## null

<br>

- `null` 키워드는 참조 형식의 기본값이다.
- `null reference`를 나타내는 키워드로, 어느 객체도 참조하지 않고 있음을 나타낸다.
- 값 형식은 [null 허용 값 형식](https://peponi-paradise.tistory.com/entry/C-Language-Null-%ED%97%88%EC%9A%A9-%EA%B0%92-%ED%98%95%EC%8B%9D-Nullable-value-type)을 제외하고 `null`을 가질 수 없다.

<br>

### Example

<br>

```cs
string a = null;
Console.WriteLine(a);

a = "A";
Console.WriteLine(a);

// int b = null;   // CS0037

int? b = null;
Console.WriteLine(b);

b = 10;
Console.WriteLine(b);

/* output:

A

10
*/
```

<br>

## default

<br>

- `default` 키워드는 `기본값`또는 `형식 제약 조건`을 나타내는 데 사용한다.

<br>

### Example - 기본값

<br>

```cs
// 기본값

Console.WriteLine($"Default of int : {default(int)}");
Console.WriteLine($"Default of double : {default(double)}");
Console.WriteLine($"Default of bool : {default(bool)}");
Console.WriteLine($"Default of int : {default(string)}");

/* output:
Default of int : 0
Default of double : 0
Default of bool : False
Default of string :
*/
```

```cs
// Switch 문의 default - 모든 패턴이 일치하지 않는 경우

int foo = 0;

switch (foo)
{
    case 1:
        foo = 10;
        break;

    case 2:
        foo = 20;
        break;

    default:
        foo = 30;
        break;
}

Console.WriteLine(foo);

/* output:
30
*/
```

<br>

### Example - 형식 제약 조건

<br>

- `default` 키워드를 이용하여 형식 제약 조건 사이 모호성을 해결할 수 있다.
- 아래 예시의 경우 Foo<T> 사이의 모호성을 나타내며, `default` 키워드를 통해 제약 조건을 사용하지 않도록 지정할 수 있다.

```cs
// 형식 제약 조건의 default

public class Base
{
    // No type constraint applied
    public virtual void Foo<T>(T? item) => Console.WriteLine("Default Foo");

    // struct type constraint applied
    public virtual void Foo<T>(T? item) where T : struct => Console.WriteLine("Struct Foo");
}

public class Derived : Base
{
    // This will be override Foo<T>(T? item)
    public override void Foo<T>(T? item) where T : default
    { }

    // This will be override Foo<T>(T? item) where T : struct
    public override void Foo<T>(T? item)
    { }
}
```

<br>

## true, false

<br>

- 리터럴 `true` 또는 `false`는 `bool` 변수 초기화, 논리값 전달 등에 사용한다.
- 값이 세 개인 논리형이 필요한 경우 `bool?` ([Nullable bool](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/boolean-logical-operators#nullable-boolean-logical-operators)) 형식을 사용할 수있다.
    - `bool?` 형식에는 `true`, `false`, `null`을 할당할 수 있다.
- 자세한 내용은 [C# - Language - 논리형 (Boolean)](https://peponi-paradise.tistory.com/entry/C-Language-%EB%85%BC%EB%A6%AC%ED%98%95-Boolean)을 참조한다.

<br>

### Example

<br>

```cs
bool boolean = true;
bool boolean2 = false;
```

<br>

## 참조 자료

<br>

- [null(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/null)
- [default(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/default)
- [bool(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/bool)
- [C# - Language - 논리형 (Boolean)](https://peponi-paradise.tistory.com/entry/C-Language-%EB%85%BC%EB%A6%AC%ED%98%95-Boolean)