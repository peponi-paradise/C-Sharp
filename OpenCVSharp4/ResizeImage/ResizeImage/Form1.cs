using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ResizeImage;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        this.Controls.Add(AddComponents());
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
            // Image load
            using var original = Cv2.ImRead(dialog.FileName);

            // Sampling
            // Mat.Resize(targetSize, scaleFactorX, scaleFactorY, InterpolationFlags)
            // targetSize가 0일 때는 호출자 (여기서는 original) 의 크기 사용
            using var upsampled = original.Resize(new(), 4, 4);
            using var downsampled = original.Resize(new(), 0.25, 0.25);

            SetImage(nameof(original), original);
            SetImage(nameof(upsampled), upsampled);
            SetImage(nameof(downsampled), downsampled);
        }
    }

    private TableLayoutPanel AddComponents()
    {
        PictureBox original = new()
        {
            Name = nameof(original),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox upsampled = new()
        {
            Name = nameof(upsampled),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox downsampled = new()
        {
            Name = nameof(downsampled),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        Button load = new()
        {
            Name = nameof(load),
            Dock = DockStyle.Fill,
            Text = nameof(load),
        };
        load.Click += Load_Click;

        TableLayoutPanel panel = new()
        {
            ColumnCount = 3,
            RowCount = 2,
            Dock = DockStyle.Fill,
        };
        panel.ColumnStyles.Add(new(SizeType.Percent, 33.34F));
        panel.ColumnStyles.Add(new(SizeType.Percent, 33.33F));
        panel.ColumnStyles.Add(new(SizeType.Percent, 33.33F));
        panel.RowStyles.Add(new(SizeType.Percent, 95));
        panel.RowStyles.Add(new(SizeType.Percent, 5));

        panel.Controls.Add(original, 0, 0);
        panel.Controls.Add(upsampled, 1, 0);
        panel.Controls.Add(downsampled, 2, 0);
        panel.Controls.Add(load, 0, 1);

        return panel;
    }

    private void SetImage(string controlName, Mat image)
    {
        if (this.Controls.Find(controlName, true).First() is PictureBox box)
        {
            box.Image = image.ToBitmap();
        }
    }
}
