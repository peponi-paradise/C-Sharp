## Introduction

<br>

- Control의 data binding을 위해서는 다음 방법 중 하나가 필요하다.
    - [INotifyPropertyChanged](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0) : XAML 바인딩을 위해 사용하는 것과 동일하다.
    - 바인딩 형식의 프로퍼티 변경 이벤트 구현
- 여기서는 `INotifyPropertyChanged` 인터페이스를 통한 바인딩 방법을 알아본다.

<br>

## Example

<br>

```cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TextBoxDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _textData = string.Empty;

        public string TextData
        {
            get => _textData;
            set
            {
                _textData = value;
                OnPropertyChanged();    // 프로퍼티가 변경되었음을 알림
            }
        }

        public Form1()
        {
            InitializeComponent();

            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            TextBox textbox1 = new();
            TextBox textbox2 = new();

            textbox1.DataBindings.Add(new Binding(nameof(textbox1.Text), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged));
            var binding = new Binding(nameof(textbox2.Text), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged);
            binding.Format += TextBoxFormat;
            binding.Parse += TextBoxParse;
            textbox2.DataBindings.Add(binding);

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(textbox1, 0, 0);
            tableLayoutPanel.Controls.Add(textbox2, 1, 0);
            tableLayoutPanel.Size = this.Size;
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);
        }

        private void TextBoxFormat(object? sender, ConvertEventArgs e)
        {
            // Format이 필요한 경우
            if (e.DesiredType == typeof(string))
            {
                e.Value = $"Format - {e.Value}";
            }
        }

        private void TextBoxParse(object? sender, ConvertEventArgs e)
        {
            // Format 설정한 컨트롤이 enabled 상태라면, 조작할 때 format 설정한 값을 빼줘야한다.
            if (e.DesiredType == typeof(string) && e.Value is not null)
            {
                if (((string)e.Value).Contains("Format - "))
                {
                    e.Value = ((string)e.Value).Replace("Format - ", "");
                }
                else e.Value = "";
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```

<br>

## 참조 자료

<br>

- [Control.DataBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.databindings?view=windowsdesktop-8.0)
- [INotifyPropertyChanged 인터페이스](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0)
- [Binding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.binding?view=windowsdesktop-8.0)