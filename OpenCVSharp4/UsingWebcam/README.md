<h1 id="title">C# - OpenCVSharp4 - Webcam Control</h1>

<br><br>

<h2 id="intro">Introduction</h2>

1. `OpenCVSharp4`을 이용해 Webcam 영상을 재생한다.
2. Video 재생과 마찬가지로 프레임 단위로 로드, 갱신한다.
3. 카메라에는 많은 프로퍼티가 있다. 값을 조절하여 화면에 변화를 줄 수 있다.
4. 카메라에 따라 지원하는 파라미터가 다르다. 사용 중인 카메라의 특징을 확인해야한다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace UsingWebcam
{
    public partial class Form1 : Form
    {
        private VideoCapture VideoCapture;
        private SynchronizationContext SyncContext;
        private bool IsStop = false;

        public Form1()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;

            DrawUI();
            ConnectingEvents();
        }

        private void DrawUI()
        {
            WebcamList.Items.AddRange(GetCameras());
            if (WebcamList.Items.Count > 0) WebcamList.SelectedIndex = 0;
        }

        private void ConnectingEvents()
        {
            StartButton.Click += StartButton_Click;
            StopButton.Click += StopButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            int selectedCam = 0;
            int frameWidth = 0;
            int frameHeight = 0;
            SyncContext.Send(delegate
            {
                selectedCam = WebcamList.SelectedIndex;
                frameWidth = (int)FrameWidth.Value;
                frameHeight = (int)FrameHeight.Value;
            }, null);

            VideoCapture = new VideoCapture();
            VideoCapture.Open(selectedCam);
            VideoCapture.Set(VideoCaptureProperties.FrameWidth, frameWidth);    // 프레임 크기 조절 시 카메라 다시 연결. 최초 연결할 때만 설정
            VideoCapture.Set(VideoCaptureProperties.FrameHeight, frameHeight);

            GetCameraParams();

            var worker = new Thread(Worker);
            worker.IsBackground = true;
            worker.Start();
        }

        private void StopButton_Click(object sender, EventArgs e) => IsStop = true;

        private void Worker()
        {
            IsStop = false;
            var videoWindow = new Window("Video");

            while (!IsStop)
            {
                SetCameraParams();

                Mat frame = new Mat();
                VideoCapture.Read(frame);

                videoWindow.ShowImage(frame);
                Application.DoEvents();
                frame.Release();
            }

            videoWindow.Close();
            GC.Collect();
        }

        /// <summary>
        /// PC에 있는 카메라 리스트를 얻어옴
        /// </summary>
        /// <returns>Cam List</returns>
        private string[] GetCameras()
        {
            var cameraNames = new List<string>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')");
            foreach (var cam in searcher.Get()) cameraNames.Add(cam["Caption"].ToString());
            return cameraNames.ToArray();
        }

        /// <summary>
        /// Exposure, Focus 등 카메라에 따라 파라미터 지원 안되는 경우 있음
        /// </summary>
        private void GetCameraParams()
        {
            SyncContext.Send(delegate
            {
                Exposure.Value = (int)(VideoCapture.Exposure);
                Focus.Value = (int)(VideoCapture.Focus);
                Brightness.Value = (int)VideoCapture.Brightness;
                Contrast.Value = (int)VideoCapture.Contrast;
                Hue.Value = (int)VideoCapture.Hue;
                Saturation.Value = (int)VideoCapture.Saturation;
                Gain.Value = (int)VideoCapture.Gain;
                Gamma.Value = (int)VideoCapture.Gamma;
                Sharpness.Value = (int)VideoCapture.Sharpness;
            }, null);
        }

        private void SetCameraParams()
        {
            double exposure = 0;
            double focus = 0;
            double brightness = 0;
            double contrast = 0;
            double hue = 0;
            double saturation = 0;
            double gain = 0;
            double gamma = 0;
            double sharpness = 0;
            SyncContext.Send(delegate
            {
                exposure = Exposure.Value;
                focus = Focus.Value;
                brightness = Brightness.Value;
                contrast = Contrast.Value;
                hue = Hue.Value;
                saturation = Saturation.Value;
                gain = Gain.Value;
                gamma = Gamma.Value;
                sharpness = Sharpness.Value;
            }, null);

            VideoCapture.Exposure = exposure;
            VideoCapture.Focus = focus;
            VideoCapture.Brightness = brightness;
            VideoCapture.Contrast = contrast;
            VideoCapture.Hue = hue;
            VideoCapture.Saturation = saturation;
            VideoCapture.Gain = gain;
            VideoCapture.Gamma = gamma;
            VideoCapture.Sharpness = sharpness;
        }
    }
}
```

<br><br>

<h2 id="ref">참조 자료</h2>

1. [Exposure (photography) - Wiki](https://en.wikipedia.org/wiki/Exposure_(photography))
2. [Focus (optics) - Wiki](https://en.wikipedia.org/wiki/Focus_(optics))
3. [Autofocus - Wiki](https://en.wikipedia.org/wiki/Autofocus)
4. [Digital Image Processing: Point Operations to Adjust Brightness and Contrast - All about circuits](https://www.allaboutcircuits.com/technical-articles/digital-image-processing-point-operations/)
5. [HSL and HSV - Wiki](https://en.wikipedia.org/wiki/HSL_and_HSV)
6. [What's the difference between "Offset | Gamma | Gain" - Adobe community](https://community.adobe.com/t5/premiere-pro-discussions/what-s-the-difference-between-quot-offset-gamma-gain-quot/m-p/4510593)
7. [Sharpness: What is it and How it is Measured - imatest](https://www.imatest.com/docs/sharpness/)