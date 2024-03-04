using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DateTimePickerDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DateTime _dateData = DateTime.Today;

        public DateTime DateData
        {
            get => _dateData;
            set
            {
                _dateData = value;
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
            DateTimePicker dateTimePicker = new();
            Button button = new() { Width = 100 };
            button.Click += delegate { DateData = DateTime.Today; };

            dateTimePicker.DataBindings.Add(new Binding(nameof(dateTimePicker.Value), this, nameof(DateData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(dateTimePicker, 0, 0);
            tableLayoutPanel.Controls.Add(button, 1, 0);
            tableLayoutPanel.Size = this.Size;
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}