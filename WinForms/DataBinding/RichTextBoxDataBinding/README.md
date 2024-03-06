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

namespace RichTextBoxDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
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

        private const string _sampleRTF = "{\\rtf1\\ansi\\ansicpg949\\deff0\\nouicompat\\deflang1033\\deflangfe1042{\\fonttbl{\\f0\\fnil\\fcharset0 Arial;}}\r\n{\\colortbl ;\\red0\\green77\\blue187;\\red0\\green0\\blue255;}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\sa200\\sl276\\slmult1\\fs20\\lang18 Hello, World!\\par\r\n\\cf1 Hello, World!\\cf0\\par\r\n\\i Hello, World!\\par\r\n\\b\\i0 Hello, World!\\par\r\n{\\i{\\field{\\*\\fldinst{HYPERLINK https://google.com }}{\\fldrslt{https://google.com\\ul0\\cf0}}}}\\f0\\fs20\\par\r\n}";

        public Form1()
        {
            InitializeComponent();

            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            RichTextBox textBox = new() { Dock = DockStyle.Fill, Height = 300 };
            TextBox checkInput = new() { Multiline = true, WordWrap = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical };       // 텍스트 입력 확인용
            Button button = new() { Text = "Sample" };
            button.Click += delegate { TextData = _sampleRTF; };        // 샘플 RTF 입력

            // RichTextBox 입력 후 포커스 이동해야 TextData가 업데이트됨
            // textBox.DataBindings.Add(new Binding(nameof(textBox.Text), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged));  // 일반 텍스트
            textBox.DataBindings.Add(new Binding(nameof(textBox.Rtf), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged));      // RTF 텍스트
            checkInput.DataBindings.Add(new Binding(nameof(checkInput.Text), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Percent, 80));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Percent, 20));
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(textBox, 0, 0);
            tableLayoutPanel.Controls.Add(checkInput, 0, 1);
            tableLayoutPanel.Controls.Add(button, 1, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```

- 여기서는 바인딩 방법만 소개한다.
- `RichTextBox`에 대한 자세한 내용은 [RichTextBox 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.richtextbox?view=windowsdesktop-8.0)를 참조한다.
    - `RTF`에 대한 자세한 내용은 [Rich Text Format (RTF) Specification, version 1.6](https://learn.microsoft.com/en-us/previous-versions/office/developer/office2000/aa140277(v=office.10))을 참조한다.

<br>

## 참조 자료

<br>

- [Control.DataBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.databindings?view=windowsdesktop-8.0)
- [INotifyPropertyChanged 인터페이스](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0)
- [Binding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.binding?view=windowsdesktop-8.0)
- [RichTextBox 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.richtextbox?view=windowsdesktop-8.0)
- [Rich Text Format (RTF) Specification, version 1.6](https://learn.microsoft.com/en-us/previous-versions/office/developer/office2000/aa140277(v=office.10))