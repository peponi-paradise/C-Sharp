using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListBoxDataBinding
{
    public enum EnumItems
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4
    }

    public class Account(int id, string name) : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id = id;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name = name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public override string ToString() => $"{ID}, {Name}";

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class Form1 : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private EnumItems _enumItem = default;

        public EnumItems EnumItem
        {
            get => _enumItem;
            set
            {
                _enumItem = value;
                OnPropertyChanged();
            }
        }

        public Form1()
        {
            InitializeComponent();

            ConfigureEnum();

            ConfigureBindingSource();
        }

        void ConfigureEnum()
        {
            ListBox listBox = new();
            Button button = new();
            button.Click += delegate { EnumItem = EnumItems.C; };

            listBox.DataSource = Enum.GetValues<EnumItems>();
            listBox.DataBindings.Add(new Binding(nameof(listBox.SelectedItem), this, nameof(EnumItem), false, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.Controls.Add(listBox, 0, 0);
            tableLayoutPanel.SetRowSpan(listBox, 2);
            tableLayoutPanel.Controls.Add(button, 1, 0);
            tableLayoutPanel.Size = this.Size / 3;
            tableLayoutPanel.Dock = DockStyle.Left;
            this.Controls.Add(tableLayoutPanel);
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
            Button button = new();
            button.Click += Update;

            // SelectedItem 변경 시 바로 textbox에 반영, textbox 수정 내용이 바로 listbox에 반영
            listBox.DataSource = source;

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(listBox, 0, 0);
            tableLayoutPanel.Controls.Add(button, 1, 0);
            tableLayoutPanel.Size = this.Size / 3;
            tableLayoutPanel.Dock = DockStyle.Right;
            this.Controls.Add(tableLayoutPanel);

            void Update(object? sender, EventArgs e)
            {
                if (listBox.SelectedItem is not null)
                {
                    ((Account)listBox.SelectedItem).ID++;
                    ((Account)listBox.SelectedItem).Name += "a";
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}