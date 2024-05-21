using System.Xml.Serialization;

namespace XmlSerializerTest;

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