using OpenCvSharp;
using System.Diagnostics;

namespace AffineTransformation;

public partial class Form1 : Form
{
    private const string _imagePrefix = "opencvsharp-affine-transformation-";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Translate(image);
        Rotate(image, Math.PI / 6);
        ReflectX(image);
        ReflectY(image);
        ReflectXY(image);
        Scale(image, 0.5f, 0.6f);
        Shear(image, 0.2f, 0.4f);
        PointToPoint(image);
    }

    private void Translate(Mat image)
    {
        // 단위행렬 생성 (1행 : [1, 0, 0],  2행 : [0, 1, 0])
        using Mat translation = Mat.Eye(2, 3, MatType.CV_32F);

        // Set(row, col, value)
        translation.Set<float>(0, 2, 150);  // 1행 : [1, 0, 150]
        translation.Set<float>(1, 2, 50);   // 2행 : [0, 1, 50]

        // Translate
        using var translated = new Mat();
        Cv2.WarpAffine(image, translated, translation, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Translated", translated);
        Cv2.ImWrite($"{_imagePrefix}translation.jpg", translated);
    }

    private void Rotate(Mat image, double angle)
    {
        // 빈 행렬 생성 (1행 : [0, 0, 0],  2행 : [0, 0, 0])
        using Mat rotation = Mat.Zeros(new OpenCvSharp.Size(3, 2), MatType.CV_32F);

        (OpenCvSharp.Size size, float tx, float ty) = ComputeSizeAndTranslation(image.Width, image.Height, angle);

        // Set(row, col, value)
        rotation.Set(0, 0, (float)Math.Cos(angle));      // 1행 : [cos(angle), 0, 0]
        rotation.Set(0, 1, -1 * (float)Math.Sin(angle));  // 1행 : [cos(angle), -sin(angle), 0]
        rotation.Set(0, 2, tx);                               // 1행 : [cos(angle), -sin(angle), tx]
        rotation.Set(1, 0, (float)Math.Sin(angle));  // 2행 : [sin(angle, 0, 0]
        rotation.Set(1, 1, (float)Math.Cos(angle));  // 2행 : [sin(angle, cos(angle), 0]
        rotation.Set(1, 2, ty);                          // 2행 : [sin(angle, cos(angle), ty]

        // Rotate
        using var rotated = new Mat();
        Cv2.WarpAffine(image, rotated, rotation, size);

        Cv2.ImShow("Rotated", rotated);
        Cv2.ImWrite($"{_imagePrefix}rotation.jpg", rotated);

        (OpenCvSharp.Size Size, float Tx, float Ty) ComputeSizeAndTranslation(int width, int height, double angle)
        {
            double tx = 0, ty = 0;

            // 각도 양수로 변경
            angle = angle >= 0 ? angle : Math.PI * 2 + angle;

            // Get quadrant constants
            (int sin, int cos) = angle switch
            {
                > Math.PI * 1.5 and <= Math.PI * 2 => (-1, 1),
                > Math.PI and <= Math.PI * 1.5 => (-1, -1),
                > Math.PI / 2 and <= Math.PI => (1, -1),
                _ => (1, 1)
            };

            // 최종 이미지 size 산출
            OpenCvSharp.Size size = new()
            {
                Width = (int)(cos * width * Math.Cos(angle) + sin * height * Math.Sin(angle)),
                Height = (int)(sin * width * Math.Sin(angle) + cos * height * Math.Cos(angle))
            };

            // ROI 바깥에 그려지는 영역 끌어오기 위한 tx, ty 계산
            switch (angle)
            {
                case > Math.PI * 1.5 and <= Math.PI * 2:
                    ty = -width * Math.Sin(angle);
                    break;

                case > Math.PI and <= Math.PI * 1.5:
                    tx = -width * Math.Cos(angle);
                    ty = size.Height;
                    break;

                case > Math.PI / 2 and <= Math.PI:
                    tx = size.Width;
                    ty = -height * Math.Cos(angle);
                    break;

                default:
                    tx = height * Math.Sin(angle);
                    break;
            }

            return (size, (float)tx, (float)ty);
        }
    }

    private void ReflectX(Mat image)
    {
        // 빈 행렬 생성 (1행 : [0, 0, 0],  2행 : [0, 0, 0])
        using Mat reflection = Mat.Zeros(new OpenCvSharp.Size(3, 2), MatType.CV_32F);

        // Set(row, col, value)
        reflection.Set<float>(0, 0, 1);  // 1행 : [1, 0, 0]
        reflection.Set<float>(1, 1, -1);  // 2행 : [0, -1, 0]
        reflection.Set<float>(1, 2, image.Height);  // 2행 : [0, -1, image.Height], ROI 바깥에 그려지는 영역 끌어옴

        // Reflect
        using var reflected = new Mat();
        Cv2.WarpAffine(image, reflected, reflection, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Reflected X", reflected);
        Cv2.ImWrite($"{_imagePrefix}reflection-x.jpg", reflected);
    }

    private void ReflectY(Mat image)
    {
        // 빈 행렬 생성 (1행 : [0, 0, 0],  2행 : [0, 0, 0])
        using Mat reflection = Mat.Zeros(new OpenCvSharp.Size(3, 2), MatType.CV_32F);

        // Set(row, col, value)
        reflection.Set<float>(0, 0, -1);  // 1행 : [-1, 0, 0]
        reflection.Set<float>(0, 2, image.Width);  // 1행 : [-1, 0, image.Width], ROI 바깥에 그려지는 영역 끌어옴
        reflection.Set<float>(1, 1, 1);  // 2행 : [0, 1, 0]

        // Reflect
        using var reflected = new Mat();
        Cv2.WarpAffine(image, reflected, reflection, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Reflected Y", reflected);
        Cv2.ImWrite($"{_imagePrefix}reflection-y.jpg", reflected);
    }

    private void ReflectXY(Mat image)
    {
        // 빈 행렬 생성 (1행 : [0, 0, 0],  2행 : [0, 0, 0])
        using Mat reflection = Mat.Zeros(new OpenCvSharp.Size(3, 2), MatType.CV_32F);

        // Set(row, col, value)
        reflection.Set<float>(0, 0, -1);  // 1행 : [-1, 0, 0]
        reflection.Set<float>(0, 2, image.Width);  // 1행 : [-1, 0, image.Width], ROI 바깥에 그려지는 영역 끌어옴
        reflection.Set<float>(1, 1, -1);  // 2행 : [0, -1, 0]
        reflection.Set<float>(1, 2, image.Height);  // 2행 : [0, -1, image.Height], ROI 바깥에 그려지는 영역 끌어옴

        // Reflect
        using var reflected = new Mat();
        Cv2.WarpAffine(image, reflected, reflection, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Reflected XY", reflected);
        Cv2.ImWrite($"{_imagePrefix}reflection-xy.jpg", reflected);
    }

    private void Scale(Mat image, float scaleX, float scaleY)
    {
        // 빈 행렬 생성 (1행 : [0, 0, 0],  2행 : [0, 0, 0])
        using Mat scale = Mat.Zeros(new OpenCvSharp.Size(3, 2), MatType.CV_32F);

        // Set(row, col, value)
        scale.Set(0, 0, scaleX);    // 1행 : [scaleX, 0, 0]
        scale.Set(1, 1, scaleY);    // 2행 : [0, scaleY, 0]

        // Scale
        using var scaled = new Mat();
        Cv2.WarpAffine(image, scaled, scale, new OpenCvSharp.Size(image.Width * scaleX, image.Height * scaleY));

        Cv2.ImShow("Scaled", scaled);
        Cv2.ImWrite($"{_imagePrefix}scale.jpg", scaled);
    }

    private void Shear(Mat image, float shearX, float shearY)
    {
        // 단위행렬 생성 (1행 : [1, 0, 0],  2행 : [0, 1, 0])
        using Mat shear = Mat.Eye(2, 3, MatType.CV_32F);

        // Set(row, col, value)
        shear.Set(0, 1, shearX);    // 1행 : [1, shearX, 0]
        if (shearX < 0)
        {
            shear.Set(0, 2, image.Height * Math.Abs(shearX));    // 1행 : [1, shearX, image.Height * Math.Abs(shearX)], ROI 바깥에 그려지는 영역 끌어옴
        }
        shear.Set(1, 0, shearY);    // 2행 : [shearY, 1, 0]
        if (shearY < 0)
        {
            shear.Set(1, 2, image.Width * Math.Abs(shearY));   // 2행 : [shearY, 1, image.Width * Math.Abs(shearY)], ROI 바깥에 그려지는 영역 끌어옴
        }

        var width = image.Width + (image.Height * Math.Abs(shearX));
        var height = image.Height + (image.Width * Math.Abs(shearY));

        // Shear
        using var sheared = new Mat();
        Cv2.WarpAffine(image, sheared, shear, new OpenCvSharp.Size(width, height));

        Cv2.ImShow("Sheared", sheared);
        Cv2.ImWrite($"{_imagePrefix}shear.jpg", sheared);
    }

    private void PointToPoint(Mat image)
    {
        // 각 collection의 순서에 맞춰 변환
        List<Point2f> select = [
            new(50,50),
            new(150,50),
            new(50,200)
            ];
        List<Point2f> destination = [
            new(150,150),
            new(200,200),
            new(100,250)
            ];

        // 이미지에 초기 좌표 표시
        select.ForEach(point => Cv2.Circle(image, new(point.X, point.Y), 5, Scalar.Crimson, -1));

        // 아핀 맵 행렬 생성
        using var affine = Cv2.GetAffineTransform(select, destination);

        // Transform
        using var affineTransformed = new Mat();
        Cv2.WarpAffine(image, affineTransformed, affine, new OpenCvSharp.Size(image.Width, image.Height));

        Cv2.ImShow("Affine transformed", affineTransformed);
        Cv2.ImWrite($"{_imagePrefix}affine-transform.jpg", affineTransformed);
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