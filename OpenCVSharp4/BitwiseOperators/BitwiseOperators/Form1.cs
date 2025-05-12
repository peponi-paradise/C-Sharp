using OpenCvSharp;

namespace BitwiseOperators
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var images = LoadImages();

            And(images[0], images[1]);
            Or(images[0], images[1]);
            Not(images[0]);
            Xor(images[0], images[1]);
        }

        private void And(Mat image1, Mat image2)
        {
            using var oper = image1 & image2;
            using var oper2 = image1 & 50;
            Cv2.ImShow("asd", oper2);

            using var withImage = new Mat();
            Cv2.BitwiseAnd(image1, image2, withImage);

            using var withScalar = new Mat();
            Cv2.BitwiseAnd(image1, 50, withScalar);

            Cv2.ImWrite("opencvsharp-bitwise-operations-and-image.jpg", withImage);
            Cv2.ImWrite("opencvsharp-bitwise-operations-and-scalar.jpg", withScalar);
        }

        private void Or(Mat image1, Mat image2)
        {
            using var withImage = new Mat();
            Cv2.BitwiseOr(image1, image2, withImage);

            using var withScalar = new Mat();
            Cv2.BitwiseOr(image1, 50, withScalar);

            Cv2.ImWrite("opencvsharp-bitwise-operations-or-image.jpg", withImage);
            Cv2.ImWrite("opencvsharp-bitwise-operations-or-scalar.jpg", withScalar);
        }

        private void Not(Mat image)
        {
            using var result = new Mat();
            Cv2.BitwiseNot(image, result);

            Cv2.ImWrite("opencvsharp-bitwise-operations-not-image.jpg", result);
        }

        private void Xor(Mat image1, Mat image2)
        {
            using var withImage = new Mat();
            Cv2.BitwiseXor(image1, image2, withImage);

            using var withScalar = new Mat();
            Cv2.BitwiseXor(image1, 50, withScalar);

            Cv2.ImWrite("opencvsharp-bitwise-operations-xor-image.jpg", withImage);
            Cv2.ImWrite("opencvsharp-bitwise-operations-xor-scalar.jpg", withScalar);
        }

        private List<Mat> LoadImages()
        {
            var images = new List<Mat>();

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
                foreach (var path in dialog.FileNames)
                {
                    images.Add(Cv2.ImRead(path));
                }
            }

            return images;
        }
    }
}