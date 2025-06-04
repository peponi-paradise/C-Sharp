using OpenCvSharp;

// image : https://pixabay.com/photos/golf-arrow-golf-ball-549230/

namespace CircleDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-circle-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        HoughCircles(image);
        FindContours(image);
        Blob(image);
    }

    private void HoughCircles(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Mat.HoughCircles(method, quantization interval, distance, ...)
        var circles = grayscale.HoughCircles(HoughModes.Gradient, 1, 20, 60, 30, 10, 20);

        if (circles is null || circles.Length == 0)
        {
            MessageBox.Show("Not found");
            return;
        }

        using var detected = image.Clone();

        foreach (var circle in circles)
        {
            detected.Circle((int)circle.Center.X, (int)circle.Center.Y, (int)circle.Radius, Scalar.Red, 2);
        }

        Cv2.ImShow("HoughCircles detected", detected);

        Cv2.ImWrite($"{_fileNamePrefix}houghcircles.jpg", detected);
    }

    private void FindContours(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var threshold = grayscale.Threshold(150, 255, ThresholdTypes.Binary);
        // Noise 제거
        using var blur = threshold.MedianBlur(3);

        blur.FindContours(out var contours, out var hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxTC89KCOS);

        if (contours is null || contours.Length == 0)
        {
            MessageBox.Show("Not found");
            return;
        }

        using var detected = image.Clone();

        foreach (var contour in contours)
        {
            // 점 또는 선인 경우 length == 0
            double length = Cv2.ArcLength(contour, true);
            if (length == 0)
                continue;

            // 너무 작은 면적은 원이 아닐 수 있음
            double area = Cv2.ContourArea(contour);
            if (area < 50)
                continue;

            Cv2.MinEnclosingCircle(contour, out var center, out var radius);

            // 검출된 원에 대해 필터링이 필요한 경우 다음과 같이 구성할 수 있다.
            // 원형 지표 계산
            double circularity = 4 * Math.PI * area / Math.Pow(length, 2);

            double minCircleArea = Math.PI * Math.Pow(radius, 2);

            // 원형 지표와 contour/minCircleArea 비율이 60% 이상인 경우 원으로 취급
            // Thresholding 값은 환경에 따라 임의 설정
            if (circularity > 0.6 && area / minCircleArea > 0.6)
            {
                detected.Circle((int)center.X, (int)center.Y, (int)radius, Scalar.Red, 2);
            }
        }

        Cv2.ImShow("Blur", blur);
        Cv2.ImShow("MinEnclosingCircle detected", detected);

        Cv2.ImWrite($"{_fileNamePrefix}blur.jpg", blur);
        Cv2.ImWrite($"{_fileNamePrefix}minenclosingcircle.jpg", detected);
    }

    private void Blob(Mat image)
    {
        // FindContours와 같은 전처리 이미지 사용
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var threshold = grayscale.Threshold(150, 255, ThresholdTypes.Binary);
        using var blur = threshold.MedianBlur(3);

        // 색상을 포함한 blob 파라미터 설정
        // 60% 이상의 원형도와 볼록함을 가질 때 원으로 취급
        var blobParams = new SimpleBlobDetector.Params()
        {
            FilterByColor = true,
            BlobColor = 255,
            FilterByArea = true,
            MinArea = 50,
            FilterByCircularity = true,
            MinCircularity = 0.6f,
            FilterByConvexity = true,
            MinConvexity = 0.6f
        };
        using var detector = SimpleBlobDetector.Create(blobParams);
        var blobs = detector.Detect(blur);

        if (blobs is null || blobs.Length == 0)
        {
            MessageBox.Show("Not found");
            return;
        }

        using var detected = image.Clone();

        // Blob key point를 이용하여 원 그리기
        // Cv2.DrawKeypoints() 메서드로 그리는 경우 포인트만 표현됨
        foreach (var blob in blobs)
        {
            detected.Circle((int)blob.Pt.X, (int)blob.Pt.Y, (int)blob.Size / 2, Scalar.Red, 2);
        }

        Cv2.ImShow("Blob detected", detected);

        Cv2.ImWrite($"{_fileNamePrefix}blob.jpg", detected);
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