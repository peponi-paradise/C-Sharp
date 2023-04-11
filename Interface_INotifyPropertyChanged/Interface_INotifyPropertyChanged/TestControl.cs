using System;
using System.Windows.Forms;

namespace Interface_INotifyPropertyChanged
{
    public partial class TestControl : UserControl
    {
        /*-------------------------------------------
         *
         *      Design time properties
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Events
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Public members
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Private members
         *
         -------------------------------------------*/

        private TestClass TestClass { get; set; }

        /*-------------------------------------------
         *
         *      Constructor / Destructor
         *
         -------------------------------------------*/

        public TestControl()
        {
            InitializeComponent();
        }

        public TestControl(TestClass testClass)
        {
            InitializeComponent();
            TestClass = testClass;
            EnumSelect.Items.AddRange(Enum.GetNames(typeof(TestEnum)));
            BindData();
            PropertyChange.Click += new EventHandler(PropertyChange_Click);
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

        // TestClass 프로퍼티 접근만으로 UI까지 변경 확인용
        private void PropertyChange_Click(object sender, EventArgs e)
        {
            TestClass.TestBool = !TestClass.TestBool;
            TestClass.TestInt += 1;
            TestClass.TestDouble += 0.1;
            TestClass.TestString += "a";
            TestClass.TestEnum = (TestEnum)Enum.Parse(typeof(TestEnum), EnumSelect.SelectedItem.ToString());
        }

        /*-------------------------------------------
         *
         *      Public functions
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Private functions
         *
         -------------------------------------------*/

        // TestClass 바인딩 : 양방향 바인딩
        // 1. 위의 이벤트 동작처럼, 프로퍼티 값을 수정하면 UI에 자동 반영
        // 2. 바인딩된 UI의 값을 바꿀 시 자동으로 프로퍼티에 반영
        private void BindData()
        {
            // CheckBox
            TestBool.DataBindings.Add(new Binding(nameof(TestBool.Checked), TestClass, nameof(TestClass.TestBool)));

            // NumericUpDown
            TestInt.DataBindings.Add(new Binding(nameof(TestInt.Value), TestClass, nameof(TestClass.TestInt)));

            // NumericUpDown
            TestDouble.DataBindings.Add(new Binding(nameof(TestDouble.Value), TestClass, nameof(TestClass.TestDouble)));

            // TextBox
            TestString.DataBindings.Add(new Binding(nameof(TestString.Text), TestClass, nameof(TestClass.TestString)));

            // TextBox : Enum의 경우 TextBox에 업데이트는 가능하나 텍스트 값을 프로퍼티에 바인딩하는 것은 안된다.
            // 양방향 바인딩을 원할 경우 ComboBox 고려하는 것이 좋다.
            TestEnum.DataBindings.Add(new Binding(nameof(TestEnum.Text), TestClass, nameof(TestClass.TestEnum)));
        }

        /*-------------------------------------------
         *
         *      Helper functions
         *
         -------------------------------------------*/
    }
}