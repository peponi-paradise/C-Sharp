using OpenCvSharp;

// image : https://pixabay.com/photos/traffic-road-pedestrian-autumn-7568718/

namespace ContourDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-contour-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        FindContours(image);
        FindContoursWithCanny(image);
    }

    private void FindContours(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var threshold = grayscale.Threshold(-1, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

        // Contour detection
        threshold.FindContours(out var contours, out var hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxTC89KCOS);

        using var contoursImage = image.Clone();

        contoursImage.DrawContours(contours, -1, Scalar.LightGreen);

        Cv2.ImShow("Threshold", threshold);
        Cv2.ImShow("Contours", contoursImage);

        Cv2.ImWrite($"{_fileNamePrefix}threshold.jpg", threshold);
        Cv2.ImWrite($"{_fileNamePrefix}contours.jpg", contoursImage);
    }

    private void FindContoursWithCanny(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var canny = grayscale.Canny(100, 200);

        // Contour detection
        canny.FindContours(out var contours, out var hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxTC89KCOS);

        using var contoursImage = image.Clone();

        contoursImage.DrawContours(contours, -1, Scalar.LightGreen);

        Cv2.ImShow("Canny", canny);
        Cv2.ImShow("Contours with Canny", contoursImage);

        Cv2.ImWrite($"{_fileNamePrefix}canny.jpg", canny);
        Cv2.ImWrite($"{_fileNamePrefix}contours-with-canny.jpg", contoursImage);
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