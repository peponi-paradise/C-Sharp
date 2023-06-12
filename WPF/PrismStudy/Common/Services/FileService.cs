using Define.Services;
using System;

namespace Common.Services;

public class FileService : IFileService
{
    public bool LoadData(string path, out object? data)
    {
        data = default;
        data = data switch
        {
            string val => StringConvert(path),
            _ => throw new Exception($"Type of {data.GetType()} is not supported")
        };
        return false;
    }

    public bool SaveData(object contents, string path)
    {
        throw new System.NotImplementedException();
    }

    private string StringConvert(string path) => System.IO.File.ReadAllText(path);
}