## 1. Introduction

<br>

- .NET의 [XmlSerializer](https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.xmlserializer?view=net-8.0) 클래스는 객체 직렬화 및 역직렬화 기능을 제공한다.
- `XmlSerializer`를 통해 직렬화 하는 경우, 파일에 쓰거나 스트림으로 내보내는 등의 작업이 가능하다.
- 기본적으로 직렬화 및 역직렬화의 대상은 객체의 `public` 멤버 (필드 또는 속성) 이며, 각 멤버는 하나의 element로 표현된다.
- 여기서는 간단한 사용 방법을 알아보도록 한다.

<br>

## 2. Serialize

<br>

- 여기서는 `class`, `struct`, `List<T>` 형식의 객체를 직렬화하는 예시를 보여준다.

<br>

### 2.1. Class

<br>

```cs
public class TestClass
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public required object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}
```
```cs
using System.Xml.Serialization;

public static void SerializeClass()
{
    TestClass classData = new() { ID = 1, Name = "class", Value = 10 };
    XmlSerializer serializer = new(typeof(TestClass));

    serializer.Serialize(Console.Out, classData);
}
```
```xml
<?xml version="1.0" encoding="Codepage - 949"?>
<TestClass xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ID>1</ID>
  <Name>class</Name>
  <Value xsi:type="xsd:int">10</Value>
</TestClass>
```

<br>

### 2.2. Struct

<br>

```cs
public struct TestStruct
{
    public int ID { get; set; }
    public string Name { get; set; }
    public object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}
```
```cs
public static void SerializeStruct()
{
    TestStruct structData = new() { ID = 2, Name = "struct", Value = 11 };
    XmlSerializer serializer = new(typeof(TestStruct));

    serializer.Serialize(Console.Out, structData);
}
```
```xml
<?xml version="1.0" encoding="Codepage - 949"?>
<TestStruct xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ID>2</ID>
  <Name>struct</Name>
  <Value xsi:type="xsd:int">11</Value>
</TestStruct>
```

<br>

### 2.3. List\<T>

<br>

```cs
public static void SerializeList()
{
    List<TestClass> listData = new();
    for (int i = 0; i < 5; i++)
    {
        listData.Add(new() { ID = i, Name = $"Name {i}", Value = i });
    }
    XmlSerializer serializer = new(typeof(List<TestClass>));

    serializer.Serialize(Console.Out, listData);
}
```
```xml
<?xml version="1.0" encoding="Codepage - 949"?>
<ArrayOfTestClass xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <TestClass>
    <ID>0</ID>
    <Name>Name 0</Name>
    <Value xsi:type="xsd:int">0</Value>
  </TestClass>
  <TestClass>
    <ID>1</ID>
    <Name>Name 1</Name>
    <Value xsi:type="xsd:int">1</Value>
  </TestClass>
  <TestClass>
    <ID>2</ID>
    <Name>Name 2</Name>
    <Value xsi:type="xsd:int">2</Value>
  </TestClass>
  <TestClass>
    <ID>3</ID>
    <Name>Name 3</Name>
    <Value xsi:type="xsd:int">3</Value>
  </TestClass>
  <TestClass>
    <ID>4</ID>
    <Name>Name 4</Name>
    <Value xsi:type="xsd:int">4</Value>
  </TestClass>
</ArrayOfTestClass>
```

<br>

## 3. Deserialize

<br>

