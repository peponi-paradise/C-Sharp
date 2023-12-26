## Introduction

<br>

- `==`, `!=` 연산자는 피연산자가 같은지 여부를 확인한다.
  - [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)은 값을 비교한다.
  - [참조 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D)은 참조 주소를 비교한다.
- `==` 연산자는 피연산자가 같으면 `true`, 다르면 `false`를 반환한다.
- `!=` 연산자는 `==` 연산자의 반대 결과를 반환한다.

<br>

## 값 형식

<br>

```cs
int X = 1;
int Y = 2;
int Z = 3;

Console.WriteLine(X == Y);
Console.WriteLine(X != Z);
Console.WriteLine(X + Y == Z);

/* output:
False
True
True
*/
```

- `struct` 형식의 경우 기본적으로 같음 연산자를 지원하지 않는다.
  - 같음 연산자를 사용하려면 형식에 해당 연산자를 오버로드해야 한다.
  ```cs
  public struct StructTest
  {
      public int X;

      public static bool operator ==(StructTest left,StructTest right)
      {
          return left.X == right.X;
      }
      public static bool operator !=(StructTest left, StructTest right)
      {
          return left.X != right.X;
      }
  }
  ```
  ```cs
  StructTest a = new StructTest();
  StructTest b = new StructTest();
  a.X = 10;
  b.X = 20;

  Console.WriteLine(a == b);
  Console.WriteLine(a != b);

  /* output:
  False
  True
  */
  ```

<br>

## 참조 형식

<br>

- `참조 형식`은 기본적으로 참조 주소를 비교한다.
  ```cs
  public class TestClass
  {
    public int X;

    public TestClass(int x) => X = x;
  }
  ```
  ```cs
  var a = new TestClass(5);
  var b = new TestClass(5);
  var c = b;

  Console.WriteLine(a == b);
  Console.WriteLine(b == c);

  /* output:
  False
  True
  */
  ```
  - `class`의 경우 `struct`와 마찬가지로 같음 연산자를 오버로드할 수 있다.
    - 같음 연산자를 오버로드 한 후 참조 주소를 비교하려는 경우, [Object.ReferenceEquals()](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.referenceequals?view=net-8.0) 메서드를 사용하여 비교할 수 있다.
      ```cs
      public class TestClass
      {
          public int X;

          public TestClass(int x) => X = x;

          public static bool operator ==(TestClass left, TestClass right)
          {
              return left.X == right.X;
          }

          public static bool operator !=(TestClass left, TestClass right)
          {
              return left.X != right.X;
          }
      }
      ```
      ```cs
      var a = new TestClass(5);
      var b = new TestClass(5);
      var c = b;

      Console.WriteLine(a == b);
      Console.WriteLine(b == c);
      Console.WriteLine(object.ReferenceEquals(a, b));
      Console.WriteLine(object.ReferenceEquals(b, c));

      /* output:
      True
      True
      False
      True
      */
      ```

- `record` 형식의 경우 기본적으로 값 비교를 지원하여 값 및 참조 형식의 비교가 가능하다.
  - `값 형식`의 경우 값을 비교한다.
  - `참조 형식`의 경우 참조 주소를 비교한다.
  ```cs
  public record Coordinate(int X, int Y);
  public record CoordinateCollection(int ID, List<Coordinate> Coordinates);
  ```
  ```cs
  var a = new Coordinate(1, 2);
  var b = new Coordinate(2, 3);
  var c = new Coordinate(2, 3);

  Console.WriteLine(a == b);
  Console.WriteLine(b == c);

  var d = new CoordinateCollection(1, new() { new(1,2) });
  var e = new CoordinateCollection(1, new() { new(1,2) });

  Console.WriteLine(d == e);

  /* output:
  False
  True
  False
  */
  ```
  - `record` 형식의 경우 같음 연산자를 오버로드 할 수 없다.
    - 같음 연산자의 동작을 변경하는 경우 [IEquatable\<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.iequatable-1?view=net-8.0)를 구현해야 한다.
      ```cs
      public record Coordinate(int X, int Y) : IEquatable<Coordinate>
      {
          public virtual bool Equals(Coordinate? other)
          {
              return other is not null && (X == other.X || Y == other.Y);
          }
      }
      ```
      ```cs
      var a = new Coordinate(1, 3);
      var b = new Coordinate(2, 1);
      var c = new Coordinate(2, 3);

      Console.WriteLine(a == b);
      Console.WriteLine(b == c);
      Console.WriteLine(a == c);

      /* output:
      False
      True
      True
      */
      ```

- `string`의 경우 참조 형식이지만 값 형식처럼 비교할 수 있다.
  - 두 문자열이 같은 길이 및 문자를 가질 때 동일하다.
  ```cs
  string a = "ABCde";
  string b = "abcDE";

  Console.WriteLine(a == b);
  Console.WriteLine(a.ToLower() == b.ToLower());

  /* output:
  False
  True
  */
  ```

- `delegate`의 경우 호출 길이가 같고 동일한 호출 순서를 가질 때 동일하다.
  ```cs
  Action action1 = () => Console.WriteLine();
  Action action2 = () => Console.WriteLine();

  Action a = action1 + action2;
  Action b = action1 + action2;
  Action c = action2 + action1;

  Console.WriteLine(a == b);
  Console.WriteLine(b == c);
  Console.WriteLine(object.ReferenceEquals(a, b));

  /* output:
  True
  False
  False
  */
  ```

<br>

## 참조 자료

<br>

- [같음 연산자](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/equality-operators)
- [값 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B0%92-%ED%98%95%EC%8B%9D)
- [참조 형식](https://peponi-paradise.tistory.com/entry/C-Language-%EC%B0%B8%EC%A1%B0-%ED%98%95%EC%8B%9D)
- [Object.ReferenceEquals()](https://learn.microsoft.com/ko-kr/dotnet/api/system.object.referenceequals?view=net-8.0)
- [IEquatable<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.iequatable-1?view=net-8.0)