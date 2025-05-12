using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace ResizeImage;

public class Form3 : Form
{
    public Form3()
    {
        this.Size = new System.Drawing.Size(1280, 768);
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
            using var nearest = original.Resize(new(), 0.5, 0.5, InterpolationFlags.Nearest);
            using var linear = original.Resize(new(), 0.5, 0.5, InterpolationFlags.Linear);
            using var cubic = original.Resize(new(), 0.5, 0.5, InterpolationFlags.Cubic);
            using var area = original.Resize(new(), 0.5, 0.5, InterpolationFlags.Area);
            using var lanczos4 = original.Resize(new(), 0.5, 0.5, InterpolationFlags.Lanczos4);

            SetImage(nameof(original), original);
            SetImage(nameof(nearest), nearest);
            SetImage(nameof(linear), linear);
            SetImage(nameof(cubic), cubic);
            SetImage(nameof(area), area);
            SetImage(nameof(lanczos4), lanczos4);
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
        PictureBox nearest = new()
        {
            Name = nameof(nearest),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox linear = new()
        {
            Name = nameof(linear),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox cubic = new()
        {
            Name = nameof(cubic),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox area = new()
        {
            Name = nameof(area),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox lanczos4 = new()
        {
            Name = nameof(lanczos4),
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
            ColumnCount = 5,
            RowCount = 2,
            Dock = DockStyle.Fill,
        };
        panel.ColumnStyles.Add(new(SizeType.Percent, 20));
        panel.ColumnStyles.Add(new(SizeType.Percent, 20));
        panel.ColumnStyles.Add(new(SizeType.Percent, 20));
        panel.ColumnStyles.Add(new(SizeType.Percent, 20));
        panel.ColumnStyles.Add(new(SizeType.Percent, 20));
        panel.RowStyles.Add(new(SizeType.Percent, 50));
        panel.RowStyles.Add(new(SizeType.Percent, 50));

        panel.Controls.Add(original, 2, 0);
        panel.Controls.Add(load, 4, 0);
        panel.Controls.Add(nearest, 0, 1);
        panel.Controls.Add(linear, 1, 1);
        panel.Controls.Add(cubic, 2, 1);
        panel.Controls.Add(area, 3, 1);
        panel.Controls.Add(lanczos4, 4, 1);

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
