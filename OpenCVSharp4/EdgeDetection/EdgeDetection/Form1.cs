using OpenCvSharp;

// imge : https://pixabay.com/photos/letter-airmail-envelope-stamp-1255872/

namespace EdgeDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-edge-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        Filter2D(image);
        Sobel(image);
        Scharr(image);
        Laplacian(image);
        Canny(image);
        Gradient(image);
    }

    private void Filter2D(Mat image)
    {
        // Sobel example

        // Kernel 생성
        float[] weightX = [-1,0,1,
                              -2,0,2,
                              -1,0,1];
        float[] weightY = [-1,-2,-1,
                               0,0,0,
                               1,2,1];

        using var kernelX = Mat.FromPixelData(3, 3, MatType.CV_32FC1, weightX);
        using var kernelY = Mat.FromPixelData(3, 3, MatType.CV_32FC1, weightY);

        using var edgeX = image.Filter2D(-1, kernelX);
        using var edgeY = image.Filter2D(-1, kernelY);
        using var edgeXYMerged = edgeX + edgeY;

        Cv2.ImShow("Filter Edge X", edgeX);
        Cv2.ImShow("Filter Edge Y", edgeY);
        Cv2.ImShow("Filter Edge X+Y", edgeXYMerged);

        Cv2.ImWrite($"{_fileNamePrefix}filter-x.jpg", edgeX);
        Cv2.ImWrite($"{_fileNamePrefix}filter-y.jpg", edgeY);
        Cv2.ImWrite($"{_fileNamePrefix}filter-xy-merged.jpg", edgeXYMerged);
    }

    private void Sobel(Mat image)
    {
        // Sobel(depth, xorder, yorder, ...)
        using var sobelX = image.Sobel(-1, 1, 0);
        using var sobelY = image.Sobel(-1, 0, 1);
        using var sobelXYMerged = sobelX + sobelY;

        Cv2.ImShow("Sobel X", sobelX);
        Cv2.ImShow("Sobel Y", sobelY);
        Cv2.ImShow("Sobel X+Y", sobelXYMerged);

        Cv2.ImWrite($"{_fileNamePrefix}sobel-x.jpg", sobelX);
        Cv2.ImWrite($"{_fileNamePrefix}sobel-y.jpg", sobelY);
        Cv2.ImWrite($"{_fileNamePrefix}sobel-xy-merged.jpg", sobelXYMerged);
    }

    private void Scharr(Mat image)
    {
        // Scharr(depth, xorder, yorder, ...)
        using var scharrX = image.Scharr(-1, 1, 0);
        using var scharrY = image.Scharr(-1, 0, 1);
        using var scharrXYMerged = scharrX + scharrY;

        Cv2.ImShow("Scharr X", scharrX);
        Cv2.ImShow("Scharr Y", scharrY);
        Cv2.ImShow("Scharr X+Y", scharrXYMerged);

        Cv2.ImWrite($"{_fileNamePrefix}scharr-x.jpg", scharrX);
        Cv2.ImWrite($"{_fileNamePrefix}scharr-y.jpg", scharrY);
        Cv2.ImWrite($"{_fileNamePrefix}scharr-xy-merged.jpg", scharrXYMerged);
    }

    private void Laplacian(Mat image)
    {
        using var gaussian = image.GaussianBlur(new OpenCvSharp.Size(3, 3), 0);

        //아래 Laplacian() 호출의 ksize가 1인 경우, 다음 주석 과정과 동일함
        //float[] weight = [0,1,0,
        //                     1,-4,1,
        //                     0,1,0];

        //using var kernel = Mat.FromPixelData(3, 3, MatType.CV_32FC1, weight);

        //using var edge = gaussian.Filter2D(-1, kernel);

        //Cv2.ImShow("Laplacian Edge", edge);

        // Laplacian(depth, ksize, ...)
        using var laplacian = gaussian.Laplacian(-1, 3);

        Cv2.ImShow("Laplacian", laplacian);

        Cv2.ImWrite($"{_fileNamePrefix}laplacian.jpg", laplacian);
    }

    private void Canny(Mat image)
    {
        // Grayscale 변환
        // Color 이미지에도 canny edge detection이 적용 가능하지만, 흑백 이미지에서 더 잘 작동하는 것으로 알려져 있다.
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Canny(threshold_min, threshold_max, ...)
        // max값을 넘어서는 pixel은 바로 엣지로 취급
        // min ~ max 사이의 pixel은 확실한 엣지와 연결이 되어 있는 경우 엣지로 취급
        // min보다 낮은 pixel은 엣지에서 제외
        using var canny = grayscale.Canny(100, 200);

        Cv2.ImShow("Canny", canny);

        Cv2.ImWrite($"{_fileNamePrefix}canny.jpg", canny);
    }

    private void Gradient(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var binary = grayscale.Threshold(200, 255, ThresholdTypes.Binary);
        using var kernel = Mat.Ones(3, 3, MatType.CV_8UC1);

        using var transformed = binary.MorphologyEx(MorphTypes.Gradient, kernel);

        Cv2.ImShow("Gradient", transformed);

        Cv2.ImWrite($"{_fileNamePrefix}gradient.jpg", transformed);
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