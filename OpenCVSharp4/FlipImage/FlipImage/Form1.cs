using OpenCvSharp;

namespace FlipImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            using var image = LoadImage();

            Flip(image);
        }

        private void Flip(Mat image)
        {
            using var xFlip = image.Flip(FlipMode.X);
            using var yFlip = image.Flip(FlipMode.Y);
            using var xyFlip = image.Flip(FlipMode.XY);

            Cv2.ImWrite("opencvsharp-flip-image-x-axis.jpg", xFlip);
            Cv2.ImWrite("opencvsharp-flip-image-y-axis.jpg", yFlip);
            Cv2.ImWrite("opencvsharp-flip-image-xy-axis.jpg", xyFlip);
        }

        private Mat LoadImage()
        {
            OpenFileDialog dialog = new()
            {
                Filter = "All files|*.*",
                InitialDirectory = $@"C:\",
                CheckPathExists = true,
                CheckFileExists = true,
                Multiselect = true,
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return Cv2.ImRead(dialog.FileName);
            }

            return null!;
        }
    }
}