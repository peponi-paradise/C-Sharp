using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataBinding
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
                OnPropertyChanged(nameof(TextData));
            }
        }

        public Form1()
        {
            InitializeComponent();

            ConfigureComponents();
        }

        void ConfigureComponents()
        {
            TextBox textbox1 = new();
            TextBox textbox2 = new();
            textbox2.Enabled = false;

            textbox1.DataBindings.Add(new Binding(nameof(textbox1.Text), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged));
            textbox2.DataBindings.Add(new Binding(nameof(textbox2.Text), this, nameof(TextData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(textbox1, 0, 0);
            tableLayoutPanel.Controls.Add(textbox2, 1, 0);
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