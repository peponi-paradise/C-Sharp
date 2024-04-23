using System.Diagnostics;

namespace TablessTabControl
{
    public partial class Form1 : Form
    {
        private CustomTabControl _tabControl = new();
        private TableLayoutPanel _panel = new();

        public Form1()
        {
            InitializeComponent();

            DrawPanel();
        }

        private void DrawPanel()
        {
            _panel.ColumnCount = 3;
            _panel.RowCount = 2;
            _panel.Dock = DockStyle.Fill;
            Controls.Add(_panel);

            Button previous = new() { Text = "Previous" };
            Button current = new() { Text = "Print Current" };
            Button next = new() { Text = "Next" };
            Button add = new() { Text = "Add" };
            Button remove = new() { Text = "Remove" };
            Button clear = new() { Text = "Clear" };

            previous.Click += (s, e) => _tabControl.PreviousTab();
            current.Click += (s, e) => PrintTab();
            next.Click += (s, e) => _tabControl.NextTab();
            add.Click += (s, e) => _tabControl.AddTab($"Page{_tabControl.TabCount + 1}");
            remove.Click += (s, e) => _tabControl.RemoveTab(_tabControl.SelectedTab!.Name);
            clear.Click += (s, e) => _tabControl.ClearTabs();

            _panel.Controls.Add(previous, 0, 0);
            _panel.Controls.Add(current, 1, 0);
            _panel.Controls.Add(next, 2, 0);
            _panel.Controls.Add(add, 0, 1);
            _panel.Controls.Add(remove, 1, 1);
            _panel.Controls.Add(clear, 2, 1);
        }

        private void PrintTab() => Trace.WriteLine($"Current tab : {_tabControl.SelectedTab!.Name}");
    }
}