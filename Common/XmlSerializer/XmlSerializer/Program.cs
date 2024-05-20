namespace XmlSerializerTest;

public partial class Program
{
    static void Main(string[] args)
    {
        SerializeClass();
        SerializeStruct();
        SerializeList();

        DeserializeClass();
        DeserializeStruct();
        DeserializeList();

        SerializeRecord();
        DeserializeRecord();
    }
}