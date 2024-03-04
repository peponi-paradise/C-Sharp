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

namespace LabelDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _intData = 0;

        public int IntData
        {
            get => _intData;
            set
            {
                _intData = value;
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
            Label label = new() { TextAlign = ContentAlignment.MiddleCenter };
            Button button = new();
            button.Click += delegate { IntData++; };        // 클릭할 때마다 +1

            label.DataBindings.Add(new Binding(nameof(label.Text), this, nameof(IntData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(label, 0, 0);
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

<br>

## 참조 자료

<br>

- [Control.DataBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.databindings?view=windowsdesktop-8.0)
- [INotifyPropertyChanged 인터페이스](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0)
- [Binding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.binding?view=windowsdesktop-8.0)