using OpenCvSharp;
using System.Diagnostics;

// apple, orange image from https://docs.opencv.org/3.4/dc/dff/tutorial_py_pyramids.html

namespace ImageBlending
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var images = LoadImages();

            Arithmetic(images[0], images[1]);
            Bitwise(images[0], images[1]);
            Weighted(images[0], images[1]);
            Concat(images[0], images[1]);
            SetTo(images[0], images[1]);
            CopyTo(images[0], images[1]);
            LaplacePyramid(images[0], images[1]);
        }

        private void Arithmetic(Mat apple, Mat orange)
        {
            using var result = new Mat();
            Cv2.Add(apple, orange, result);

            Cv2.ImWrite("opencvsharp-image-blending-arithmetic.jpg", result);
        }

        private void Bitwise(Mat apple, Mat orange)
        {
            using var result = new Mat();
            Cv2.BitwiseAnd(apple, orange, result);

            Cv2.ImWrite("opencvsharp-image-blending-bitwise.jpg", result);
        }

        private void Weighted(Mat apple, Mat orange)
        {
            using var result = new Mat();
            // Cv2.AddWeighted(image1, weight1, image2, weight2, gamma, result)
            Cv2.AddWeighted(apple, 0.8, orange, 0.5, 0, result);

            Cv2.ImWrite("opencvsharp-image-blending-arithmetic-weighted.jpg", result);
        }

        private void SetTo(Mat apple, Mat orange)
        {
            using var hsv = apple.CvtColor(ColorConversionCodes.BGR2HSV);

            using var redLow = new Mat();
            using var redHigh = new Mat();

            Cv2.InRange(hsv, new(0, 50, 50), new(15, 255, 255), redLow);
            Cv2.InRange(hsv, new(150, 50, 50), new(180, 255, 255), redHigh);
            using var mask = redLow + redHigh;

            using var result = orange.Clone();
            result.SetTo(Scalar.Crimson, mask);

            Cv2.ImWrite("opencvsharp-image-blending-apple-mask.jpg", mask);
            Cv2.ImWrite("opencvsharp-image-blending-set-to.jpg", result);
        }

        private void CopyTo(Mat apple, Mat orange)
        {
            using var hsv = apple.CvtColor(ColorConversionCodes.BGR2HSV);

            using var redLow = new Mat();
            using var redHigh = new Mat();

            Cv2.InRange(hsv, new(0, 50, 50), new(15, 255, 255), redLow);
            Cv2.InRange(hsv, new(150, 50, 50), new(180, 255, 255), redHigh);
            using var mask = redLow + redHigh;

            using var result = orange.Clone();
            apple.CopyTo(result, mask);

            Cv2.ImWrite("opencvsharp-image-blending-copy-to.jpg", result);
        }

        private void Concat(Mat apple, Mat orange)
        {
            using var result = new Mat();
            using var appleLeft = apple.SubMat(new Rect(0, 0, apple.Width / 2, apple.Height));
            using var orangeRight = orange.SubMat(new Rect(orange.Width / 2, 0, orange.Width / 2, orange.Height));
            Cv2.HConcat([appleLeft, orangeRight], result);

            Cv2.ImWrite("opencvsharp-image-blending-concat.jpg", result);
        }

        private void LaplacePyramid(Mat apple, Mat orange)
        {
            // Build gaussian pyramid
            var appleGaussian = apple.BuildPyramid(4).ToList();
            var orangeGaussian = orange.BuildPyramid(4).ToList();

            // Build laplasian pyramid
            List<Mat> appleLaplacian = BuildLaplacianPyramid(appleGaussian);
            List<Mat> orangeLaplacian = BuildLaplacianPyramid(orangeGaussian);

            // Blending base
            var result = new Mat();
            using var appleLeft = appleGaussian.Last().SubMat(new Rect(0, 0, appleGaussian.Last().Width / 2, appleGaussian.Last().Height));
            using var orangeRight = orangeGaussian.Last().SubMat(new Rect(orangeGaussian.Last().Width / 2, 0, orangeGaussian.Last().Width / 2, orangeGaussian.Last().Height));
            Cv2.HConcat([appleLeft, orangeRight], result);

            // Laplacian blending
            for (int i = 0; i < appleLaplacian.Count; i++)
            {
                result = result.PyrUp();
                if (result.Size() != appleLaplacian[i].Size())
                    Cv2.Resize(result, result, appleLaplacian[i].Size());

                using var laplacian = new Mat();
                using var laplacianLeft = appleLaplacian[i].SubMat(new Rect(0, 0, appleLaplacian[i].Width / 2, appleLaplacian[i].Height));
                using var laplacianRight = orangeLaplacian[i].SubMat(new Rect(orangeLaplacian[i].Width / 2, 0, orangeLaplacian[i].Width / 2, orangeLaplacian[i].Height));
                Cv2.HConcat([laplacianLeft, laplacianRight], laplacian);

                result = result.Add(laplacian);
            }

            Cv2.ImWrite("opencvsharp-image-blending-laplace.jpg", result);

            result.Release();

            List<Mat> BuildLaplacianPyramid(List<Mat> gaussian)
            {
                var laplacian = new List<Mat>();
                for (int i = gaussian.Count - 1; i > 0; i--)
                {
                    using var tempUp = gaussian[i].PyrUp();
                    if (tempUp.Size() != gaussian[i - 1].Size())
                        Cv2.Resize(tempUp, tempUp, gaussian[i - 1].Size());

                    laplacian.Add(gaussian[i - 1] - tempUp);
                }
                return laplacian;
            }
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