## Introduction

<br>

- Control의 data binding을 위해서는 다음 방법 중 하나가 필요하다.
    - [INotifyPropertyChanged](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0) : XAML 바인딩을 위해 사용하는 것과 동일하다.
    - 바인딩 형식의 프로퍼티 변경 이벤트 구현
- 여기서는 `INotifyPropertyChanged` 인터페이스를 통한 바인딩 방법을 알아본다.
- 아래는 `SelectionRange`, `SelectionStart, SelectionEnd` 프로퍼티를 사용하는 두 가지 예시를 보여준다.
    - 첫번째 예시가 조금 더 나은 것 같지만, `Start`, `End`를 따로 바인딩 하는 경우에는 그리 매끄럽지 않게 된다.
        (`SelectionRange` 클래스가 `sealed`이고 `INotifyPropertyChanged`가 구현되어 있지 않다)

<br>

## Example - SelectionRange

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

        private SelectionRange _range = new() { Start = DateTime.Today, End = DateTime.Today.AddDays(4) };

        public SelectionRange TimeRange
        {
            get => _range;
            set
            {
                _range = value;
                OnPropertyChanged();        // 프로퍼티가 변경되었음을 알림
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
            TextBox textBox = new() { Width = 200 };
            Button button = new();
            button.Click += delegate { TimeRange = new(DateTime.Today, DateTime.Today.AddDays(4)); };   // 날짜 리셋

            monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionRange), this, nameof(TimeRange), false, DataSourceUpdateMode.OnPropertyChanged));

            // 캘린더 조작 후 포커스 이동해야 TimeRange가 업데이트됨
            textBox.DataBindings.Add(new Binding(nameof(textBox.Text), this, nameof(TimeRange), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(monthCalendar, 0, 0);
            tableLayoutPanel.Controls.Add(textBox, 1, 0);
            tableLayoutPanel.Controls.Add(button, 1, 1);
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

## Example - SelectionStart, SelectionEnd

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

        private DateTime _endDate = DateTime.Today.AddDays(4);

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
            TextBox textBox = new() { Width = 200 };
            TextBox textBox2 = new() { Width = 200 };
            Button button = new();
            button.Click += delegate { StartDate = DateTime.Today; EndDate = DateTime.Today.AddDays(4); };   // 날짜 리셋

            // 캘린더 조작 시 먼저 설정한 필드만 값이 정상 수정됨 (나중에 설정한 필드의 경우, 수정 전 값이 setter로 들어옴)
            // 이벤트를 추가로 설정하는게 양방향 바인딩으로는 최선 (이벤트만 설정하는 경우 양방향 바인딩 불가)
            monthCalendar.DateSelected += MonthCalendar_DateSelected;
            monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionStart), this, nameof(StartDate), false, DataSourceUpdateMode.OnPropertyChanged));
            monthCalendar.DataBindings.Add(new Binding(nameof(monthCalendar.SelectionEnd), this, nameof(EndDate), false, DataSourceUpdateMode.OnPropertyChanged));

            // 이벤트 설정 안하면 캘린더 조작 후 포커스 이동해야 업데이트됨
            textBox.DataBindings.Add(new Binding(nameof(textBox.Text), this, nameof(StartDate), false, DataSourceUpdateMode.OnPropertyChanged));
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), this, nameof(EndDate), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(monthCalendar, 0, 0);
            tableLayoutPanel.Controls.Add(textBox, 1, 0);
            tableLayoutPanel.Controls.Add(textBox2, 1, 1);
            tableLayoutPanel.Controls.Add(button, 0, 1);
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);
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

- 위의 주석 내용과 같이, 이벤트 설정 없이 바인딩하고 조작하는 경우 setter 동작이 비정상적으로 수행된다.
    - 위의 예제에서는 `EndDate` 값이 정상적으로 설정되지 않는다.
    - 코드를 통해 수정된 값 (버튼 클릭 이벤트) 에 대해서는 정상적으로 getter가 동작한다.
- 정상적으로 바인딩 할 수 있는 방법이 있을 것 같은데, 아직까지 뚜렷한 해결책을 찾지는 못했다.
- 따라서 위와 같은 경우 캘린더 조작에 대한 바인딩은 이벤트로 구현하는 것이 나아보인다.

<br>

## 참조 자료

<br>

- [Control.DataBindings Property](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.databindings?view=windowsdesktop-8.0)
- [INotifyPropertyChanged 인터페이스](https://learn.microsoft.com/ko-kr/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-8.0)
- [Binding Class](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.binding?view=windowsdesktop-8.0)
- [MonthCalendar.SelectionRange Property](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.monthcalendar.selectionrange?view=windowsdesktop-8.0#system-windows-forms-monthcalendar-selectionrange)
- [MonthCalendar.SelectionStart 속성](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.monthcalendar.selectionstart?view=windowsdesktop-8.0)
- [MonthCalendar.SelectionEnd 속성](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.monthcalendar.selectionend?view=windowsdesktop-8.0)