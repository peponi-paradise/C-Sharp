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

namespace MaskedTextBoxDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _mask = "000-0000-0000";

        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                OnPropertyChanged();    // 프로퍼티가 변경되었음을 알림
            }
        }

        private string _inputText = string.Empty;

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
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
            MaskedTextBox textBox = new();
            Label checkInput = new();       // 텍스트 입력 확인용
            Button button = new();
            button.Click += delegate { InputText = "010-1234-5678"; };      // 문자열 초기화

            textBox.DataBindings.Add(new Binding(nameof(textBox.Mask), this, nameof(Mask), false, DataSourceUpdateMode.OnPropertyChanged));
            textBox.DataBindings.Add(new Binding(nameof(textBox.Text), this, nameof(InputText), false, DataSourceUpdateMode.OnPropertyChanged));
            checkInput.DataBindings.Add(new Binding(nameof(checkInput.Text), this, nameof(InputText), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
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
- `MaskedTextBox`에 대한 자세한 내용은 [MaskedTextBox 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.maskedtextbox?view=windowsdesktop-8.0)를 참조한다.
    - `MaskedTextBox.Mask`에 대한 내용은 [MaskedTextBox.Mask 속성](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.maskedtextbox.mask?view=windowsdesktop-8.0)을 참조한다.

<br>

## 참조 자료

<br>

- [Control.DataBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.databindings?view=windowsdesktop-8.0)
- [INotifyPropertyChanged 인터페이스](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0)
- [Binding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.binding?view=windowsdesktop-8.0)
- [MaskedTextBox 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.maskedtextbox?view=windowsdesktop-8.0)
- [MaskedTextBox.Mask 속성](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.maskedtextbox.mask?view=windowsdesktop-8.0)