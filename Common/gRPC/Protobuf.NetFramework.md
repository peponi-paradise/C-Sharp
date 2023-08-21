## Introduction

<br>

## Installation

<br>

- .NET framework에서 `Protobuf`를 사용하는 방법은 두 가지가 있다.
    - [Protocol Buffer compiler](https://protobuf.dev/downloads/) 설치 후 컴파일러 따로 사용
    - `Grpc.Tools` Nuget 패키지 설치 후 Visual studio에서 컴파일
- 아래는 `Grpc.Tools`를 이용한 예시이다.
- 다음 Nuget package를 설치한다.
    - Google.Protobuf
    - Grpc.Tools
- 솔루션 탐색기에서 프로젝트를 언로드한 후, `종속성과 함께 프로젝트 다시 로드`를 실행한다.
- 코드 파일의 속성 - 빌드 작업 - Protobuf 존재를 확인한다.

<br>

## Proto file 작성 및 컴파일

<br>

- 여기서는 간단한 `*.proto` 파일 작성 및 컴파일 방법을 알아본다.

<br>

### Proto file 작성

<br>

```proto
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

1. 솔루션 탐색기의 `*.proto` 파일의 속성 - 빌드 작업을 `Protobuf`로 설정
2. 빌드 작업 아래의 `사용자 지정 도구`에 `MSBuild:Compile` 입력

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