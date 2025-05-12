using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;

namespace ArithmeticOperations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var images = LoadImages();

            Add(images[0], images[1]);
            Subtract(images[0], images[1]);
            Multiply(images[0], images[1]);
            Multiply(images[0]);
            Divide(images[0], images[1]);
            Divide(images[0]);
            Max(images[0], images[1]);
            Max(images[0]);
            Min(images[0], images[1]);
            Min(images[0]);
            Abs(images[0], images[1]);
            Abs(images[0]);
            AbsDiff(images[0], images[1]);
            AbsDiff(images[0]);
        }

        private void Add(Mat image1, Mat image2)
        {
            using var op = image1 + image2;

            using var method = new Mat();
            Cv2.Add(image1, image2, method);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-add.jpg", method);
        }

        private void Add(Mat image)
        {
            using var result = new Mat();
            Cv2.Add(image, 20, result);
        }

        private void Subtract(Mat image1, Mat image2)
        {
            using var result1 = image1 - image2;

            using var result2 = new Mat();
            Cv2.Subtract(image1, image2, result2);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-subtract.jpg", result2);
        }

        private void Subtract(Mat image)
        {
            using var result = new Mat();
            Cv2.Subtract(image, 20, result);
        }

        private void Multiply(Mat image1, Mat image2)
        {
            using var result = new Mat();
            Cv2.Multiply(image1, image2, result);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-multiply.jpg", result);
        }

        private void Multiply(Mat image)
        {
            using var result1 = image * 5;

            using var result2 = new Mat();
            Cv2.Multiply(image, 10, result2);
        }

        private void Divide(Mat image1, Mat image2)
        {
            using var result1 = image1 / image2;

            using var result2 = new Mat();
            Cv2.Divide(image1, image2, result2);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-divide.jpg", result2);
        }

        private void Divide(Mat image)
        {
            using var result1 = image / 5;

            using var result2 = new Mat();
            Cv2.Divide(image, 10, result2);
        }

        private void Max(Mat image1, Mat image2)
        {
            using var result = new Mat();
            Cv2.Max(image1, image2, result);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-max.jpg", result);
        }

        private void Max(Mat image)
        {
            using var result = new Mat();
            Cv2.Max(image, 50, result);
        }

        private void Min(Mat image1, Mat image2)
        {
            using var result = new Mat();
            Cv2.Min(image1, image2, result);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-min.jpg", result);
        }

        private void Min(Mat image)
        {
            using var result = new Mat();
            Cv2.Min(image, 50, result);
        }

        private void Abs(Mat image1, Mat image2)
        {
            using var expr = image1 - image2;
            using var result = Cv2.Abs(expr);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-abs.jpg", result);
        }

        private void Abs(Mat image)
        {
            using var result = Cv2.Abs(image);

            Cv2.ImShow("result1", result);
        }

        private void AbsDiff(Mat image1, Mat image2)
        {
            using var result = new Mat();
            Cv2.Absdiff(image1, image2, result);

            Cv2.ImWrite("opencvsharp-arithmetic-operations-absdiff.jpg", result);
        }

        private void AbsDiff(Mat image)
        {
            using var result = new Mat();
            Cv2.Absdiff(image, 50, result);

            Cv2.ImShow("result1", result);
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