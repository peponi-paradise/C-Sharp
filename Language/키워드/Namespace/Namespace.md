## Introduction

<br>

- `namespace` 키워드는 코드를 구성하는 고유한 `전역 형식`을 선언하는 데 사용한다.
- 모든 `namespace`는 암시적으로 `public`이며 한정자를 적용할 수 없다.
- `namespace` 내에는 다음과 같은 형식을 선언할 수 있다.
    - [class](https://peponi-paradise.tistory.com/entry/C-Language-%ED%81%B4%EB%9E%98%EC%8A%A4-class)
    - [interface](https://peponi-paradise.tistory.com/entry/C-Language-%EC%9D%B8%ED%84%B0%ED%8E%98%EC%9D%B4%EC%8A%A4-Interface)
    - [struct](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B5%AC%EC%A1%B0%EC%B2%B4-struct)
    - [enum](https://peponi-paradise.tistory.com/entry/C-Language-%EC%97%B4%EA%B1%B0%ED%98%95-enum)
    - [delegate](https://peponi-paradise.tistory.com/entry/C-Language-%EB%8C%80%EB%A6%AC%EC%9E%90-Delegate)
    - [namespace](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/namespace) : 파일 범위 네임스페이스 (C# 10 이상) 를 선언한 경우 허용되지 않는다.

<br>

### Example

<br>

```cs
// namespace 선언

namespace Foo
{
    class Bar { }

    namespace Nested
    {
        class MyClass { }
    } 
}
```

- `namespace`는 [partial](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/partial-type) 형식과 마찬가지로 여러 파일에 나누어 선언할 수 있다.
    ```cs
    // File1.cs

    namespace Foo
    {
        class Bar { }
    }
    ```
    ```cs
    // File2.cs

    namespace Foo
    {
        namespace Nested
        {
            class MyClass { }
        }
    }
    ```
- 상기 `File2.cs`의 선언은 아래의 선언과 동일하다.
    ```cs
    // File2.cs
    
    namespace Foo.Nested
    {
        class MyClass { }
    }
    ```

<br>

## File scoped namespace

<br>

- C# 10 버전 이상에서는 파일 단위 네임스페이스 선언이 가능하다.
    ```cs
    // File1.cs

    namespace Foo;

    class Bar { }
    ```
- 상기 예시와 같이 namespace를 선언하게 되면 `File1.cs`에 있는 모든 코드는 `Foo` namespace에 속하는 것으로 간주된다.
- `File scoped namespace` 선언은 nested를 허용하지 않는다. 아래와 같은 namespace 선언은 파일 단위 네임스페이스에서는 불가능하다.
    ```cs
    // File1.cs

    namespace Foo;

    class Bar { }

    namespace Nested;    // CS8954
    namespace Nested { } // CS8955
    ```
- 한편, 파일 단위 네임스페이스 선언을 개별 파일에 적용하는 것은 일반 namespace 선언과 마찬가지로 허용된다.
    ```cs
    // File1.cs

    namespace Foo;

    class Bar { }
    ```
    ```cs
    // File2.cs

    namespace Foo;

    class MyClass { }
    ```

<br>

## Default namespace

<br>

- 모든 네임스페이스의 최상위 요소에는 컴파일러가 추가한 기본 네임스페이스가 있다.
    - `Console project`에 추가되는 ConsoleApp namespace, `WinForms project`에 기본으로 추가되는 WinFormsApp namespace 등은 모두 기본 네임스페이스 아래에 있다.
    ```cs
    // Assembly 1

    using System;

    // default namespace (Global)

    public class DefaultNSClass
    {
        public void Write() => Console.WriteLine("Hello, World!");
    }

    namespace ConsoleApp1
    {
        internal class Program
        {
            private static void Main(string[] args)
            {
                DefaultNSClass defaultNSClass = new DefaultNSClass();
                defaultNSClass.Write();
            }
        }
    }
    ```

    ```cs
    // Assembly 2

    using System;

    namespace ConsoleApp2
    {
        internal class Program
        {
            private static void Main(string[] args)
            {
                // Don't need to declare 'using ~'
                DefaultNSClass defaultNSClass = new DefaultNSClass();
                defaultNSClass.Write();
            }
        }
    }
    ```

<br>

## 참조 자료

<br>

- [namespace](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/namespace)