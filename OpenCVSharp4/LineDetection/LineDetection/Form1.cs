using OpenCvSharp;

namespace LineDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-line-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Cv2.ImShow("Original", image);

        HoughLines(image);
        HoughLinesP(image);
        LineSegmentDetector(image);
    }

    private void HoughLines(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var canny = grayscale.Canny(250, 500);

        var houghLines = canny.HoughLines(1, Math.PI / 180 / 4, 150);

        if (houghLines is null)
        {
            MessageBox.Show("Not found");
            return;
        }

        using var detected = image.Clone();

        foreach (var line in houghLines)
        {
            var x0 = Math.Cos(line.Theta) * line.Rho;
            var y0 = Math.Sin(line.Theta) * line.Rho;

            var point1 = new OpenCvSharp.Point(Math.Round(x0 + 1000 * (-1 * Math.Sin(line.Theta))), Math.Round(y0 + 1000 * Math.Cos(line.Theta)));
            var point2 = new OpenCvSharp.Point(Math.Round(x0 - 1000 * (-1 * Math.Sin(line.Theta))), Math.Round(y0 - 1000 * Math.Cos(line.Theta)));

            detected.Line(point1, point2, Scalar.Red);
        }

        Cv2.ImShow("Canny edge", canny);
        Cv2.ImShow("HoughLines detected", detected);

        Cv2.ImWrite($"{_fileNamePrefix}canny.jpg", canny);
        Cv2.ImWrite($"{_fileNamePrefix}houghlines.jpg", detected);
    }

    private void HoughLinesP(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);
        using var canny = grayscale.Canny(250, 500);

        var houghLinesP = canny.HoughLinesP(1, Math.PI / 180 / 4, 50, 5, 1);

        if (houghLinesP is null)
        {
            MessageBox.Show("Not found");
            return;
        }

        using var detected = image.Clone();

        foreach (var line in houghLinesP)
        {
            detected.Line(line.P1, line.P2, Scalar.Red);
        }

        Cv2.ImShow("HoughLinesP detected", detected);
        Cv2.ImWrite($"{_fileNamePrefix}houghlinesp.jpg", detected);
    }

    private void LineSegmentDetector(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        using var detector = OpenCvSharp.LineSegmentDetector.Create(LineSegmentDetectorModes.RefineAdv, quant: 3, angTh: 10, densityTh: 0.8);

        detector.Detect(grayscale, out var lsdLines, out var widths, out var precisions, out var nfas);

        if (lsdLines is null)
        {
            MessageBox.Show("Not found");
            return;
        }

        using var detected = image.Clone();

        detector.DrawSegments(detected, Mat.FromArray(lsdLines));

        Cv2.ImShow("LSD detected", detected);
        Cv2.ImWrite($"{_fileNamePrefix}lsd.jpg", detected);
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