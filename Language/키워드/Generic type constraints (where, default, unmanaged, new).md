## where

<br>

- `where`는 제네릭 정의에 대해 형식 제약 조건을 지정하게 해준다.
- 형식 제약 조건에는 인터페이스, 클래스 및 구조체 형식 등을 설정할 수 있다.
- 제네릭 정의에는 여러 제약 조건을 지정할 수 있으며 각각의 제약 조건에 `where`를 적용할 수 있다.

<br>

### Example

<br>

```cs
// T에 where 적용
public class Foo<T> where T : ICollection<T> { }

// 참조 형식
public class Bar<T> where T : class
// 값 형식
public class Baz<T> where T : struct
```
```cs
public class Base { }
public class Derived : Base { }
public class Other { }

// Base 또는 Base에서 파생된 class만 허용
public class Foo<T> where T : Base { }

static void Main()
{
    Foo<Derived> foo = new();   // OK
    // Foo<Other> bar = new();     // CS0311
}
```
```cs
// 여러 가지 제약 조건
public class Foo<T, U, V> where T : class
                          where U : struct
                          where V : System.Enum
{ }
```

<br>

## Nullable context

<br>

- 형식 제약 조건이 nullable인 경우 제네릭 메서드에서 `T?` 형식 제약 조건 사이에 모호성이 발생할 수 있다.
- `override` 메서드에 제약 조건을 포함할 수 없기 때문인데, 이 때 [default](https://peponi-paradise.tistory.com/entry/C-Language-Literal-keywords#default-1) 키워드를 통해 해결할 수 있다.

<br>

### Example

<br>

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

## unmanaged constraint

<br>

- 형식 제약 조건에는 `unmanaged`가 설정될 수 있다.
- `unmanaged`로 설정하는 경우 매개 변수를 [비관리형 형식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/unmanaged-types)으로 제한한다.

<br>

### Example

<br>

```cs
public class InteropClass<T> where T : unmanaged
{
    public void Write(T input) => Console.WriteLine(input);
}

static void Main()
{
    InteropClass<int> interopClass = new InteropClass<int>();
    interopClass.Write(50);
}
```

<br>

## new constraint

<br>

- `new` 제약 조건은 제네릭 클래스 또는 메서드에 전달되는 형식 매개 변수에 파라미터가 없는 생성자가 있어야 한다는 것을 의미한다.
- 또한 제약 조건에 의해 `abstract`로 선언이 불가능하다.
- 다른 제약 조건과 함께 적용하는 경우, `new`가 가장 마지막에 지정되어야 한다.

<br>

### Example

<br>

```cs
// 파라미터를 받아야 하는 생성자를 가진 클래스 생성
public class Foo
{
    public Foo(int i) => Console.WriteLine(i);
}
```
```cs
// 제네릭 클래스 new() 제약 조건
public class Bar<T> where T : new()
{ }

public class Baz
{
    // 제네릭 메서드 new() 제약 조건
    public void Qux<T>(T quux) where T : class, new()
    {
        Console.WriteLine(quux);
    }
}
```
```cs
static void Main(string[] args)
{
    Bar<Foo> bar = new Bar<Foo>();  // CS0310: 'Foo'가 매개 변수가 없는 public 생성자를 사용하는 비추상 형식이어야 합니다.
    Baz baz = new();
    baz.Qux<Foo>(new Foo(1));       // CS0310: 'Foo'가 매개 변수가 없는 public 생성자를 사용하는 비추상 형식이어야 합니다.
}
```

<br>

## 참조 자료

<br>

- [where(제네릭 형식 제약 조건)(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/where-generic-type-constraint)
- [C# - Language - Literal keywords (null, default, bool)](https://peponi-paradise.tistory.com/entry/C-Language-Literal-keywords)