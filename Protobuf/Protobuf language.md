## Introduction

<br>

- 이 문서에서는 [proto3](https://protobuf.dev/programming-guides/proto3/) 문법 및 C#에서의 사용 방법을 알아본다.

<br>

## Proto definitions

<br>

```protobuf
// Protocol version 지정
syntax = "proto3";

// Package name 지정
package ProtoTest;

// C# namespace 지정 
// 옵션을 지정하지 않은 경우 package name이 namespace로 지정됨
option csharp_namespace = "MyProto";

// 다른 proto import
import "ProtoTest.Transaction.proto";
```

- `package` 또는 `csharp_namespace`로 namespace를 지정하는 방법은 C#과 동일하다.

<br>

### import public

<br>

```protobuf
// A.proto
```

```protobuf
// B.proto
// All proto files that import this proto will be import under proto also.
import public "A.proto";
```

```protobuf
// C.proto
import "B.proto";

// Use definitions from 'B.proto' and 'A.proto'
```

- `import public`을 선언 후 다른 곳에서 해당 proto를 import하면 `import public` 또한 같이 import 된다.

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
message Transaction
{
    string SenderAddress = 1;
    int32 SenderPort = 2;
    int32 TransactionID = 3;
}

message Message
{
    Transaction Transaction = 1;
    string Message = 2;
}
```

```protobuf
// Nested message

message Outer
{
    message Inner
    {
        int32 IntValue = 1 ;
    }

    Inner InnerValue = 1;
}

message Other
{
    Outer.Inner InnerValue = 1;
}
```

- 메시지는 필드를 담는 객체이며 [gRPC](https://grpc.io/) 통신에 사용되기도 한다. C#의 [class](https://peponi-paradise.tistory.com/entry/C-Language-%ED%81%B4%EB%9E%98%EC%8A%A4-class)와 비슷하다.
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
        1. 필드에 값이 할당된 경우 : 일반 필드와 같이 사용한다. wire로 serialize 될 수 있다.
        2. 필드에 값이 할당되지 않은 경우 : 기본값을 반환하며 wire로 serialize 될 수 없다.
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

#### Reserved field

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

<br>

#### Oneof

<br>

```protobuf
message OneOf
{
    oneof Selected{
        string A = 1;
        int32 B = 2;
        bool C = 3;
    }
}
```

```cs
private static void Main(string[] args)
{
    OneOf oneOf = new OneOf();
    oneOf.B = 1010;
    CheckOneof(oneOf);
}

private static void CheckOneof(OneOf oneOf)
{
    switch (oneOf.SelectedCase)
    {
        case OneOf.SelectedOneofCase.A:
            Console.WriteLine(oneOf.A);
            break;

        case OneOf.SelectedOneofCase.B:
            Console.WriteLine(oneOf.B);
            break;

        case OneOf.SelectedOneofCase.C:
            Console.WriteLine(oneOf.C);
            break;

        default:
            Console.WriteLine("None");
            break;
    }
}

/* output:
1010
*/
```

- `oneof`는 message field를 구성하는 기능 중 하나이다.
- `oneof`에 등록된 멤버들 중 하나만 값을 가질 수 있다.
- 어떤 형식이라도 멤버로 사용할 수 있지만 다음 제약 조건이 있다.
    - `oneof` 멤버의 형식에 `map`은 사용이 불가하다.
    - `oneof`는 `repeated`로 지정할 수 없다.
- Protobuf 컴파일러는 컴파일 시 `oneof` 키워드를 처리하여 enum을 생성해준다.
    - 이를 이용하여 C# 코드에서 설정된 필드가 무엇인지 찾을 수 있다.

<br>

### Comments

<br>

```protobuf
// Comments in single line

/* Comments in 
   Multiple line */
```

- Protobuf의 주석 작성법은 C#과 동일하다.

<br>

### Enumerations

<br>

```protobuf
enum NoticeType
{
    None = 0;
    UserNotify = 1;
    SystemMessage = 2;
    Warn = 3;
    Error = 4;
    Exception = 5;
}

message Notice
{
    NoticeType Type = 1;
    string Message = 2;
}
```

- Protobuf의 enum 작성법은 C#과 동일하다.
- enum의 제약사항으로, `첫번째 항목은 반드시 0`으로 지정되어야 한다.

<br>

#### Reserved value

<br>

```protobuf
enum Reserved
{
    reserved 1, 2, 3 to 5;
    reserved "NameA", "NameB";
}
```

- Reserved value는 key 또는 value를 점유하여 해당 값을 사용하지 못하게 한다.
- 향후 추가될 값을 미리 정의하거나 enum에서 제거된 값을 사용하지 못하게 하는 데 활용할 수 있다.

<br>

### Any

<br>

```protobuf
import "google/protobuf/any.proto";

package ProtoTest;

message Foo { }

message Bar { }

message Baz 
{
    google.protobuf.Any Data = 1;
}
```

```cs
using Google.Protobuf.WellKnownTypes;
using ProtoTest;

private static void Main(string[] args)
{
    Baz baz = new Baz();
    baz.Data = Any.Pack(new Foo());
    CheckType(baz);
}

private static void CheckType(Baz baz)
{
    if (baz.Data.Is(Foo.Descriptor))
    {
        Console.WriteLine("Foo");
    }
    else if (baz.Data.Is(Bar.Descriptor))
    {
        Console.WriteLine("Bar");
    }
    else
    {
        throw new ArgumentException($"{baz.Data} is unknown type");
    }
}

/* output:
Foo
*/
```

- `Any`는 재사용 가능한 형식으로, C#의 `object`와 비슷한 면이 있다.
- `google/protobuf/any.proto`를 import 해주어야 한다.
- `Pack(IMessage)`, `Unpack<T>()`, `TryUnpack<T>(out T)`, `Is(MessageDescriptor)` 등의 메서드를 지원하여 형변환 및 확인이 가능하다.

<br>

### Service

<br>

- `service`를 정의하기 위해 다음 Nuget 패키지를 설치한다.
    - Grpc

```protobuf
package ProtoTest;

message Transaction
{
    string SenderAddress = 1;
    int32 SenderPort = 2;
    int32 TransactionID = 3;
}

message Message
{
    Transaction Transaction = 1;
    string Message = 2;
}

service CoreService
{
    rpc Ping(Message) returns (Message);
}
```

- `service`는 RPC service (WCF, gRPC 등...) 에 message를 사용 가능하게 해준다.
- Protobuf compiler는 `service` 선언을 확인하여 interface code 및 stub을 생성한다.
- 아래는 생성한 service를 이용한 gRPC 통신의 예시다.

```cs
using ProtoTest;
using Grpc.Core;

private static void Main(string[] args)
{
    // RPC channel 생성
    Channel channel = new Channel("localhost", 50001, SslCredentials.Insecure);

    // Service client 생성
    var coreService = new CoreService.CoreServiceClient(channel);

    Transaction transaction = new Transaction() { SenderAddress = "127.0.0.1", SenderPort = 50001, TransactionID = 0 };
    Message message = new Message() { Transaction = transaction, Message_ = "Hello" };

    // Server에 Ping service 요청
    var returnMessage = coreService.Ping(message);
    Console.WriteLine(returnMessage);
}
```

<br>

#### gRPC - Protocol buffer 정의의 중요성

<br>

- `gRPC`통신을 하는 경우 Protocol buffer 요소는 통신을 위한 인터페이스이기 때문에 매우 중요하다.
    - 서버와 클라이언트 개발의 주체가 다르다면 특히 신중하게 다뤄야 한다.
        - Wire 형식이 달라지는 경우, 서버측에서 변경된 `*.proto` 파일을 제공하거나 알려주지 않으면 클라이언트 측에서는 이를 알기 어렵다.
          외국의 서버와 통신하는 과정에서 이런 일이 발생하였는데, 업데이트 시점마다 달라지는 인터페이스로 인해 고생한 일이 있었다. (물어볼 때 까지 알려주지를 않아 매번 통신 테스트를 하며 변경사항이 있는지 일일이 확인해야 했다.)
        - Enum의 key, value의 순서 또는 값을 변경하는 경우도 마찬가지다. enum을 제어 요소로 활용하거나 하는 경우, 클라이언트 측에서는 변경된 요소에 대해 알지 못해 어리둥절한 상황이 발생할 수 있다.
- 메시지에 대해 주의해야 하는 사항에 대한 공식 자료는 다음을 참조한다.
    - [Updating A Message Type](https://protobuf.dev/programming-guides/proto3/#updating)
    - [Consequences of Reusing Field Numbers](https://protobuf.dev/programming-guides/proto3/#consequences)
    - [Proto Best Practices](https://protobuf.dev/programming-guides/dos-donts/)

<br>

## 참조 자료

<br>

- [Language Guide (proto 3)](https://protobuf.dev/programming-guides/proto3/)
- [Protocol Buffers .NET Runtime Library API Reference](https://protobuf.dev/reference/csharp/api-docs/)
- [변형 형식을 위한 Protobuf Any 및 OneOf 필드](https://learn.microsoft.com/ko-kr/dotnet/architecture/grpc-for-wcf-developers/protobuf-any-oneof)