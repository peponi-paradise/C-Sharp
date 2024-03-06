using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataGridViewDataBinding
{
    public class Account(int id, string name) : INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
        public event PropertyChangedEventHandler? PropertyChanged;

        private int _id = id;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();        // 프로퍼티가 변경되었음을 알림
            }
        }

        private string _name = name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();        // 프로퍼티가 변경되었음을 알림
            }
        }

        public override string ToString() => $"{ID}, {Name}";

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            BindingList<Account> accounts = new()
            {
                new(0,"A"),
                new(1,"B"),
                new(2,"C")
            };
            BindingSource source = new(accounts, null);

            DataGridView gridView = new() { DataSource = source };
            Button button = new();
            button.Click += Update;     // ID, Name 변경

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(gridView, 0, 0);
            tableLayoutPanel.Controls.Add(button, 1, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);

            void Update(object? sender, EventArgs e)
            {
                if (gridView.CurrentRow is not null && gridView.CurrentRow.DataBoundItem is Account item)
                {
                    item.ID++;
                    item.Name += "a";
                }
            }
        }
    }
}