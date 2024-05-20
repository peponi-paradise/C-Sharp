namespace XmlSerializerTest;

public class TestClass
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public required object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}

public struct TestStruct
{
    public int ID { get; set; }
    public string Name { get; set; }
    public object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}