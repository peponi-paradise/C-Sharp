using OpenCvSharp;

// image : https://pixabay.com/photos/traffic-road-pedestrian-autumn-7568718/

namespace CornerDetection;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        Harris(image);
        ShiTomasi(image);
    }

    private void Harris(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        using var corners = new Mat();

        // Corner detection (일반적으로 blockSize = 2, ksize = 3, k = 0.04~0.06)
        Cv2.CornerHarris(grayscale, corners, 2, 3, 0.05);

        // 결과를 표시하기 위해 변환
        using var normalized = corners.Normalize(0, 255, NormTypes.MinMax);
        using var harris = normalized.ConvertScaleAbs();

        Cv2.ImShow("Harris response", harris);

        // 원본 이미지에 검출된 결과 필터링
        using var detected = image.Clone();

        // 검출 최대값 찾기
        corners.MinMaxIdx(out var min, out var max);

        for (int i = 0; i < corners.Width; i++)
        {
            for (int j = 0; j < corners.Height; j++)
            {
                // 검출 최대값의 2% 이상을 가진 픽셀을 코너로 취급 (thresholding)
                if (corners.Get<float>(j, i) >= max * 0.02)
                    detected.Circle(i, j, 3, Scalar.LightGreen);
            }
        }

        Cv2.ImShow("Harris corner detected", detected);
    }

    private void ShiTomasi(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Corner detection (최대 코너의 수, 코너 품질 최소값 (0~1), 코너 사이의 최소 거리 (픽셀), ROI mask, 이웃 픽셀 크기, harris options...)
        var corners = grayscale.GoodFeaturesToTrack(500, 0.02, 10, null!, 3, false, 0);

        using var detected = image.Clone();

        foreach (var corner in corners)
        {
            detected.Circle(new OpenCvSharp.Point(corner.X, corner.Y), 3, Scalar.LightGreen);
        }

        Cv2.ImShow("Shi-Tomasi corner detected", detected);
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