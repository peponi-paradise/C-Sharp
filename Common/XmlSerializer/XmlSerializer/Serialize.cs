using System.Xml.Serialization;

namespace XmlSerializerTest;

public partial class Program
{
    public static void SerializeClass()
    {
        TestClass classData = new() { ID = 1, Name = "class", Value = 10 };
        XmlSerializer serializer = new(typeof(TestClass));

        serializer.Serialize(Console.Out, classData);
    }

    public static void SerializeStruct()
    {
        TestStruct structData = new() { ID = 2, Name = "struct", Value = 11 };
        XmlSerializer serializer = new(typeof(TestStruct));

        serializer.Serialize(Console.Out, structData);
    }

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
}