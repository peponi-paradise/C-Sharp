using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

namespace XmlSerializerTest;

public class TestClass
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public required object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}

public record TestRecord : IXmlSerializable
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public object? Value { get; set; }

    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        ID = int.Parse(reader.GetAttribute(nameof(ID))!);
        reader.Read();
        Name = reader.ReadElementContentAsString(nameof(Name), "");
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteAttributeString(nameof(ID), ID.ToString());
        writer.WriteElementString(nameof(Name), Name);
    }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}

public struct TestStruct
{
    public int ID { get; set; }
    public string Name { get; set; }
    public object Value { get; set; }

    public override string ToString() => $"ID: {ID}, Name: {Name}, Value: {Value}";
}