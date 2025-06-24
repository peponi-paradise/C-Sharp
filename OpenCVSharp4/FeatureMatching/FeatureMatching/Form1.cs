// image 1 : https://pixabay.com/ko/photos/%EB%AA%A8%EB%82%98%EB%A6%AC%EC%9E%90-%ED%8E%98%EC%9D%B8%ED%8A%B8-%EB%93%B1-%EB%AF%B8%EC%88%A0-67506/
// image 2 : https://pixabay.com/ko/photos/%EC%98%81%EC%83%81-%EA%B7%B8%EB%A6%BC-%EB%AF%B8%EC%88%A0%EA%B4%80-%ED%99%94%EA%B0%80-1053852/

using OpenCvSharp;

namespace FeatureMatching;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image1 = LoadImage();
        using var image2 = LoadImage();

        BFMatching(image1, image2);
        FLANNMatching(image1, image2);
    }

    private void BFMatching(Mat image1, Mat image2)
    {
        using var kaze = KAZE.Create();
        using var descriptor1 = new Mat();
        using var descriptor2 = new Mat();

        kaze.DetectAndCompute(image1, null, out var keyPoints1, descriptor1);
        kaze.DetectAndCompute(image2, null, out var keyPoints2, descriptor2);

        using var matcher = new BFMatcher();

        var matches = matcher.KnnMatch(descriptor1, descriptor2, 2);

        var matched = new List<DMatch>();

        foreach (var match in matches)
        {
            if (match.Length > 1 && match[0].Distance < 0.8 * match[1].Distance)
                matched.Add(match[0]);
        }

        using var detected = new Mat();

        Cv2.DrawMatches(image1, keyPoints1, image2, keyPoints2, matched, detected, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);

        Cv2.ImShow("BFMatching", detected);
    }

    private void FLANNMatching(Mat image1, Mat image2)
    {
        using var kaze = KAZE.Create();
        using var descriptor1 = new Mat();
        using var descriptor2 = new Mat();

        kaze.DetectAndCompute(image1, null, out var keyPoints1, descriptor1);
        kaze.DetectAndCompute(image2, null, out var keyPoints2, descriptor2);

        var indexParams = new OpenCvSharp.Flann.KDTreeIndexParams(5);
        var searchParams = new OpenCvSharp.Flann.SearchParams(50);

        using var matcher = new FlannBasedMatcher(indexParams, searchParams);

        var matches = matcher.KnnMatch(descriptor1, descriptor2, 2);

        var matched = new List<DMatch>();

        foreach (var match in matches)
        {
            if (match.Length > 1 && match[0].Distance < 0.8 * match[1].Distance)
                matched.Add(match[0]);
        }

        using var detected = new Mat();

        Cv2.DrawMatches(image1, keyPoints1, image2, keyPoints2, matched, detected, Scalar.All(-1), Scalar.All(-1), null, DrawMatchesFlags.NotDrawSinglePoints);

        Cv2.ImShow("FLANNMatching", detected);
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