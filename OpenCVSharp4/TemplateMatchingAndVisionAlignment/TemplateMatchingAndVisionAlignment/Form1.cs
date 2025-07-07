using OpenCvSharp;

// Image by Rudy and Peter Skitterians : https://pixabay.com/ko/photos/%ED%83%84%EC%95%BD-%EB%B0%9C%EC%82%AC-%EC%B4%9D%EC%95%8C-%EA%B5%B0%EB%8C%80-%EC%B4%9D-2428627/

namespace TemplateMatchingAndVisionAlignment;

public partial class Form1 : Form
{
    private const string _imagePrefix = "opencvsharp-template-matching-";

    public Form1()
    {
        InitializeComponent();

        var original = LoadImage();
        var rotated = LoadImage();
        var template = LoadImage();

        Match(original, template);
        MatchWithTemplateRotation(rotated, template);
        ExecuteAlignment(rotated, template);
    }

    private void Match(Mat image, Mat template)
    {
        using var thresholdImage = image.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(170, 255, ThresholdTypes.BinaryInv);
        using var thresholdTemplate = template.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(170, 255, ThresholdTypes.BinaryInv);

        using var result = thresholdImage.MatchTemplate(thresholdTemplate, TemplateMatchModes.CCoeffNormed);

        // maxValue, maxLocation에 가장 잘 매칭된 경우의 값과 지점이 저장됨
        result.MinMaxLoc(out var minValue, out var maxValue, out var minLocation, out var maxLocation);

        using var matched = image.Clone();

        matched.Rectangle(maxLocation, new OpenCvSharp.Point(maxLocation.X + template.Width, maxLocation.Y + template.Height), Scalar.Crimson, 2);

        Cv2.ImShow("Matched", matched);
        Cv2.ImWrite($"{_imagePrefix}matched.jpg", matched);
    }

    private void MatchWithTemplateRotation(Mat image, Mat template)
    {
        int rotateAngle = 0;

        using var thresholdImage = image.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(170, 255, ThresholdTypes.BinaryInv);
        using var thresholdTemplate = template.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(170, 255, ThresholdTypes.BinaryInv);

        while (true)
        {
            // 템플릿 이미지를 돌려가며 매칭 수행
            using var matchingTemplate = Rotate(thresholdTemplate, rotateAngle * Math.PI / 180);

            using var result = thresholdImage.MatchTemplate(matchingTemplate, TemplateMatchModes.CCoeffNormed);

            // maxValue를 이용해 얼마나 잘 매칭되었는지 확인
            result.MinMaxLoc(out var minValue, out var maxValue, out var minLocation, out var maxLocation);

            // 0.7 이상의 정확도를 가질 때 매칭된 것으로 판정
            if (maxValue > 0.7 && maxValue < 1)
            {
                using var matched = image.Clone();

                matched.Rectangle(maxLocation, new OpenCvSharp.Point(maxLocation.X + matchingTemplate.Width, maxLocation.Y + matchingTemplate.Height), Scalar.Crimson, 2);

                Cv2.ImShow("Matched with rotation", matched);
                Cv2.ImShow("Template with rotation", matchingTemplate);

                Cv2.ImWrite($"{_imagePrefix}matched-with-rotation.jpg", matched);
                Cv2.ImWrite($"{_imagePrefix}matched-with-rotation-template.jpg", matchingTemplate);

                break;
            }
            else
            {
                // 매칭이 잘 되지 않는 구간은 회전을 크게 돌려 탐색 속도를 빠르게 함
                rotateAngle += maxValue switch
                {
                    >= 30 and < 50 => 3,
                    >= 50 and < 60 => 2,
                    >= 60 => 1,
                    _ => 5
                };

                if (rotateAngle > 360)
                    break;
            }
        }
    }

