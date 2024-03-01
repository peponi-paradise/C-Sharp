## Introduction

<br>

- `sizeof` 연산자는 주어진 형식의 크기를 바이트 단위로 반환한다.
- `sizeof`에 사용 가능한 형식은 [비관리형 형식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/unmanaged-types)이다.
- 관리 메모리 영역에 할당될 byte 수를 반환하며, 컴파일 타임에 상수 값으로 계산된다.
- 메모리 레이아웃 규칙에 따라 `sizeof`에 의해 예상되는 크기가 실제와는 다를 수 있다.
- 메모리 레이아웃 규칙에 대한 자세한 내용은 [C# - Data structure alignment (Memory layout)](https://peponi-paradise.tistory.com/entry/C-Data-structure-alignment-Memory-layout)를 참조한다.

<br>

## Example

<br>

```cs
public struct Test
{
   public bool A;
   public short B;
}
```
```cs
static void Main(string[] args)
{
    unsafe
    {
        Console.WriteLine(sizeof(Test));    // 예상 크기 : 3
    }
}

/* output:
4
*/
```

<br>

## Marshal.SizeOf

<br>

- 일반적으로는 `sizeof` 연산자 대신 [Marshal.SizeOf](https://learn.microsoft.com/ko-kr/dotnet/api/system.runtime.interopservices.marshal.sizeof?view=net-8.0) 메서드를 사용한다.
  - 런타임에 동작하는 `Marshal.SizeOf` 메서드는 값 형식과 참조 형식 모두 사용 가능하다.
  - 비관리 메모리 영역에 할당될 byte 수를 반환한다.
  - 클래스의 경우에는 LayoutKind.Sequential 또는 LayoutKind.Explicit 선언을 해주어야 한다.

```cs
static void Main(string[] args)
{
    unsafe
    {
        Console.WriteLine(sizeof(Test));
    }
    Console.WriteLine(Marshal.SizeOf<Test>());
}

/* output:
4
8
*/
```

<br>

## 값 형식의 기본 크기

<br>

- C# 의 값 형식에 따른 기본 크기는 다음 테이블과 같다.

|형식|값 (bytes)|
|-------|-------|
|sbyte|1|
|byte|1|
|bool|1|
|char|2|
|short|2|
|ushort|2|
|int|4|
|uint|4|
|long|8|
|ulong|8|
|float|4|
|double|8|
|decimal|16|

<br>

## 참조 자료

<br>

- [sizeof 연산자 - 지정된 형식에 대한 메모리 요구 사항 결정](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/operators/sizeof)
- [비관리형 형식](https://learn.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/unmanaged-types)
- [Marshal.SizeOf](https://learn.microsoft.com/ko-kr/dotnet/api/system.runtime.interopservices.marshal.sizeof?view=net-8.0)
- [Using sizeof() Operator in C#](https://code-maze.com/csharp-sizeof-operator/)
- [C# - Data structure alignment (Memory layout)](https://peponi-paradise.tistory.com/entry/C-Data-structure-alignment-Memory-layout)