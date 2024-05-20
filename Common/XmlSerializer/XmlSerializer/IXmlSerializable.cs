using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XmlSerializerTest;

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

public partial class Program
{
    public static void SerializeRecord()
    {
        TestRecord record = new() { ID = 1, Name = "record", Value = "Hello" };
        XmlSerializer serializer = new(typeof(TestRecord));

        serializer.Serialize(Console.Out, record);
    }

    public static void DeserializeRecord()
    {
        TestRecord record = new() { ID = 1, Name = "record", Value = "Hello" };
        XmlSerializer serializer = new(typeof(TestRecord));
        Stream stream = new MemoryStream();

        serializer.Serialize(stream, record);

        stream.Position = 0;

        Console.WriteLine(serializer.Deserialize(stream));
    }
}