    private void ExecuteAlignment(Mat image, Mat template)
    {
        int rotateAngle = 0;

        using var thresholdImage = image.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(170, 255, ThresholdTypes.BinaryInv);
        using var thresholdTemplate = template.CvtColor(ColorConversionCodes.BGR2GRAY).Threshold(170, 255, ThresholdTypes.BinaryInv);

        while (true)
        {
            // Matching 이미지를 돌려가며 매칭 수행
            using var matchingImage = Rotate(thresholdImage, rotateAngle * Math.PI / 180);

            using var result = matchingImage.MatchTemplate(thresholdTemplate, TemplateMatchModes.CCoeffNormed);

            // maxValue를 이용해 얼마나 잘 매칭되었는지 확인
            result.MinMaxLoc(out var minValue, out var maxValue, out var minLocation, out var maxLocation);

            // 0.7 이상의 정확도를 가질 때 매칭된 것으로 판정
            if (maxValue > 0.7 && maxValue < 1)
            {
                // 찾은 총알 표시
                using var matched = Rotate(image, rotateAngle * Math.PI / 180);
                matched.Rectangle(maxLocation, new OpenCvSharp.Point(maxLocation.X + thresholdTemplate.Width, maxLocation.Y + thresholdTemplate.Height), Scalar.Crimson, 2);

                // 단위행렬 생성 (1행 : [1, 0, 0],  2행 : [0, 1, 0])
                using Mat translation = Mat.Eye(2, 3, MatType.CV_32F);

                // 찾은 총알을 화면 중앙으로 가져오기 위해 center 계산
                float centerX = -1 * maxLocation.X + matched.Width / 2 - thresholdTemplate.Width / 2;
                float centerY = -1 * maxLocation.Y + matched.Height / 2 - thresholdTemplate.Height / 2;
                translation.Set(0, 2, centerX);
                translation.Set(1, 2, centerY);

                using var translated = new Mat();

                // 화면 중앙으로 찾은 총알 가져오기
                Cv2.WarpAffine(matched, translated, translation, new OpenCvSharp.Size(matched.Width, matched.Height));

                Cv2.ImShow("Matched with alignment", translated);
                Cv2.ImWrite($"{_imagePrefix}matched-with-alignment.jpg", translated);

                break;
            }
            else
            {
                // 매칭이 잘 되지 않는 구간은 회전을 크게 돌려 탐색 속도를 빠르게 함
                rotateAngle += maxValue switch
                {
                    >= 30 and < 50 => 3,
                    >= 50 and < 60 => 2,
                    >= 60 => 1,
                    _ => 5
                };

                if (rotateAngle > 360)
                    break;
            }
        }
    }

    private Mat Rotate(Mat image, double angle)
    {
        // 빈 행렬 생성 (1행 : [0, 0, 0],  2행 : [0, 0, 0])
        using Mat rotation = Mat.Zeros(new OpenCvSharp.Size(3, 2), MatType.CV_32F);

        (OpenCvSharp.Size size, float tx, float ty) = ComputeSizeAndTranslation(image.Width, image.Height, angle);

        // Set(row, col, value)
        rotation.Set(0, 0, (float)Math.Cos(angle));       // 1행 : [cos(angle), 0, 0]
        rotation.Set(0, 1, -1 * (float)Math.Sin(angle));  // 1행 : [cos(angle), -sin(angle), 0]
        rotation.Set(0, 2, tx);                                    // 1행 : [cos(angle), -sin(angle), tx]
        rotation.Set(1, 0, (float)Math.Sin(angle));       // 2행 : [sin(angle, 0, 0]
        rotation.Set(1, 1, (float)Math.Cos(angle));      // 2행 : [sin(angle, cos(angle), 0]
        rotation.Set(1, 2, ty);                                   // 2행 : [sin(angle, cos(angle), ty]

        // Rotate
        var rotated = new Mat();
        Cv2.WarpAffine(image, rotated, rotation, size);

        return rotated;

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

    private Mat LoadImage()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "All files|*.*",
            CheckPathExists = true,
            CheckFileExists = true,
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return Cv2.ImRead(dialog.FileName);
        }

        return null!;
    }
}