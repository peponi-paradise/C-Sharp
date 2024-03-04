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

namespace MonthCalendarDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
        public event PropertyChangedEventHandler? PropertyChanged;

        private DateTime _startDate = DateTime.Today;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();    // 프로퍼티가 변경되었음을 알림
            }
        }

        private DateTime _endDate = DateTime.Today.AddDays(1);

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
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
            MonthCalendar monthCalendar = new();
            DateTimePicker startPicker = new();
            DateTimePicker endPicker = new();
            Button button = new();
            button.Click += Update;     // 날짜 초기화

            // 먼저 설정한 필드만 값이 수정되어 정상적으로 바인딩 불가

            // monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionStart), this, nameof(StartDate), false, DataSourceUpdateMode.OnPropertyChanged));
            // monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionEnd), this, nameof(EndDate), false, DataSourceUpdateMode.OnPropertyChanged));

            // 이벤트를 추가로 설정하는게 양방향 바인딩으로는 최선 (이벤트만 설정하는 경우 양방향 바인딩 불가)
            monthCalendar.DateSelected += MonthCalendar_DateSelected;
            monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionStart), this, nameof(StartDate), false, DataSourceUpdateMode.OnPropertyChanged));
            monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionEnd), this, nameof(EndDate), false, DataSourceUpdateMode.OnPropertyChanged));

            // 값 확인용 (이벤트 설정 안하면, 캘린더 설정 후 포커스 이동해야 StartDate 반영됨. EndDate는 반영안됨)
            startPicker.DataBindings.Add(new Binding(nameof(startPicker.Value), this, nameof(StartDate), false, DataSourceUpdateMode.OnPropertyChanged));
            endPicker.DataBindings.Add(new Binding(nameof(endPicker.Value), this, nameof(EndDate), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(monthCalendar, 0, 0);
            tableLayoutPanel.Controls.Add(button, 0, 1);
            tableLayoutPanel.Controls.Add(startPicker, 1, 0);
            tableLayoutPanel.Controls.Add(endPicker, 1, 1);
            tableLayoutPanel.Size = this.Size;
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);

            void Update(object? sender, EventArgs e)
            {
                StartDate = DateTime.Today;
                EndDate = DateTime.Today.AddDays(3);
            }
        }

        private void MonthCalendar_DateSelected(object? sender, DateRangeEventArgs e)
        {
            StartDate = e.Start;
            EndDate = e.End;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```

- 위의 주석 내용과 같이, 이벤트 설정 없이 바인딩 하는 경우

<br>

## 참조 자료

<br>

- [Control.DataBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.databindings?view=windowsdesktop-8.0)
- [INotifyPropertyChanged 인터페이스](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0)
- [Binding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.binding?view=windowsdesktop-8.0)