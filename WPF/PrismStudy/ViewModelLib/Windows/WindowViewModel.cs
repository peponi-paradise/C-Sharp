using Define.ViewModel;
using Prism.Mvvm;

namespace ViewModelLib.Windows;

public class WindowViewModel : BindableBase, IViewModel
{
    private string _TextData = "Hello World!";

    public string TextData
    {
        get => _TextData;
        set
        {
            SetProperty(ref _TextData, value);
        }
    }

    private string _NameData = "Test Name";

    public string NameData
    {
        get => _NameData;
        set
        {
            SetProperty(ref _NameData, value);
        }
    }
}