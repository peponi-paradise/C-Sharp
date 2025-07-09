using OpenCvSharp;

// Video by Сергей Новокрещенных : https://pixabay.com/videos/nha-trang-vietnam-nature-summer-34552/
// Video by Luzivan De Jesus Moreno Vieira : https://pixabay.com/videos/kitten-animal-cat-265819/
// haarcascades data : https://github.com/opencv/opencv/blob/master/data/haarcascades/haarcascade_frontalface_alt2.xml
// haarcascades data (cat) : https://github.com/opencv/opencv/blob/master/data/haarcascades/haarcascade_frontalcatface.xml

namespace FaceRecognition;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        using var video = LoadVideo();

        if (video != null)
        {
            //Perform(video, "haarcascade_frontalface_alt2.xml");
            Perform(video, "haarcascade_frontalcatface.xml");
        }
    }

    private void Perform(VideoCapture video, string trainFileName)
    {
        // OpenCv 제공 window
        Window videoWindow = new("Viewer");
        using var classifier = new CascadeClassifier(trainFileName);

        while (true)
        {
            using var frame = new Mat();
            video.Read(frame);

            // 동영상이 끝나면 frame.Empty 가 true로 바뀜
            if (frame.Empty())
                break;

            // 전처리 (여기서는 간단히 grayscale만 적용
            using var grayscale = frame.CvtColor(ColorConversionCodes.BGR2GRAY);

            // 객체 찾기
            var detected = classifier.DetectMultiScale(grayscale);

            // 찾은 객체 표시
            foreach (var rect in detected)
            {
                frame.Rectangle(rect, Scalar.Crimson, 2);
            }

            videoWindow.ShowImage(frame);
            Cv2.WaitKey(1);
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