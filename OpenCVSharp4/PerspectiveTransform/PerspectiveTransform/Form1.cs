// https://pixabay.com/photos/railway-travel-adventure-rail-6914829/

using OpenCvSharp;

namespace PerspectiveTransform;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        PerspectiveTransform(image);
    }

    private void PerspectiveTransform(Mat image)
    {
        // 각 collection의 순서에 맞춰 변환
        List<Point2f> origin = [
            new(180, 470),
            new(294, 470),
            new(90, 625),
            new(387, 625)
            ];
        List<Point2f> destination = [
            new(0, 0),
            new(480, 0),
            new(0, 640),
            new(480, 640)
            ];

        // 이미지에 초기 좌표 표시
        origin.ForEach(point => image.Circle((int)point.X, (int)point.Y, 8, Scalar.LightGreen, -1));

        // 원근 변환 행렬 생성
        using var perspectiveTransformation = Cv2.GetPerspectiveTransform(origin, destination);

        // Transform
        using var perspectiveTransformed = new Mat();
        Cv2.WarpPerspective(image, perspectiveTransformed, perspectiveTransformation, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Original", image);
        Cv2.ImShow("Transformed", perspectiveTransformed);

        Cv2.ImWrite("opencvsharp-perspective-transformation-original.jpg", image);
        Cv2.ImWrite("opencvsharp-perspective-transformation-transformed.jpg", perspectiveTransformed);
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