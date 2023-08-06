## Introduction

<br>

- `using` 지시문은 네임스페이스에 정의된 형식을 정규화된 형식명 없이 사용 가능하게 해준다.
- `using` 지시문의 범위는 해당 namespace가 정의되는 파일이다.
    - `using` 지시문은 지정한 namespace 안의 nested namespace까지는 액세스 권한을 제공하지 않는다.
- `using` 지시문은 다음 범위에 선언해야 한다.
    - 소스 파일의 시작 부분
    - namespace의 시작 부분
- `using` 지시문에는 다음 한정자를 적용할 수 있다.
    - global
    - static
    - global static

<br>

### Example

<br>

```cs
// Without using

class Foo
{
    void Bar() => System.Console.WriteLine("Hello world!");
}
```

```cs
// With using

using System;

class Foo
{
    void Bar() => Console.WriteLine("Hello world!");
}
```

<br>

### Example - Nested namespace

<br>

- `using` 지시문을 사용하더라도, nested namespace의 경우 정규화된 형식명을 사용해야 접근이 가능하다.
    ```cs
    namespace BaseNamespace
    {
        namespace NestedNamespace
        {
            public class Class
            {
                public void Method() => System.Console.WriteLine("Hello world!");
            }
        }
    }
    ```
    ```cs
    using BaseNamespace;

    public class TestClass
    {
        private void Method()
        {
            // Class Class = new Class(); // CS0246
            BaseNamespace.NestedNamespace.Class c = new BaseNamespace.NestedNamespace.Class();
            c.Method();
        }
    }
    ```

<br>

## Global using

<br>

- C# 10부터는 `using` 지시문에 `global` 한정자를 추가하는 것이 가능하다.
- `global` 한정자는 프로젝트의 모든 파일에 해당 `using`이 적용되는 것을 의미한다.
- `global using` 지시문은 `using`을 포함한 모든 항목 앞에 위치해야 한다.
    ```cs
    // OK

    global using System.Net;
    using System.Threading;
    ```
    ```cs
    // CS8915

    using System.Threading;
    global using System.Net;
    ```
- `global using`은 가능하면 하나의 파일에 추가하는 것이 좋다.
    - 여러 곳에 분산되면 사용되는 `using`을 관리하기 어려워질 수 있다.

<br>

## Using static

<br>

- `using static` 지시문은 정적 멤버 및 nested type에 액세스할 수 있는 기능을 제공한다.
    - 인스턴스 멤버의 경우 형식 인스턴스를 통해서만 호출 가능하다.

```cs
namespace Foo
{
    public class Bar
    {
        public static void Write() => System.Console.WriteLine("Hello world!");

        public void Write2() => System.Console.WriteLine("Hello world2!");
    }
}
```
```cs
using Foo;

internal class Program
{
    static void Main(string[] args)
    {
        Bar.Write();

        //Bar.Write2();   // CS0120
        new Bar().Write2();
    }
}
```

<br>

## Global using static

<br>

- `using` 지시문은 `global` 및 `static` 한정자를 모두 적용할 수 있다.
    - 이 때, 적용되는 특성은 global + static이다.

```cs
// File1.cs

namespace Foo
{
    public class Bar
    {
        public static void Write() => System.Console.WriteLine("Hello world!");

        public void Write2() => System.Console.WriteLine("Hello world2!");
    }
}
```
```cs
// File2.cs

global using static Foo.Bar;
```
```cs
// File3.cs

namespace Other
{
    public class Test
    {
        void Method() => Write();
    }
}
```

<br>

## Using alias

<br>

- `using` 지시문은 별칭을 지정하는 것이 가능하다.

```cs
// File1.cs

namespace Foo
{
    public class Bar
    {
        public void Write() => System.Console.WriteLine("Hello world!");
    }
}
```
```cs
// File2.cs

using MyNamespace = Foo;

public class Test
{
    void Method() => new MyNamespace.Bar().Write();
}
```

<br>

- 한편, `using` 별칭 지시문은 제네릭 형식을 가질 수 없다.

```cs
// File1.cs

namespace Foo
{
    public class Bar<T>
    {
        public void Write() => System.Console.WriteLine("Hello world!");
    }
}
```
```cs
// File2.cs

// using MyNamespace = Foo.Bar;    // CS0305
using MyNamespace = Foo.Bar<int>;

public class Test
{
    void Method() => new MyNamespace().Write();
}
```

<br>

## 참조 자료

<br>

- [using 지시문](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/using-directive)