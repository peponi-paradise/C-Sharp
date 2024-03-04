using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CheckedListBoxDataBinding
{
    public enum EnumItems
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4
    }

    public class Account(int id, string name)
    {
        public int ID { get; init; } = id;
        public string Name { get; init; } = name;

        public override string ToString() => $"{ID}, {Name}";
    }

    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private CheckedListBox.CheckedItemCollection _enumItems = null!;

        public CheckedListBox.CheckedItemCollection EnumItems
        {
            get => _enumItems;
            set
            {
                _enumItems = value;
                OnPropertyChanged();
            }
        }

        public Form1()
        {
            InitializeComponent();

            ConfigureEnum();

            //ConfigureBindingSource();
        }

        void ConfigureEnum()
        {
            CheckedListBox listBox = new();
            TextBox textBox = new();

            // Focus가 변경될 때 textbox에 정상적으로 값 반영이 됨
            listBox.DataSource = Enum.GetValues<EnumItems>();
            listBox.DataBindings.Add(new Binding(nameof(listBox.), this, nameof(EnumItems), false, DataSourceUpdateMode.OnPropertyChanged));
            var binding = new Binding(nameof(textBox.Text), this, nameof(EnumItems), false, DataSourceUpdateMode.OnPropertyChanged);
            binding.Format += FormatTextBox;
            textBox.DataBindings.Add(binding);

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(listBox, 0, 0);
            tableLayoutPanel.Controls.Add(textBox, 1, 0);
            tableLayoutPanel.Size = this.Size / 3;
            tableLayoutPanel.Dock = DockStyle.Left;
            this.Controls.Add(tableLayoutPanel);
        }

        private void FormatTextBox(object? sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(EnumItems))
            {
                e.Value = string.Join(", ", EnumItems);
            }
        }

        void ConfigureBindingSource()
        {
            ObservableCollection<Account> accounts = new()
            {
                new(0,"A"),
                new(1,"B"),
                new(2,"C")
            };
            BindingSource source = new(accounts, null);

            ListBox listBox = new();
            TextBox textBox = new();
            TextBox textBox2 = new();

            // SelectedItem 변경 시 바로 textbox에 반영, textbox 수정 내용이 바로 listbox에 반영
            listBox.DataSource = source;
            textBox.DataBindings.Add(new Binding(nameof(textBox.Text), source, nameof(Account.ID), false, DataSourceUpdateMode.OnPropertyChanged));
            textBox2.DataBindings.Add(new Binding(nameof(textBox2.Text), source, nameof(Account.Name), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(listBox, 0, 0);
            tableLayoutPanel.SetRowSpan(listBox, 2);
            tableLayoutPanel.Controls.Add(textBox, 1, 0);
            tableLayoutPanel.Controls.Add(textBox2, 1, 1);
            tableLayoutPanel.Size = this.Size / 3;
            tableLayoutPanel.Dock = DockStyle.Right;
            this.Controls.Add(tableLayoutPanel);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}