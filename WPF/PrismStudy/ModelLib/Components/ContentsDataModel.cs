using Define.EventAggregator;
using Define.Model;
using Define.Services;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace ModelLib.Components;

public class ContentsDataModel : BindableBase, IModel
{
    private IFileService<ContentsDataModel>? FileService;
    private string? _TextData = "";

    public string? TextData
    {
        get => _TextData;
        set => SetProperty(ref _TextData, value);
    }

    private string? _LoadText = "";

    public string? LoadText
    {
        get => _LoadText;
        set => SetProperty(ref _LoadText, value);
    }

    private bool _IsIdle = true;

    public bool IsIdle
    {
        get => _IsIdle;
        set => SetProperty(ref _IsIdle, value);
    }

    private IEventAggregator? _Aggregator;

    public ContentsDataModel()
    {
    }

    public ContentsDataModel(IFileService<ContentsDataModel> fileService, IEventAggregator ea)
    {
        FileService = fileService;
        _Aggregator = ea;
        _Aggregator.GetEvent<TextLoadDoneEvent>().Subscribe(LoadDone);
        LoadData();
    }

    public async void LoadData()
    {
        if (FileService != null)
        {
            var rtn = await FileService.LoadDataAsync($@"{AppDomain.CurrentDomain.BaseDirectory}\{nameof(ContentsDataModel)}.yaml");
            if (rtn.IsSuccess && rtn.Data != null) TextData = rtn.Data.TextData;
        }
    }

    public void Load()
    {
        IsIdle = false;
        _Aggregator?.GetEvent<TextLoadCallEvent>().Publish(@$"{AppDomain.CurrentDomain.BaseDirectory}\TextTest.txt");
    }

    private void LoadDone(string contents)
    {
        LoadText = contents;
        IsIdle = true;
    }
}