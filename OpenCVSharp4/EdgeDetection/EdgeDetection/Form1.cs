using OpenCvSharp;

// imge : https://pixabay.com/photos/letter-airmail-envelope-stamp-1255872/

namespace EdgeDetection;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        Filter(image);
        Sobel(image);
        Scharr(image);
        Laplacian(image);
        Canny(image);
        Gradient(image);
    }

    private void Filter(Mat image)
    {
        // Sobel example
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

        Cv2.ImShow("Filter Edge X", edgeX);
        Cv2.ImShow("Filter Edge Y", edgeY);
        Cv2.ImShow("Filter Edge", edgeX + edgeY);
    }

    private void Sobel(Mat image)
    {
        using var sobelX = image.Sobel(-1, 1, 0);
        using var sobelY = image.Sobel(-1, 0, 1);
        using var sobelXY = image.Sobel(-1, 1, 1);

        Cv2.ImShow("Sobel X", sobelX);
        Cv2.ImShow("Sobel Y", sobelY);
        Cv2.ImShow("Sobel X+Y", sobelX + sobelY);
        Cv2.ImShow("Sobel XY", sobelXY);
    }

    private void Scharr(Mat image)
    {
        using var scharrX = image.Scharr(-1, 1, 0);
        using var scharrY = image.Scharr(-1, 0, 1);

        Cv2.ImShow("Scharr X", scharrX);
        Cv2.ImShow("Scharr Y", scharrY);
        Cv2.ImShow("Scharr X+Y", scharrX + scharrY);
    }

    private void Laplacian(Mat image)
    {
        //아래 Laplacian 호출은 다음 주석 과정과 동일함
        //float[] weight = [0,1,0,
        //                     1,-4,1,
        //                     0,1,0];

        //using var kernel = Mat.FromPixelData(3, 3, MatType.CV_32FC1, weight);

        //using var edge = image.Filter2D(-1, kernel);

        //Cv2.ImShow("Laplacian Edge", edge);

        using var laplacian = image.Laplacian(-1);

        Cv2.ImShow("Laplacian", laplacian);
    }

    private void Canny(Mat image)
    {
        using var canny = image.Canny(100, 200);

        Cv2.ImShow("Canny", canny);
    }

    private void Gradient(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var binary = grayscale.Threshold(200, 255, ThresholdTypes.Binary);
        using var kernel = Mat.Ones(3, 3, MatType.CV_8UC1);

        using var transformed = binary.MorphologyEx(MorphTypes.Gradient, kernel);

        Cv2.ImShow("Gradient", transformed);
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