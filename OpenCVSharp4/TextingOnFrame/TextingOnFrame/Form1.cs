using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace TextingOnFrame
{
    /// <summary>
    /// FPS를 계산할 클래스
    /// </summary>
    public class FPS
    {
        private List<DateTime> Counter = new List<DateTime>();

        public int Get()
        {
            // 1초 이상 지난 항목 제거
            Counter.RemoveAll(frame => frame.AddSeconds(1) < DateTime.Now);

            // 카운트 올린 후 리턴
            Counter.Add(DateTime.Now);
            return Counter.Count;
        }
    }

    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            LoadButton.Click += LoadButton_Click;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All files|*.*";
            dialog.InitialDirectory = $@"C:\";
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // 동영상 로드
                var video = new VideoCapture(dialog.FileName);
                VideoPath.Text = dialog.FileName;

                // 동영상 재생
                var videoThread = new Thread(new ParameterizedThreadStart(PlayMethod_OpenCVUI));
                videoThread.IsBackground = true;
                videoThread.Start(video);
            }
        }

        private void PlayMethod_OpenCVUI(object videoCapture)
        {
            var video = videoCapture as VideoCapture;
            var FPS = new FPS();

            double frameIntervalBase = 1000 / video.Fps - 15.6;
            int frameInterval_ms = frameIntervalBase > 0 ? (int)frameIntervalBase : 0;

            Window window = new Window("Video");

            while (true)
            {
                var image = new Mat();
                video.Read(image);
                if (image.Empty()) break;
                Cv2.PutText(image, $"FPS : {FPS.Get()}", new Point(0, 50), HersheyFonts.HersheySimplex, 3, Scalar.Blue);    // FPS 출력

                window.ShowImage(image);

                Cv2.WaitKey(frameInterval_ms);
                image.Release();
            }

            video.Release();
            window.Close();
            GC.Collect();
        }
    }
}