using System;
using System.Windows.Forms;

namespace INotifyPropertyChangedExample
{
    public partial class TestControl : UserControl
    {
        private TestClass _testClass { get; set; }

        public TestControl()
        {
            InitializeComponent();
        }

        public TestControl(TestClass testClass)
        {
            InitializeComponent();
            EnumSelect.Items.AddRange(Enum.GetNames(typeof(TestEnum)));

            _testClass = testClass;

            BindData();

            PropertyChange.Click += new EventHandler(PropertyChange_Click);
        }

        // TestClass 프로퍼티 접근만으로 UI까지 변경 확인용
        private void PropertyChange_Click(object sender, EventArgs e)
        {
            _testClass.TestBool = !_testClass.TestBool;
            _testClass.TestInt += 1;
            _testClass.TestDouble += 0.1;
            _testClass.TestString += "a";
            _testClass.TestEnum = (TestEnum)Enum.Parse(typeof(TestEnum), EnumSelect.SelectedItem.ToString());
        }

        // TestClass 바인딩 : 양방향 바인딩
        // 1. 위의 이벤트 동작처럼, 프로퍼티 값을 수정하면 UI에 자동 반영
        // 2. 바인딩된 UI의 값을 바꿀 시 자동으로 프로퍼티에 반영
        private void BindData()
        {
            // CheckBox
            TestBool.DataBindings.Add(new Binding(nameof(TestBool.Checked), _testClass, nameof(TestClass.TestBool)));

            // NumericUpDown
            TestInt.DataBindings.Add(new Binding(nameof(TestInt.Value), _testClass, nameof(TestClass.TestInt)));

            // NumericUpDown
            TestDouble.DataBindings.Add(new Binding(nameof(TestDouble.Value), _testClass, nameof(TestClass.TestDouble)));

            // TextBox
            TestString.DataBindings.Add(new Binding(nameof(TestString.Text), _testClass, nameof(TestClass.TestString)));

            // TextBox : Enum의 경우 TextBox에 업데이트는 가능하나 텍스트 값을 프로퍼티에 바인딩하는 것은 안된다.
            // 양방향 바인딩을 원할 경우 ComboBox를 고려하는 것이 좋다.
            TestEnum.DataBindings.Add(new Binding(nameof(TestEnum.Text), _testClass, nameof(TestClass.TestEnum)));
        }
    }
}