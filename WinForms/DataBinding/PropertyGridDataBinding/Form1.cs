using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PropertyGridDataBinding
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

            ListBox listBox = new() { Width = 200, DataSource = source };
            PropertyGrid grid = new() { Width = 200, Height = 300 };
            Button button = new();
            button.Click += Update;     // ID, Name 변경

            grid.DataBindings.Add(new Binding(nameof(grid.SelectedObject), source, null));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.Controls.Add(listBox, 0, 0);
            tableLayoutPanel.Controls.Add(grid, 1, 0);
            tableLayoutPanel.Controls.Add(button, 2, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);

            void Update(object? sender, EventArgs e)
            {
                if (grid.SelectedObject is not null && grid.SelectedObject is Account item)
                {
                    item.ID++;
                    item.Name += "a";
                }
            }
        }
    }
}