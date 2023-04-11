## 1. Introduction

<br>

1. `INotifyPropertyChanged` 인터페이스는 클라이언트에 속성 값이 변경됨을 알리는데 사용한다.
2. 이벤트로 구현되어 있으며, 인터페이스 구현 또는 프로퍼티 변경 이벤트를 넣어 UI에 반영한다.
    (아래 예제는 프로퍼티 변경 이벤트를 예시로 한다.)
3. 구현이 쉽고 간결하다. 수동으로 멤버를 바인딩 하는 경우에 비해 제작 편의성 및 재사용성이 대폭 향상된다.
4. 기본적으로 양방향 바인딩이 된다. (이게 가장 큰 장점인 것 같다)
    - 프로퍼티 값 수정 시 UI에 자동 반영이 된다.
    - UI 값 수정 시 프로퍼티에 맞는 값이라면 자동 반영이 되며 잘못된 값 입력하는 경우 자동으로 원래 값으로 돌아간다.
5. 만능은 아니기 때문에 경우에 따라 여전히 수동으로 바인딩 하는 경우가 필요할 수 있다.

<br>

## 2. 사용 방법

<br>

- 하단 샘플은 `INotifyPropertyChanged` 인터페이스에 대한 간단한 사용 방법이다.

<br>

### 2.1. Data class

```cs
public class TestClass : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    private bool testBool;
    public bool TestBool
    {
        get => testBool;
        set
        {
            testBool = value;
            OnPropertyChanged(nameof(TestBool));
        }
    }

    private readonly SynchronizationContext UISyncContext;

    public TestClass(SynchronizationContext UISyncContext = null)
    {
        this.UISyncContext = UISyncContext;
    }

    // 바인딩된 컴포넌트 반환. new로 반환하여 이 클래스의 데이터를 표시하는 여러개의 컴포넌트 생성 가능
    public UserControl GetComponent() => new TestControl(this);

    // 프로퍼티 변경 전파
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        UISyncContext.Post(delegate
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }, null);
    }
}
```

<br>

### 2.2. Control class

```cs
public partial class TestControl : UserControl
{
    private TestClass TestClass { get; set; }

    public TestControl(TestClass testClass)
    {
        InitializeComponent();
        TestClass = testClass;
        BindData();
    }

    private void BindData()
    {
        // TestBool : boolean 바인딩 테스트를 위한 CheckBox
        TestBool.DataBindings.Add(new Binding(nameof(TestBool.Checked), TestClass, nameof(TestClass.TestBool)));
    }
}
```

<br>

## 3. Summary

<br>

내용을 요약하자면,

- Test class
   1. `INotifyPropertyChanged`를 상속받는 객체 생성
   2. `INotifyPropertyChanged` 인터페이스 구현 또는 프로퍼티 변경 이벤트를 제공한다.
   3. UI와 관련된 내용은 항상 그렇지만, 여기서도 `Cross thread exception`에 유의한다.

- Control class
   1. Bindable 클라이언트에 데이터 바인딩

위의 내용과 같이 심플하게 구현이 가능하게 된다.
만약 추가 예제가 필요한 경우, 아래 섹션을 참고하면 된다.

<br>

## A. 추가 예제

<br>

### A.1. TestClass (데이터 객체)

```cs
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
```

<br>

### A.2. TestControl

```cs
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
```

<br>

### A.3. Form

```cs
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
```