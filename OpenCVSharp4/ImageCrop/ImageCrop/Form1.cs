using OpenCvSharp;

namespace ImageCrop;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        SubMat(image);
        Indexer(image);
        Ctor(image);

        image.Rectangle(new Rect(200, 100, 300, 150), Scalar.Crimson);
        Cv2.ImWrite("opencvsharp-crop-image-roi.jpg", image);
    }

    private void SubMat(Mat image)
    {
        using var cropped = image.SubMat(new Rect(200, 100, 300, 150));
        Cv2.ImWrite("opencvsharp-crop-image-submat.jpg", cropped);
    }

    private void Indexer(Mat image)
    {
        // Mat[rowStart, rowEnd, colStart, colEnd]
        using var cropped = image[100, 250, 200, 500];
        Cv2.ImWrite("opencvsharp-crop-image-indexer.jpg", cropped);
    }

    private void Ctor(Mat image)
    {
        using var cropped = new Mat(image, new Rect(200, 100, 300, 150));
        Cv2.ImWrite("opencvsharp-crop-image-ctor.jpg", cropped);
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