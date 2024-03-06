using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PictureBoxDataBinding
{
    public partial class Form1 : Form, INotifyPropertyChanged
    {
        // INotifyPropertyChanged 구현
        public event PropertyChangedEventHandler? PropertyChanged;

        private Image? _imageSource { get; set; }

        public Image? ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged();    // 프로퍼티가 변경되었음을 알림
            }
        }

        public Form1()
        {
            InitializeComponent();

            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            PictureBox box = new() { Width = 400, Height = 400, SizeMode = PictureBoxSizeMode.Zoom };
            Button button = new() { Text = "Load" };
            button.Click += LoadImage;      // 이미지 불러오기

            // formattingEnabled 값 true로 설정해야함
            box.DataBindings.Add(new Binding(nameof(box.Image), this, nameof(ImageSource), true, DataSourceUpdateMode.OnPropertyChanged));

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.Controls.Add(box, 0, 0);
            tableLayoutPanel.Controls.Add(button, 1, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            this.Controls.Add(tableLayoutPanel);
        }

        private void LoadImage(object? sender, EventArgs e)
        {
            OpenFileDialog dialog = new() { Filter = "png (*.png)|*.png|jpg (*.jpg)|*.jpg|All files (*.*)|*.*" };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImageSource = Image.FromFile(dialog.FileName);
                }
                catch
                {
                    MessageBox.Show("Could not load an image");
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}