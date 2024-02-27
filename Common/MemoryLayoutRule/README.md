## Introduction

<br>

- [구조체](https://peponi-paradise.tistory.com/entry/C-Language-%EA%B5%AC%EC%A1%B0%EC%B2%B4-struct) 또는 [클래스](https://peponi-paradise.tistory.com/entry/C-Language-%ED%81%B4%EB%9E%98%EC%8A%A4-class)는 필드, 메서드 등을 캡슐화하는 데 사용한다.
- 선언된 멤버의 수에 따라 크기가 달라지게 되는데, 그 크기는 [sizeof, Marshal.SizeOf](https://주소넣기)로 구할 수 있다.
- 구조체 또는 클래스는 정의한 크기보다 크거나 필드의 위치가 바뀌는 일이 발생하는데, 이를 해결하기 위해 어느 정도 메모리 레이아웃 규칙에 대해 알 필요가 있다.

<br>

## 기본 레이아웃

<br>

- 다음 구조체의 크기는 C#의 형식 기본 크기에 따라 3byte가 되어야 한다.
    ```cs
    public struct Test
    {
        public byte A;      // 1 byte
        public short B;     // 2 byte
    }
    ```
- 하지만 실제로 크기를 조사해보면, 4byte가 나오게 된다.
    ```cs
    unsafe static void Main(string[] args)
    {
        Console.WriteLine(Marshal.SizeOf<Test>());
    }

    /* output:
    4
    */
    ```
- 다음 구조체의 크기는 11byte로 예상되지만, 실제로는 16byte가 나오게 된다.
    ```cs
    public struct Test
    {
        public byte A;      // 1 byte
        public short B;     // 2 byte
        public double C;    // 8 byte
    }

    unsafe static void Main(string[] args)
    {
        Console.WriteLine(Marshal.SizeOf<Test>());
    }

    /* output:
    16
    */
    ```
- 상기 예제의 현상을 자세히 파악하기 위해 메모리 할당을 확인해보면 아래와 같다.
    ```cs
    unsafe static void Main(string[] args)
    {
        Test t = new();
        var addr = (byte*)&t;

        Console.WriteLine(Marshal.SizeOf<Test>());

        Console.WriteLine($"Offset : {(byte*)&t.A - addr}");
        Console.WriteLine($"Offset : {(byte*)&t.B - addr}");
        Console.WriteLine($"Offset : {(byte*)&t.C - addr}");

        Console.WriteLine($"Size : {Marshal.SizeOf(t.A)}");
        Console.WriteLine($"Size : {Marshal.SizeOf(t.B)}");
        Console.WriteLine($"Size : {Marshal.SizeOf(t.C)}");
    }

    /* output:
    16
    Offset : 0
    Offset : 2
    Offset : 8
    Size : 1
    Size : 2
    Size : 8
    */
    ```
    ![basicLayout](./image/BasicLayout.png)
- 상기 예제와 같은 현상이 발생하는 이유는 아래에서 확인한다.

<br>

## 메모리 레이아웃 규칙

<br>

- C#의 기본 메모리 레이아웃에는 아래 두 가지 규칙이 적용된다.
    1. 각 필드는 선언된 순서에 따라 메모리에 할당된다.
    2. 모든 필드 중 가장 큰 변수의 크기를 기준으로 필드들이 정렬된다.
        - 예로, 가장 큰 변수의 크기가 short라면 2byte, double이라면 8byte 단위로 필드가 정렬된다.
- 위 규칙을 이용하여 중간에 비어 있는 메모리 공간이 최소화되도록 상기 예제를 정렬하면 아래와 같다.
    ```cs
    public struct Test
    {
        public double A;    
        public short B;     
        public byte C;      
    }

    unsafe static void Main(string[] args)
    {
        Test t = new();
        var addr = (byte*)&t;

        Console.WriteLine(Marshal.SizeOf<Test>());

        Console.WriteLine($"Offset : {(byte*)&t.A - addr}");
        Console.WriteLine($"Offset : {(byte*)&t.B - addr}");
        Console.WriteLine($"Offset : {(byte*)&t.C - addr}");

        Console.WriteLine($"Size : {Marshal.SizeOf(t.A)}");
        Console.WriteLine($"Size : {Marshal.SizeOf(t.B)}");
        Console.WriteLine($"Size : {Marshal.SizeOf(t.C)}");
    }

    /* output:
    16
    Offset : 0
    Offset : 8
    Offset : 10
    Size : 8
    Size : 2
    Size : 1
    */
    ```
    ![arrangedBasicLayout](./image/ArrangedBasicLayout.png)
- 위의 규칙을 잘 이용하면, 구조체를 효율적으로 작성할 수 있다.
    ```cs
    // 비효율적인 예
    public struct Inefficient
    {
        public byte A;
        public int B;
        public byte C;
        public short D;
    }

    unsafe static void Main(string[] args)
    {
        Console.WriteLine(Marshal.SizeOf<Inefficient>());
    }
    
    /* output:
    12
    */
    ```
    ```cs
    // 레이아웃 수정
    public struct Efficient
    {
        public byte A;
        public byte C;
        public short D;
        public int B;
    }

    unsafe static void Main(string[] args)
    {
        Console.WriteLine(Marshal.SizeOf<Efficient>());
    }
    
    /* output:
    8
    */
    ```

<br>

## 메모리 레이아웃 변경

<br>

- 통신을 하거나 dll에 구조체를 넘기는 등의 작업을 할 때는 레이아웃이 비효율적이더라도 따라야 하고, 크기 역시 강제되어 있는 경우가 있다.
- .NET에서 자동으로 처리되는 메모리 구조가 이에 맞지 않는 경우가 발생할 수 있는데, 다음 어트리뷰트를 사용해 레이아웃을 변경할 수가 있다.
    ```cs
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Test
    { ... }
    ```
    - 상기 어트리뷰트의 필드에 대한 설명은 아래를 참조한다.
        |항목|세부 항목|내용|
        |-------|-------|-------|
        |Layoutkind|Sequential|필드 선언 순서에 따라 레이아웃 정렬을 수행한다. (값 형식 기본값)<br>Unmanaged memory 영역에서 레이아웃이 유지된다.|
        ||Explicit|구체적인 필드의 위치를 지정한다.<br>각 필드는 `[FieldOffset(value)]` 어트리뷰트를 넣어주어야 한다.<br>Managed, unmanaged memory 영역에서 레이아웃이 유지된다.|
        ||Auto|런타임에 객체의 각 멤버들을 적절한 구조로 재구성한다. (참조 형식 기본값)<br>Managed 영역 밖에 노출될 수 없다. (마샬링을 할 수 없다)|
        |Pack||정렬을 수행할 byte 단위를 설정한다<br>값은 0, 1, 2, 4, 8, 16, 32, 64, 128이 허용된다.|

- 앞 부분의 정리되지 않은 `Test` struct에 `StructLayout` 어트리뷰트를 달아 레이아웃 정렬이 가능하며, 할당되는 크기를 줄일 수 있다.
    ```cs
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Test
    {
        [FieldOffset(10)]
        public byte A;

        [FieldOffset(8)]
        public short B;

        [FieldOffset(0)]
        public double C;
    }

    unsafe static void Main(string[] args)
    {
        Test t = new();
        var addr = (byte*)&t;

        Console.WriteLine(Marshal.SizeOf<Test>());

        Console.WriteLine($"Offset : {(byte*)&t.A - addr}");
        Console.WriteLine($"Offset : {(byte*)&t.B - addr}");
        Console.WriteLine($"Offset : {(byte*)&t.C - addr}");

        Console.WriteLine($"Size : {Marshal.SizeOf(t.A)}");
        Console.WriteLine($"Size : {Marshal.SizeOf(t.B)}");
        Console.WriteLine($"Size : {Marshal.SizeOf(t.C)}");
    }

    /* output:
    11
    Offset : 10
    Offset : 8
    Offset : 0
    Size : 1
    Size : 2
    Size : 8
    */
    ```

<br>

## Win32 BOOL type

<br>

- 기본적으로 `bool` 형식은 마샬링이 일어날 시 형식이 달라지게 된다.
- `Managed type`의 `bool`은 크기가 1 byte지만, `Unmanaged memory`로 마샬링되는 `bool`은 Win32의 `BOOL` type (INT로 정의됨) 으로 변환되는데, 4 byte의 크기를 가지게 된다.
- 따라서 통신 등에 활용 시 주의가 필요한 경우가 발생할 수 있다.
    ```cs
    public struct Test
    {
        public bool A;
    }

    static void Main(string[] args)
    {
        Console.WriteLine(Marshal.SizeOf<Test>());
    }

    /* output:
    4
    */
    ```

<br>

## 참조 자료

<br>

- [StructLayoutAttribute Class](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.structlayoutattribute?view=net-8.0)
- [StructLayoutAttribute.Pack Field](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.structlayoutattribute.pack?view=net-8.0)
- [CA1414: MarshalAs로 부울 P/Invoke 인수를 표시합니다.](https://learn.microsoft.com/ko-kr/visualstudio/code-quality/ca1414?view=vs-2019&tabs=csharp)
- [객체의 메모리 레이아웃에 대하여](https://www.csharpstudy.com/DevNote/Article/10)
- [제목 작성해야함](주소)

<br>

## 더 공부해야 할 것

<br>

- [눈으로 확인하는 LayoutKind 옵션 효과](https://www.sysnet.pe.kr/2/0/1558)