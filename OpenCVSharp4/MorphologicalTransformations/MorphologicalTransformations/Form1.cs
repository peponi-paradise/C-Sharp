using OpenCvSharp;

// image : https://pixabay.com/vectors/coffee-beans-love-typography-7175199/

namespace MorphologicalTransformations;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var grayscale = LoadImage();
        using var image = grayscale.Threshold(40, 255, ThresholdTypes.Binary);
        //using var image = grayscale;
        MakeNoise(image);

        Cv2.ImShow("Original", image);
        Cv2.ImWrite("opencvsharp-morphological-transformation-original.jpg", image);

        Erosion(image);
        Dilation(image);
        MorphologyEx(image, MorphTypes.Open);
        MorphologyEx(image, MorphTypes.Close);
        MorphologyEx(image, MorphTypes.Gradient);
        MorphologyEx(image, MorphTypes.TopHat);
        MorphologyEx(image, MorphTypes.BlackHat);
    }

    private void Erosion(Mat image)
    {
        using var kernel = Mat.Ones(3, 3, MatType.CV_8UC1);

        using var erode = image.Erode(kernel, iterations: 2);

        // 또는 다음 메서드 사용

        // using var erode = image.MorphologyEx(MorphTypes.Erode, kernel);

        Cv2.ImShow("Erosion", erode);
        Cv2.ImWrite("opencvsharp-morphological-transformation-erosion.jpg", erode);
    }

    private void Dilation(Mat image)
    {
        using var kernel = Mat.Ones(3, 3, MatType.CV_8UC1);

        using var dilate = image.Dilate(kernel, iterations: 2);

        // 또는 다음 메서드 사용

        // using var dilate = image.MorphologyEx(MorphTypes.Dilate, kernel);

        Cv2.ImShow("Dilation", dilate);
        Cv2.ImWrite("opencvsharp-morphological-transformation-dilation.jpg", dilate);
    }

    private void MorphologyEx(Mat image, MorphTypes type)
    {
        using var kernel = Mat.Ones(3, 3, MatType.CV_8UC1);

        using var transformed = image.MorphologyEx(type, kernel, iterations: 2);

        Cv2.ImShow($"{type}", transformed);
        Cv2.ImWrite($"opencvsharp-morphological-transformation-{type.ToString().ToLower()}.jpg", transformed);
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
            return Cv2.ImRead(dialog.FileName, ImreadModes.Grayscale);
        }

        return null!;
    }

    private void MakeNoise(Mat image)
    {
        for (int i = 0; i < image.Height; i++)
        {
            for (int j = 0; j < image.Width; j++)
            {
                var rand = Random.Shared.Next(0, 1000);
                if (rand < 1)
                    image.Set(i, j, 255);
            }
        }
    }
}