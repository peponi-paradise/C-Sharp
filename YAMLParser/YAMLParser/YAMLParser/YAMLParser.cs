using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace FileParser.YAML
{
    public interface IYAML
    {
        bool LoadData(string dataPath);

        void SaveData(string dataPath);
    }

    public static class YAMLParser
    {
        public static bool LoadData<T>(string dataPath, out T outData, List<Type> dataTags = null)
        {
            bool isSuccess = true;
            outData = default;

            string YAMLContents = File.ReadAllText(dataPath);

            var deserializer = dataTags == null ? new DeserializerBuilder().Build() : SerializerDesigner(new DeserializerBuilder(), dataTags).Build();

            outData = deserializer.Deserialize<T>(YAMLContents);

            return isSuccess;
        }

        public static void SaveData<T>(string dataPath, T saveData, List<Type> dataTags = null)
        {
            var serializer = dataTags == null ? new SerializerBuilder().Build() : SerializerDesigner(new SerializerBuilder(), dataTags).Build();
            var YAMLContents = serializer.Serialize(saveData);
            File.WriteAllText(dataPath, YAMLContents);
        }

        // Type명을 태그 이름으로 사용하게 지정
        private static dynamic SerializerDesigner(dynamic currentBuilder, List<Type> dataTags)
        {
            foreach (var dataTag in dataTags) currentBuilder.WithTagMapping($"!{dataTag.Name}", dataTag);
            return currentBuilder;
        }
    }
}