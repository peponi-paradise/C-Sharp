using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrackBarDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private double _doubleData = 0;

        public double DoubleData
        {
            get => _doubleData;
            set
            {
                _doubleData = value;
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
            TrackBar trackBar = new() { Maximum = 100 };
            Button button = new();
            button.Click += delegate { DoubleData += 5; };

            trackBar.DataBindings.Add(new Binding(nameof(trackBar.Value), this, nameof(DoubleData), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(trackBar, 0, 0);
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