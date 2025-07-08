using OpenCvSharp.Extensions;
using OpenCvSharp;
using System.Diagnostics;
using Tesseract;

// Tesseract 설치 : https://github.com/UB-Mannheim/tesseract/wiki
// kor.traineddata (설치 경로\tessdata\ 에 모아둠) : https://github.com/tesseract-ocr/tessdata/blob/main/kor.traineddata

namespace OCRWithTesseract;

public partial class Form1 : Form
{
    private const string _tesseractPath = @"C:\Program Files\Tesseract-OCR\tessdata";

    public Form1()
    {
        InitializeComponent();

        using var image = LoadImage();

        Perform(image, "eng");
        Perform(image, "kor");
        Perform(image, "kor+eng");
        PerformWithVisualize(image, "kor+eng");
    }

    private void Perform(Mat image, string languageCode)
    {
        // Preprocessing
        using var blur = image.CvtColor(ColorConversionCodes.BGR2GRAY)
                                    .Threshold(100, 255, ThresholdTypes.Binary)
                                    .GaussianBlur(new OpenCvSharp.Size(3, 3), -1);

        // _tesseractPath 폴더의 languageCode.traineddata 불러와 Tesseract OCR 엔진 초기화
        using var engine = new TesseractEngine(_tesseractPath, languageCode);

        // OCR 수행
        using var data = engine.Process(Pix.LoadFromMemory(blur.ToBytes()));

        // 결과 출력
        Debug.WriteLine("Start writing text");
        Debug.WriteLine($"Text :{Environment.NewLine} {data.GetText()}");
        Debug.WriteLine($"Confidence : {data.GetMeanConfidence()}");

        Cv2.ImShow("Blur", blur);
    }

    private void PerformWithVisualize(Mat image, string languageCode)
    {
        // Preprocessing
        using var blur = image.CvtColor(ColorConversionCodes.BGR2GRAY)
                                    .Threshold(100, 255, ThresholdTypes.Binary)
                                    .GaussianBlur(new OpenCvSharp.Size(3, 3), -1);

        // _tesseractPath 폴더의 languageCode.traineddata 불러와 Tesseract OCR 엔진 초기화
        using var engine = new TesseractEngine(_tesseractPath, languageCode);

        // OCR 수행
        using var data = engine.Process(Pix.LoadFromMemory(blur.ToBytes()));

        // 결과 표시
        var result = blur.CvtColor(ColorConversionCodes.GRAY2BGR);

        using var iterator = data.GetIterator();
        iterator.Begin();

        do
        {
            string word = iterator.GetText(PageIteratorLevel.Word);

            // 단어 영역의 박스 얻기
            if (iterator.TryGetBoundingBox(PageIteratorLevel.Word, out var bound))
            {
                // 문자 인식 영역 사각형 그리기
                result.Rectangle(new OpenCvSharp.Rect(bound.X1, bound.Y1, bound.Width, bound.Height), Scalar.MediumSeaGreen);

                // OpenCV는 utf-8 텍스트 지원이 되지 않기 때문에 직접 그림 (영문은 Cv2.PutText() 메서드 사용해도 무방)
                using var textTarget = result.ToBitmap();
                using var graphic = Graphics.FromImage(textTarget);
                graphic.DrawString(word, new Font("Consolas", 10), Brushes.Crimson, new PointF(bound.X1, bound.Y1 + bound.Height + 1));

                // 이전 객체 dispose 후 새로 할당
                result.Dispose();
                result = BitmapConverter.ToMat(textTarget);
            }
        }
        while (iterator.Next(PageIteratorLevel.Word));

        Cv2.ImShow("Result", result);
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