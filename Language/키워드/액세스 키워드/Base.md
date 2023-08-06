## Introduction

<br>

- `base` 키워드는 상속받은 클래스의 멤버에 액세스하는 데 사용한다.
    - 상속받은 클래스의 생성자, 메서드, 인스턴스 속성 호출이 가능하다.
    - `static` method에서는 상속받은 클래스 호출이 불가능하다.

<br>

## Example

<br>

```cs
// Base class

public class Base
{
    public virtual bool Property { get; set; } = false;

    public Base(bool property)
    {
        Property = property;
        Console.WriteLine($"Base ctor - {Property}");
    }

    public virtual void Method() => Console.WriteLine("Base Method");
}
```

```cs
// Derived class

public class Derived : Base
{
    public Derived(bool property) : base(property) => Console.WriteLine($"Derived ctor - {property}");

    public override bool Property { get => base.Property; set => base.Property = value; }

    public override void Method()
    {
        base.Method();
        Console.WriteLine($"Derived Method - {Property}");
    }

    // public static void DerivedMethod() => base.Method();  // CS1511
}
```

```cs
// Test

static void Main()
{
    Derived d = new Derived(true);
    d.Method();
}

/* output:
Base ctor - True
Derived ctor - True
Base Method
Derived Method - True
*/
```

<br>

## 참조 자료

<br>

- [base(C# 참조)](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/base)