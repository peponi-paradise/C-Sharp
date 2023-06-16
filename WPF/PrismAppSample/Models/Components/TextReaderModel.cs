using Define.Services;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace Model.Components;

public class TextReaderModel : BindableBase
{
    private readonly IFileService<string> _FileService;

    public TextReaderModel(IFileService<string> fileService) => _FileService = fileService;

    private string? _LoadedText = "";

    public string? LoadedText
    {
        get => _LoadedText;
        set => SetProperty(ref _LoadedText, value);
    }

    public async Task Load(string filePath)
    {
        var data = await _FileService.LoadDataAsync(filePath);
        if (data.IsSuccess) LoadedText = "Text Edited by Model - " + data.Data;
    }
}