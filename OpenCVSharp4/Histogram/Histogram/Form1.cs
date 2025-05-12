using OpenCvSharp;
using System.Diagnostics.Metrics;

namespace Histogram;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        GrayscaleHistogram(image);
        NormalizeHistogram(image);
        NormalizeColorHistogram(image);
        EqualizeHistogram(image);
        EqualizeColorHistogram(image);
        CLAHEHistogram(image);
        CLAHEColorHistogram(image);
    }

    private void GrayscaleHistogram(Mat image)
    {
        // Grayscale 변환
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var histogram = GetHistogram(grayscale);

        Cv2.ImShow(nameof(grayscale), grayscale);
        Cv2.ImShow(nameof(histogram), histogram);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-grayscale.jpg", grayscale);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-grayscale-histogram.jpg", histogram);
    }

    private void NormalizeHistogram(Mat image)
    {
        // 도수 분포에 최대, 최소값이 있는 경우에는 normalize 잘 수행되지 않음
        // Grayscale 변환
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Normalize
        using var normalized = grayscale.Normalize(0, 255, NormTypes.MinMax);
        using var normalizedHistogram = GetHistogram(normalized);

        Cv2.ImShow("Normalized", normalized);
        Cv2.ImShow("Normalized histogram", normalizedHistogram);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-normalized.jpg", normalized);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-normalized-histogram.jpg", normalizedHistogram);
    }

    private void NormalizeColorHistogram(Mat image)
    {
        using var hsvImage = image.CvtColor(ColorConversionCodes.BGR2HSV);

        // H, S, V 순으로 분리
        var split = hsvImage.Split();

        // Normalize
        using var valueChannel = split[2].Normalize(0, 255, NormTypes.MinMax);

        // Color image로 다시 합성
        using var merge = new Mat();
        Cv2.Merge([split[0], split[1], valueChannel], merge);
        using var normalized = merge.CvtColor(ColorConversionCodes.HSV2BGR);

        Cv2.ImShow("Value normalized", valueChannel);
        Cv2.ImShow("Color normalized", normalized);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-value-normalized.jpg", valueChannel);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-color-normalized.jpg", normalized);
    }

    private void EqualizeHistogram(Mat image)
    {
        // Grayscale 변환
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // 전역 histogram 평준화
        // Mat.EqualizeHist()
        using var equalized = grayscale.EqualizeHist();
        using var equalizedHistogram = GetHistogram(equalized);

        Cv2.ImShow("Equalized", equalized);
        Cv2.ImShow("Equalized histogram", equalizedHistogram);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-equalized.jpg", equalized);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-equalized-histogram.jpg", equalizedHistogram);
    }

    private void EqualizeColorHistogram(Mat image)
    {
        using var hsvImage = image.CvtColor(ColorConversionCodes.BGR2HSV);

        // H, S, V 순으로 분리
        var split = hsvImage.Split();

        // 밝기 채널에 대해 전역 histogram 평준화
        using var valueChannel = split[2].EqualizeHist();

        // Color image로 다시 합성
        using var equalized = new Mat();
        Cv2.Merge([split[0], split[1], valueChannel], equalized);
        using var result = equalized.CvtColor(ColorConversionCodes.HSV2BGR);

        Cv2.ImShow("Value equalized", valueChannel);
        Cv2.ImShow("Color equalized", result);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-value-equalized.jpg", valueChannel);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-color-equalized.jpg", result);
    }

    private void CLAHEHistogram(Mat image, double clipLimit = 4, OpenCvSharp.Size? gridSize = null)
    {
        // Grayscale 변환
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var result = new Mat();

        // 지역 histogram 평준화
        using var clahe = Cv2.CreateCLAHE(clipLimit, gridSize);
        clahe.Apply(grayscale, result);

        using var resultHistogram = GetHistogram(result);

        Cv2.ImShow("CLAHE", result);
        Cv2.ImShow("CLAHE histogram", resultHistogram);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-clahe.jpg", result);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-clahe-histogram.jpg", resultHistogram);
    }

    private void CLAHEColorHistogram(Mat image, double clipLimit = 4, OpenCvSharp.Size? gridSize = null)
    {
        using var hsvImage = image.CvtColor(ColorConversionCodes.BGR2HSV);

        // H, S, V 순으로 분리
        var split = hsvImage.Split();

        using var valueChannel = new Mat();

        // 밝기 채널에 대해 지역 histogram 평준화
        using var clahe = Cv2.CreateCLAHE(clipLimit, gridSize);
        clahe.Apply(split[2], valueChannel);

        // Color image로 합성
        using var equalized = new Mat();
        Cv2.Merge([split[0], split[1], valueChannel], equalized);
        using var result = equalized.CvtColor(ColorConversionCodes.HSV2BGR);

        Cv2.ImShow("CLAHE value equalized", valueChannel);
        Cv2.ImShow("CLAHE color", result);

        Cv2.ImWrite("opencvsharp-contrast-enhancement-clahe-value-equalized.jpg", valueChannel);
        Cv2.ImWrite("opencvsharp-contrast-enhancement-clahe-color-equalized.jpg", result);
    }

    private Mat GetHistogram(Mat grayscale)
    {
        using var histogram = new Mat();

        // Calculate histogram
        // Cv2.CalcHist(image, channel, mask, histogram, dims, bins, range)
        Cv2.CalcHist([grayscale], [0], null, histogram, 1, [256], [[0, 256]]);

        Mat histogramImage = Mat.Zeros(256, 256, MatType.CV_8UC3);

        // Normalize histogram's Y axis for display
        histogram.MinMaxLoc(out double min, out double max);

        if (max != 0)
        {
            Cv2.Multiply(histogram, 256 / max, histogram);
        }

        // Draw histogram
        for (int i = 0; i < 256; i++)
        {
            histogramImage.Rectangle(new OpenCvSharp.Point(i, histogramImage.Rows - (int)histogram.Get<float>(i)), new((i + 1), histogramImage.Rows), Scalar.White);
        }

        return histogramImage;
    }

    private Mat LoadImage()
    {
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
            return Cv2.ImRead(dialog.FileName);
        }

        return null!;
    }
}