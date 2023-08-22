## Introduction

<br>

- 이 문서에서는 [proto3](https://protobuf.dev/programming-guides/proto3/) 문법 및 C#에서의 사용 방법을 알아본다.

<br>

## Protocol format

<br>

```protobuf
// Protocol version 지정
syntax = "proto3";

// Package name 지정
package ProtoTest;

// C# namespace 지정 
// 해당 옵션을 사용하지 않는 경우 package name이 namespace로 지정됨
option csharp_namespace = "MyProto";

// 다른 proto import
import "ProtoTest.Core.proto";
```

<br>

## Scalar value types

<br>

- `Protobuf`는 자체 type을 가지고 있으며, C# 언어에 대응하는 형식은 아래 표를 참조한다.

|Protobuf|C#|Note|
|---|---|---|
|double|double||
|float|float||
|int32|int|Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint32 instead.|
|int64|long|Uses variable-length encoding. Inefficient for encoding negative numbers – if your field is likely to have negative values, use sint64 instead.|
|uint32|uint|Uses variable-length encoding.|
|uint64|ulong|Uses variable-length encoding.|
|sint32|int|Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int32s.|
|sint64|long|Uses variable-length encoding. Signed int value. These more efficiently encode negative numbers than regular int64s.|
|fixed32|uint|Always four bytes. More efficient than uint32 if values are often greater than 2<sup>28</sup>.|
|fixed64|ulong|Always eight bytes. More efficient than uint64 if values are often greater than 2<sup>56</sup>.|
|sfixed32|int|Always four bytes.|
|sfixed64|long|Always eight bytes.|
|bool|bool|
|string|string|A string must always contain UTF-8 encoded or 7-bit ASCII text, and cannot be longer than 2<sup>32</sup>.|
|bytes|ByteString|May contain any arbitrary sequence of bytes no longer than 2<sup>32</sup>.|

- 다른 언어에 대한 내용은 [Scalar Value Types](https://protobuf.dev/programming-guides/proto3/#scalar)을 참조한다.

<br>

## Language

<br>

### Message

<br>

```protobuf
// Message
message Transaction
{
    string SenderAddress = 1;
    int32 SenderPort = 2;
    int32 TransactionID = 3;
}
```

- 메시지는 필드를 담는 객체이며 [gRPC](https://grpc.io/) 통신에 사용한다. C#의 [Record](https://peponi-paradise.tistory.com/entry/C-Language-%EB%A0%88%EC%BD%94%EB%93%9C-%ED%98%95%EC%8B%9D-Record)와 비슷하다.
- 메시지 안에 들어있는 필드는 name/value pair로 구성된다.
    Value 값의 범위는 1 ~ 536,870,911 이며 다음 제약을 가진다.
    - Value 값은 한 메시지 내에서 고유하다.
    - 19,000 ~ 19,999 값은 Protocol buffer 정의에 의해 예약되어 있다.
    - [Reserved](https://protobuf.dev/programming-guides/proto3/#fieldreserved) field 또는 [Extension](https://protobuf.dev/programming-guides/proto2/#extensions)에 정의된 값은 사용할 수 없다.
- 필드에는 다음과 같은 레이블을 추가할 수 있다.
    ```protobuf
    message Optional
    {
    	optional int32 TestInt = 1;
    }

    message Repeated
    {
    	repeated int32 TestInts = 1;
    }

    message Map
    {
    	map<string, int32> TestPairs = 1;
    }
    ```
    - optional
        1. 필드에 값이 할당된 경우 일반 필드와 같이 사용한다. wire로 serialize 될 수 있다.
        2. 필드에 값이 할당되지 않은 경우 기본값을 반환하며 wire로 serialize 될 수 없다.
    - repeated
        - repeated 레이블이 지정된 필드는 enumerator type으로 동작한다.
        - [IList<T>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.ilist-1?view=net-7.0)를 구현하여 LINQ 쿼리 사용 및 배열, 리스트로 변환이 가능하다.
    - map
        - map 레이블이 지정된 필드는 key/value pair type으로 동작한다.
        - [IDictionary<TKey,TValue>](https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.idictionary-2?view=net-7.0)를 구현하여 LINQ 쿼리가 가능하다.

<br>

#### Message 사용 방법

<br>

```cs
private static void Main(string[] args)
{
    Optional opt = new Optional();

    Console.WriteLine($"Has value : {opt.HasTestInt}, value : {opt.TestInt}");

    Repeated rpt = new Repeated();
    rpt.TestInts.Add(1);
    rpt.TestInts.Add(2);

    Console.WriteLine(rpt);

    Map map = new Map();
    map.TestPairs.Add("A", 1);
    map.TestPairs.Add("B", 2);

    Console.WriteLine(map);
}

/* output:
Has value : False, value : 0
{ "TestInts": [ 1, 2 ] }
{ "TestPairs": { "A": 1, "B": 2 } }
*/
```

<br>

### Reserved field

<br>

```protobuf
message Reserved
{
    reserved 1, 2, 3 to 5;
    reserved "NameA", "NameB";

    // Following lines will be occurring error by protobuf compiler
    int32 TestInt = 2;
    int32 NameA = 6;
}
```

- Reserved field는 key 또는 value를 점유하여 해당 값을 사용하지 못하게 한다.
- 향후 추가될 필드를 미리 정의하거나 메시지에서 제거된 필드를 사용하지 못하게 하는 데 활용할 수 있다.
    - gRPC의 메시지는 통신을 위한 인터페이스이기 때문에 매우 중요하다.
      서버와 클라이언트 개발 및 운용의 주체가 다르다면 메시지를 신중하게 다뤄야 하는데, wire 형식이 달라지면 서버측에서 알려주지 않는 한 클라이언트 측에서는 변경된 인터페이스를 알 방법이 없다.
      `.proto` 파일을 제공해주지 않는 외국의 서버와 통신해야 하는 과정에서 이런 일이 발생하였는데, 업데이트 시점마다 달라지는 인터페이스로 인해 고생한 일이 있다. (물어볼 때 까지 알려주지를 않아 매번 통신 테스트를 하며 변경사항을 확인해야 했다.)
    - 메시지에 대해 주의해야 하는 사항에 대한 공식 자료는 [Consequences of Reusing Field Numbers](https://protobuf.dev/programming-guides/proto3/#consequences), [Proto Best Practices](https://protobuf.dev/programming-guides/dos-donts/)에서 확인할 수 있다.

<br>

### Comments

<br>

```protobuf
// Comments in single line

/* Comments in 
   Multiple line */
```

- Protobuf의 주석 작성법은 C#과 동일하다.