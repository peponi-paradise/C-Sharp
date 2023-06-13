using System.Threading.Tasks;

namespace Define.Services;

public interface IFileService<T>
{
    (bool IsSuccess, T? Data) LoadData(string path);

    Task<(bool IsSuccess, T? Data)> LoadDataAsync(string path);

    bool SaveData(string path, T contents);

    Task<bool> SaveDataAsync(string path, T contents);
}