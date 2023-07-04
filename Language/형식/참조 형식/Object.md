## Introduction

<br>

- `object (System.Object)`는 C# 모든 형식이 직간접적으로 상속하는 형식이다.
- `null`을 포함한 모든 형식의 값을 할당할 수 있다.
- `값 형식`의 변수가 `object`로 변환되면 `boxed`, `object`가 `값 형식`으로 변환되면 `unboxed`라고 한다. ([Boxing 및 Unboxing(C# 프로그래밍 가이드)](https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/types/boxing-and-unboxing))

<br>

## object 초기화

<br>

- `object`를 초기화 하는 방법은 아래와 같다.
    ```cs
    // object 형식에는 모든 형식의 값을 할당할 수 있다.
    object A = 1;
    object B = "A";
    object C = new MyClass();
    ```

<br>

## object 상속

<br>

- .NET의 모든 클래스는 암시적으로 `object`를 상속받는다.
- 따라서 `object`의 메서드 중 일부를 재정의하여 사용할 수 있다.
    - [public virtual bool Equals (object? obj)](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.equals?view=net-7.0) : 값이 같은지 확인
    - [public virtual string? ToString()](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.tostring?view=net-7.0) : 객체를 나타내는 문자열 반환
    - [public virtual int GetHashCode()](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.gethashcode?view=net-7.0) : 객체의 해시 코드 반환
    - [Finalize](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.finalize?view=net-7.0) : 소멸자
    ```cs
    internal class MyClass
    {
        public int X { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return obj is MyClass && X == ((MyClass)obj).X;
        }

        public override string ToString() => X.ToString();

        public override int GetHashCode() => X.GetHashCode();
    }
    ```

<br>

## object 비교

<br>

- `object`로 boxing 되어있는 변수를 비교하는 일이 발생할 수 있다.
- 값을 비교하는 경우 [Object.Equals](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.equals?view=net-7.0)을 사용한다.
- 참조를 비교하는 경우 (동일 인스턴스인지 확인) [Object.ReferenceEquals](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.referenceequals?view=net-7.0)을 사용한다.

```cs
internal class MyClass
{
    public int X { get; init; }

    public MyClass(int x) => X = x;

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        return obj is MyClass && X == ((MyClass)obj).X;
    }

    public override string ToString() => X.ToString();

    public override int GetHashCode() => X.GetHashCode();
}
```
```cs
object C = new MyClass(10);

Console.WriteLine(C.Equals(new MyClass(10)));         // True
Console.WriteLine(C.Equals(10));                    // False

object D = new MyClass();

Console.WriteLine(Object.ReferenceEquals(C, D));    // False

D = C;

Console.WriteLine(Object.ReferenceEquals(C, D));    // True
```