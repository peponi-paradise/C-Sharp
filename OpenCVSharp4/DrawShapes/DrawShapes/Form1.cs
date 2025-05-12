using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace DrawShapes;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        Draw();
    }

    private void Draw()
    {
        var background = new Bitmap(256, 256);

        using var graphic = Graphics.FromImage(background);

        // Draw background
        var brush = new SolidBrush(Color.White);
        graphic.FillRectangle(brush, 0, 0, 256, 256);

        // OpenCvSharp4 객체로 변환
        using var image = background.ToMat();

        // Draw line
        // Cv2.Line(image, start, end, color, ...)
        Cv2.Line(image, new(3, 3), new(53, 53), Scalar.MediumPurple);

        // Draw rectangle, 내부를 채우려면 두께를 음수로 지정
        // Cv2.Rectangle(image, leftTop, rightBottom, color, ...)
        Cv2.Rectangle(image, new(78, 3), new(128, 53), Scalar.MediumSeaGreen);

        // Draw circle, 내부를 채우려면 두께를 음수로 지정
        // Cv2.Circle(image, center, radius, color, ...)
        Cv2.Circle(image, new(178, 28), 25, Scalar.Crimson);

        // Draw ellipse, 내부를 채우려면 두께를 음수로 지정
        // Cv2.Ellipse(image, center, axes, rotateAngle, startAngle, endAngle, color, lineThickness, ...)
        Cv2.Ellipse(image, new(28, 103), new(25, 10), 45, 0, 360, Scalar.Brown, -1);

        var trianglePoints = new List<List<OpenCvSharp.Point>>()
        {
            new()
            {
                new (103, 78),
                new (78, 128),
                new (128, 128)
            }
        };
        // Draw poly (triangle example), 닫힌 곡선으로 설정하면 삼각형이 된다.
        // Cv2.Polylines(image, points, isClosed, color, ...)
        Cv2.Polylines(image, trianglePoints, true, Scalar.Black);

        var trianglePoints2 = new List<List<OpenCvSharp.Point>>()
        {
            new()
            {
                new (178, 78),
                new (153, 128),
                new (203, 128)
            }
        };
        // Draw filled poly, 사용법은 Cv2.Polylines()와 유사
        // Cv2.FillPoly(image, points, color, ...)
        Cv2.FillPoly(image, trianglePoints2, Scalar.DarkOrange);

        // Draw border
        // Mat.CopyMakeBorder(top, bottom, left, right, borderType, color)
        using var border = image.CopyMakeBorder(4, 4, 4, 4, BorderTypes.Constant, Scalar.SkyBlue);

        // 결과 이미지 출력
        Cv2.ImShow("Result", border);
    }
}