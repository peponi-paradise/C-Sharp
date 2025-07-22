using OpenCvSharp;

// Test video by ChristianBodhi : https://pixabay.com/videos/traffic-cars-highway-road-street-20581/

namespace BackgroundSubtraction;

public partial class Form1 : Form
{
    private string _fileNamePrefix = "opencvsharp-background-subtraction-";

    public Form1()
    {
        InitializeComponent();

        using var video = LoadVideo();

        if (video != null)
            Perform(video);
    }

    private void Perform(VideoCapture video)
    {
        // OpenCV 제공 window
        Window processed = new("Processed");
        Window result = new("Result");

        // BackgroundSubtractor 생성
        // history : 배경 모델링에 사용할 프레임 수 설정
        // dist2Threshold : 이웃 거리에 대한 임계값
        // detectShadows : 그림자 검출 여부 설정 (그림자는 전경과 구분 가능하게 출력됨)
        using var backgroundSubtractor = BackgroundSubtractorKNN.Create(100, 500);

        while (true)
        {
            using var frame = new Mat();
            video.Read(frame);

            // 동영상이 끝나면 frame.Empty 가 true로 바뀜
            if (frame.Empty())
                break;

            // 배경 분리
            using var backgroundRemoved = new Mat();
            backgroundSubtractor.Apply(frame, backgroundRemoved);

            // 그림자 제거
            using var shadowRemoved = backgroundRemoved.Threshold(200, 255, ThresholdTypes.Binary);

            // 객체 보존하면서 노이즈 제거
            using var kernel = Mat.Ones(3, 3, MatType.CV_8UC1);
            using var morphed = shadowRemoved.MorphologyEx(MorphTypes.Open, kernel);

            // 객체 찾기
            morphed.FindContours(out var contours, out var hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxTC89L1);

            // 면적을 기준으로 객체 필터링
            var filtered = contours.Where(contour => Cv2.ContourArea(contour) > 100);

            // 최종 객체 표시
            foreach (var contour in filtered)
            {
                var rect = Cv2.BoundingRect(contour);
                frame.Rectangle(rect, Scalar.Crimson, 3);
            }

            processed.ShowImage(morphed);
            result.ShowImage(frame);

            Cv2.ImWrite($"{_fileNamePrefix}background-removed.jpg", backgroundRemoved);
            Cv2.ImWrite($"{_fileNamePrefix}processed.jpg", morphed);
            Cv2.ImWrite($"{_fileNamePrefix}result.jpg", frame);

            Cv2.WaitKey(10);
        }
    }

    private VideoCapture? LoadVideo()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "All files|*.*",
            InitialDirectory = $@"C:\",
            CheckPathExists = true,
            CheckFileExists = true
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // 동영상 로드
            return new VideoCapture(dialog.FileName);
        }

        return null;
    }
}