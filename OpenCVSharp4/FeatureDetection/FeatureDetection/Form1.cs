using OpenCvSharp;
using OpenCvSharp.Features2D;
using OpenCvSharp.XFeatures2D;

// image 1 : https://pixabay.com/ko/photos/%EB%AA%A8%EB%82%98%EB%A6%AC%EC%9E%90-%ED%8E%98%EC%9D%B8%ED%8A%B8-%EB%93%B1-%EB%AF%B8%EC%88%A0-67506/
// image 2 (나중에 matching용으로 사용해보자) : https://pixabay.com/ko/photos/%EC%98%81%EC%83%81-%EA%B7%B8%EB%A6%BC-%EB%AF%B8%EC%88%A0%EA%B4%80-%ED%99%94%EA%B0%80-1053852/

namespace FeatureDetection;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-feature-detection-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        SIFTDetection(image);
        SURFDetection(image);
        ORBDetection(image);
        KAZEDetection(image);
        AKAZEDetection(image);
        BRISKDetection(image);
    }

    private void SIFTDetection(Mat image)
    {
        using var sift = SIFT.Create();

        var keyPoints = sift.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("SIFT", detected);
    }

    private void SURFDetection(Mat image)
    {
        using var surf = SURF.Create(200);

        var keyPoints = surf.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("SURF", detected);
    }

    private void ORBDetection(Mat image)
    {
        using var orb = ORB.Create(1000);

        var keyPoints = orb.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("ORB", detected);
    }

    private void KAZEDetection(Mat image)
    {
        using var kaze = KAZE.Create();

        var keyPoints = kaze.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("KAZE", detected);
    }

    private void AKAZEDetection(Mat image)
    {
        using var akaze = AKAZE.Create();

        var keyPoints = akaze.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("AKAZE", detected);
    }

    private void BRISKDetection(Mat image)
    {
        using var brisk = BRISK.Create();

        var keyPoints = brisk.Detect(image);

        using var detected = new Mat();

        Cv2.DrawKeypoints(image, keyPoints, detected, Scalar.DeepSkyBlue);

        Cv2.ImShow("BRISK", detected);
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