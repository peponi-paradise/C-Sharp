using OpenCvSharp;

namespace TranslateRotateImage;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Translate(image);
        Rotate(image);
        RotateWithAngle(image);
    }

    private void Translate(Mat image)
    {
        // Translation matrix
        using Mat translation = Mat.Eye(2, 3, MatType.CV_32F); // 단위행렬 생성 (1행 : [1, 0, 0],  2행 : [0, 1, 0])
        // Set(row, col, value)
        translation.Set<float>(0, 2, 150);  // 1행 : [1, 0, 150]
        translation.Set<float>(1, 2, 50);   // 2행 : [0, 1, 50]

        // Translate
        using var result = new Mat();
        Cv2.WarpAffine(image, result, translation, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Translate", result);
    }

    private void Rotate(Mat image)
    {
        using var result = new Mat();
        Cv2.Rotate(image, result, RotateFlags.Rotate90Clockwise);

        Cv2.ImShow("Rotate", result);
    }

    private void RotateWithAngle(Mat image)
    {
        // GetRotationMatrix2D(center, angle, scale)
        using var rotation = Cv2.GetRotationMatrix2D(new Point2f(image.Width / 2, image.Height / 2), 60, 1);

        // Rotate
        using var result = new Mat();
        Cv2.WarpAffine(image, result, rotation, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("RotateAngle", result);
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