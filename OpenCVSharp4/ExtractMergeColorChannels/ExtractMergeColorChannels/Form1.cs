using OpenCvSharp;

namespace ExtractMergeColorChannels;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        SplitBGR(image);
        SplitHSV(image);
        ExtractBGR(image);
        ExtractHSV(image);
        MergeHSVWithBR(image);
        RemoveGreenUsingBRMask(image);
    }

    private void SplitBGR(Mat image)
    {
        // B, G, R 순으로 할당
        var splitted = Cv2.Split(image);

        for (int i = 0; i < splitted.Length; i++)
        {
            Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-split-bgr-channel{i}.jpg", splitted[i]);
        }
    }

    private void SplitHSV(Mat image)
    {
        using var hsv = image.CvtColor(ColorConversionCodes.BGR2HSV);

        // H, S, V 순으로 할당
        Mat[] splitted = Cv2.Split(hsv);

        for (int i = 0; i < splitted.Length; i++)
        {
            Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-split-hsv-channel{i}.jpg", splitted[i]);
        }
    }

    private void ExtractBGR(Mat image)
    {
        using var blue = new Mat();
        using var green = new Mat();
        using var red = new Mat();

        // Cv2.InRange(input, lower bound, upper bound, output)
        Cv2.InRange(image, new(50, 0, 0), new(255, 160, 160), blue);
        Cv2.InRange(image, new(0, 50, 0), new(160, 255, 160), green);
        Cv2.InRange(image, new(0, 0, 50), new(160, 160, 255), red);

        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-extract-bgr-blue.jpg", blue);
        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-extract-bgr-green.jpg", green);
        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-extract-bgr-red.jpg", red);
    }

    private void ExtractHSV(Mat image)
    {
        // H : 0 ~ 180 (360도까지의 값을 2로 나누어 입력), S : 0 ~ 255 , V : 0 ~ 255
        // 색상 참조 : https://ko.rakko.tools/tools/30/
        using var hsv = image.CvtColor(ColorConversionCodes.BGR2HSV);

        using var blue = new Mat();
        using var green = new Mat();
        using var redLow = new Mat();
        using var redHigh = new Mat();

        Cv2.InRange(hsv, new(90, 50, 50), new(150, 255, 255), blue);
        Cv2.InRange(hsv, new(30, 50, 50), new(90, 255, 255), green);
        Cv2.InRange(hsv, new(0, 50, 50), new(30, 255, 255), redLow);
        Cv2.InRange(hsv, new(150, 50, 50), new(180, 255, 255), redHigh);
        using var red = redLow + redHigh;

        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-extract-hsv-blue.jpg", blue);
        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-extract-hsv-green.jpg", green);
        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-extract-hsv-red.jpg", red);
    }

    private void MergeHSVWithBR(Mat image)
    {
        // HSV 색상 공간으로 변경
        using var hsv = image.CvtColor(ColorConversionCodes.BGR2HSV);

        using var blue = new Mat();
        using var redLow = new Mat();
        using var redHigh = new Mat();

        Cv2.InRange(hsv, new(90, 50, 50), new(150, 255, 255), blue);
        Cv2.InRange(hsv, new(0, 50, 50), new(30, 255, 255), redLow);
        Cv2.InRange(hsv, new(150, 50, 50), new(180, 255, 255), redHigh);
        using var red = redLow + redHigh;

        using var merged = new Mat();

        // BGR 순으로 할당
        Cv2.Merge([blue, Mat.Zeros(image.Size(), MatType.CV_8UC1), red], merged);

        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-merge-hsv.jpg", merged);
    }

    private void RemoveGreenUsingBRMask(Mat image)
    {
        // HSV 색상 공간으로 변경
        using var hsv = image.CvtColor(ColorConversionCodes.BGR2HSV);

        using var blue = new Mat();
        using var redLow = new Mat();
        using var redHigh = new Mat();

        // Blue, red channel 추출 및 mask 생성
        Cv2.InRange(hsv, new(90, 50, 50), new(150, 255, 255), blue);
        Cv2.InRange(hsv, new(0, 50, 50), new(30, 255, 255), redLow);
        Cv2.InRange(hsv, new(150, 50, 50), new(180, 255, 255), redHigh);
        using var mask = blue + redLow + redHigh;

        // And 연산을 통해 mask의 검정 영역 제거
        using var greenRemoved = new Mat();
        Cv2.BitwiseAnd(image, image, greenRemoved, mask);

        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-br-mask.jpg", mask);
        Cv2.ImWrite($"opencvsharp-extract-merge-color-channels-remove-green.jpg", greenRemoved);
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