using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Interface_INotifyPropertyChanged
{
    public enum TestEnum
    {
        A, B, C
    }

    public class TestClass : INotifyPropertyChanged
    {
        /*-------------------------------------------
         *
         *      Events
         *
         -------------------------------------------*/

        public event PropertyChangedEventHandler PropertyChanged;

        /*-------------------------------------------
         *
         *      Public members
         *
         -------------------------------------------*/

        public bool TestBool
        {
            get => testBool;
            set
            {
                testBool = value;
                OnPropertyChanged(nameof(TestBool));
            }
        }

        public int TestInt
        {
            get => testInt;
            set
            {
                testInt = value;
                OnPropertyChanged(nameof(TestInt));
            }
        }

        public double TestDouble
        {
            get => testDouble;
            set
            {
                testDouble = value;
                OnPropertyChanged(nameof(TestDouble));
            }
        }

        public string TestString
        {
            get => testString;
            set
            {
                testString = value;
                OnPropertyChanged(nameof(TestString));
            }
        }

        public TestEnum TestEnum
        {
            get => testEnum;
            set
            {
                testEnum = value;
                OnPropertyChanged(nameof(TestEnum));
            }
        }

        /*-------------------------------------------
         *
         *      Private members
         *
         -------------------------------------------*/

        private bool testBool = false;
        private int testInt = 0;
        private double testDouble = 0;
        private string testString = string.Empty;
        private TestEnum testEnum = TestEnum.A;

        private readonly SynchronizationContext UISyncContext;

        /*-------------------------------------------
         *
         *      Constructor / Destructor
         *
         -------------------------------------------*/

        public TestClass(SynchronizationContext UISyncContext = null)
        {
            this.UISyncContext = UISyncContext == null ? SynchronizationContext.Current : UISyncContext;
        }

        /*-------------------------------------------
         *
         *      Event functions
         *
         -------------------------------------------*/

        /*-------------------------------------------
         *
         *      Public functions
         *
         -------------------------------------------*/

        // 바인딩된 컴포넌트 반환. new로 반환하여 이 클래스의 데이터를 표시하는 여러개의 컴포넌트 생성 가능
        public UserControl GetComponent() => new TestControl(this);

        /*-------------------------------------------
         *
         *      Private functions
         *
         -------------------------------------------*/

        // 프로퍼티 변경 전파
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            UISyncContext.Post(delegate
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }, null);
        }

        /*-------------------------------------------
         *
         *      Helper functions
         *
         -------------------------------------------*/
    }
}