using OpenCvSharp;

// image : https://pixabay.com/illustrations/vintage-parchment-old-document-6554322/

namespace ImageSharpening
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            using var image = LoadImage();

            Filter2D(image);
            UnsharpMaskFilter(image);
            LaplacianFilter(image);
        }

        private void Filter2D(Mat image)
        {
            // 아래 커널에 지정할 공간 크기만큼 배열 할당
            // 블러 또는 샤프닝 효과를 얻는데 사용 가능하며 커널 설정에 따라 평균값, 가우시안 등이 적용된다.
            // 중앙 픽셀 값을 강조하여 샤프닝 효과를 얻는 예시
            float[] weight = [ 0, -1, 0,
                                -1, 5, -1,
                                0, -1, 0 ];

            using var kernel = Mat.FromPixelData(3, 3, MatType.CV_32FC1, weight);

            using var sharpened = image.Filter2D(-1, kernel);

            Cv2.ImShow("Filter2D", sharpened);
            Cv2.ImWrite("opencvsharp-image-sharpening-filter2d.jpg", sharpened);
        }

        private void UnsharpMaskFilter(Mat image)
        {
            // 이미지 샤프닝을 위한 gaussian 생성
            using var gaussian = image.GaussianBlur(new OpenCvSharp.Size(9, 9), 0);

            using var sharpened = new Mat();

            // 가중치 설정을 통해 gaussian 반영 정도 조절
            Cv2.AddWeighted(image, 2, gaussian, -1, 0, sharpened);

            Cv2.ImShow("UnsharpMaskFilter", sharpened);
            Cv2.ImWrite("opencvsharp-image-sharpening-unsharp-mask-filter.jpg", sharpened);
        }

        private void LaplacianFilter(Mat image)
        {
            // 이미지 샤프닝을 위한 laplacian 생성
            using var laplacian = image.Laplacian(-1, 3);

            using var sharpened = new Mat();

            // 가중치 설정을 통해 laplacian 반영 정도 조절
            Cv2.AddWeighted(image, 1, laplacian, -1, 0, sharpened);

            Cv2.ImShow("LaplacianFilter", sharpened);
            Cv2.ImWrite("opencvsharp-image-sharpening-laplacian-filter.jpg", sharpened);
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