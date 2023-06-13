using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

/// <summary>
/// YamlDotNet (Nuget) : Name must be equal. (DON'T USE YamlDotNet.NetCore only)
/// </summary>
namespace Common.Utility.Parser;

public static class YAMLParser
{
    private static object Locker = new object();

    public static bool LoadData<T>(string dataPath, out T? outData, List<Type>? dataTags = null)
    {
        bool isSuccess = true;
        outData = default;

        lock (Locker)
        {
            string YAMLContents = File.ReadAllText(dataPath);

            var deserializer = dataTags == null ? new DeserializerBuilder().Build() : SerializerDesigner(new DeserializerBuilder(), dataTags).Build();

            outData = (T)deserializer.Deserialize<T>(YAMLContents);
        }
        return isSuccess;
    }

    public static void SaveData<T>(string dataPath, T saveData, List<Type>? dataTags = null)
    {
        lock (Locker)
        {
            var serializer = dataTags == null ? new SerializerBuilder().Build() : SerializerDesigner(new SerializerBuilder(), dataTags).Build();
            var YAMLContents = serializer.Serialize(saveData);
            File.WriteAllText(dataPath, YAMLContents);
        }
    }

    private static dynamic SerializerDesigner(dynamic currentBuilder, List<Type> dataTags)
    {
        foreach (var dataTag in dataTags) currentBuilder.WithTagMapping($"!{dataTag.Name}", dataTag);
        return currentBuilder;
    }
}