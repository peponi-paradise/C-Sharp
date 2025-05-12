using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;

namespace ResizeImage;

public partial class Form4 : Form
{
    public Form4()
    {
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
            using var downsampled = original.PyrDown();   // Width / 2, Height / 2
            using var upsampled = original.PyrUp();          // Width * 2, Height * 2

            // Making pyramid
            // 레벨 0 (원본) ~ 레벨 4까지 총 5개가 pyramid에 할당됨
            IEnumerable<Mat> pyramid = original.BuildPyramid(4);

            using var blur = downsampled.PyrUp();
            using var resized = blur.Resize(original.Size());
            using var laplacian = new Mat();
            Cv2.Subtract(original, resized, laplacian);

            Cv2.ImWrite("blur.png", blur);
            Cv2.ImWrite("laplacian.png", laplacian);

            using var restore = new Mat();
            Cv2.Add(resized, laplacian, restore);
            Cv2.ImWrite("restore.png", restore);

            var current = original;

            for (int i = 0; i < 4; i++)
            {
                var downsampled = current.PyrDown();

                // 원본 크기로 다시 up
                using var blur = downsampled.PyrUp();

                // 크기 조정 (원본 이미지와 크기 다를 수 있음)
                using var resized = blur.Resize(current.Size());

                using var laplacian = new Mat();

                // Laplacian 계산
                Cv2.Subtract(current, resized, laplacian);

                // 계산된 이미지 저장
                Cv2.ImWrite($"laplacian{i}.png", laplacian);

                current = downsampled;
            }
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
        PictureBox scaleUp = new()
        {
            Name = nameof(scaleUp),
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        PictureBox scaleDown = new()
        {
            Name = nameof(scaleDown),
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
        panel.Controls.Add(scaleUp, 1, 0);
        panel.Controls.Add(scaleDown, 2, 0);
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