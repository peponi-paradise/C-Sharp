using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace INotifyPropertyChangedExample
{
    public enum TestEnum
    {
        A, B, C
    }

    public class TestClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _testBool = false;

        public bool TestBool
        {
            get => _testBool;
            set
            {
                _testBool = value;
                OnPropertyChanged();
            }
        }

        private int _testInt = 0;

        public int TestInt
        {
            get => _testInt;
            set
            {
                _testInt = value;
                OnPropertyChanged();
            }
        }

        private double _testDouble = 0;

        public double TestDouble
        {
            get => _testDouble;
            set
            {
                _testDouble = value;
                OnPropertyChanged();
            }
        }

        private string _testString = string.Empty;

        public string TestString
        {
            get => _testString;
            set
            {
                _testString = value;
                OnPropertyChanged();
            }
        }

        private TestEnum _testEnum = TestEnum.A;

        public TestEnum TestEnum
        {
            get => _testEnum;
            set
            {
                _testEnum = value;
                OnPropertyChanged();
            }
        }

        private readonly SynchronizationContext _syncContext;

        public TestClass(SynchronizationContext syncContext = null)
        {
            _syncContext = syncContext ?? SynchronizationContext.Current;
        }

        // 바인딩된 컴포넌트 반환. new로 반환하여 이 클래스의 데이터를 표시하는 여러개의 컴포넌트 생성 가능
        public UserControl GetComponent() => new TestControl(this);

        // 프로퍼티 변경 전파
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                _syncContext.Post(delegate
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }, null);
            }
        }
    }
}