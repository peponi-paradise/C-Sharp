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