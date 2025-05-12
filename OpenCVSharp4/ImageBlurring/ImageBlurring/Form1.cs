using OpenCvSharp;

namespace ImageBlurring;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Blur(image);
        Filter2D(image);
        BoxFilter(image);
        GaussianBlur(image);
        MedianBlur(image);
        BilateralFilter(image);
    }

    private void Blur(Mat image)
    {
        // 커널 내 픽셀 값에 대한 평균 블러링 수행
        using var blurred = image.Blur(new OpenCvSharp.Size(3, 3));

        Cv2.ImShow("Blur", blurred);
        Cv2.ImWrite("opencvsharp-image-blurring-blur.jpg", blurred);
    }

    private void Filter2D(Mat image)
    {
        // 아래 커널에 지정할 공간 크기만큼 배열 할당
        // 블러 또는 샤프닝 효과를 얻는데 사용 가능하며 커널 설정에 따라 평균값, 가우시안 등이 적용된다.
        // 모든 가중치를 동일하게 부여하여 평균 블러링을 적용하는 예시
        float[] weight = [ 1/9f, 1/9f, 1/9f,
                            1/9f, 1/9f, 1/9f,
                            1/9f, 1/9f, 1/9f ];

        using var kernel = Mat.FromPixelData(3, 3, MatType.CV_32FC1, weight);

        using var blurred = image.Filter2D(-1, kernel);

        Cv2.ImShow("Filter2D", blurred);
        Cv2.ImWrite("opencvsharp-image-blurring-filter2d.jpg", blurred);
    }

    private void BoxFilter(Mat image)
    {
        // 기본 Blur와 유사하고, 경계 처리 방법 등의 추가 기능 제공
        using var blurred = image.BoxFilter(-1, new OpenCvSharp.Size(3, 3));

        Cv2.ImShow("BoxFilter", blurred);
        Cv2.ImWrite("opencvsharp-image-blurring-boxfilter.jpg", blurred);
    }

    private void GaussianBlur(Mat image)
    {
        // 가우시안 함수를 이용하여 이미지 블러링
        // 너무 심하게 적용하는 경우 엣지 훼손 가능성 있음
        // sigmaX and Y : 해당 축 방향 표준편차 (0 : 자동), 값이 커질수록 더 많이 블러링
        using var blurred = image.GaussianBlur(new OpenCvSharp.Size(3, 3), 0);

        Cv2.ImShow("GaussianBlur", blurred);
        Cv2.ImWrite("opencvsharp-image-blurring-gaussianblur.jpg", blurred);
    }

    private void MedianBlur(Mat image)
    {
        // 커널 내의 픽셀 중 중간값으로 픽셀 대체
        // 노이즈를 제거하는 것에 유용함
        // 소금-후추 잡음 (노이즈로 인한 비정상 픽셀값) 처리에 탁월함
        using var blurred = image.MedianBlur(3);

        Cv2.ImShow("MedianBlur", blurred);
        Cv2.ImWrite("opencvsharp-image-blurring-medianblur.jpg", blurred);
    }

    private void BilateralFilter(Mat image)
    {
        // 인접 픽셀의 값 (거리, 색상) 을 고려하여 블러링 수행
        // 노이즈를 줄이면서 엣지를 보존하는 기법
        // 지름 d를 음수로 지정하면 sigmaSpace (공간 거리의 표준 편차) 에 따라 자동 설정됨
        // sigmaColor 값이 클수록 색상 차이가 큰 픽셀도 연산에 포함됨
        // sigmaSpace 값이 클수록 더 멀리 떨어진 픽셀도 연산에 포함됨
        using var blurred = image.BilateralFilter(-1, 105, 15);

        Cv2.ImShow("BilateralFilter", blurred);
        Cv2.ImWrite("opencvsharp-image-blurring-bilateralfilter.jpg", blurred);
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