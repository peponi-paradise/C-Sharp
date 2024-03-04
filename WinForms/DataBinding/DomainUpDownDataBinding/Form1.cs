using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DomainUpDownDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _stringData = string.Empty;

        public string StringData
        {
            get => _stringData;
            set
            {
                _stringData = value;
                OnPropertyChanged();
            }
        }

        public Form1()
        {
            InitializeComponent();

            ConfigureComponents();
        }

        void ConfigureComponents()
        {
            List<string> contents = new()
            {
                "A",
                "B",
                "C",
                "D",
            };

            DomainUpDown domainUpDown = new();
            Button button = new();
            button.Click += delegate { StringData = "C"; };

            domainUpDown.Items.AddRange(contents);
            domainUpDown.DataBindings.Add(new Binding(nameof(domainUpDown.SelectedItem), this, nameof(StringData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(domainUpDown, 0, 0);
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