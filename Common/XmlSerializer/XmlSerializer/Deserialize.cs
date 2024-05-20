using System.Xml.Serialization;

namespace XmlSerializerTest;

public partial class Program
{
    public static void DeserializeClass()
    {
        TestClass classData = new() { ID = 1, Name = "class", Value = 10 };
        XmlSerializer serializer = new(typeof(TestClass));
        Stream stream = new MemoryStream();

        serializer.Serialize(stream, classData);

        stream.Position = 0;

        Console.WriteLine(serializer.Deserialize(stream));
    }

    public static void DeserializeStruct()
    {
        TestStruct structData = new() { ID = 2, Name = "struct", Value = 11 };
        XmlSerializer serializer = new(typeof(TestStruct));
        Stream stream = new MemoryStream();

        serializer.Serialize(stream, structData);

        stream.Position = 0;

        Console.WriteLine(serializer.Deserialize(stream));
    }

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
}