- 여기서는 `class`, `struct`, `List<T>` 형식의 객체를 역직렬화하는 예시를 보여준다.
- 편의상, [MemoryStream](https://learn.microsoft.com/ko-kr/dotnet/api/system.io.memorystream?view=net-8.0)에 쓰고 바로 deserialize를 수행한다.

<br>

### 3.1. Class

<br>

```cs
public class TestClass
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public required object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}
```
```cs
public static void DeserializeClass()
{
    TestClass classData = new() { ID = 1, Name = "class", Value = 10 };
    XmlSerializer serializer = new(typeof(TestClass));
    Stream stream = new MemoryStream();

    serializer.Serialize(stream, classData);

    stream.Position = 0;

    Console.WriteLine(serializer.Deserialize(stream));
}
```
```text
ID: 1, Name: class, Value: 10
```

<br>

### 3.2. Struct

<br>

```cs
public struct TestStruct
{
    public int ID { get; set; }
    public string Name { get; set; }
    public object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}
```
```cs
public static void DeserializeStruct()
{
    TestStruct structData = new() { ID = 2, Name = "struct", Value = 11 };
    XmlSerializer serializer = new(typeof(TestStruct));
    Stream stream = new MemoryStream();

    serializer.Serialize(stream, structData);

    stream.Position = 0;

    Console.WriteLine(serializer.Deserialize(stream));
}
```
```text
ID: 2, Name: struct, Value: 11
```

<br>

### 3.3. List\<T>

<br>

```cs
public static void DeserializeList()
{
    List<TestClass> listData = new();
    for (int i = 0; i < 5; i++)
    {
        listData.Add(new() { ID = i, Name = $"Name {i}", Value = i });
    }
    XmlSerializer serializer = new(typeof(List<TestClass>));
    Stream stream = new MemoryStream();

    serializer.Serialize(stream, listData);

    stream.Position = 0;

    Console.WriteLine(string.Join(Environment.NewLine, (List<TestClass>)serializer.Deserialize(stream)!));
}
```
```text
ID: 0, Name: Name 0, Value: 0
ID: 1, Name: Name 1, Value: 1
ID: 2, Name: Name 2, Value: 2
ID: 3, Name: Name 3, Value: 3
ID: 4, Name: Name 4, Value: 4
```

<br>

## 4. IXmlSerializable

<br>

- 위에 서술된 바와 같이, 기본적으로 직렬화 및 역직렬화의 대상은 객체의 `public` 멤버 (필드 또는 속성) 이다.
- 직렬화 및 역직렬화에 대한 커스터마이징을 하고 싶은 경우에는 [IXmlSerializable](https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.ixmlserializable?view=net-8.0) 인터페이스를 사용할 수 있다.
- 해당 인터페이스 구현 시 주의해야 할 사항이 있는데, [IXmlSerializable.GetSchema()](https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.ixmlserializable.getschema?view=net-8.0#system-xml-serialization-ixmlserializable-getschema) 메서드는 반드시 `null`을 반환해야 한다.
    > This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the XmlSchemaProviderAttribute to the class.

```cs
public record TestRecord : IXmlSerializable
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public object? Value { get; set; }

    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        // Deserialize
        ID = int.Parse(reader.GetAttribute(nameof(ID))!);
        reader.Read();
        Name = reader.ReadElementContentAsString(nameof(Name), "");
    }

    public void WriteXml(XmlWriter writer)
    {
        // Serialize
        writer.WriteAttributeString(nameof(ID), ID.ToString());
        writer.WriteElementString(nameof(Name), Name);
    }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}
```
```cs
public static void SerializeRecord()
{
    TestRecord record = new() { ID = 1, Name = "record", Value = "Hello" };
    XmlSerializer serializer = new(typeof(TestRecord));

    serializer.Serialize(Console.Out, record);
}
```
```xml
<?xml version="1.0" encoding="Codepage - 949"?>
<TestRecord ID="1">
  <Name>record</Name>
</TestRecord>
```
```cs
public static void DeserializeRecord()
{
    TestRecord record = new() { ID = 1, Name = "record", Value = "Hello" };
    XmlSerializer serializer = new(typeof(TestRecord));
    Stream stream = new MemoryStream();

    serializer.Serialize(stream, record);

    stream.Position = 0;

    Console.WriteLine(serializer.Deserialize(stream));
}
```
```text
ID: 1, Name: record, Value:
```

<br>

## 5. 참조 자료

<br>

- [XmlSerializer](https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.xmlserializer?view=net-8.0)
- [IXmlSerializable](https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.ixmlserializable?view=net-8.0)
- [MemoryStream](https://learn.microsoft.com/ko-kr/dotnet/api/system.io.memorystream?view=net-8.0)