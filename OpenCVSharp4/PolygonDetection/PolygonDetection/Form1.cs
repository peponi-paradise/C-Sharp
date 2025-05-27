using OpenCvSharp;

namespace PolygonDetection
{
    public partial class Form1 : Form
    {
        private string _fileNamePrefix = "opencvsharp-polygon-detection-";

        public Form1()
        {
            InitializeComponent();

            using var image = LoadImage();

            Cv2.ImShow("Original", image);

            ApproxPolyDP(image);
        }

        private void ApproxPolyDP(Mat image)
        {
            using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
            using var canny = grayscale.Canny(50, 150);

            // Contour detection : 가장 바깥쪽 외곽선만 가져오고, 필요한 최소한의 포인트만 구함
            canny.FindContours(out var contours, out var hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            using var detected = image.Clone();

            // 면적이 너무 작은 외곽선은 객체가 아닐 가능성이 높음
            var contourThreshold = 10;

            foreach (var contour in contours)
            {
                if (Cv2.ContourArea(contour) < contourThreshold)
                    continue;

                // 다각형 계산 : epsilon 값은 보통 외곽선 길이의 5% 이하로 설정 (허용 오차)
                var polygon = Cv2.ApproxPolyDP(contour, 0.03 * Cv2.ArcLength(contour, true), true);

                // 도형 외곽선 그리기
                detected.DrawContours([polygon], 0, Scalar.White, 2);

                // 꼭지점의 수를 도형 안에 표현
                detected.PutText($"{polygon.Length}", GetCenterApprox(polygon), HersheyFonts.HersheySimplex, 1, Scalar.Black, 2);
            }

            Cv2.ImShow("Canny", canny);
            Cv2.ImShow("ApproxPolyDP", detected);

            Cv2.ImWrite($"{_fileNamePrefix}canny.jpg", canny);
            Cv2.ImWrite($"{_fileNamePrefix}approxpolydp.jpg", detected);

            OpenCvSharp.Point GetCenterApprox(OpenCvSharp.Point[] polygon)
            {
                double sumX = 0, sumY = 0;

                foreach (var corner in polygon)
                {
                    sumX += corner.X;
                    sumY += corner.Y;
                }

                return new((int)(sumX / polygon.Length), (int)(sumY / polygon.Length));
            }
        }

        private Mat LoadImage()
        {
            OpenFileDialog dialog = new()
            {
                Filter = "All files|*.*",
                InitialDirectory = $@"C:\Study\Blog\public\posts\nuget-package\",
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