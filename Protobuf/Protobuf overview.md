## Introduction

<br>

- [Protobuf](https://protobuf.dev/overview/)는 [Interface description language](https://en.wikipedia.org/wiki/Interface_description_language)로, 구조화된 데이터를 직렬화하는 언어 중립적인 형식이다.
- Google의 [gRPC](https://ko.wikipedia.org/wiki/GRPC) 서비스에서 통신 메시지 형식으로 사용된다.
- 공식 문서의 overview에는 다음과 같이 소개되어 있다.
    > Protocol Buffers are a language-neutral, platform-neutral extensible mechanism for serializing structured data.
    >> It’s like JSON, except it’s smaller and faster, and it generates native language bindings. You define how you want your data to be structured once, then you can use special generated source code to easily write and read your structured data to and from a variety of data streams and using a variety of languages.<br>
    Protocol buffers are a combination of the definition language (created in `.proto` files), the code that the proto compiler generates to interface with data, language-specific runtime libraries, and the serialization format for data that is written to a file (or sent across a network connection).

<br>

## Installation

<br>

- 일반적인 .NET 프로젝트에서 `Protobuf`를 사용하는 방법은 두 가지가 있다. (ASP.NET Core의 gRPC 서비스 프로젝트는 자동으로 패키지 include가 되어있다.)
    - [Protocol Buffer compiler](https://protobuf.dev/downloads/) 설치 후 컴파일러를 따로 사용
    - `Grpc.Tools` Nuget 패키지 설치 후 Visual studio에서 컴파일
- 아래는 `Grpc.Tools`를 이용한 예시이다.

<br>

### .NET framework

<br>

1. 다음 Nuget package를 설치한다.
    - Google.Protobuf
    - Grpc.Tools
2. 솔루션 탐색기에서 프로젝트를 언로드한 후, `종속성과 함께 프로젝트 다시 로드`를 실행한다.
3. 코드 파일의 속성 - 빌드 작업 - Protobuf 존재를 확인한다.

<br>

### .NET

1. 다음 Nuget package를 설치한다.
    - Google.Protobuf
    - Grpc.Tools
2. 코드 파일의 속성 - 빌드 작업 - Protobuf compiler 를 확인한다.

<br>

## Proto file 작성 및 컴파일

<br>

- 여기서는 간단한 `*.proto` 파일 작성 및 컴파일 방법을 알아본다.

<br>

### Proto file 작성

<br>

```protobuf
// ProtoTest.proto

syntax ="proto3";

package ProtoTest;

message Foo
{
	int32 TransactionID=1;
	string Message=2;
}
```

<br>

### Proto file compile

<br>

1. .NET framework
    - 솔루션 탐색기의 `*.proto` 파일의 속성 - 빌드 작업을 `Protobuf`로 설정
    - 빌드 작업 아래의 `사용자 지정 도구`에 `MSBuild:Compile` 입력
2. .NET
    - 솔루션 탐색기의 `*.proto` 파일의 속성 - 빌드 작업을 `Protobuf compiler`로 설정
    - `gRPC Stub Classes` 옵션은 환경에 맞게 선택

<br>

## 컴파일된 Proto 이용

<br>

```cs
// 기본 사용법

using ProtoTest;

internal class Program
{
    private static void Main(string[] args)
    {
        Foo foo = new Foo() { TransactionID = 1, Message = "Hello, World!" };

        Console.WriteLine(foo);
    }
}

/* output:
{ "TransactionID": 1, "Message": "Hello, World!" }
*/
```

```cs
// 파일로 출력

using Google.Protobuf;
using ProtoTest;

private static void Main(string[] args)
{
    Foo foo = new Foo() { TransactionID = 1, Message = "Hello, World!" };

    System.IO.StreamWriter writer = new System.IO.StreamWriter(@"C:\temp\ProtoTest.txt");
    foo.WriteTo(writer.BaseStream);
}

/* output:

Hello, World!
*/
```

```cs
// 파일에서 읽어오기

using ProtoTest;

private static void Main(string[] args)
{
    System.IO.StreamReader reader = new System.IO.StreamReader(@"C:\temp\ProtoTest.txt");

    Console.WriteLine(Foo.Parser.ParseFrom(reader.BaseStream));
}

/* output:
{ "TransactionID": 1, "Message": "Hello, World!" }
*/
```

<br>

## 참조 자료

<br>

- [Protocol Buffer Basics: C#](https://protobuf.dev/getting-started/csharptutorial/)