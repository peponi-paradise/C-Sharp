using OpenCvSharp;
using System.Management;

namespace UsingWebcam;

public partial class Form1 : Form
{
    private VideoCapture _videoCapture;
    private readonly SynchronizationContext _syncContext;
    private volatile bool _isStop = false;

    public Form1()
    {
        InitializeComponent();
        _syncContext = SynchronizationContext.Current!;

        _videoCapture = new VideoCapture();

        WebcamList.Items.AddRange(GetImageDevices());
        if (WebcamList.Items.Count > 0)
            WebcamList.SelectedIndex = 0;

        StartButton.Click += StartButton_Click;
        StopButton.Click += StopButton_Click;
    }

    private void StartButton_Click(object? sender, EventArgs e)
    {
        // 카메라 열기
        _videoCapture.Open(WebcamList.SelectedIndex);

        // 프레임 크기 조절 시 카메라 다시 연결. 최초 연결할 때만 설정
        _videoCapture.Set(VideoCaptureProperties.FrameWidth, (int)FrameWidth.Value);
        _videoCapture.Set(VideoCaptureProperties.FrameHeight, (int)FrameHeight.Value);

        var worker = new Thread(Worker)
        {
            IsBackground = true
        };
        worker.Start();
    }

    private void StopButton_Click(object? sender, EventArgs e) => _isStop = true;

    private void Worker()
    {
        _isStop = false;

        // OpenCvSharp 제공 window
        var videoWindow = new Window("Video");

        while (!_isStop)
        {
            SetCameraParams();

            // Frame 단위 재생
            using var frame = new Mat();
            _videoCapture.Read(frame);

            videoWindow.ShowImage(frame);

            // 카메라에 설정된 Fps 값만큼 wait
            Cv2.WaitKey((int)Math.Round(1000 / _videoCapture.Fps));
        }

        videoWindow.Close();
    }

    /// <summary>
    /// PC에 있는 이미지 장치를 얻어옴 <br/>
    /// 스캐너 등 다른 장치 또한 나타날 수 있음
    /// </summary>
    /// <returns>Device names</returns>
    private string[] GetImageDevices()
    {
        var deviceNames = new List<string>();

        // System.Management Nuget 설치 필요
        var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')");
        foreach (var device in searcher.Get())
            deviceNames.Add(device["Caption"].ToString()!);

        return [.. deviceNames];
    }

    // 카메라에 따라 적용 가능한 설정이 다를 수 있음
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

        // UI 값을 읽어옴
        _syncContext.Send(delegate
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

        if (_videoCapture.Exposure != exposure)
            _videoCapture.Exposure = exposure;
        if (_videoCapture.Focus != focus)
            _videoCapture.Focus = focus;
        if (_videoCapture.Brightness != brightness)
            _videoCapture.Brightness = brightness;
        if (_videoCapture.Contrast != contrast)
            _videoCapture.Contrast = contrast;
        if (_videoCapture.Hue != hue)
            _videoCapture.Hue = hue;
        if (_videoCapture.Saturation != saturation)
            _videoCapture.Saturation = saturation;
        if (_videoCapture.Gain != gain)
            _videoCapture.Gain = gain;
        if (_videoCapture.Gamma != gamma)
            _videoCapture.Gamma = gamma;
        if (_videoCapture.Sharpness != sharpness)
            _videoCapture.Sharpness = sharpness;
    }
}
