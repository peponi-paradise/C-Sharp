using Common.Utility.Parser;
using Define.EventAggregator;
using Define.Services;
using Prism.Events;
using System.Threading.Tasks;

namespace Common.Services;

public class TextFileService : IFileService<string>
{
    private IEventAggregator _Aggregator;

    public TextFileService(IEventAggregator ea)
    {
        _Aggregator = ea;
        _Aggregator.GetEvent<TextLoadCallEvent>().Subscribe(async (path) => { await LoadFromCommand(path); });
    }

    public (bool IsSuccess, string? Data) LoadData(string path)
    {
        try
        {
            var data = System.IO.File.ReadAllText(path);
            if (!string.IsNullOrWhiteSpace(data)) return (true, data);
            else return (false, null);
        }
        catch
        {
            return (false, null);
        }
    }

    public Task<(bool IsSuccess, string? Data)> LoadDataAsync(string path) => Task.Run(() => LoadData(path));

    public bool SaveData(string path, string contents)
    {
        try
        {
            System.IO.File.WriteAllText(path, contents);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> SaveDataAsync(string path, string contents) => Task.Run(() => SaveData(path, contents));

    private Task LoadFromCommand(string path)
    {
        return Task.Run(() =>
        {
            var rtn = LoadData(path);
            if (rtn.IsSuccess)
            {
                if (rtn.Data != default) _Aggregator.GetEvent<TextLoadDoneEvent>().Publish(rtn.Data);
            }
        });
    }
}

public class YAMLFileService<T> : IFileService<T>
{
    public (bool IsSuccess, T? Data) LoadData(string path)
    {
        try
        {
            var success = YAMLParser.LoadData(path, out T? data);
            return (success, data);
        }
        catch
        {
            return (false, default);
        }
    }

    public Task<(bool IsSuccess, T? Data)> LoadDataAsync(string path) => Task.Run(() => LoadData(path));

    public bool SaveData(string path, T contents)
    {
        try
        {
            YAMLParser.SaveData(path, contents);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> SaveDataAsync(string path, T contents) => Task.Run(() => SaveData(path, contents));
}