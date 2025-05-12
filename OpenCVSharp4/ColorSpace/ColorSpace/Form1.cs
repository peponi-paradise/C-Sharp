using OpenCvSharp;

namespace ColorSpace;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        ToGrayscale(image);
        ToRGB(image);
        ToHSV(image);
    }

    private void ToGrayscale(Mat image)
    {
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        Cv2.ImWrite("opencvsharp-change-color-space-grayscale.jpg", grayscale);
    }

    private void ToRGB(Mat image)
    {
        using var rgb = image.CvtColor(ColorConversionCodes.BGR2RGB);

        Cv2.ImWrite("opencvsharp-change-color-space-rgb.jpg", rgb);
    }

    private void ToHSV(Mat image)
    {
        using var hsv = image.CvtColor(ColorConversionCodes.BGR2HSV);

        Cv2.ImWrite("opencvsharp-change-color-space-hsv.jpg", hsv);
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