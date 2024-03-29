using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CheckBoxDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _boolData = false;

        public bool BoolData
        {
            get => _boolData;
            set
            {
                _boolData = value;
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
            CheckBox checkBox1 = new();
            Button button = new();
            button.Click += delegate { BoolData = !BoolData; };     // 클릭할 때마다 뒤집기

            checkBox1.DataBindings.Add(new Binding(nameof(checkBox1.Checked), this, nameof(BoolData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(checkBox1, 0, 0);
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