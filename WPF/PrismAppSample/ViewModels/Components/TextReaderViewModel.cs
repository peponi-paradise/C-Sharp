using Define.Services;

namespace ViewModel.Components;

public class TextReaderViewModel
{
    private readonly IFileService<string> _FileService;

    public TextReaderViewModel(IFileService<string> fileService)
    {
    }
}