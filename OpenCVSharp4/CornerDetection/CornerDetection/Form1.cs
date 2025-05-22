using OpenCvSharp;

// image : https://pixabay.com/photos/traffic-road-pedestrian-autumn-7568718/

namespace CornerDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-corner-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        Harris(image);
        ShiTomasi(image);
        GoodFeaturesToTrackWithHarris(image);
        FAST(image);
        FindContoursWithApproxPolyDP(image);
        ShiTomasiWithSubPixelCorrection(image);
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
        Cv2.ImWrite($"{_fileNamePrefix}harris-response.jpg", harris);

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
        Cv2.ImWrite($"{_fileNamePrefix}harris-detected.jpg", detected);
    }

    private void ShiTomasi(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Corner detection (최대 코너의 수, 코너 품질 최소값 (0~1), 코너 사이의 최소 거리 (픽셀), ROI mask, 이웃 픽셀 크기, harris options...)
        // Harris detector를 false로 설정하는 경우 Shi-Tomasi algorithm 적용
        var corners = grayscale.GoodFeaturesToTrack(500, 0.02, 10, null!, 3, false, 0);

        using var detected = image.Clone();

        foreach (var corner in corners)
        {
            detected.Circle(new OpenCvSharp.Point(corner.X, corner.Y), 3, Scalar.LightGreen);
        }

        Cv2.ImShow("Shi-Tomasi corner detected", detected);
        Cv2.ImWrite($"{_fileNamePrefix}shi-tomasi-detected.jpg", detected);
    }

    private void GoodFeaturesToTrackWithHarris(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Corner detection (최대 코너의 수, 코너 품질 최소값 (0~1), 코너 사이의 최소 거리 (픽셀), ROI mask, 이웃 픽셀 크기, harris options...)
        // Harris detector를 true로 설정하는 경우, 코너의 반응 값을 minimum eigenvalue 대신 Harris 공식으로 변경
        var corners = grayscale.GoodFeaturesToTrack(500, 0.02, 10, null!, 3, true, 0.05);

        using var detected = image.Clone();

        foreach (var corner in corners)
        {
            detected.Circle(new OpenCvSharp.Point(corner.X, corner.Y), 3, Scalar.LightGreen);
        }

        Cv2.ImShow("GoodFeaturesToTrack with harris corner detected", detected);
        Cv2.ImWrite($"{_fileNamePrefix}goodfeaturestotrack-harris-detected.jpg", detected);
    }

    private void FAST(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // FAST 코너 검출 : 검출 픽셀과 주변 픽셀의 밝기, threshold 값을 이용하여 코너 판정
        var corners = Cv2.FAST(grayscale, 75);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, corners, detected, Scalar.LightGreen);

        Cv2.ImShow("FAST detected", detected);
        Cv2.ImWrite($"{_fileNamePrefix}fast-detected.jpg", detected);
    }

    private void FindContoursWithApproxPolyDP(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var threshold = grayscale.Threshold(-1, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

        // Contour detection : 가장 바깥쪽 외곽선만 가져오고, 필요한 최소한의 포인트만 구함
        threshold.FindContours(out var contours, out var hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

        using var detected = image.Clone();

        // 면적이 너무 작은 외곽선은 객체가 아닐 가능성이 높음
        var contourThreshold = 10;

        foreach (var contour in contours)
        {
            if (Cv2.ContourArea(contour) < contourThreshold)
                continue;

            // 다각형 계산 : epsilon 값은 보통 외곽선 길이의 5% 이하로 설정 (허용 오차)
            var corners = Cv2.ApproxPolyDP(contour, 0.03 * Cv2.ArcLength(contour, true), true);

            foreach (var corner in corners)
            {
                detected.Circle(corner, 3, Scalar.LightGreen);
            }
        }

        Cv2.ImShow("FindContours with ApproxPolyDP", detected);
        Cv2.ImWrite($"{_fileNamePrefix}findcontours-approxpolydp-detected.jpg", detected);
    }

    private void ShiTomasiWithSubPixelCorrection(Mat image)
    {
        // 예시로 ShiTomasi 사용

        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Corner detection (최대 코너의 수, 코너 품질 최소값 (0~1), 코너 사이의 최소 거리 (픽셀), ROI mask, 이웃 픽셀 크기, harris options...)
        var corners = grayscale.GoodFeaturesToTrack(500, 0.02, 10, null!, 3, false, 0);

        // 위에서 찾은 정수 단위 corners에 대한 정밀 연산 : 소수점 단위로 조정 (실제로는 코너가 픽셀 사이에 걸쳐있을 수 있음)
        // winSize : 코너 위치를 기준으로 정밀 탐색을 수행할 윈도우 크기의 1/4 지정 (아래처럼 3, 3 지정하는 경우 실제 윈도우는 7, 7)
        // zeroZone : 탐색 윈도우 안에서 무시할 영역의 크기 (winSize와 마찬가지로 1/4 지정). 보통 -1, -1 사용하여 무시하는 영역이 없게 설정
        // criteria : 탐색 반복 설정 - type : 멈추는 조건 설정, maxCount : 최대 반복 수, epsilon : 정확도 수준 (값이 작을 수록 정밀)
        var correctedCorners = Cv2.CornerSubPix(grayscale, corners, new OpenCvSharp.Size(3, 3), new OpenCvSharp.Size(-1, -1), new TermCriteria(CriteriaTypes.Eps, 10, 0.05));

        using var detected = image.Clone();

        foreach (var corner in correctedCorners)
        {
            detected.Circle(new OpenCvSharp.Point(corner.X, corner.Y), 3, Scalar.LightGreen);
        }

        Cv2.ImShow("Shi-Tomasi corner detected with subpixel correction", detected);
        Cv2.ImWrite($"{_fileNamePrefix}shi-tomasi-subpixel-correction-detected.jpg", detected);
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