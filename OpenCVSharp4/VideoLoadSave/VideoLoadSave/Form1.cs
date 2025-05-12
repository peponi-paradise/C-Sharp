using OpenCvSharp;

namespace VideoLoadSave;

public partial class Form1 : Form
{
    private VideoCapture _video = new();

    public Form1()
    {
        InitializeComponent();
        InitializeViewArea();
    }

    private void Load_Click(object? sender, EventArgs e)
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
            // 이전 리소스 할당 해제
            _video.Release();

            // 동영상 로드
            _video = new(dialog.FileName);

            // 프레임 간격
            var frameInterval_ms = (int)Math.Round(1000 / _video.Fps);

            // OpenCvSharp 제공 window
            Window videoWindow = new(dialog.FileName);

            while (true)
            {
                using var frame = new Mat();
                _video.Read(frame);
                // 동영상이 끝나면 frame.Empty 가 true로 바뀜
                if (frame.Empty())
                    break;

                // frame 단위로 불러오기 때문에 PictureBox 같은 컨트롤 이용하여 동영상 재생도 가능함
                videoWindow.ShowImage(frame);

                Cv2.WaitKey(frameInterval_ms);
            }
        }
    }

    private void Save_Click(object? sender, EventArgs e)
    {
        if (!_video.IsOpened())
        {
            MessageBox.Show("Load the video first");
            return;
        }

        // 먼저 Load를 통해 재생한 영상을 처음으로 돌리기 위함
        _video.PosFrames = 0;

        SaveFileDialog dialog = new()
        {
            // 적절한 동영상 형식 설정 필요
            Filter = "mp4|*.mp4",
            InitialDirectory = $@"C:\",
            CheckPathExists = true,
            AddExtension = true
        };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // using 키워드에 유의한다. writer의 모든 쓰기 작업 완료 후 Release()가 호출되어야 정상적으로 저장된다.
            using VideoWriter writer = new(dialog.FileName, VideoWriter.FourCC(_video.FourCC), _video.Fps, new(_video.FrameWidth, _video.FrameHeight));

            while (true)
            {
                using var frame = new Mat();
                _video.Read(frame);
                // 동영상이 끝나면 frame.Empty 가 true로 바뀜
                if (frame.Empty())
                    break;

                writer.Write(frame);
            }
        }
    }

    private void InitializeViewArea()
    {
        Button load = new()
        {
            Text = "LOAD",
            Dock = DockStyle.Fill
        };
        load.Click += Load_Click;

        Button save = new()
        {
            Text = "SAVE",
            Dock = DockStyle.Fill
        };
        save.Click += Save_Click;

        TableLayoutPanel panel = new()
        {
            ColumnCount = 2,
            Dock = DockStyle.Fill
        };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        panel.Controls.Add(load, 0, 0);
        panel.Controls.Add(save, 1, 0);

        this.Controls.Add(panel);
    }
}
