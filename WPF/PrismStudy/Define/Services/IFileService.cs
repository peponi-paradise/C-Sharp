namespace Define.Services;

public interface IFileService
{
    public bool LoadData(string path, out object? data);

    public bool SaveData(object contents, string path);
}