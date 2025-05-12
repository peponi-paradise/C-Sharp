using OpenCvSharp;
using OpenCvSharp.XImgProc;

namespace Thresholding;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Thresholding(image);
        AdaptiveThresholding(image);
    }

    private void Thresholding(Mat image)
    {
        // Mat.Threshold(threshold, maxval, thresholdType)
        using var binary = image.Threshold(127, 255, ThresholdTypes.Binary);
        using var binaryInv = image.Threshold(127, 255, ThresholdTypes.BinaryInv);

        // 이하는 maxval을 설정할 필요 없음 (구동 상 의미 없는 파라미터)
        using var trunc = image.Threshold(127, 0, ThresholdTypes.Trunc);
        using var toZero = image.Threshold(127, 0, ThresholdTypes.Tozero);
        using var toZeroInv = image.Threshold(127, 0, ThresholdTypes.TozeroInv);

        // Mask는 모든 픽셀의 값을 0으로 만든다
        using var mask = image.Threshold(0, 0, ThresholdTypes.Mask);

        // Otsu, Triangle thresholding은 8bit 단일 채널 image에 대해서만 가능
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Otsu, Triangle 사용 시 threshold 넣어줄 필요 없음 (최적값 자동으로 찾아줌)
        using var otsu = grayscale.Threshold(0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
        using var triangle = grayscale.Threshold(0, 255, ThresholdTypes.Binary | ThresholdTypes.Triangle);

        Cv2.ImWrite("opencvsharp-thresholding-threshold-binary.jpg", binary);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-binaryinv.jpg", binaryInv);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-trunc.jpg", trunc);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-tozero.jpg", toZero);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-tozeroinv.jpg", toZeroInv);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-mask.jpg", mask);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-otsu.jpg", otsu);
        Cv2.ImWrite("opencvsharp-thresholding-threshold-triangle.jpg", triangle);
    }

    private void AdaptiveThresholding(Mat image)
    {
        // AdaptiveThresholding은 grayscale image에 대해서만 가능
        using var grayscale = image.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Mat.AdaptiveThreshold(maxval, adaptiveThresholdType, thresholdType, blockSize, constantToSubtract)
        // ThresholdTypes은 Binary, BinaryInv만 지원
        using var meanC = grayscale.AdaptiveThreshold(255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 5, 1);
        using var gaussianC = grayscale.AdaptiveThreshold(255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 5, 1);

        Cv2.ImWrite("opencvsharp-thresholding-adaptive-threshold-meanc.jpg", meanC);
        Cv2.ImWrite("opencvsharp-thresholding-adaptive-threshold-gaussianc.jpg", gaussianC);

        using var niblack = new Mat();
        using var sauvola = new Mat();
        using var wolf = new Mat();
        using var nick = new Mat();

        CvXImgProc.NiblackThreshold(grayscale, niblack, 255, ThresholdTypes.Binary, 5, 0.3);
        CvXImgProc.NiblackThreshold(grayscale, sauvola, 255, ThresholdTypes.Binary, 5, 0.3, LocalBinarizationMethods.Sauvola);
        CvXImgProc.NiblackThreshold(grayscale, wolf, 255, ThresholdTypes.Binary, 5, 0.3, LocalBinarizationMethods.Wolf);
        CvXImgProc.NiblackThreshold(grayscale, nick, 255, ThresholdTypes.Binary, 5, 0.3, LocalBinarizationMethods.Nick);

        Cv2.ImWrite("opencvsharp-thresholding-niblack-threshold-niblack.jpg", niblack);
        Cv2.ImWrite("opencvsharp-thresholding-niblack-threshold-sauvola.jpg", sauvola);
        Cv2.ImWrite("opencvsharp-thresholding-niblack-threshold-wolf.jpg", wolf);
        Cv2.ImWrite("opencvsharp-thresholding-niblack-threshold-nick.jpg", nick);
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