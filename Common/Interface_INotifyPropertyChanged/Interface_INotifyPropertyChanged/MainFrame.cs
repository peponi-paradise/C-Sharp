using System.Threading;
using System.Windows.Forms;

namespace Interface_INotifyPropertyChanged
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            SetUI();
        }

        private void SetUI()
        {
            TestClass testClass = new TestClass(SynchronizationContext.Current);
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 3;

            // 같은 데이터를 원본으로 하는 컴포넌트 세개 추가. 어느 한 곳에서 프로퍼티 변경 시 나머지 컴포넌트 또한 값이 자동 변경
            tableLayoutPanel.Controls.Add(testClass.GetComponent(), 0, 0);
            tableLayoutPanel.Controls.Add(testClass.GetComponent(), 1, 0);
            tableLayoutPanel.Controls.Add(testClass.GetComponent(), 2, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);
        }
    }
}