using Define.Model;
using Define.Services;
using Prism.Mvvm;
using System;
using System.Diagnostics;

namespace ModelLib.Windows;

public class MainWindowModel : BindableBase, IModel
{
    private IFileService<MainWindowModel>? _fileService;
    private string _TextData = "";

    public string TextData
    {
        get => _TextData;
        set => SetProperty(ref _TextData, value);
    }

    public MainWindowModel()
    {
    }

    public MainWindowModel(IFileService<MainWindowModel> fileService)
    {
        _fileService = fileService;
        LoadData();
    }

    public async void LoadData()
    {
        if (_fileService != null)
        {
            var rtn = await _fileService.LoadDataAsync($@"{AppDomain.CurrentDomain.BaseDirectory}\{nameof(MainWindowModel)}.yaml");
            if (rtn.IsSuccess && rtn.Data != null) this.TextData = rtn.Data.TextData;
        }
    }
}