using System;
using System.Windows.Forms;
using OpenCvSharp;

namespace ImageLoadSave
{
    public partial class MainFrame : Form
    {
        Mat LoadImage = new Mat();  // OpenCVSharp 객체

        public MainFrame()
        {
            InitializeComponent();
            LoadButton.Click += LoadButton_Click;
            SaveButton.Click += SaveButton_Click;
            ImageMode.Items.AddRange(Enum.GetNames(typeof(ImreadModes)));
            ImageMode.SelectedIndex = 0;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadImage.Release();    // 메모리 누수 발생으로 명시적 초기화 권장
            GC.Collect();
            GC.WaitForPendingFinalizers();  // 메모리 사용량이 대용량인 경우 즉시 GC에서 메모리 수거하도록

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All files|*.*";
            dialog.InitialDirectory = $@"C:\";
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // 이미지 로드
                LoadImage = Cv2.ImRead(dialog.FileName, (ImreadModes)Enum.Parse(typeof(ImreadModes), ImageMode.SelectedItem.ToString()));
                ImagePath.Text = dialog.FileName;

                // PictureBox에 이미지 할당. OpenCvSharp4.Extensions 설치 필요
                PictureView.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(LoadImage);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Bitmap file|*.bmp";
            dialog.InitialDirectory = $@"C:\";
            dialog.CheckPathExists = true;
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // 이미지 저장
                Cv2.ImWrite(dialog.FileName, LoadImage);
            }
        }
    }
}