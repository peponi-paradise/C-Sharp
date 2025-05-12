using OpenCvSharp;

namespace ImageLoadSave;

public partial class Form1 : Form
{
    private Mat _currentImage = new();

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
            // 이전 이미지 할당 해제
            _currentImage.Release();

            // 이미지 로드
            _currentImage = Cv2.ImRead(dialog.FileName, (ImreadModes)Enum.Parse(typeof(ImreadModes), (Controls.Find("ImageModes", true)[0] as ListBox)!.SelectedItem!.ToString()!));

            // PictureBox에 이미지 할당. OpenCvSharp4.Extensions 설치 필요
            (Controls.Find("ImageView", true)[0] as PictureBox)!.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_currentImage);
        }
    }

    private void Save_Click(object? sender, EventArgs e)
    {
        SaveFileDialog dialog = new()
        {
            Filter = "Bitmap file|*.bmp",
            InitialDirectory = $@"C:\",
            CheckPathExists = true,
            AddExtension = true
        };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            // 이미지 저장
            Cv2.ImWrite(dialog.FileName, _currentImage);
        }
    }

    private void InitializeViewArea()
    {
        PictureBox picture = new()
        {
            Name = "ImageView",
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        // OpenCvSharp4 이미지 로드 옵션
        ListBox imageModes = new()
        {
            Name = "ImageModes",
            Dock = DockStyle.Fill
        };
        imageModes.Items.AddRange(Enum.GetNames(typeof(ImreadModes)));
        imageModes.SelectedIndex = 0;

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
            ColumnCount = 3,
            RowCount = 2,
            Dock = DockStyle.Fill
        };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

        panel.Controls.Add(picture, 0, 0);
        panel.SetColumnSpan(picture, 3);

        panel.Controls.Add(imageModes, 0, 1);
        panel.Controls.Add(load, 1, 1);
        panel.Controls.Add(save, 2, 1);

        this.Controls.Add(panel);
    }
}